using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurProjectSmartNeiborhood.Configuration;
using SmartNeighborhoodAPI.Configuration;
using SmartNeighborhoodAPI.Entites;
using System.Reflection.Emit;
using static SmartNeighborhoodAPI.Helpers.Router;


namespace SmartNeighborhoodAPI
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new MemberFamilyRoleConfiguration());
            builder.ApplyConfiguration(new FamilyCatgoryConfiguration());
            builder.ApplyConfiguration(new IdentityRoleSeedConfiguration());
            builder.ApplyConfiguration(new AppUserSeedConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleSeedConfiguration());
            builder.ApplyConfiguration(new ProjectCatgoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProjectConfiguration());

            builder.Entity<TeamRole>().HasData(
                   new TeamRole { Id = 1, Name = "مدير المشروع"},
                   new TeamRole { Id = 2, Name = "النائب"},
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

            // Admin user seed
            var adminUserId = "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa";

            var hasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                Id = adminUserId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "sys.smartneighborhood@gmail.com",
                NormalizedEmail = "SYS.SMARTNEIGHBORHOOD@GMAIL.COM",
                EmailConfirmed = true,
                PersonId = 1,
                PasswordHash = hasher.HashPassword(null, "Mub_12345")
            };

            builder.Entity<AppUser>().HasData(adminUser);

        }
        public DbSet<Contact> Contacts { get; set; }

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
    }
}
