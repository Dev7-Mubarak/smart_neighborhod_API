using System.Security.Claims;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services.Auth
{
    /// <summary>
    /// Reads the authenticated user's identity and neighbourhood context from
    /// the active HTTP request's JWT claims.
    ///
    /// Design decisions:
    ///   • <b>Deny-by-default</b>: every property returns the most restrictive
    ///     value when the claim is absent, the token is invalid, or no HTTP
    ///     context is available (e.g. background workers, EF migrations).
    ///   • <b>Lazy evaluation</b>: claims are resolved on each property access so
    ///     the service always reflects the <em>current</em> request — compatible
    ///     with the scoped <see cref="ApplicationDbContext"/> lifetime.
    ///   • <b>Sealed</b>: prevents accidental subclassing that might loosen the
    ///     security contract.
    /// </summary>
    public sealed class CurrentUserService : ICurrentUserService
    {
        // ── Constants ────────────────────────────────────────────────────────────
        /// <summary>Custom JWT claim that carries the neighbourhood primary key.</summary>
        public const string NeighborhoodIdClaimType = "neighborhood_id";

        private const string SuperAdminRole = "SuperAdmin";

        // ── Dependencies ─────────────────────────────────────────────────────────
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        // ── Private helpers ───────────────────────────────────────────────────────

        /// <summary>
        /// Returns the <see cref="ClaimsPrincipal"/> for the active request,
        /// or <c>null</c> when no HTTP context is available.
        /// </summary>
        private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

        // ── ICurrentUserService ───────────────────────────────────────────────────

        /// <inheritdoc/>
        public string? UserId =>
            Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <inheritdoc/>
        public string? Role =>
            Principal?.FindFirstValue(ClaimTypes.Role);

        /// <inheritdoc/>
        /// <remarks>
        /// Iterates <em>all</em> role claims (a user may have multiple) and returns
        /// <c>true</c> only on an exact match. Defaults to <c>false</c> on any
        /// failure — including a missing principal.
        /// </remarks>
        public bool IsSuperAdmin =>
            Principal?
                .FindAll(ClaimTypes.Role)
                .Any(c => c.Value.Equals(SuperAdminRole, StringComparison.Ordinal))
            ?? false;   // deny when principal is unavailable

        /// <inheritdoc/>
        public int? AssignedNeighborhoodId
        {
            get
            {
                var raw = Principal?.FindFirstValue(NeighborhoodIdClaimType);

                // Explicit null (not 0): EF Core translates `column = NULL` to FALSE
                // in SQL, so missing claims automatically produce zero rows.
                if (string.IsNullOrWhiteSpace(raw))
                    return null;

                // Malformed claim → treat as missing → deny.
                return int.TryParse(raw, out var id) ? id : null;
            }
        }
    }
}
