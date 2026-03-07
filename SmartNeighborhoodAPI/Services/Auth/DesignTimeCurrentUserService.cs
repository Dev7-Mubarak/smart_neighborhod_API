using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.Auth
{
    /// <summary>
    /// A no-op implementation of <see cref="ICurrentUserService"/> used exclusively
    /// by <see cref="ApplicationDbContextFactory"/> at EF Core design time
    /// (migrations, scaffolding). It impersonates a SuperAdmin so that all
    /// global query filters evaluate to <c>true</c> and the full schema is visible.
    ///
    /// ⚠️  This class must NEVER be registered in the production DI container.
    /// </summary>
    internal sealed class DesignTimeCurrentUserService : ICurrentUserService
    {
        public string? UserId => "design-time";
        public string? Role => "SuperAdmin";
        public bool IsSuperAdmin => true;
        public int? AssignedNeighborhoodId => null; // irrelevant — IsSuperAdmin bypasses all filters
    }
}
