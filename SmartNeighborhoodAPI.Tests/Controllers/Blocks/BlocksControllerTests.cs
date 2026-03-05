using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.Blocks;

public class BlocksControllerTests
{
    private readonly Mock<IBlockServices> _mockService;
    private readonly BlocksController _controller;

    public BlocksControllerTests()
    {
        _mockService = new Mock<IBlockServices>();
        _controller = new BlocksController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenBlocksExist()
    {
        // Arrange
        var blocks = new List<RetrunBlockDto>
        {
            new() { Id = 1, Name = "Block A" },
            new() { Id = 2, Name = "Block B" }
        };
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<IEnumerable<RetrunBlockDto>>.Success(blocks));

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<RetrunBlockDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<IEnumerable<RetrunBlockDto>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<RetrunBlockDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Data!);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenBlockExists()
    {
        var block = new Block { Id = 1, Name = "Block A" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<Block>.Success(block));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<Block>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenBlockDoesNotExist()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<Block>.Error(HttpStatusCode.NotFound, "Block not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenBlockCreated()
    {
        var dto = new BlockDto { Name = "New Block" };
        var returned = new RetrunBlockDto { Id = 3, Name = "New Block" };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<RetrunBlockDto>.Success(returned));

        var result = await _controller.AddAsync(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<RetrunBlockDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal("New Block", response.Data!.Name);
    }

    [Fact]
    public async Task AddAsync_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new BlockDto { Name = "" };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.BadRequest, "Invalid data"));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenBlockUpdated()
    {
        var dto = new UpdateBlockDto { Name = "Updated Block" };
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenBlockMissing()
    {
        var dto = new UpdateBlockDto { Name = "X" };
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenBlockDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenBlockMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── ChangeBlockManager ───────────────────────────────────────────────────

    [Fact]
    public async Task ChangeBlockManager_ReturnsOk_WhenManagerChanged()
    {
        var dto = new ChangeManagerDto { Identifier = "user-1", Password = "Password1!", PersonId = 1 };
        var returned = new RetrunBlockDto { Id = 1, Name = "Block A" };
        _mockService.Setup(s => s.ChangeManager(1, dto))
            .ReturnsAsync(ApiResponse<RetrunBlockDto>.Success(returned));

        var result = await _controller.ChangeBlockManager(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }
}
