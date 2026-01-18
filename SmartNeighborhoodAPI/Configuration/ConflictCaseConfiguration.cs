using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class ConflictCaseConfiguration : IEntityTypeConfiguration<ConflictCase>
    {
        public void Configure(EntityTypeBuilder<ConflictCase> builder)
        {
            builder.ToTable("ConfilctCases");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Notes)
                .IsRequired(false);

            builder.Property(c => c.ImagePath)
                .IsRequired(false);

            builder.Property(c => c.Title)
                .IsRequired();

            builder.Property(c => c.ManagerId)
                .IsRequired(false);

            builder.HasOne(c => c.Manager)
                .WithMany()
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.Block)
                .WithMany(b => b.ConflictCases)
                .HasForeignKey(c => c.BlockId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.FirstParty)
                .WithMany(fm => fm.FirstPartyConflictCases)
                .HasForeignKey(c => c.FirstPartyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.SecondParty)
                .WithMany(fm => fm.SecondPartyConflictCases)
                .HasForeignKey(c => c.SecondPartyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ConflictType)
                .WithMany()
                .HasForeignKey(c => c.ConflictTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}