using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.Controllers.FamilyMembers;

public class FamilyMembersControllerTests
{
    private readonly Mock<IFamilyMemberService> _mockService;
    private readonly FamilyMembersController _controller;

    public FamilyMembersControllerTests()
    {
        _mockService = new Mock<IFamilyMemberService>();
        _controller = new FamilyMembersController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenMembersExist()
    {
        var members = new List<ReturnFamilyMemberWithFullInfo>
        {
            new() { FamilyMemberId = 1 },
            new() { FamilyMemberId = 2 }
        };
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>.Success(members));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>.Success([]));

        var result = await _controller.GetAllAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>>(ok.Value);
        Assert.Empty(response.Data!);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenMemberExists()
    {
        var dto = new ReturnFamilyMemberWithFullInfo { FamilyMemberId = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<ReturnFamilyMemberWithFullInfo>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ReturnFamilyMemberWithFullInfo>>(ok.Value);
        Assert.Equal(1, response.Data!.FamilyMemberId);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenMemberMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<ReturnFamilyMemberWithFullInfo>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenMemberCreated()
    {
        var dto = new FamilyMemberDto { PersonId = 1, FamilyId = 1 };
        var member = new FamilyMember { Id = 1 };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<FamilyMember>.Success(member));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddAsync_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new FamilyMemberDto();
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<FamilyMember>.Error(HttpStatusCode.BadRequest, "Invalid"));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenMemberUpdated()
    {
        var entity = new FamilyMember { Id = 1 };
        _mockService.Setup(s => s.UpdateAsync(1, entity))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, entity);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenMemberDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1, 1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1, 1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenMemberMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999, 1))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999, 1);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
