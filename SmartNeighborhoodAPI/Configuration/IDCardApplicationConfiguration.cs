
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class IDCardApplicationConfiguration : IEntityTypeConfiguration<IDCardApplication>
    {
        public void Configure(EntityTypeBuilder<IDCardApplication> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ApplicationNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Status)
                .IsRequired();

            builder.Property(i => i.LastUpdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

 

            builder.HasData(
                new IDCardApplication
                {
                    Id = 1,
                    ResidentId = 1,
                    ApplicationNumber = "IDC-5001",
                    Status = ApplicationStatus.Received,
                    Notes = "Initial application submitted"
                },
                new IDCardApplication
                {
                    Id = 2,
                    ResidentId = 3,
                    ApplicationNumber = "IDC-5002",
                    Status = ApplicationStatus.InProgress,
                    Notes = "Biometric verification pending"
                },
         
                new IDCardApplication { Id = 3, ResidentId = 3, ApplicationNumber = "IDC2003", Status = ApplicationStatus.Received },
                new IDCardApplication { Id = 4, ResidentId = 4, ApplicationNumber = "IDC2004", Status = ApplicationStatus.Rejected, Notes = "Missing ID copy" },
                new IDCardApplication { Id = 5, ResidentId = 5, ApplicationNumber = "IDC2005", Status = ApplicationStatus.Ready },
                new IDCardApplication { Id = 6, ResidentId = 6, ApplicationNumber = "IDC2006", Status = ApplicationStatus.Received },
                new IDCardApplication { Id = 7, ResidentId = 7, ApplicationNumber = "IDC2007", Status = ApplicationStatus.InProgress },
                new IDCardApplication { Id = 8, ResidentId = 8, ApplicationNumber = "IDC2008", Status = ApplicationStatus.Ready },
                new IDCardApplication { Id = 9, ResidentId = 9, ApplicationNumber = "IDC2009", Status = ApplicationStatus.Received },
                new IDCardApplication { Id = 10, ResidentId = 10, ApplicationNumber = "IDC2010", Status = ApplicationStatus.Ready },
                new IDCardApplication { Id = 11, ResidentId = 11, ApplicationNumber = "IDC2011", Status = ApplicationStatus.Rejected },
                new IDCardApplication { Id = 12, ResidentId = 12, ApplicationNumber = "IDC2012", Status = ApplicationStatus.InProgress },
                new IDCardApplication { Id = 13, ResidentId = 13, ApplicationNumber = "IDC2013", Status = ApplicationStatus.Ready },
                new IDCardApplication { Id = 14, ResidentId = 14, ApplicationNumber = "IDC2014", Status = ApplicationStatus.Received },
                new IDCardApplication { Id = 15, ResidentId = 15, ApplicationNumber = "IDC2015", Status = ApplicationStatus.InProgress },
                new IDCardApplication { Id = 16, ResidentId = 16, ApplicationNumber = "IDC2016", Status = ApplicationStatus.Ready },
                new IDCardApplication { Id = 17, ResidentId = 17, ApplicationNumber = "IDC2017", Status = ApplicationStatus.Received },
                new IDCardApplication { Id = 18, ResidentId = 18, ApplicationNumber = "IDC2018", Status = ApplicationStatus.Ready },
                new IDCardApplication { Id = 19, ResidentId = 19, ApplicationNumber = "IDC2019", Status = ApplicationStatus.Rejected },
                new IDCardApplication { Id = 20, ResidentId = 20, ApplicationNumber = "IDC2020", Status = ApplicationStatus.InProgress }

            );
        }
    }
}
