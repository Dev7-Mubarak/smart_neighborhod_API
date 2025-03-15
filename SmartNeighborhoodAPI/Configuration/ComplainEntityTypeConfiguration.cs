namespace OurProjectSmartNeiborhood.Configuration
{
    public class ComplainEntityTypeConfiguration:IEntityTypeConfiguration<Complain>
    {
        public void Configure(EntityTypeBuilder<Complain> builder)
        {
            builder
                .HasOne(CT => CT.ComplainType)
                .WithMany()
                .HasForeignKey(a => a.ComplainTypeId);
              

     
        }
    }
}
