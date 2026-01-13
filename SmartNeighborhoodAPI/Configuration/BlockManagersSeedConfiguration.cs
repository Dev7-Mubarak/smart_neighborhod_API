using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class BlockManagersSeedConfiguration 
        : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            // Seed Block Managers for testing
            builder.HasData(
                // Neighborhood Manager
                new AppUser
                {
                    Id = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                    UserName = "NeighborhoodManager1",
                    NormalizedUserName = "NEIGHBORHOODMANAGER1",
                    Email = "neighborhood.manager@test.com",
                    NormalizedEmail = "NEIGHBORHOOD.MANAGER@TEST.COM",
                    EmailConfirmed = true,
                    PersonId = 2,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                // Unit Manager
                new AppUser
                {
                    Id = "cccccccc-cccc-cccc-cccc-cccccccccccc",
                    UserName = "UnitManager1",
                    NormalizedUserName = "UNITMANAGER1",
                    Email = "unit.manager@test.com",
                    NormalizedEmail = "UNIT.MANAGER@TEST.COM",
                    EmailConfirmed = true,
                    PersonId = 3,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                // Block Manager 1
                new AppUser
                {
                    Id = "dddddddd-dddd-dddd-dddd-dddddddddddd",
                    UserName = "BlockManager1",
                    NormalizedUserName = "BLOCKMANAGER1",
                    Email = "block.manager1@test.com",
                    NormalizedEmail = "BLOCK.MANAGER1@TEST.COM",
                    EmailConfirmed = true,
                    PersonId = 4,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                // Block Manager 2
                new AppUser
                {
                    Id = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                    UserName = "BlockManager2",
                    NormalizedUserName = "BLOCKMANAGER2",
                    Email = "block.manager2@test.com",
                    NormalizedEmail = "BLOCK.MANAGER2@TEST.COM",
                    EmailConfirmed = true,
                    PersonId = 5,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                // Block Manager 3
                new AppUser
                {
                    Id = "ffffffff-ffff-ffff-ffff-ffffffffffff",
                    UserName = "BlockManager3",
                    NormalizedUserName = "BLOCKMANAGER3",
                    Email = "block.manager3@test.com",
                    NormalizedEmail = "BLOCK.MANAGER3@TEST.COM",
                    EmailConfirmed = true,
                    PersonId = 6,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                // Block Manager 4
                new AppUser
                {
                    Id = "10101010-1010-1010-1010-101010101010",
                    UserName = "BlockManager4",
                    NormalizedUserName = "BLOCKMANAGER4",
                    Email = "block.manager4@test.com",
                    NormalizedEmail = "BLOCK.MANAGER4@TEST.COM",
                    EmailConfirmed = true,
                    PersonId = 7,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                // Block Manager 5
                new AppUser
                {
                    Id = "20202020-2020-2020-2020-202020202020",
                    UserName = "BlockManager5",
                    NormalizedUserName = "BLOCKMANAGER5",
                    Email = "block.manager5@test.com",
                    NormalizedEmail = "BLOCK.MANAGER5@TEST.COM",
                    EmailConfirmed = true,
                    PersonId = 8,
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                }
            );
        }
    }
}
