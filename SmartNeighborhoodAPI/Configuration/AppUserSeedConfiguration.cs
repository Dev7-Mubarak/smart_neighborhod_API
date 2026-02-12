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
        var blockManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb";
        var unitManagerId = "cccccccc-cccc-cccc-cccc-cccccccccccc";
        var rnManagerId = "dddddddd-dddd-dddd-dddd-dddddddddddd";

        builder.HasData(
            // Admin
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
            },
            // Block Manager
            new AppUser
            {
                Id = blockManagerId,
                UserName = "BlockManager",
                NormalizedUserName = "BLOCKMANAGER",
                Email = "block.manager@local",
                NormalizedEmail = "BLOCK.MANAGER@LOCAL",
                EmailConfirmed = true,
                PersonId = 2,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null!, "Mub_123456")
            },
            // Unit Manager
            new AppUser
            {
                Id = unitManagerId,
                UserName = "UnitManager",
                NormalizedUserName = "UNITMANAGER",
                Email = "unit.manager@local",
                NormalizedEmail = "UNIT.MANAGER@LOCAL",
                EmailConfirmed = true,
                PersonId = 3,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null!, "Mub_123457")
            },
            // Residential Neighborhood Manager
            new AppUser
            {
                Id = rnManagerId,
                UserName = "ResidentialNeighborhoodManager",
                NormalizedUserName = "RESIDENTIALNEIGHBORHOODMANAGER",
                Email = "rn.manager@local",
                NormalizedEmail = "RN.MANAGER@LOCAL",
                EmailConfirmed = true,
                PersonId = 4,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null!, "Mub_123458")
            }
        );
    }
}