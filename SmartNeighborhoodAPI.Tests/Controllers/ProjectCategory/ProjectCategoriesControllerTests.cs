using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.ProjectCategory;

public class ProjectCategoriesControllerTests
{
    private readonly Mock<IProjectCatgoryService> _mockService;
    private readonly ProjectCategoriesController _controller;

    public ProjectCategoriesControllerTests()
    {
        _mockService = new Mock<IProjectCatgoryService>();
        _controller = new ProjectCategoriesController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenCategoriesExist()
    {
        var categories = new List<ProjectCatogory>
        {
            new() { Id = 1, Name = "Category A" },
            new() { Id = 2, Name = "Category B" }
        };
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<ProjectCatogory>>.Success(categories));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<ProjectCatogory>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<ProjectCatogory>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<ProjectCatogory>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenCategoryExists()
    {
        var dto = new ProjectCatgoryDto { Id = 1, Name = "Category A" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<ProjectCatgoryDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ProjectCatgoryDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenCategoryMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<ProjectCatgoryDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenCategoryCreated()
    {
        var dto = new ProjectCatgoryDto { Name = "New Category" };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<ProjectCatgoryDto>.Success(dto));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenCategoryUpdated()
    {
        var dto = new ProjectCatgoryDto { Name = "Updated" };
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenCategoryMissing()
    {
        var dto = new ProjectCatgoryDto { Name = "X" };
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenCategoryDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenCategoryMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
