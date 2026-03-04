using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Services;

namespace SmartNeighborhoodAPI.Tests.Controllers.MemberFamilyRole;

public class MemberFamilyRoleControllerTests
{
    private readonly Mock<IMemberFamilyRoleService> _mockService;
    private readonly MemberFamilyRoleController _controller;

    public MemberFamilyRoleControllerTests()
    {
        _mockService = new Mock<IMemberFamilyRoleService>();
        _controller = new MemberFamilyRoleController(_mockService.Object);
    }

    // ── GetAll ───────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk_WhenRolesExist()
    {
        var roles = new List<MemberFamilyRoleDto>
        {
            new() { Id = 1, RoleName = "Father" },
            new() { Id = 2, RoleName = "Mother" }
        };
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<MemberFamilyRoleDto>>.Success(roles));

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<MemberFamilyRoleDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAll())
            .ReturnsAsync(ApiResponse<IEnumerable<MemberFamilyRoleDto>>.Success([]));

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<MemberFamilyRoleDto>>)ok.Value!).IsSuccess);
    }

    // ── GetById ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetById_ReturnsOk_WhenRoleExists()
    {
        var dto = new MemberFamilyRoleDto { Id = 1, RoleName = "Father" };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<MemberFamilyRoleDto>.Success(dto));

        var result = await _controller.GetById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<MemberFamilyRoleDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenRoleMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<MemberFamilyRoleDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetById(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── Add ──────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Add_ReturnsOk_WhenRoleCreated()
    {
        var entity = new SmartNeighborhoodAPI.Entites.MemberFamilyRole { Id = 1, RoleName = "Son" };
        _mockService.Setup(s => s.AddAsync("Son"))
            .ReturnsAsync(ApiResponse<SmartNeighborhoodAPI.Entites.MemberFamilyRole>.Success(entity));

        var result = await _controller.Add("Son");

        Assert.IsType<OkObjectResult>(result);
    }

    // ── Update ───────────────────────────────────────────────────────────────

    [Fact]
    public async Task Update_ReturnsOk_WhenRoleUpdated()
    {
        _mockService.Setup(s => s.UpdateAsync(1, "Updated Role"))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.Update(1, "Updated Role");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenRoleMissing()
    {
        _mockService.Setup(s => s.UpdateAsync(999, "X"))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.Update(999, "X");

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── Delete ───────────────────────────────────────────────────────────────

    [Fact]
    public async Task Delete_ReturnsOk_WhenRoleDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.Delete(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenRoleMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.Delete(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
