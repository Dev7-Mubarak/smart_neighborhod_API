using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Tests.Controllers.FamilyCategory;

public class FamilyCategoryControllerTests
{
    private readonly Mock<IFamilyCatgoryService> _mockService;
    private readonly FamilyCatgoryController _controller;

    public FamilyCategoryControllerTests()
    {
        _mockService = new Mock<IFamilyCatgoryService>();
        _controller = new FamilyCatgoryController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenCategoriesExist()
    {
        var categories = new List<FamilyCatgoryDto>
        {
            new() { Id = 1, Name = "Cat A" },
            new() { Id = 2, Name = "Cat B" }
        };
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<FamilyCatgoryDto>>.Success(categories));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<FamilyCatgoryDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<FamilyCatgoryDto>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<FamilyCatgoryDto>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenCategoryExists()
    {
        var dto = new FamilyCatgoryDto { Id = 1, Name = "Cat A" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<FamilyCatgoryDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<FamilyCatgoryDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenCategoryMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<FamilyCatgoryDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenCategoryCreated()
    {
        _mockService.Setup(s => s.AddAsync("New Category"))
            .ReturnsAsync(ApiResponse<string>.Success("Created"));

        var result = await _controller.AddAsync("New Category");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddAsync_ReturnsBadRequest_WhenServiceFails()
    {
        _mockService.Setup(s => s.AddAsync(""))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Name required"));

        var result = await _controller.AddAsync("");

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenCategoryUpdated()
    {
        _mockService.Setup(s => s.UpdateAsync(1, "Updated Name"))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, "Updated Name");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenCategoryMissing()
    {
        _mockService.Setup(s => s.UpdateAsync(999, "X"))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, "X");

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
