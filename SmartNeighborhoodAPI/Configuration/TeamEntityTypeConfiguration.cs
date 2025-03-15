namespace OurProjectSmartNeiborhood.Configuration
{
    public class TeamEntityTypeConfiguration:IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder
                .HasMany(TM => TM.TeamMembers)
                .WithOne()
                .HasForeignKey(a => a.TeamId);
                


        }
    }
}
