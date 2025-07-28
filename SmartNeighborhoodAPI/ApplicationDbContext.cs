using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurProjectSmartNeiborhood.Configuration;
using SmartNeighborhoodAPI.Configuration;
using SmartNeighborhoodAPI.Entites;
using System.Reflection.Emit;


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
            builder.ApplyConfiguration(new FamilyTypeConfiguration());
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


        }

        public DbSet<Person> People { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyCatgory> FamilyCatgories { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<FamilyType> FamilyTypes { get; set; }
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
        public DbSet<DeliveryCaptain> DeliveryCaptains { get; set; }
    }
}
