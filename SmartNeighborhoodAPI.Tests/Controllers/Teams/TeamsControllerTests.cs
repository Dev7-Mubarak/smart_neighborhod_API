using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Tests.Controllers.Teams;

public class TeamsControllerTests
{
    private readonly Mock<ITeamsService> _mockService;
    private readonly TeamsController _controller;

    public TeamsControllerTests()
    {
        _mockService = new Mock<ITeamsService>();
        _controller = new TeamsController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenTeamsExist()
    {
        var teams = new List<CustomTeamDto>
        {
            new() { Id = 1, Name = "Alpha Team" },
            new() { Id = 2, Name = "Beta Team" }
        };
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<CustomTeamDto>>.Success(teams));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<CustomTeamDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<CustomTeamDto>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<CustomTeamDto>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenTeamExists()
    {
        var dto = new TeamDto { Name = "Alpha Team", TeamLeadId = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<TeamDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<TeamDto>>(ok.Value);
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenTeamMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenTeamCreated()
    {
        var dto = new TeamDto { Name = "New Team", TeamLeadId = 1 };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<TeamDto>.Success(dto));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenTeamUpdated()
    {
        var dto = new TeamDto { Name = "Updated Team", TeamLeadId = 2 };
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<TeamDto>.Success(dto));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenTeamMissing()
    {
        var dto = new TeamDto { Name = "X", TeamLeadId = 1 };
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<TeamDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenTeamDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenTeamMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── GetTeamProjects ──────────────────────────────────────────────────────

    [Fact]
    public async Task GetTeamProjects_ReturnsOk_WhenProjectsExist()
    {
        var projects = new List<ReturnProjectDto>
        {
            new() { Id = 1, Name = "Project A" }
        };
        _mockService.Setup(s => s.GetTeamProjects(1))
            .ReturnsAsync(ApiResponse<IEnumerable<ReturnProjectDto>>.Success(projects));

        var result = await _controller.GetTeamProjects(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<ReturnProjectDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Single(response.Data!);
    }
}
