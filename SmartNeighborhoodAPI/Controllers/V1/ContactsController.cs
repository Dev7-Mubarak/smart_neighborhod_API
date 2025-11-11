using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.Contact;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [SwaggerTag("Contacts management endpoints")]
    public class ContactsController : AppControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet(Contacts.GetAllByAuthority)]
        public async Task<IActionResult> GetByAuthorityAsync([FromRoute] int authorityId, CancellationToken ct = default)
            => Response(await _contactService.GetContactsByAuthorityAsync(authorityId, ct));

        [HttpGet(Contacts.GetById)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken ct = default)
            => Response(await _contactService.GetContactByIdAsync(id, ct));

        [HttpPost(Contacts.AddToAuthority)]
        public async Task<IActionResult> AddToAuthorityAsync([FromRoute] int authorityId, [FromBody] AddContactDto dto, CancellationToken ct = default)
            => Response(await _contactService.AddContactAsync(authorityId, dto, ct));

        [HttpPut(Contacts.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateContactDto dto, CancellationToken ct = default)
            => Response(await _contactService.UpdateContactAsync(id, dto, ct));

        [HttpDelete(Contacts.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken ct = default)
            => Response(await _contactService.DeleteContactAsync(id, ct));
    }
}
