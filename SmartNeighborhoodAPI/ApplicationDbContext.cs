using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurProjectSmartNeiborhood.Configuration;
using SmartNeighborhoodAPI.Configuration;
using SmartNeighborhoodAPI.Entites;


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
        //public DbSet<ConfilctParty> ConfilctParties { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<ComplainType> ComplainTypes { get; set; }
        public DbSet<ProjectFamily> ProjectFamilies { get; set; }
        public DbSet<ProjectCatogory> ProjectCatogories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
    }
}
