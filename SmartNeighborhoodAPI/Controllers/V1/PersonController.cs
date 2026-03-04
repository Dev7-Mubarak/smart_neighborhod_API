using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using OurProjectSmartNeiborhood.Services;

namespace SmartNeighborhoodAPI.Controllers.V1
{

    [SwaggerTag("Person management endpoints")]
    public class PersonController : AppControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }
        [HttpGet(Router.Persons.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(
            Summary = "Retrieve all people",
            Description = "Retrieves people with optional filtering by name, gender, marital/occupational/residency status, blood type, chronic diseases, and person type. Supports sorting and pagination."
        )]
        [ProducesResponseType(typeof(PaginatedResult<PersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PersonFilterParams filter)
        {
            return Response(await _personService.GetAllAsync(filter));
        }



        [HttpGet(Router.Persons.GetById)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Get person by ID", Description = "Retrieve a person by their ID.")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, SwaggerParameter("Person ID to retrieve", Required = true)] int id)
        {
            return Response(await _personService.GetByIdAsync(id));
        }

        [HttpPost(Router.Persons.Add)]
        [MapToApiVersion("1.0")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Add a new person", Description = "Adds a new person including optional image.")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromForm, SwaggerParameter("Person data to create", Required = true)] CreatePersonDto dto)
        {
            return Response(await _personService.AddAsync(dto));
        }

        [HttpPut(Router.Persons.Update)]
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

        [HttpDelete(Router.Persons.Delete)]
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
