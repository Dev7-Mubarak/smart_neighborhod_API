using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.ConflictCase;

public class ConflictCaseControllerTests
{
    private readonly Mock<IConflictCaseService> _mockService;
    private readonly ConflictCaseController _controller;

    public ConflictCaseControllerTests()
    {
        _mockService = new Mock<IConflictCaseService>();
        _controller = new ConflictCaseController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenCasesExist()
    {
        var items = new List<GetConflictCaseDto>
        {
            new() { Id = 1 },
            new() { Id = 2 }
        };
        var paged = new PaginatedResult<GetConflictCaseDto>(items);
        var filter = new ConflictCaseFilterParams();

        _mockService.Setup(s => s.GetAllAsync(filter))
            .ReturnsAsync(ApiResponse<PaginatedResult<GetConflictCaseDto>>.Success(paged));

        var result = await _controller.GetAllAsync(filter);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PaginatedResult<GetConflictCaseDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.items.Count);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        var paged = new PaginatedResult<GetConflictCaseDto>(new List<GetConflictCaseDto>());
        var filter = new ConflictCaseFilterParams();

        _mockService.Setup(s => s.GetAllAsync(filter))
            .ReturnsAsync(ApiResponse<PaginatedResult<GetConflictCaseDto>>.Success(paged));

        var result = await _controller.GetAllAsync(filter);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PaginatedResult<GetConflictCaseDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Data!.items);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenCaseExists()
    {
        var dto = new GetConflictCaseDto { Id = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<GetConflictCaseDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<GetConflictCaseDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenCaseDoesNotExist()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<GetConflictCaseDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenCaseUpdated()
    {
        var dto = new UpdateConflictCaseDto();
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenCaseMissing()
    {
        var dto = new UpdateConflictCaseDto();
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenCaseDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenCaseMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── GetByFamilyMemberId ──────────────────────────────────────────────────

    [Fact]
    public async Task GetByFamilyMemberId_ReturnsOk_WhenCasesExist()
    {
        var cases = new List<GetConflictCaseDto> { new() { Id = 1 } };
        _mockService.Setup(s => s.GetByFamilyMemberIdAsync(5))
            .ReturnsAsync(ApiResponse<IEnumerable<GetConflictCaseDto>>.Success(cases));

        var result = await _controller.GetByFamilyMemberId(5);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<GetConflictCaseDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
    }
}
