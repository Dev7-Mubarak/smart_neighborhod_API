using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.ConflictType;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.ConflictCaseType;

public class ConflictCaseTypeControllerTests
{
    private readonly Mock<IConflictTypeService> _mockService;
    private readonly ConfilctCaseTypeController _controller;

    public ConflictCaseTypeControllerTests()
    {
        _mockService = new Mock<IConflictTypeService>();
        _controller = new ConfilctCaseTypeController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenTypesExist()
    {
        var types = new List<GetConflictTypeDto>
        {
            new() { Id = 1, Name = "Type A" },
            new() { Id = 2, Name = "Type B" }
        };
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<GetConflictTypeDto>>.Success(types));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<GetConflictTypeDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<GetConflictTypeDto>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<GetConflictTypeDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Data!);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenTypeExists()
    {
        var dto = new GetConflictTypeDto { Id = 1, Name = "Type A" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<GetConflictTypeDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<GetConflictTypeDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenTypeDoesNotExist()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<GetConflictTypeDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenTypeCreated()
    {
        var dto = new AddConflictTypeDto { Name = "New Type" };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<AddConflictTypeDto>.Success(dto));

        var result = await _controller.AddAsync(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<AddConflictTypeDto>>(ok.Value);
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task AddAsync_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new AddConflictTypeDto { Name = "" };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<AddConflictTypeDto>.Error(HttpStatusCode.BadRequest, "Invalid"));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenTypeUpdated()
    {
        var dto = new UpdateConflictTypeDto { Name = "Updated" };
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenTypeMissing()
    {
        var dto = new UpdateConflictTypeDto { Name = "X" };
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenTypeDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenTypeMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
