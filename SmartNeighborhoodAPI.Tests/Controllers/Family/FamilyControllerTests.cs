using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.Family;

public class FamilyControllerTests
{
    private readonly Mock<IFamilyService> _mockService;
    private readonly FamilyController _controller;

    public FamilyControllerTests()
    {
        _mockService = new Mock<IFamilyService>();
        _controller = new FamilyController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenFamiliesExist()
    {
        var families = new List<ReturnFamilyDto>
        {
            new() { Id = 1 },
            new() { Id = 2 }
        };
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<List<ReturnFamilyDto>>.Success(families));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<List<ReturnFamilyDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<List<ReturnFamilyDto>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<List<ReturnFamilyDto>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenFamilyExists()
    {
        var dto = new ReturnFamilyDto { Id = 1 };
        _mockService.Setup(s => s.GetById(1))
            .ReturnsAsync(ApiResponse<ReturnFamilyDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ReturnFamilyDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenFamilyMissing()
    {
        _mockService.Setup(s => s.GetById(999))
            .ReturnsAsync(ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenFamilyCreated()
    {
        var dto = new FamilyDto();
        var returned = new ReturnFamilyDto { Id = 3 };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<ReturnFamilyDto>.Success(returned));

        var result = await _controller.AddAsync(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ReturnFamilyDto>>(ok.Value);
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task AddAsync_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new FamilyDto();
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.BadRequest, "Invalid"));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenFamilyUpdated()
    {
        var dto = new FamilyDto();
        var returned = new ReturnFamilyDto { Id = 1 };
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<ReturnFamilyDto>.Success(returned));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenFamilyMissing()
    {
        var dto = new FamilyDto();
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenFamilyDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenFamilyMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
