using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.TeamMembers;

public class TeamMembersControllerTests
{
    private readonly Mock<ITeamMemberService> _mockService;
    private readonly TeamMembersController _controller;

    public TeamMembersControllerTests()
    {
        _mockService = new Mock<ITeamMemberService>();
        _controller = new TeamMembersController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenMembersExist()
    {
        var members = new List<TeamMemberDetailsDto>
        {
            new() { TeamMemberId = 1 },
            new() { TeamMemberId = 2 }
        };
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<TeamMemberDetailsDto>>.Success(members));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<TeamMemberDetailsDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<TeamMemberDetailsDto>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<TeamMemberDetailsDto>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenMemberExists()
    {
        var dto = new TeamMemberDto { TeamId = 1, MemberId = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<TeamMemberDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<TeamMemberDto>>(ok.Value);
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenMemberMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<TeamMemberDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenMemberAdded()
    {
        var dto = new AddTeamMemberDto { TeamId = 1, PersonId = 1 };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<string>.Success("Added"));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddAsync_ReturnsConflict_WhenMemberAlreadyExists()
    {
        var dto = new AddTeamMemberDto { TeamId = 1, PersonId = 1 };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.Conflict, "Already exists"));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<ConflictObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenMemberUpdated()
    {
        var dto = new UpdateTeamMemberDto();
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenMemberDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenMemberMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
