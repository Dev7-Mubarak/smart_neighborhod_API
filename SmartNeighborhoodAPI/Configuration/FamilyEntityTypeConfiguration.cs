//namespace OurProjectSmartNeiborhood.Configuration
//{
//    public class FamilyEntityTypeConfiguration:IEntityTypeConfiguration<Family>
//    {
//        public void Configure(EntityTypeBuilder<Family> builder)
//        {
//            builder
//                .HasOne(FC => FC.FamilyCatgory)
//                .WithMany()
//                .HasForeignKey(a => a.FamilyCatgoryId);


//            builder
//                .HasOne(FT => FT.FamilyType)
//                .WithMany()
//                .HasForeignKey(a => a.FamilyTypeId);

//            builder
//                .HasMany(FM => FM.FamilyMembers)
//                .WithOne()
//                .HasForeignKey(a => a.FamilyId);


//            builder
//                 .HasMany(PF => PF.projectFamilies)
//                .WithOne()
//                .HasForeignKey(a => a.FamilyId);



//            builder
//                .HasOne(B => B.Block)
//                .WithMany();
//            builder
//                .Property(L => L.Location)
//                .HasMaxLength(20);
//            builder
//                .Property(FN => FN.FamilyNotes)
//                .HasMaxLength(150);


//        }
//    }
//}
