using Microsoft.AspNetCore.Identity;

namespace SmartNeighborhoodAPI.Configuration
{


    public class IdentityUserRoleSeedConfiguration
        : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.HasData(
                new IdentityUserRole<int>
                {
                    UserId = 1, 
                    RoleId = 1 
                }
            );
        }
    }


}
