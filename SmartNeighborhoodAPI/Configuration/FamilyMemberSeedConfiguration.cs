using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class FamilyMemberSeedConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.HasData(
                // Family 1 (Block 1)
                new FamilyMember { Id = 1, PersonId = 11, FamilyId = 1, MemberFamilyRoleId = 1 }, // أب
                new FamilyMember { Id = 2, PersonId = 12, FamilyId = 1, MemberFamilyRoleId = 2 }, // أم
                new FamilyMember { Id = 3, PersonId = 13, FamilyId = 1, MemberFamilyRoleId = 3 }, // ابن

                // Family 2 (Block 1)
                new FamilyMember { Id = 4, PersonId = 14, FamilyId = 2, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 5, PersonId = 15, FamilyId = 2, MemberFamilyRoleId = 2 },
                new FamilyMember { Id = 6, PersonId = 16, FamilyId = 2, MemberFamilyRoleId = 4 },

                // Family 3 (Block 2)
                new FamilyMember { Id = 7, PersonId = 17, FamilyId = 3, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 8, PersonId = 18, FamilyId = 3, MemberFamilyRoleId = 2 },
                new FamilyMember { Id = 9, PersonId = 19, FamilyId = 3, MemberFamilyRoleId = 4 },

                // Family 4 (Block 2)
                new FamilyMember { Id = 10, PersonId = 20, FamilyId = 4, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 11, PersonId = 21, FamilyId = 4, MemberFamilyRoleId = 2 },
                new FamilyMember { Id = 12, PersonId = 22, FamilyId = 4, MemberFamilyRoleId = 3 },

                // Family 5 (Block 3)
                new FamilyMember { Id = 13, PersonId = 23, FamilyId = 5, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 14, PersonId = 24, FamilyId = 5, MemberFamilyRoleId = 2 },
                new FamilyMember { Id = 15, PersonId = 25, FamilyId = 5, MemberFamilyRoleId = 4 },

                // Family 6 (Block 3)
                new FamilyMember { Id = 16, PersonId = 26, FamilyId = 6, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 17, PersonId = 27, FamilyId = 6, MemberFamilyRoleId = 2 },
                new FamilyMember { Id = 18, PersonId = 28, FamilyId = 6, MemberFamilyRoleId = 3 },

                // Family 7 (Block 4)
                new FamilyMember { Id = 19, PersonId = 29, FamilyId = 7, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 20, PersonId = 30, FamilyId = 7, MemberFamilyRoleId = 2 },
                new FamilyMember { Id = 21, PersonId = 31, FamilyId = 7, MemberFamilyRoleId = 3 },

                // Family 8 (Block 4)
                new FamilyMember { Id = 22, PersonId = 32, FamilyId = 8, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 23, PersonId = 33, FamilyId = 8, MemberFamilyRoleId = 2 },
                new FamilyMember { Id = 24, PersonId = 34, FamilyId = 8, MemberFamilyRoleId = 4 },

                // New families 11-20
                new FamilyMember { Id = 25, PersonId = 35, FamilyId = 11, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 26, PersonId = 36, FamilyId = 11, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 27, PersonId = 37, FamilyId = 12, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 28, PersonId = 38, FamilyId = 12, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 29, PersonId = 39, FamilyId = 13, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 30, PersonId = 40, FamilyId = 13, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 31, PersonId = 41, FamilyId = 14, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 32, PersonId = 42, FamilyId = 14, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 33, PersonId = 43, FamilyId = 15, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 34, PersonId = 44, FamilyId = 15, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 35, PersonId = 45, FamilyId = 16, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 36, PersonId = 46, FamilyId = 16, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 37, PersonId = 47, FamilyId = 17, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 38, PersonId = 48, FamilyId = 17, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 39, PersonId = 49, FamilyId = 18, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 40, PersonId = 50, FamilyId = 18, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 41, PersonId = 51, FamilyId = 19, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 42, PersonId = 52, FamilyId = 19, MemberFamilyRoleId = 2 },

                new FamilyMember { Id = 43, PersonId = 53, FamilyId = 20, MemberFamilyRoleId = 1 },
                new FamilyMember { Id = 44, PersonId = 54, FamilyId = 20, MemberFamilyRoleId = 2 }
            );
        }
    }
}
