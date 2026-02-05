using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [ApiController]
    [Authorize]
    public class IssuesController : AppControllerBase
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet(Router.Issues.GetAll)]
        public async Task<IActionResult> GetIssues(
            [FromQuery] string status, [FromQuery] string priority, [FromQuery] string category,
            [FromQuery] string sortBy, [FromQuery] string sortOrder,
            [FromQuery] int page = 1, [FromQuery] int limit = 20)
        {
            var result = await _issueService.GetAllIssuesAsync(status, priority, category, sortBy, sortOrder, page, limit);
            return Response(result);
        }

        [HttpGet(Router.Issues.GetById)]
        public async Task<IActionResult> GetIssue(Guid id)
        {
            var result = await _issueService.GetIssueByIdAsync(id);
            return Response(result);
        }

        [HttpPost(Router.Issues.Add)]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto createIssueDto)
        {
            var reporterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _issueService.CreateIssueAsync(createIssueDto, reporterId);
            return Response(result);
        }

        [HttpPatch(Router.Issues.Update)]
        public async Task<IActionResult> UpdateIssue(Guid id, [FromBody] UpdateIssueDto updateIssueDto)
        {
            var result = await _issueService.UpdateIssueAsync(id, updateIssueDto);
            return Response(result);
        }

        [HttpDelete(Router.Issues.Delete)]
        public async Task<IActionResult> DeleteIssue(Guid id)
        {
            var result = await _issueService.DeleteIssueAsync(id);
            return Response(result);
        }
    }
}
