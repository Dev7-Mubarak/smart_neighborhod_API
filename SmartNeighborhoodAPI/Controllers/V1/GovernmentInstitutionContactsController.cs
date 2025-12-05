using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MramProject.Interface;
using SmartNeighborhoodAPI.Controllers.V1;
using SmartNeighborhoodAPI.Helpers.DTOs.Government_InstitutionsContacts;
using Swashbuckle.AspNetCore.Annotations;
using static SmartNeighborhoodAPI.Helpers.Router;

namespace MramProject.Controllers
{
    [SwaggerTag("Government Institution Contacts management endpoints")]
    public class GovernmentInstitutionContactsController : AppControllerBase
    {
        private readonly IGovernmentInstitutionContactService _contactService;
        private readonly ILogger<GovernmentInstitutionContactsController> _logger;

        public GovernmentInstitutionContactsController(IGovernmentInstitutionContactService contactService, ILogger<GovernmentInstitutionContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet(GovernmentInstitutionContacts.GetAllByAuthority)]
        [SwaggerOperation(Summary = "Get contacts for an authority", Description = "Returns all government institution contacts for a specific authority.")]
        [ProducesResponseType(typeof(IEnumerable<GetGovernmentInstitutionContactDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAuthorityAsync(
            [FromRoute, SwaggerParameter("ID of the authority", Required = true)] int authorityId,
            CancellationToken ct = default)
            => Response(await _contactService.GetContactsByAuthorityAsync(authorityId, ct));

        [HttpGet(GovernmentInstitutionContacts.GetById)]
        [SwaggerOperation(Summary = "Get contact by ID", Description = "Retrieve a government institution contact by its ID.")]
        [ProducesResponseType(typeof(GetGovernmentInstitutionContactDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute, SwaggerParameter("Contact ID to retrieve", Required = true)] int id,
            CancellationToken ct = default)
            => Response(await _contactService.GetContactByIdAsync(id, ct));

        [HttpPost(GovernmentInstitutionContacts.AddToAuthority)]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Create a new contact for an authority", Description = "Adds a new government institution contact to the specified authority.")]
        [ProducesResponseType(typeof(GetGovernmentInstitutionContactDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToAuthorityAsync(
            [FromRoute, SwaggerParameter("ID of the authority to add contact to", Required = true)] int authorityId,
            [FromBody, SwaggerParameter("Contact data to create", Required = true)] AddGovernmentInstitutionContactDto dto,
            CancellationToken ct = default)
            => Response(await _contactService.AddContactAsync(authorityId, dto, ct));

        [HttpPut(GovernmentInstitutionContacts.Update)]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Update an existing contact", Description = "Updates fields of an existing government institution contact.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute, SwaggerParameter("Contact ID to update", Required = true)] int id,
            [FromBody, SwaggerParameter("Updated contact data", Required = true)] UpdateGovernmentInstitutionContactDto dto,
            CancellationToken ct = default)
            => Response(await _contactService.UpdateContactAsync(id, dto, ct));

        [HttpDelete(GovernmentInstitutionContacts.Delete)]
        [SwaggerOperation(Summary = "Delete a contact", Description = "Deletes a government institution contact by ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute, SwaggerParameter("Contact ID to delete", Required = true)] int id,
            CancellationToken ct = default)
            => Response(await _contactService.DeleteContactAsync(id, ct));
    }
}
