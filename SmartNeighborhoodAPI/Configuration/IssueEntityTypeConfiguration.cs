using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class IssueEntityTypeConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Title).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Description).IsRequired();
            builder.Property(i => i.Category).IsRequired();

            builder.Property(i => i.Priority)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(i => i.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(i => i.Reporter)
                .WithMany()
                .HasForeignKey(i => i.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Assignee)
                .WithMany()
                .HasForeignKey(i => i.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
