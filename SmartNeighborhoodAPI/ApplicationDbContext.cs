using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            builder.Entity<MemberType>().HasData(
                new MemberType { Id = 1, Name = "Father" },
                new MemberType { Id = 2, Name = "Mother" },
                new MemberType { Id = 3, Name = "Son" },
                new MemberType { Id = 4, Name = "Daughter" },
                new MemberType { Id = 5, Name = "Grandfather" },
                new MemberType { Id = 6, Name = "Grandmother" }
            );

            builder.Entity<FamilyType>().HasData(
                new FamilyType { Id = 1, Name = "Orphans Family" },
                new FamilyType { Id = 2, Name = "Widow Family" },
                new FamilyType { Id = 3, Name = "Single Parent Family" },
                new FamilyType { Id = 4, Name = "Extended Family" }
            );

            builder.Entity<FamilyCatgory>().HasData(
                new FamilyCatgory { Id = 2, Name = "A" },
                new FamilyCatgory { Id = 3, Name = "B" },
                new FamilyCatgory { Id = 4, Name = "C" }
            );

            var adminRoleId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "BlockManager", NormalizedName = "BLOCKMANAGER" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }
            );

            var adminUserId = Guid.NewGuid().ToString();

            var adminUser = new AppUser
            {
                Id = adminUserId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin@123")
            };

            builder.Entity<AppUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            );
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyCatgory> FamilyCatgories { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<FamilyType> FamilyTypes { get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }
        public DbSet<Block> Blocks { get; set; }


        public DbSet<Ad> Ads { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<ConfilctParty> ConfilctParties { get; set; }
        //public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<ComplainType> ComplainTypes { get; set; }
        public DbSet<ProjectFamily> ProjectFamilies { get; set; }
        public DbSet<ProjectCatogory> ProjectCatogories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        //public DbSet<PersonComplain> PersonComplains { get; set; }

    }
}
