using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.Project;

public class ProjectControllerTests
{
    private readonly Mock<IProjectService> _mockService;
    private readonly ProjectsController _controller;

    public ProjectControllerTests()
    {
        _mockService = new Mock<IProjectService>();
        _controller = new ProjectsController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenProjectsExist()
    {
        var projects = new List<ReturnProjectDto>
        {
            new() { Id = 1, Name = "Project A" },
            new() { Id = 2, Name = "Project B" }
        };
        var paged = new PaginatedResult<ReturnProjectDto>(projects);
        var filter = new ProjectFilterParams();

        _mockService.Setup(s => s.GetAllAsync(filter))
            .ReturnsAsync(ApiResponse<PaginatedResult<ReturnProjectDto>>.Success(paged));

        var result = await _controller.GetAllAsync(filter);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PaginatedResult<ReturnProjectDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.items.Count);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenProjectExists()
    {
        var dto = new ReturnProjectDto { Id = 1, Name = "Project A" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<ReturnProjectDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ReturnProjectDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenProjectMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<ReturnProjectDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenProjectCreated()
    {
        var dto = new ProjectDto { Name = "New Project" };
        var returned = new ReturnProjectDto { Id = 3, Name = "New Project" };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<ReturnProjectDto>.Success(returned));

        var result = await _controller.AddAsync(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ReturnProjectDto>>(ok.Value);
        Assert.True(response.IsSuccess);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenProjectUpdated()
    {
        var dto = new ProjectDto { Name = "Updated" };
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenProjectMissing()
    {
        var dto = new ProjectDto { Name = "X" };
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenProjectDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenProjectMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── ChangeStatusAsync ────────────────────────────────────────────────────

    [Fact]
    public async Task ChangeStatusAsync_ReturnsOk_WhenStatusChanged()
    {
        _mockService.Setup(s => s.ChangeStatusAsync(1, ProjectStatus.Active))
            .ReturnsAsync(ApiResponse<string>.Success("Status changed"));

        var result = await _controller.ChangeStatusAsync(1, ProjectStatus.Active);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── AssignTeamToProject ──────────────────────────────────────────────────

    [Fact]
    public async Task AssignTeamToProject_ReturnsOk_WhenTeamAssigned()
    {
        _mockService.Setup(s => s.AssignTeamToProjectAsync(1, 2))
            .ReturnsAsync(ApiResponse<string>.Success("Team assigned"));

        var result = await _controller.AssignTeamToProject(1, 2);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── GetProjectBlocksWithBeneficiaryFamilies ──────────────────────────────

    [Fact]
    public async Task GetProjectBlocksWithBeneficiaryFamilies_ReturnsOk()
    {
        var list = new List<BeneficiaryFamilies>();
        _mockService.Setup(s => s.GetProjectBlocksWithBeneficiaryFamilies(1))
            .ReturnsAsync(ApiResponse<List<BeneficiaryFamilies>>.Success(list));

        var result = await _controller.GetProjectBlocksWithBeneficiaryFamilies(1);

        Assert.IsType<OkObjectResult>(result);
    }
}
