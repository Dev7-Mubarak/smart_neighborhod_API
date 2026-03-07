using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurProjectSmartNeiborhood.Configuration;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Configuration;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Interfaces;
using System.Reflection.Emit;
using static SmartNeighborhoodAPI.Helpers.Router;


namespace SmartNeighborhoodAPI
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        // ── Row-Level Security context ────────────────────────────────────────────
        // Captured per-request (scoped lifetime). EF Core evaluates the HasQueryFilter
        // lambdas lazily on each query, so the current user's claims are always fresh.
        private readonly ICurrentUserService _currentUser;

        /// <summary>
        /// Runtime constructor — used by the DI container.
        /// <paramref name="currentUser"/> drives all global query filters.
        /// </summary>
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUser)
            : base(options)
        {
            _currentUser = currentUser
                ?? throw new ArgumentNullException(nameof(currentUser));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ── Global Query Filters (Row-Level Security) ─────────────────────────
            // Every filter short-circuits to TRUE for SuperAdmins.
            // For all other roles the predicate traverses the neighbourhood FK chain.
            // IMPORTANT: EF Core translates NULL == int to FALSE in SQL, so a missing
            // AssignedNeighborhoodId claim automatically produces zero rows — the
            // deny-by-default guarantee is enforced at the database level.

            // ── Block (direct FK: Block → ResidentialUnit → Neighbourhood) ─────────
            builder.Entity<Block>().HasQueryFilter(b =>
                _currentUser.IsSuperAdmin ||
                b.ResidentialUnit.ResidentialNeighborhoodId == _currentUser.AssignedNeighborhoodId);

            // ── Family (Block → ResidentialUnit → Neighbourhood) ─────────────────
            builder.Entity<Family>().HasQueryFilter(f =>
                _currentUser.IsSuperAdmin ||
                f.Block.ResidentialUnit.ResidentialNeighborhoodId == _currentUser.AssignedNeighborhoodId);

            // ── ConflictCase (nullable Block → ResidentialUnit → Neighbourhood) ──
            // Block is nullable on ConflictCase, so we guard with a null-check first.
            // In SQL this becomes: BlockId IS NOT NULL AND Block.ResUnit.NeighbId = @p
            builder.Entity<ConflictCase>().HasQueryFilter(c =>
                _currentUser.IsSuperAdmin ||
                (c.Block != null &&
                 c.Block.ResidentialUnit.ResidentialNeighborhoodId == _currentUser.AssignedNeighborhoodId));

            // ── Project (via ProjectBlocks M2M → Block → ResidentialUnit → Neighbourhood)
            // This is the advanced indirect filter: a project belongs to a neighbourhood
            // if ANY of its assigned blocks are in that neighbourhood.
            builder.Entity<Project>().HasQueryFilter(p =>
                _currentUser.IsSuperAdmin ||
                p.ProjectBlocks.Any(pb =>
                    pb.Block.ResidentialUnit.ResidentialNeighborhoodId == _currentUser.AssignedNeighborhoodId));

            // ── Issue (nullable Block → ResidentialUnit → Neighbourhood) ──────────
            // Issue.BlockId was added specifically to anchor issues to a neighbourhood.
            // Pattern mirrors ConflictCase — Block is optional until an issue is
            // formally assigned to a location.
            builder.Entity<Issue>().HasQueryFilter(i =>
                _currentUser.IsSuperAdmin ||
                (i.Block != null &&
                 i.Block.ResidentialUnit.ResidentialNeighborhoodId == _currentUser.AssignedNeighborhoodId));

            // ─────────────────────────────────────────────────────────────────────

            builder.ApplyConfiguration(new MemberFamilyRoleConfiguration());
            builder.ApplyConfiguration(new FamilyCatgoryConfiguration());
            builder.ApplyConfiguration(new IdentityRoleSeedConfiguration());
            builder.ApplyConfiguration(new AppUserSeedConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleSeedConfiguration());
            builder.ApplyConfiguration(new ProjectCatgoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new ResidentialNeighborhoodConfiguration());
            builder.ApplyConfiguration(new ConflictCaseConfiguration());


            builder.ApplyConfiguration(new IssueEntityTypeConfiguration());

            // Seed configurations for حي الثوره
            builder.ApplyConfiguration(new ResidentialUnitSeedConfiguration());
            builder.ApplyConfiguration(new BlockEntityTypeConfiguration());
            builder.ApplyConfiguration(new PersonSeedConfiguration());
            builder.ApplyConfiguration(new FamilyConfiguration());
            builder.ApplyConfiguration(new FamilyMemberSeedConfiguration());

            // Additional entity configurations (relationships + seed)
            builder.ApplyConfiguration(new ConfilctTypeConfiguration());
            builder.ApplyConfiguration(new GovernmentInstitutionConfiguration());
            builder.ApplyConfiguration(new GovernmentInstitutionContactSeedConfiguration());
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new GroupEntityTypeConfiguration());
            builder.ApplyConfiguration(new AdEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProjectFamilyEntityTypeConfiguration());
            builder.ApplyConfiguration(new TeamSeedConfiguration());
            builder.ApplyConfiguration(new TeamMemberSeedConfiguration());
            builder.ApplyConfiguration(new ProjectTeamSeedConfiguration());


            base.OnModelCreating(builder);

            builder.Entity<ResidentialUnit>()
                .HasOne(u => u.UnitManager)
                .WithOne(a => a.ManagesUnit)
                .HasForeignKey<ResidentialUnit>(u => u.UnitManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Block>()
                .HasOne(b => b.ResidentialUnit)
                .WithMany(u => u.Blocks)
                .HasForeignKey(b => b.ResidentialUnitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Block>()
                .HasOne(b => b.BlockManager)
                .WithOne(a => a.ManagesBlock)
                .HasForeignKey<Block>(b => b.BlockManagerId)
                .OnDelete(DeleteBehavior.Restrict);




            builder.Entity<ProjectBlock>()
                .HasKey(pb => new { pb.ProjectId, pb.BlockId });

            builder.Entity<ProjectBlock>()
                .HasOne(pb => pb.Project)
                .WithMany(p => p.ProjectBlocks)
                .HasForeignKey(pb => pb.ProjectId);

            builder.Entity<ProjectBlock>()
                .HasOne(pb => pb.Block)
                .WithMany(b => b.ProjectBlocks)
                .HasForeignKey(pb => pb.BlockId);

            builder.Entity<TeamRole>().HasData(
                new TeamRole { Id = 1, Name = "مدير المشروع" },
                new TeamRole { Id = 2, Name = "النائب" },
                new TeamRole { Id = 3, Name = "عضو" }
            );

            builder.Entity<ConflictCase>()
                .HasOne(c => c.FirstParty)
                .WithMany(f => f.FirstPartyConflictCases)
                .HasForeignKey(c => c.FirstPartyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ConflictCase>()
                .HasOne(c => c.SecondParty)
                .WithMany(f => f.SecondPartyConflictCases)
                .HasForeignKey(c => c.SecondPartyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ConflictCase>()
                .Property(c => c.Notes)
                .IsRequired(false);

            //// Admin user seed
            //var adminUserId = "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa";

            //var hasher = new PasswordHasher<AppUser>();
            //var adminUser = new AppUser
            //{
            //    Id = adminUserId,
            //    UserName = "Admin",
            //    NormalizedUserName = "ADMIN",
            //    Email = "sys.smartneighborhood@gmail.com",
            //    NormalizedEmail = "SYS.SMARTNEIGHBORHOOD@GMAIL.COM",
            //    EmailConfirmed = true,
            //    PersonId = 1,
            //    PasswordHash = hasher.HashPassword(null, "Mub_12345")
            //};

            //builder.Entity<AppUser>().HasData(adminUser);

        }
        public DbSet<GovernmentInstitutionContact> GovernmentInstitutionContacts { get; set; }

        public DbSet<GovernmentInstitution> GovernmentInstitutions { get; set; }

        public DbSet<Person> People { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyCatgory> FamilyCatgories { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<MemberFamilyRole> MemberFamilyRoles { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ConflictCase> ConfilctCases { get; set; }
        public DbSet<ConfilctType> ConfilctTypes { get; set; }
        public DbSet<ProjectFamily> ProjectFamilies { get; set; }
        public DbSet<ProjectCatogory> ProjectCatogories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<TeamRole> TeamRoles { get; set; }
        public DbSet<ResidentialUnit> ResidentialUnits { get; set; }
        public DbSet<ResidentialNeighborhood> ResidentialNeighborhoods { get; set; }
        public DbSet<Issue> Issues { get; set; }


    }
}
