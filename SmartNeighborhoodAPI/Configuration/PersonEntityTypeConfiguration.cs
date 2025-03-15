//namespace OurProjectSmartNeiborhood.Configuration
//{
//    public class PersonEntityTypeConfiguration:IEntityTypeConfiguration<Person>
//    {
//        public void Configure(EntityTypeBuilder<Person> builder)
//        {
//            builder
//                 .HasMany(TM => TM.TeamMembers)
//                 .WithOne();

//            builder
//                .HasMany(CI => CI.ContactInfos)
//                .WithOne()
//                .HasForeignKey(a => a.PersonId);

//            builder
//                .HasMany(FM => FM.FamilyMembers)
//                .WithOne()
//                .HasForeignKey(a => a.PersonId);

//            builder
//                .HasMany(PC => PC.PersonComplains)
//                .WithOne()
//                .HasForeignKey(a => a.ComplainId);

//            builder
//                .Property(BT => BT.BloodType)
//                .HasMaxLength(4);
//            builder
//                .Property(IN => IN.IdentityNumber)
//                .HasMaxLength(30);
//            builder
//                .Property(LN => LN.LastName)
//                .HasMaxLength(30);
//            builder
//               .Property(MN => MN.MidName)
//               .HasMaxLength(30);
//            builder
//               .Property(MN => MN.Status)
//               .HasMaxLength(15);

//        }
//    }
//}
