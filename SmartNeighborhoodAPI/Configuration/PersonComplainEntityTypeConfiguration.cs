namespace OurProjectSmartNeiborhood.Configuration
{
    public class PersonComplainEntityTypeConfiguration:IEntityTypeConfiguration<PersonComplain>
    {
        public void Configure(EntityTypeBuilder<PersonComplain> builder)
        {
            builder
                .HasOne(C => C.Complain)
                .WithMany();
                
            builder
                .HasOne(CP => CP.ConfilctParty)
                .WithMany()
                .HasForeignKey(a => a.ConfilctPartyId);
               


        }
    }
}
