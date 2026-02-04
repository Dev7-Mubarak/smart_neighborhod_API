using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites;

public class AppUserSeedConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        var hasher = new PasswordHasher<AppUser>();
        var adminUserId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa";

        builder.HasData(
            new AppUser
            {
                Id = adminUserId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "sys.smartneighborhood@gmail.com",
                NormalizedEmail = "SYS.SMARTNEIGHBORHOOD@GMAIL.COM",
                EmailConfirmed = true,
                PersonId = 1,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null!, "Mub_12345")
            }
            );
    }
}