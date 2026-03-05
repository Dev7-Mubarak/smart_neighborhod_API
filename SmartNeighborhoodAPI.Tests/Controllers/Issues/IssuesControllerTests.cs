using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Tests.Controllers.Helpers;

namespace SmartNeighborhoodAPI.Tests.Controllers.Issues;

public class IssuesControllerTests
{
    private readonly Mock<IIssueService> _mockService;
    private readonly IssuesController _controller;

    public IssuesControllerTests()
    {
        _mockService = new Mock<IIssueService>();
        _controller = new IssuesController(_mockService.Object);
        // Set fake authenticated user so CreateIssue can read the userId claim
        ControllerTestHelper.SetFakeUser(_controller, "test-user-001");
    }

    // ── GetIssues ────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetIssues_ReturnsOk_WhenIssuesExist()
    {
        var issues = new List<IssueDto>
        {
            new() { Id = Guid.NewGuid() },
            new() { Id = Guid.NewGuid() }
        };
        var paged = new PaginatedResult<IssueDto> { Items = issues, TotalCount = 2 };

        _mockService.Setup(s => s.GetAllIssuesAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(ApiResponse<PaginatedResult<IssueDto>>.Success(paged));

        var result = await _controller.GetIssues("", "", "", "", "", 1, 20);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PaginatedResult<IssueDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Items.Count);
    }

    [Fact]
    public async Task GetIssues_ReturnsOk_WithEmptyList()
    {
        var paged = new PaginatedResult<IssueDto> { Items = [], TotalCount = 0 };

        _mockService.Setup(s => s.GetAllIssuesAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(ApiResponse<PaginatedResult<IssueDto>>.Success(paged));

        var result = await _controller.GetIssues("", "", "", "", "", 1, 20);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PaginatedResult<IssueDto>>>(ok.Value);
        Assert.Empty(response.Data!.Items);
    }

    // ── GetIssue ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetIssue_ReturnsOk_WhenIssueExists()
    {
        var id = Guid.NewGuid();
        var dto = new IssueDto { Id = id };
        _mockService.Setup(s => s.GetIssueByIdAsync(id))
            .ReturnsAsync(ApiResponse<IssueDto>.Success(dto));

        var result = await _controller.GetIssue(id);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IssueDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(id, response.Data!.Id);
    }

    [Fact]
    public async Task GetIssue_ReturnsNotFound_WhenIssueMissing()
    {
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.GetIssueByIdAsync(id))
            .ReturnsAsync(ApiResponse<IssueDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetIssue(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── CreateIssue ──────────────────────────────────────────────────────────

    [Fact]
    public async Task CreateIssue_ReturnsOk_WhenIssueCreated()
    {
        var dto = new CreateIssueDto();
        var created = new IssueDto { Id = Guid.NewGuid() };
        _mockService.Setup(s => s.CreateIssueAsync(dto, "test-user-001"))
            .ReturnsAsync(ApiResponse<IssueDto>.Success(created));

        var result = await _controller.CreateIssue(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task CreateIssue_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new CreateIssueDto();
        _mockService.Setup(s => s.CreateIssueAsync(dto, "test-user-001"))
            .ReturnsAsync(ApiResponse<IssueDto>.Error(HttpStatusCode.BadRequest, "Invalid"));

        var result = await _controller.CreateIssue(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── UpdateIssue ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateIssue_ReturnsOk_WhenIssueUpdated()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateIssueDto();
        _mockService.Setup(s => s.UpdateIssueAsync(id, dto))
            .ReturnsAsync(ApiResponse<IssueDto>.Success(new IssueDto { Id = id }));

        var result = await _controller.UpdateIssue(id, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateIssue_ReturnsNotFound_WhenIssueMissing()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateIssueDto();
        _mockService.Setup(s => s.UpdateIssueAsync(id, dto))
            .ReturnsAsync(ApiResponse<IssueDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateIssue(id, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteIssue ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteIssue_ReturnsOk_WhenIssueDeleted()
    {
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.DeleteIssueAsync(id))
            .ReturnsAsync(ApiResponse<bool>.Success(true));

        var result = await _controller.DeleteIssue(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteIssue_ReturnsNotFound_WhenIssueMissing()
    {
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.DeleteIssueAsync(id))
            .ReturnsAsync(ApiResponse<bool>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteIssue(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
