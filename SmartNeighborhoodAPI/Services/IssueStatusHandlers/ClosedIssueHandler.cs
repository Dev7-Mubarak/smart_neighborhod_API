using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.IssueStatusHandlers
{
    public class ClosedIssueHandler : IIssueStatusHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClosedIssueHandler> _logger;

        public ClosedIssueHandler(ApplicationDbContext context, IMapper mapper, ILogger<ClosedIssueHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<ApiResponse<bool>> HandleAsync(Issue issue, UpdateIssueDto dto)
        {
            _logger.LogInformation("Issue {IssueId} transitioned to Closed status.", issue.Id);
            return Task.FromResult(ApiResponse<bool>.Success(true));
        }
    }
}
