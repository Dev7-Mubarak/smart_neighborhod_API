using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Interfaces;
using static SmartNeighborhoodAPI.Helpers.Router;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    public class GovernmentInstitutionsController : AppControllerBase
    {
        private readonly IGovernmentInstitutionsService _authorityService;

        public GovernmentInstitutionsController(IGovernmentInstitutionsService authorityService)
        {
            _authorityService = authorityService;
        }

        [HttpGet(GovernmentInstitutions.GetAll)]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
        {
            var result = await _authorityService.GetAllAuthoritiesAsync(ct);
            return Response(result);
        }

        [HttpGet(GovernmentInstitutions.GetById)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken ct = default)
        {
            return Response(await _authorityService.GetAuthorityByIdAsync(id, ct));
        }

        [HttpPost(GovernmentInstitutions.Add)]
        public async Task<IActionResult> AddAsync([FromBody] AddGovernmentInstitutionsDto dto, CancellationToken ct = default)
        {
            return Response(await _authorityService.AddAuthorityAsync(dto, ct));
        }
        [HttpPut(GovernmentInstitutions.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateAuthorityDto dto, CancellationToken ct = default)
        {
            return Response(await _authorityService.UpdateAuthorityAsync(id, dto, ct));
        }

        [HttpDelete(GovernmentInstitutions.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken ct = default)
        {
            return Response(await _authorityService.DeleteAuthorityAsync(id, ct));
        }
    }
}
