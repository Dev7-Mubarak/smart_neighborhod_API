namespace SmartNeighborhoodAPI.Interfaces
{
    /// <summary>
    /// Exposes a secure, centralised view of the currently authenticated user's
    /// identity and authorisation context, extracted from JWT claims.
    ///
    /// Security contract (deny-by-default):
    ///   • Every property returns the most restrictive value when the claim is
    ///     absent or the HTTP context is unavailable.
    ///   • <see cref="IsSuperAdmin"/> is <c>false</c> unless the role is explicitly
    ///     present in the token.
    ///   • <see cref="AssignedNeighborhoodId"/> is <c>null</c> when the claim is
    ///     missing, which causes all row-level query filters to produce no rows
    ///     (NULL ≠ anything in SQL).
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// ASP.NET Identity user ID (the <c>ClaimTypes.NameIdentifier</c> claim).
        /// <c>null</c> when the user is unauthenticated.
        /// </summary>
        string? UserId { get; }

        /// <summary>
        /// Primary role of the authenticated user.
        /// <c>null</c> when no role claim is present.
        /// </summary>
        string? Role { get; }

        /// <summary>
        /// <c>true</c> only when the user explicitly holds the
        /// <b>SuperAdmin</b> role in the JWT. Defaults to <c>false</c>.
        /// </summary>
        bool IsSuperAdmin { get; }

        /// <summary>
        /// The <see cref="SmartNeighborhoodAPI.Entites.ResidentialNeighborhood"/>
        /// this manager is responsible for, encoded as the custom
        /// <c>"neighborhood_id"</c> JWT claim.
        ///
        /// Returns <c>null</c> (never zero) when the claim is absent or unparseable,
        /// so that EF Core's query filters evaluate the SQL predicate
        /// <c>NeighborhoodId = NULL</c> — which yields no rows — guaranteeing
        /// zero-data-leak on misconfigured tokens.
        /// </summary>
        int? AssignedNeighborhoodId { get; }
    }
}
