using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IIssueStatusHandlerFactory
    {
        IIssueStatusHandler Create(IssueStatus status);
    }
}
