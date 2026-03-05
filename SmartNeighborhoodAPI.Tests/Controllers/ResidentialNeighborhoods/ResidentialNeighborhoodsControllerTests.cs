using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Tests.Controllers.Helpers;

namespace SmartNeighborhoodAPI.Tests.Controllers.ResidentialNeighborhoods;

public class ResidentialNeighborhoodsControllerTests
{
    private readonly Mock<IResidentialNeighborhoodService> _mockService;
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    private readonly ResidentialNeighborhoodsController _controller;

    public ResidentialNeighborhoodsControllerTests()
    {
        _mockService = new Mock<IResidentialNeighborhoodService>();
        _mockUserManager = new Mock<UserManager<AppUser>>(
            Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        _controller = new ResidentialNeighborhoodsController(_mockService.Object, _mockUserManager.Object);
    }

    // ── GetAll ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk_WhenNeighborhoodsExist()
    {
        var data = new List<ReturnResidentialNeighborhoodDto>
        {
            new() { Id = 1, Name = "NH-A" },
            new() { Id = 2, Name = "NH-B" }
        };
        _mockService.Setup(s => s.GetAllAsync(default))
            .ReturnsAsync(ApiResponse<List<ReturnResidentialNeighborhoodDto>>.Success(data));

        var result = await _controller.GetAll(default);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<List<ReturnResidentialNeighborhoodDto>>>(ok.Value);
        Assert.Equal(2, response.Data!.Count);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAllAsync(default))
            .ReturnsAsync(ApiResponse<List<ReturnResidentialNeighborhoodDto>>.Success([]));

        var result = await _controller.GetAll(default);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── GetById ───────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetById_ReturnsOk_WhenNeighborhoodExists()
    {
        var dto = new ReturnResidentialNeighborhoodDto { Id = 1, Name = "NH-A" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<ReturnResidentialNeighborhoodDto>.Success(dto));

        var result = await _controller.GetById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ReturnResidentialNeighborhoodDto>>(ok.Value);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenNeighborhoodMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetById(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── Create ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Create_ReturnsOk_WhenSuccessful()
    {
        var dto = new CreateResidentialNeighborhoodDto { Name = "NH-New" };
        var returned = new ReturnResidentialNeighborhoodDto { Id = 3, Name = "NH-New" };
        _mockService.Setup(s => s.CreateAsync(dto))
            .ReturnsAsync(ApiResponse<ReturnResidentialNeighborhoodDto>.Success(returned));

        var result = await _controller.Create(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenFails()
    {
        var dto = new CreateResidentialNeighborhoodDto { Name = "" };
        _mockService.Setup(s => s.CreateAsync(dto))
            .ReturnsAsync(ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "Invalid"));

        var result = await _controller.Create(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── Update ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Update_ReturnsOk_WhenSuccessful()
    {
        var dto = new UpdateResidentialNeighborhoodDto { Name = "Updated" };
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.Update(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenNeighborhoodMissing()
    {
        var dto = new UpdateResidentialNeighborhoodDto { Name = "X" };
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.Update(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── ChangeManager ─────────────────────────────────────────────────────────

    [Fact]
    public async Task ChangeManager_ReturnsOk_WhenSuccessful()
    {
        var dto = new ChangeResidentialManagerDto { ManagerId = "mgr-1" };
        var returned = new ReturnResidentialNeighborhoodDto { Id = 1, Name = "NH-A" };
        _mockService.Setup(s => s.ChangeManagerAsync(1, dto))
            .ReturnsAsync(ApiResponse<ReturnResidentialNeighborhoodDto>.Success(returned));

        var result = await _controller.ChangeManager(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── GetDashboard ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetDashboard_ReturnsOk_WhenSuccessful()
    {
        var dash = new ResidentialDashboardDto();
        _mockService.Setup(s => s.GetDashboardAsync(default))
            .ReturnsAsync(ApiResponse<ResidentialDashboardDto>.Success(dash));

        var result = await _controller.GetDashboard(default);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── GetUnitsAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetUnitsAsync_ReturnsOk_WhenUnitsExist()
    {
        var unit = new ReturnResidentialUnitDto { Id = 1 };
        _mockService.Setup(s => s.GetUnitsAsync(1))
            .ReturnsAsync(ApiResponse<ReturnResidentialUnitDto>.Success(unit));

        var result = await _controller.GetUnitsAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetUnitsAsync_ReturnsNotFound_WhenNeighborhoodMissing()
    {
        _mockService.Setup(s => s.GetUnitsAsync(999))
            .ReturnsAsync(ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetUnitsAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── GetMyDashboard ────────────────────────────────────────────────────────

    [Fact]
    public async Task GetMyDashboard_ReturnsOk_WhenSuccessful()
    {
        var dash = new ResidentialDashboardDto();
        _mockService.Setup(s => s.GetMyDashboardAsync(default))
            .ReturnsAsync(ApiResponse<ResidentialDashboardDto>.Success(dash));

        var result = await _controller.GetMyDashboard(default);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── GetMyNeighborhoods ────────────────────────────────────────────────────

    [Fact]
    public async Task GetMyNeighborhoods_ReturnsOk_WhenSuccessful()
    {
        const string userId = "user-1";
        ControllerTestHelper.SetFakeUser(_controller, userId, "Manager");

        var units = new List<ReturnResidentialUnitDto> { new() { Id = 1 } };
        _mockService.Setup(s => s.GetMyNeighborhoodsAsync(userId, default))
            .ReturnsAsync(ApiResponse<List<ReturnResidentialUnitDto>>.Success(units));

        var result = await _controller.GetMyNeighborhoods(default);

        Assert.IsType<OkObjectResult>(result);
    }
}
