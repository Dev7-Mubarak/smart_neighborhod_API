using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
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
            Description = "Retrieves all people in the system with optional paging and searching."
        )]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync(
            [SwaggerParameter(Description = "Page number (starting from 1). Default is 1.")]
            int pageNumber = 1,

            [SwaggerParameter(Description = "Number of records per page. Default is 10.")]
            int pageSize = 10,

            [SwaggerParameter(Description = "Optional search term to filter people by name or other criteria.")]
            string? search = null
        )
        {
            return Response(await _personService.GetAllAsync(pageNumber, pageSize, search));
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
