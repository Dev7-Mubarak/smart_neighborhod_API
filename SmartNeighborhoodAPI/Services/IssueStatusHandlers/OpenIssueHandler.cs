using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.IssueStatusHandlers
{
    public class OpenIssueHandler : IIssueStatusHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OpenIssueHandler> _logger;

        public OpenIssueHandler(ApplicationDbContext context, IMapper mapper, ILogger<OpenIssueHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<ApiResponse<bool>> HandleAsync(Issue issue, UpdateIssueDto dto)
        {
            _logger.LogInformation("Issue {IssueId} transitioned to Open status.", issue.Id);
            return Task.FromResult(ApiResponse<bool>.Success(true));
        }
    }
}
