using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.IssueStatusHandlers
{
    public class ResolvedIssueHandler : IIssueStatusHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ResolvedIssueHandler> _logger;
        private readonly INotificationFactory _notificationFactory;

        public ResolvedIssueHandler(
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<ResolvedIssueHandler> logger,
            INotificationFactory notificationFactory)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _notificationFactory = notificationFactory;
        }

        public async Task<ApiResponse<bool>> HandleAsync(Issue issue, UpdateIssueDto dto)
        {
            issue.ResolvedAt = DateTime.UtcNow;
            _logger.LogInformation("Issue {IssueId} resolved at {ResolvedAt}.", issue.Id, issue.ResolvedAt);

            await _context.SaveChangesAsync();

            var sender = _notificationFactory.Create(NotificationChannel.Push);
            await sender.SendAsync(
                issue.ReporterId,
                "Issue Resolved",
                "Your issue has been resolved.");

            return ApiResponse<bool>.Success(true);
        }
    }
}
