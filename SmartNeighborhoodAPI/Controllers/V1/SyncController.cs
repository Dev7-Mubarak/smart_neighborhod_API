using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Sync;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    public class SyncController : AppControllerBase
    {
        private readonly ISyncService _syncService;
        private readonly ILogger<SyncController> _logger;
        private readonly TeamMemberService _teamMemberService;

        public SyncController(ISyncService syncService, ILogger<SyncController> logger, TeamMemberService teamMemberService)
        {
            _syncService = syncService;
            _logger = logger;
            _teamMemberService = teamMemberService;
        }

        [HttpGet(Router.Sync.Pull)]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SyncPullResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PullChanges(
            [FromQuery(Name = "sync_token")] DateTime syncToken,
            [FromQuery(Name = "entity_types")] string? entityTypes = null,
            [FromQuery(Name = "batch_size")] int batchSize = 1000,
            [FromQuery(Name = "include_deleted")] bool includeDeleted = true)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _syncService.PullChangesAsync(syncToken, entityTypes, batchSize, includeDeleted, userId);
            return Response(ApiResponse<SyncPullResponse>.Success(result));
        }


        [HttpPost(Router.Sync.Push)]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SyncPushResponse))]
        [ProducesResponseType(StatusCodes.Status207MultiStatus, Type = typeof(SyncPushResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PushChanges([FromBody] SyncPushRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var idempotencyKey = Request.Headers["Idempotency-Key"].ToString();

            var response = await _syncService.PushChangesAsync(request, userId, idempotencyKey);

            if (response.TotalConflicts > 0)
                return StatusCode(207, response);

            return Response(ApiResponse<SyncPushResponse>.Success(response));
        }


        [HttpGet(Router.Sync.Status)]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SyncStatusResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSyncStatus()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _syncService.GetSyncStatusAsync(userId);
            return Response(ApiResponse<SyncStatusResponse>.Success(response));
        }

        [HttpGet(Router.TeamMembers.GetAll)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all team members", Description = "Retrieves all team members.")]
        [ProducesResponseType(typeof(IEnumerable<TeamMemberDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTeamMembersForSync()
        {
            return Response(await _teamMemberService.GetAll());
        }
    }
}
