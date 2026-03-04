using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MramProject.Interface;
using MramProject.Controllers;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Government_InstitutionsContacts;

namespace SmartNeighborhoodAPI.Tests.Controllers.GovernmentInstitutionContacts;

public class GovernmentInstitutionContactsControllerTests
{
    private readonly Mock<IGovernmentInstitutionContactService> _mockService;
    private readonly Mock<ILogger<GovernmentInstitutionContactsController>> _mockLogger;
    private readonly GovernmentInstitutionContactsController _controller;

    public GovernmentInstitutionContactsControllerTests()
    {
        _mockService = new Mock<IGovernmentInstitutionContactService>();
        _mockLogger = new Mock<ILogger<GovernmentInstitutionContactsController>>();
        _controller = new GovernmentInstitutionContactsController(_mockService.Object, _mockLogger.Object);
    }

    // ── GetByAuthority ────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByAuthorityAsync_ReturnsOk_WhenContactsExist()
    {
        var contacts = new List<GetGovernmentInstitutionContactDto>
        {
            new() { Id = 1, Name = "Contact A" },
            new() { Id = 2, Name = "Contact B" }
        };
        _mockService.Setup(s => s.GetContactsByAuthorityAsync(1, default))
            .ReturnsAsync(ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>.Success(contacts));

        var result = await _controller.GetByAuthorityAsync(1, default);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.Count());
    }

    [Fact]
    public async Task GetByAuthorityAsync_ReturnsOk_WithEmptyList()
    {
        _mockService.Setup(s => s.GetContactsByAuthorityAsync(1, default))
            .ReturnsAsync(ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>.Success([]));

        var result = await _controller.GetByAuthorityAsync(1, default);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>)ok.Value!).IsSuccess);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenContactExists()
    {
        var dto = new GetGovernmentInstitutionContactDto { Id = 1, Name = "Contact A" };
        _mockService.Setup(s => s.GetContactByIdAsync(1, default))
            .ReturnsAsync(ApiResponse<GetGovernmentInstitutionContactDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1, default);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<GetGovernmentInstitutionContactDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenContactMissing()
    {
        _mockService.Setup(s => s.GetContactByIdAsync(999, default))
            .ReturnsAsync(ApiResponse<GetGovernmentInstitutionContactDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999, default);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddToAuthorityAsync ───────────────────────────────────────────────────

    [Fact]
    public async Task AddToAuthorityAsync_ReturnsOk_WhenContactAdded()
    {
        var dto = new AddGovernmentInstitutionContactDto { Name = "New Contact" };
        var returned = new GetGovernmentInstitutionContactDto { Id = 3, Name = "New Contact" };
        _mockService.Setup(s => s.AddContactAsync(1, dto, default))
            .ReturnsAsync(ApiResponse<GetGovernmentInstitutionContactDto>.Success(returned));

        var result = await _controller.AddToAuthorityAsync(1, dto, default);

        Assert.IsType<OkObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenContactUpdated()
    {
        var dto = new UpdateGovernmentInstitutionContactDto { Name = "Updated" };
        _mockService.Setup(s => s.UpdateContactAsync(1, dto, default))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto, default);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenContactMissing()
    {
        var dto = new UpdateGovernmentInstitutionContactDto { Name = "X" };
        _mockService.Setup(s => s.UpdateContactAsync(999, dto, default))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto, default);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenContactDeleted()
    {
        _mockService.Setup(s => s.DeleteContactAsync(1, default))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1, default);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenContactMissing()
    {
        _mockService.Setup(s => s.DeleteContactAsync(999, default))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999, default);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
