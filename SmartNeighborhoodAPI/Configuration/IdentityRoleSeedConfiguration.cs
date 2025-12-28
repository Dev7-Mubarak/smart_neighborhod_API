using Microsoft.AspNetCore.Identity;

namespace SmartNeighborhoodAPI.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SmartNeighborhoodAPI.Entites;

    public class IdentityRoleSeedConfiguration
        : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = "BlockManager",
                    NormalizedName = "BLOCKMANAGER"
                },
                new ApplicationRole
                {
                    Id = 3,
                    Name = "UnitManager",
                    NormalizedName = "UNITMANAGER"
                },
                new ApplicationRole
                {
                    Id = 4,
                    Name = "ResidentialNeighborhoodManager",
                    NormalizedName = "RESIDENTIALNEIGHBORHOODMANAGER"
                }
            );
        }
    }


}
