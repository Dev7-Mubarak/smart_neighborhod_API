using Microsoft.EntityFrameworkCore;
using Moq;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Tests.GlobalQueryFilters;

/// <summary>
/// Integration tests that verify EF Core Global Query Filters enforce
/// Row-Level Security (data isolation) based on ICurrentUserService.
///
/// Strategy
/// ─────────
/// • Each test gets its own InMemory database (unique name) so they are
///   fully isolated and can run in parallel.
/// • A SuperAdmin context is used to seed data, bypassing all filters.
/// • A restricted context is then created to assert that only the
///   expected rows are visible.
/// • Related entities (ResidentialUnit → Block) are pre-loaded into the
///   test context's change tracker with .Load() so that EF Core's
///   in-memory LINQ filter can traverse the navigation chain
///   (Issue → Block → ResidentialUnit → ResidentialNeighborhoodId).
/// </summary>
public class GlobalQueryFilterTests
{
    // ── Context factory ───────────────────────────────────────────────────────

    /// <summary>Creates a fresh context backed by a named InMemory database.</summary>
    private static ApplicationDbContext CreateContext(string dbName, ICurrentUserService user)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new ApplicationDbContext(options, user);
    }

    // ── Mock helpers ──────────────────────────────────────────────────────────

    /// <summary>Simulates a Neighborhood Manager scoped to Neighborhood A (Id = 1).</summary>
    private static ICurrentUserService MockNeighborhoodAManager()
    {
        var mock = new Mock<ICurrentUserService>();
        mock.Setup(s => s.IsSuperAdmin).Returns(false);
        mock.Setup(s => s.AssignedNeighborhoodId).Returns(1);
        mock.Setup(s => s.UserId).Returns("manager-a");
        mock.Setup(s => s.Role).Returns("NeighborhoodManager");
        return mock.Object;
    }

    /// <summary>Simulates a SuperAdmin with no neighborhood restriction.</summary>
    private static ICurrentUserService MockSuperAdmin()
    {
        var mock = new Mock<ICurrentUserService>();
        mock.Setup(s => s.IsSuperAdmin).Returns(true);
        mock.Setup(s => s.AssignedNeighborhoodId).Returns((int?)null);
        mock.Setup(s => s.UserId).Returns("super-admin");
        mock.Setup(s => s.Role).Returns("SuperAdmin");
        return mock.Object;
    }

    // ── Seeder ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Seeds the InMemory store with:
    ///   • 2 ResidentialNeighborhoods  (Id 1 = "Neighborhood A", Id 2 = "Neighborhood B")
    ///   • 1 ResidentialUnit per neighborhood
    ///   • 1 Block per unit
    ///   • 2 Issues per block  →  4 issues total
    ///
    /// Uses a SuperAdmin context so every global query filter is bypassed
    /// and all rows reach the store.
    /// </summary>
    private static void Seed(string dbName)
    {
        using var ctx = CreateContext(dbName, MockSuperAdmin());

        // Neighborhoods
        ctx.ResidentialNeighborhoods.AddRange(
            new ResidentialNeighborhood { Id = 1, Name = "Neighborhood A", NeighborhoodManagerId = "mgr-a" },
            new ResidentialNeighborhood { Id = 2, Name = "Neighborhood B", NeighborhoodManagerId = "mgr-b" }
        );

        // Residential units — one per neighborhood
        ctx.ResidentialUnits.AddRange(
            new ResidentialUnit { Id = 1, Name = "Unit A", ResidentialNeighborhoodId = 1, UnitManagerId = "mgr-a" },
            new ResidentialUnit { Id = 2, Name = "Unit B", ResidentialNeighborhoodId = 2, UnitManagerId = "mgr-b" }
        );

        // Blocks — one per unit
        // BlockManagerId is a string FK to AppUser; InMemory doesn't enforce FKs,
        // so we can use arbitrary marker strings without seeding full AppUser records.
        ctx.Blocks.AddRange(
            new Block { Id = 1, Name = "Block A", ResidentialUnitId = 1, BlockManagerId = "mgr-a" },
            new Block { Id = 2, Name = "Block B", ResidentialUnitId = 2, BlockManagerId = "mgr-b" }
        );

        // Issues — 2 anchored to Neighborhood A, 2 anchored to Neighborhood B.
        // Description and Category are IsRequired() per IssueEntityTypeConfiguration,
        // so the InMemory provider enforces them at SaveChanges time.
        ctx.Issues.AddRange(
            new Issue { Id = Guid.NewGuid(), Title = "Issue A-1", Description = "Desc A-1", Category = "General", ReporterId = "user-a", BlockId = 1 },
            new Issue { Id = Guid.NewGuid(), Title = "Issue A-2", Description = "Desc A-2", Category = "General", ReporterId = "user-a", BlockId = 1 },
            new Issue { Id = Guid.NewGuid(), Title = "Issue B-1", Description = "Desc B-1", Category = "General", ReporterId = "user-b", BlockId = 2 },
            new Issue { Id = Guid.NewGuid(), Title = "Issue B-2", Description = "Desc B-2", Category = "General", ReporterId = "user-b", BlockId = 2 }
        );

        ctx.SaveChanges();
    }

    // ── Helper ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Pre-loads entities required for the global query filter navigation chain
    /// into the context's change tracker.
    ///
    /// Execution order matters:
    ///   1. ResidentialUnits — no filter on this type; all units are loaded so
    ///      the Block filter can resolve ResidentialNeighborhoodId.
    ///   2. Blocks          — the Block filter is applied here, so the tracker
    ///      only retains blocks from the current user's neighborhood.
    ///
    /// Once these two sets are tracked, EF Core's relationship-fixup engine
    /// populates Issue.Block and Block.ResidentialUnit automatically when
    /// Issues are loaded, allowing the Issue query filter to evaluate correctly.
    /// </summary>
    private static void PreLoadNavigationChain(ApplicationDbContext ctx)
    {
        ctx.ResidentialUnits.Load();   // no filter — loads both units
        ctx.Blocks.Load();             // Block filter applied — only the user's blocks
    }

    // ── Test 1: Neighborhood Manager sees only his own issues ─────────────────

    [Fact(DisplayName = "Test 1 — Neighborhood Manager sees only issues in his neighborhood")]
    public void NeighborhoodManager_Sees_OnlyIsssuesInAssignedNeighborhood()
    {
        // Arrange
        var dbName = $"TestDb_{Guid.NewGuid()}";
        Seed(dbName);

        using var ctx = CreateContext(dbName, MockNeighborhoodAManager());

        PreLoadNavigationChain(ctx);   // populate tracker so filter can traverse nav props

        // Act
        var issues = ctx.Issues.ToList();

        // Assert
        Assert.Equal(2, issues.Count);

        // Every returned issue must belong to Neighborhood A (BlockId 1)
        Assert.All(issues, i =>
        {
            Assert.NotNull(i.Block);
            Assert.Equal(1, i.Block!.ResidentialUnit.ResidentialNeighborhoodId);
        });
    }

    // ── Test 2: Super Admin sees all issues regardless of neighborhood ─────────

    [Fact(DisplayName = "Test 2 — Super Admin sees all issues across all neighborhoods")]
    public void SuperAdmin_Sees_AllIssues()
    {
        // Arrange
        var dbName = $"TestDb_{Guid.NewGuid()}";
        Seed(dbName);

        using var ctx = CreateContext(dbName, MockSuperAdmin());

        // Act — no pre-loading needed; IsSuperAdmin == true short-circuits every filter
        var issues = ctx.Issues.ToList();

        // Assert — all 4 seeded issues must be visible
        Assert.Equal(4, issues.Count);
    }
}
