using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using OurProjectSmartNeiborhood.Services;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ValidateActionFilter]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Person management endpoints")]
    public class PersonController : AppControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("get-all")]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all people", Description = "Retrieves all people in the system.")]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Response(await _personService.GetAll());
        }

        [HttpGet("get-by-id/{id:int}")]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get person by ID", Description = "Retrieve a person by their ID.")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Person ID to retrieve", Required = true)] int id)
        {
            return Response(await _personService.GetByIdAsync(id));
        }

        [HttpPost("[action]")]
        [MapToApiVersion("1.0")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Add a new person", Description = "Adds a new person including optional image.")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromForm, SwaggerParameter("Person data to create", Required = true)] CreatePersonDto dto)
        {
            return Response(await _personService.AddAsync(dto));
        }

        [HttpPut("[action]/{id:int}")]
        [MapToApiVersion("1.0")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Update person", Description = "Updates an existing person including optional image.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Person ID to update", Required = true)] int id,
            [FromForm, SwaggerParameter("Updated person data", Required = true)] CreatePersonDto dto)
        {
            return Response(await _personService.UpdateAsync(id, dto));
        }

        [HttpDelete("[action]/{id:int}")]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Delete person", Description = "Deletes a specific person by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute, SwaggerParameter("Person ID to delete", Required = true)] int id)
        {
            return Response(await _personService.DeleteAsync(id));
        }
    }
}
