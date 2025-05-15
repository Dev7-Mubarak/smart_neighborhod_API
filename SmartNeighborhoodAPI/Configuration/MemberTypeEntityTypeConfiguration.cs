namespace OurProjectSmartNeiborhood.Configuration
{
    public class MemberFamilyRoleConfiguration : IEntityTypeConfiguration<MemberFamilyRole>
    {
        public void Configure(EntityTypeBuilder<MemberFamilyRole> builder)
        {
            builder.HasKey(role => role.Id);

            builder.Property(role => role.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(role => role.FamilyMembers)
                   .WithOne(fm => fm.MemberFamilyRole)
                   .HasForeignKey(fm => fm.MemberFamilyRoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new MemberFamilyRole { Id = 1, RoleName = "أب" },
                new MemberFamilyRole { Id = 2, RoleName = "أم" },
                new MemberFamilyRole { Id = 3, RoleName = "ابن" },
                new MemberFamilyRole { Id = 4, RoleName = "ابنة" },
                new MemberFamilyRole { Id = 5, RoleName = "جد" },
                new MemberFamilyRole { Id = 6, RoleName = "جدة" }
            );
        }
    }
}
