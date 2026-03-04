using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Tests.Controllers.Person;

public class PersonControllerTests
{
    private readonly Mock<IPersonService> _mockService;
    private readonly PersonController _controller;

    public PersonControllerTests()
    {
        _mockService = new Mock<IPersonService>();
        _controller = new PersonController(_mockService.Object);
    }

    // ── GetAllAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenPersonsExist()
    {
        var persons = new List<PersonDto>
        {
            new() { Id = 1 },
            new() { Id = 2 }
        };
        var paged = new PaginatedResult<PersonDto>(persons);
        var filter = new PersonFilterParams();

        _mockService.Setup(s => s.GetAllAsync(filter))
            .ReturnsAsync(ApiResponse<PaginatedResult<PersonDto>>.Success(paged));

        var result = await _controller.GetAllAsync(filter);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PaginatedResult<PersonDto>>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(2, response.Data!.items.Count);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithEmptyList()
    {
        var paged = new PaginatedResult<PersonDto>(new List<PersonDto>());
        var filter = new PersonFilterParams();

        _mockService.Setup(s => s.GetAllAsync(filter))
            .ReturnsAsync(ApiResponse<PaginatedResult<PersonDto>>.Success(paged));

        var result = await _controller.GetAllAsync(filter);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PaginatedResult<PersonDto>>>(ok.Value);
        Assert.Empty(response.Data!.items);
    }

    // ── GetByIdAsync ─────────────────────────────────────────────────────────

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WhenPersonExists()
    {
        var dto = new PersonDto { Id = 1 };
        _mockService.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(ApiResponse<PersonDto>.Success(dto));

        var result = await _controller.GetByIdAsync(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<PersonDto>>(ok.Value);
        Assert.True(response.IsSuccess);
        Assert.Equal(1, response.Data!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenPersonMissing()
    {
        _mockService.Setup(s => s.GetByIdAsync(999))
            .ReturnsAsync(ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.GetByIdAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── AddAsync ─────────────────────────────────────────────────────────────

    [Fact]
    public async Task AddAsync_ReturnsOk_WhenPersonCreated()
    {
        var dto = new CreatePersonDto();
        var entity = new SmartNeighborhoodAPI.Entites.Person { Id = 1 };
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<SmartNeighborhoodAPI.Entites.Person>.Success(entity));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddAsync_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new CreatePersonDto();
        _mockService.Setup(s => s.AddAsync(dto))
            .ReturnsAsync(ApiResponse<SmartNeighborhoodAPI.Entites.Person>.Error(HttpStatusCode.BadRequest, "Invalid"));

        var result = await _controller.AddAsync(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── UpdateAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_ReturnsOk_WhenPersonUpdated()
    {
        var dto = new CreatePersonDto();
        _mockService.Setup(s => s.UpdateAsync(1, dto))
            .ReturnsAsync(ApiResponse<string>.Success("Updated"));

        var result = await _controller.UpdateAsync(1, dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNotFound_WhenPersonMissing()
    {
        var dto = new CreatePersonDto();
        _mockService.Setup(s => s.UpdateAsync(999, dto))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.UpdateAsync(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── DeleteAsync ──────────────────────────────────────────────────────────

    [Fact]
    public async Task DeleteAsync_ReturnsOk_WhenPersonDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1))
            .ReturnsAsync(ApiResponse<string>.Success("Deleted"));

        var result = await _controller.DeleteAsync(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenPersonMissing()
    {
        _mockService.Setup(s => s.DeleteAsync(999))
            .ReturnsAsync(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Not found"));

        var result = await _controller.DeleteAsync(999);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
