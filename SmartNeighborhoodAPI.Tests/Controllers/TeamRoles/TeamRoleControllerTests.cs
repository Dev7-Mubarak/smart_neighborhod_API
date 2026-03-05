using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.TeamRoles;

public class TeamRoleControllerTests
{
    private readonly Mock<ITeamRoleService> _mockService;
    private readonly TeamRoleController _controller;

    public TeamRoleControllerTests()
    {
        _mockService = new Mock<ITeamRoleService>();
        _controller = new TeamRoleController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenRolesExist()
    {
        var roles = new List<TeamRole>
        {
            new() { Id = 1, Name = "Leader" },
            new() { Id = 2, Name = "Member" }
        };
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<IEnumerable<TeamRole>>.Success(roles));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<TeamRole>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<IEnumerable<TeamRole>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<TeamRole>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenRoleExists()
    {
        var role = new TeamRole { Id = 1, Name = "Leader" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<TeamRole>.Success(role));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<TeamRole>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenRoleMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<TeamRole>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenRoleCreated()
    {
        var created = new TeamRole { Id = 3, Name = "Analyst" };
        _mockService.Setup(s => s.AddAsync("Analyst"))
            .ReturnsAsync(ApiResponse<TeamRole>.Success(created));

        var result = await _controller.AddAsync("Analyst");

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<TeamRole>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal("Analyst", response.Data!.Name);
    }

    [Fact]
    public async Task AddAsync_ReturnsConflict_WhenRoleAlreadyExists()
    {
        _mockService.Setup(s => s.AddAsync("Leader"))
            .ReturnsAsync(ApiResponse<TeamRole>.Error(HttpStatusCode.Conflict, "Already exists"));

        var result = await _controller.AddAsync("Leader");

        Assert.IsType<ConflictObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenRoleUpdated()
    {
        var updated = new TeamRole { Id = 1, Name = "Senior Leader" };
        _mockService.Setup(s => s.UpdateAsync(1, "Senior Leader"))
            .ReturnsAsync(ApiResponse<TeamRole>.Success(updated));

        var result = await _controller.UpdateAsync(1, "Senior Leader");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenRoleMissing()
    {
        _mockService.Setup(s => s.UpdateAsync(999, "X"))
            .ReturnsAsync(ApiResponse<TeamRole>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, "X");

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenRoleDeleted()
    {
        var deleted = new TeamRole { Id = 1, Name = "Leader" };
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<TeamRole>.Success(deleted));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenRoleMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<TeamRole>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
