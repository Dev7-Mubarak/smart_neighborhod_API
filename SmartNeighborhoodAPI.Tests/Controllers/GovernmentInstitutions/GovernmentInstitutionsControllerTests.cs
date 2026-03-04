using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.GovernmentInstitutions;

public class GovernmentInstitutionsControllerTests
{
    private readonly Mock<IGovernmentInstitutionsService> _mockService;
    private readonly GovernmentInstitutionsController _controller;

    public GovernmentInstitutionsControllerTests()
    {
        _mockService = new Mock<IGovernmentInstitutionsService>();
        _controller = new GovernmentInstitutionsController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenInstitutionsExist()
    {
        var institutions = new List<GetGovernmentInstitutionsDto>
        {
            new() { Id = 1, Name = "Ministry A" },
            new() { Id = 2, Name = "Ministry B" }
        };
        _mockService.Setup(s => s.GetAllAuthoritiesAsync(default))
            .ReturnsAsync(ApiResponse<IEnumerable<GetGovernmentInstitutionsDto>>.Success(institutions));

        var result = await _controller.GetAllAsync(default);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<GetGovernmentInstitutionsDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAllAuthoritiesAsync(default))
            .ReturnsAsync(ApiResponse<IEnumerable<GetGovernmentInstitutionsDto>>.Success([]));

        var result = await _controller.GetAllAsync(default);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<GetGovernmentInstitutionsDto>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenInstitutionExists()
    {
        var dto = new GetGovernmentInstitutionsDto { Id = 1, Name = "Ministry A" };
        _mockService.Setup(s => s.GetAuthorityByIdAsync(1, default))
            .ReturnsAsync(ApiResponse<GetGovernmentInstitutionsDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1, default);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<GetGovernmentInstitutionsDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenInstitutionMissing()
    {
        _mockService.Setup(s => s.GetAuthorityByIdAsync(999, default))
            .ReturnsAsync(ApiResponse<GetGovernmentInstitutionsDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999, default);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenInstitutionCreated()
    {
        var dto = new AddGovernmentInstitutionsDto { Name = "New Ministry" };
        var returned = new GetGovernmentInstitutionsDto { Id = 3, Name = "New Ministry" };
        _mockService.Setup(s => s.AddAuthorityAsync(dto, default))
            .ReturnsAsync(ApiResponse<GetGovernmentInstitutionsDto>.Success(returned));

        var result = await _controller.AddAsync(dto, default);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<GetGovernmentInstitutionsDto>>(ok.Value);
        Assert.True(response.IsSuccess);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenInstitutionUpdated()
    {
        var dto = new UpdateAuthorityDto { Name = "Updated Ministry" };
        _mockService.Setup(s => s.UpdateAuthorityAsync(1, dto, default))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto, default);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenInstitutionMissing()
    {
        var dto = new UpdateAuthorityDto { Name = "X" };
        _mockService.Setup(s => s.UpdateAuthorityAsync(999, dto, default))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto, default);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenInstitutionDeleted()
    {
        _mockService.Setup(s => s.DeleteAuthorityAsync(1, default))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1, default);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenInstitutionMissing()
    {
        _mockService.Setup(s => s.DeleteAuthorityAsync(999, default))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999, default);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
