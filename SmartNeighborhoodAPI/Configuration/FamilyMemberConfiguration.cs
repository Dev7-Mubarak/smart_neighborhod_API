using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourNamespace.Configurations
{
    public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.ToTable("FamilyMembers");

            builder.HasKey(fm => fm.Id);

            builder.HasOne(fm => fm.Person)
                   .WithMany(p => p.FamilyMembers)
                   .HasForeignKey(fm => fm.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Family relationship
            builder.HasOne(fm => fm.Family)
                   .WithMany() 
                   .HasForeignKey(fm => fm.FamilyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // MemberFamilyRole relationship
            builder.HasOne(fm => fm.MemberFamilyRole)
                   .WithMany() // or .WithMany(r => r.FamilyMembers)
                   .HasForeignKey(fm => fm.MemberFamilyRoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
