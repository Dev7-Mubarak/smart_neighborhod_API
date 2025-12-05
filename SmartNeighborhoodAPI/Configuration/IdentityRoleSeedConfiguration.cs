using Microsoft.AspNetCore.Identity;

namespace SmartNeighborhoodAPI.Configuration
{
    public class IdentityRoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = "11111111-1111-1111-1111-111111111111", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "22222222-2222-2222-2222-222222222222", Name = "BlockManager", NormalizedName = "BLOCKMANAGER" },
                new IdentityRole { Id = "33333333-3333-3333-3333-333333333333", Name = "UnitManager", NormalizedName = "UNITMANAGER" }
            );
        }
    }

}
