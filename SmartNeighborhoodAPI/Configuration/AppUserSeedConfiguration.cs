using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration;

    public class AppUserSeedConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var adminUserId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa";

            var adminUser = new AppUser
            {
                Id = adminUserId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "sys.smartneighborhood@gmail.com",
                NormalizedEmail = "SYS.SMARTNEIGHBORHOOD@GMAIL.COM",
                EmailConfirmed = true,
                PersonId = 1,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null!, "Mub_12345")
            };

            builder.HasData(adminUser);
        }
    }


