using Microsoft.Extensions.DependencyInjection;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.IssueStatusHandlers
{
    public class IssueStatusHandlerFactory : IIssueStatusHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public IssueStatusHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IIssueStatusHandler Create(IssueStatus status) => status switch
        {
            IssueStatus.Open => _serviceProvider.GetRequiredService<OpenIssueHandler>(),
            IssueStatus.InProgress => _serviceProvider.GetRequiredService<InProgressIssueHandler>(),
            IssueStatus.Resolved => _serviceProvider.GetRequiredService<ResolvedIssueHandler>(),
            IssueStatus.Closed => _serviceProvider.GetRequiredService<ClosedIssueHandler>(),
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, $"No handler registered for IssueStatus '{status}'.")
        };
    }
}
