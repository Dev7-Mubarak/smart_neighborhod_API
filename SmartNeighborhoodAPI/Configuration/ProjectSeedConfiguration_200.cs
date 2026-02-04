//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OurProjectSmartNeiborhood.Entites;
//using SmartNeighborhoodAPI.Entites.Enums;

//public class ProjectSeedConfiguration_200 : IEntityTypeConfiguration<Project>
//{
//    public void Configure(EntityTypeBuilder<Project> builder)
//    {
//        builder.HasData(
//            new Project
//            {
//                Id = 1,
//                Name = "مشروع رقم 1",
//                Description = "وصف مشروع رقم 1",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 2, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 10500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 2,
//                Name = "مشروع رقم 2",
//                Description = "وصف مشروع رقم 2",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 3, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 11000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 3,
//                Name = "مشروع رقم 3",
//                Description = "وصف مشروع رقم 3",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 4, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 11500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 4,
//                Name = "مشروع رقم 4",
//                Description = "وصف مشروع رقم 4",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 5, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 12000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 5,
//                Name = "مشروع رقم 5",
//                Description = "وصف مشروع رقم 5",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 6, 6),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 12500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 6,
//                Name = "مشروع رقم 6",
//                Description = "وصف مشروع رقم 6",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 7, 7),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 13000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 7,
//                Name = "مشروع رقم 7",
//                Description = "وصف مشروع رقم 7",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 8, 8),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 13500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 8,
//                Name = "مشروع رقم 8",
//                Description = "وصف مشروع رقم 8",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 9, 9),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 14000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 9,
//                Name = "مشروع رقم 9",
//                Description = "وصف مشروع رقم 9",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 10, 10),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 14500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 10,
//                Name = "مشروع رقم 10",
//                Description = "وصف مشروع رقم 10",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 11, 11),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 15000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 11,
//                Name = "مشروع رقم 11",
//                Description = "وصف مشروع رقم 11",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 12, 12),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 15500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 12,
//                Name = "مشروع رقم 12",
//                Description = "وصف مشروع رقم 12",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 1, 13),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 16000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 13,
//                Name = "مشروع رقم 13",
//                Description = "وصف مشروع رقم 13",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 2, 14),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 16500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 14,
//                Name = "مشروع رقم 14",
//                Description = "وصف مشروع رقم 14",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 3, 15),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 17000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 15,
//                Name = "مشروع رقم 15",
//                Description = "وصف مشروع رقم 15",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 4, 16),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 17500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 16,
//                Name = "مشروع رقم 16",
//                Description = "وصف مشروع رقم 16",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 5, 17),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 18000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 17,
//                Name = "مشروع رقم 17",
//                Description = "وصف مشروع رقم 17",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 6, 18),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 18500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 18,
//                Name = "مشروع رقم 18",
//                Description = "وصف مشروع رقم 18",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 7, 19),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 19000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 19,
//                Name = "مشروع رقم 19",
//                Description = "وصف مشروع رقم 19",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 8, 20),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 19500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 20,
//                Name = "مشروع رقم 20",
//                Description = "وصف مشروع رقم 20",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 9, 21),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 20000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 21,
//                Name = "مشروع رقم 21",
//                Description = "وصف مشروع رقم 21",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 10, 22),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 20500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 22,
//                Name = "مشروع رقم 22",
//                Description = "وصف مشروع رقم 22",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 11, 23),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 21000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 23,
//                Name = "مشروع رقم 23",
//                Description = "وصف مشروع رقم 23",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 12, 24),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 21500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 24,
//                Name = "مشروع رقم 24",
//                Description = "وصف مشروع رقم 24",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 1, 25),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 22000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 25,
//                Name = "مشروع رقم 25",
//                Description = "وصف مشروع رقم 25",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 2, 26),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 22500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 26,
//                Name = "مشروع رقم 26",
//                Description = "وصف مشروع رقم 26",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 3, 27),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 23000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 27,
//                Name = "مشروع رقم 27",
//                Description = "وصف مشروع رقم 27",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 4, 28),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 23500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 28,
//                Name = "مشروع رقم 28",
//                Description = "وصف مشروع رقم 28",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 5, 1),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 24000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 29,
//                Name = "مشروع رقم 29",
//                Description = "وصف مشروع رقم 29",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 6, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 24500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 30,
//                Name = "مشروع رقم 30",
//                Description = "وصف مشروع رقم 30",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 7, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 25000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 31,
//                Name = "مشروع رقم 31",
//                Description = "وصف مشروع رقم 31",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 8, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 25500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 32,
//                Name = "مشروع رقم 32",
//                Description = "وصف مشروع رقم 32",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 9, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 26000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 33,
//                Name = "مشروع رقم 33",
//                Description = "وصف مشروع رقم 33",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 10, 6),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 26500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 34,
//                Name = "مشروع رقم 34",
//                Description = "وصف مشروع رقم 34",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 11, 7),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 27000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 35,
//                Name = "مشروع رقم 35",
//                Description = "وصف مشروع رقم 35",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 12, 8),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 27500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 36,
//                Name = "مشروع رقم 36",
//                Description = "وصف مشروع رقم 36",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 1, 9),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 28000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 37,
//                Name = "مشروع رقم 37",
//                Description = "وصف مشروع رقم 37",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 2, 10),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 28500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 38,
//                Name = "مشروع رقم 38",
//                Description = "وصف مشروع رقم 38",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 3, 11),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 29000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 39,
//                Name = "مشروع رقم 39",
//                Description = "وصف مشروع رقم 39",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 4, 12),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 29500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 40,
//                Name = "مشروع رقم 40",
//                Description = "وصف مشروع رقم 40",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 5, 13),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 30000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 41,
//                Name = "مشروع رقم 41",
//                Description = "وصف مشروع رقم 41",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 6, 14),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 30500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 42,
//                Name = "مشروع رقم 42",
//                Description = "وصف مشروع رقم 42",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 7, 15),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 31000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 43,
//                Name = "مشروع رقم 43",
//                Description = "وصف مشروع رقم 43",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 8, 16),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 31500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 44,
//                Name = "مشروع رقم 44",
//                Description = "وصف مشروع رقم 44",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 9, 17),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 32000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 45,
//                Name = "مشروع رقم 45",
//                Description = "وصف مشروع رقم 45",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 10, 18),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 32500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 46,
//                Name = "مشروع رقم 46",
//                Description = "وصف مشروع رقم 46",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 11, 19),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 33000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 47,
//                Name = "مشروع رقم 47",
//                Description = "وصف مشروع رقم 47",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 12, 20),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 33500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 48,
//                Name = "مشروع رقم 48",
//                Description = "وصف مشروع رقم 48",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 1, 21),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 34000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 49,
//                Name = "مشروع رقم 49",
//                Description = "وصف مشروع رقم 49",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 2, 22),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 34500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 50,
//                Name = "مشروع رقم 50",
//                Description = "وصف مشروع رقم 50",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 3, 23),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 35000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 51,
//                Name = "مشروع رقم 51",
//                Description = "وصف مشروع رقم 51",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 4, 24),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 35500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 52,
//                Name = "مشروع رقم 52",
//                Description = "وصف مشروع رقم 52",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 5, 25),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 36000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 53,
//                Name = "مشروع رقم 53",
//                Description = "وصف مشروع رقم 53",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 6, 26),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 36500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 54,
//                Name = "مشروع رقم 54",
//                Description = "وصف مشروع رقم 54",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 7, 27),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 37000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 55,
//                Name = "مشروع رقم 55",
//                Description = "وصف مشروع رقم 55",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 8, 28),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 37500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 56,
//                Name = "مشروع رقم 56",
//                Description = "وصف مشروع رقم 56",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 9, 1),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 38000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 57,
//                Name = "مشروع رقم 57",
//                Description = "وصف مشروع رقم 57",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 10, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 38500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 58,
//                Name = "مشروع رقم 58",
//                Description = "وصف مشروع رقم 58",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 11, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 39000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 59,
//                Name = "مشروع رقم 59",
//                Description = "وصف مشروع رقم 59",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 12, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 39500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 60,
//                Name = "مشروع رقم 60",
//                Description = "وصف مشروع رقم 60",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 1, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 40000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 61,
//                Name = "مشروع رقم 61",
//                Description = "وصف مشروع رقم 61",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 2, 6),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 40500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 62,
//                Name = "مشروع رقم 62",
//                Description = "وصف مشروع رقم 62",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 3, 7),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 41000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 63,
//                Name = "مشروع رقم 63",
//                Description = "وصف مشروع رقم 63",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 4, 8),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 41500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 64,
//                Name = "مشروع رقم 64",
//                Description = "وصف مشروع رقم 64",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 5, 9),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 42000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 65,
//                Name = "مشروع رقم 65",
//                Description = "وصف مشروع رقم 65",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 6, 10),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 42500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 66,
//                Name = "مشروع رقم 66",
//                Description = "وصف مشروع رقم 66",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 7, 11),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 43000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 67,
//                Name = "مشروع رقم 67",
//                Description = "وصف مشروع رقم 67",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 8, 12),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 43500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 68,
//                Name = "مشروع رقم 68",
//                Description = "وصف مشروع رقم 68",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 9, 13),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 44000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 69,
//                Name = "مشروع رقم 69",
//                Description = "وصف مشروع رقم 69",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 10, 14),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 44500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 70,
//                Name = "مشروع رقم 70",
//                Description = "وصف مشروع رقم 70",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 11, 15),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 45000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 71,
//                Name = "مشروع رقم 71",
//                Description = "وصف مشروع رقم 71",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 12, 16),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 45500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 72,
//                Name = "مشروع رقم 72",
//                Description = "وصف مشروع رقم 72",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 1, 17),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 46000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 73,
//                Name = "مشروع رقم 73",
//                Description = "وصف مشروع رقم 73",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 2, 18),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 46500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 74,
//                Name = "مشروع رقم 74",
//                Description = "وصف مشروع رقم 74",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 3, 19),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 47000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 75,
//                Name = "مشروع رقم 75",
//                Description = "وصف مشروع رقم 75",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 4, 20),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 47500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 76,
//                Name = "مشروع رقم 76",
//                Description = "وصف مشروع رقم 76",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 5, 21),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 48000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 77,
//                Name = "مشروع رقم 77",
//                Description = "وصف مشروع رقم 77",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 6, 22),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 48500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 78,
//                Name = "مشروع رقم 78",
//                Description = "وصف مشروع رقم 78",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 7, 23),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 49000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 79,
//                Name = "مشروع رقم 79",
//                Description = "وصف مشروع رقم 79",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 8, 24),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 49500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 80,
//                Name = "مشروع رقم 80",
//                Description = "وصف مشروع رقم 80",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 9, 25),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 50000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 81,
//                Name = "مشروع رقم 81",
//                Description = "وصف مشروع رقم 81",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 10, 26),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 50500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 82,
//                Name = "مشروع رقم 82",
//                Description = "وصف مشروع رقم 82",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 11, 27),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 51000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 83,
//                Name = "مشروع رقم 83",
//                Description = "وصف مشروع رقم 83",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 12, 28),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 51500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 84,
//                Name = "مشروع رقم 84",
//                Description = "وصف مشروع رقم 84",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 1, 1),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 52000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 85,
//                Name = "مشروع رقم 85",
//                Description = "وصف مشروع رقم 85",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 2, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 52500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 86,
//                Name = "مشروع رقم 86",
//                Description = "وصف مشروع رقم 86",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 3, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 53000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 87,
//                Name = "مشروع رقم 87",
//                Description = "وصف مشروع رقم 87",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 4, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 53500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 88,
//                Name = "مشروع رقم 88",
//                Description = "وصف مشروع رقم 88",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 5, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 54000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 89,
//                Name = "مشروع رقم 89",
//                Description = "وصف مشروع رقم 89",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 6, 6),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 54500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 90,
//                Name = "مشروع رقم 90",
//                Description = "وصف مشروع رقم 90",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 7, 7),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 55000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 91,
//                Name = "مشروع رقم 91",
//                Description = "وصف مشروع رقم 91",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 8, 8),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 55500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 92,
//                Name = "مشروع رقم 92",
//                Description = "وصف مشروع رقم 92",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 9, 9),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 56000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 93,
//                Name = "مشروع رقم 93",
//                Description = "وصف مشروع رقم 93",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 10, 10),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 56500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 94,
//                Name = "مشروع رقم 94",
//                Description = "وصف مشروع رقم 94",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 11, 11),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 57000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 95,
//                Name = "مشروع رقم 95",
//                Description = "وصف مشروع رقم 95",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 12, 12),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 57500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 96,
//                Name = "مشروع رقم 96",
//                Description = "وصف مشروع رقم 96",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 1, 13),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 58000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 97,
//                Name = "مشروع رقم 97",
//                Description = "وصف مشروع رقم 97",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 2, 14),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 58500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 98,
//                Name = "مشروع رقم 98",
//                Description = "وصف مشروع رقم 98",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 3, 15),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 59000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 99,
//                Name = "مشروع رقم 99",
//                Description = "وصف مشروع رقم 99",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 4, 16),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 59500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 100,
//                Name = "مشروع رقم 100",
//                Description = "وصف مشروع رقم 100",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 5, 17),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 60000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 101,
//                Name = "مشروع رقم 101",
//                Description = "وصف مشروع رقم 101",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 6, 18),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 60500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 102,
//                Name = "مشروع رقم 102",
//                Description = "وصف مشروع رقم 102",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 7, 19),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 61000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 103,
//                Name = "مشروع رقم 103",
//                Description = "وصف مشروع رقم 103",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 8, 20),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 61500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 104,
//                Name = "مشروع رقم 104",
//                Description = "وصف مشروع رقم 104",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 9, 21),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 62000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 105,
//                Name = "مشروع رقم 105",
//                Description = "وصف مشروع رقم 105",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 10, 22),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 62500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 106,
//                Name = "مشروع رقم 106",
//                Description = "وصف مشروع رقم 106",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 11, 23),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 63000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 107,
//                Name = "مشروع رقم 107",
//                Description = "وصف مشروع رقم 107",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 12, 24),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 63500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 108,
//                Name = "مشروع رقم 108",
//                Description = "وصف مشروع رقم 108",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 1, 25),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 64000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 109,
//                Name = "مشروع رقم 109",
//                Description = "وصف مشروع رقم 109",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 2, 26),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 64500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 110,
//                Name = "مشروع رقم 110",
//                Description = "وصف مشروع رقم 110",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 3, 27),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 65000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 111,
//                Name = "مشروع رقم 111",
//                Description = "وصف مشروع رقم 111",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 4, 28),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 65500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 112,
//                Name = "مشروع رقم 112",
//                Description = "وصف مشروع رقم 112",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 5, 1),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 66000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 113,
//                Name = "مشروع رقم 113",
//                Description = "وصف مشروع رقم 113",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 6, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 66500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 114,
//                Name = "مشروع رقم 114",
//                Description = "وصف مشروع رقم 114",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 7, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 67000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 115,
//                Name = "مشروع رقم 115",
//                Description = "وصف مشروع رقم 115",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 8, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 67500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 116,
//                Name = "مشروع رقم 116",
//                Description = "وصف مشروع رقم 116",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 9, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 68000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 117,
//                Name = "مشروع رقم 117",
//                Description = "وصف مشروع رقم 117",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 10, 6),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 68500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 118,
//                Name = "مشروع رقم 118",
//                Description = "وصف مشروع رقم 118",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 11, 7),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 69000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 119,
//                Name = "مشروع رقم 119",
//                Description = "وصف مشروع رقم 119",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 12, 8),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 69500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 120,
//                Name = "مشروع رقم 120",
//                Description = "وصف مشروع رقم 120",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 1, 9),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 70000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 121,
//                Name = "مشروع رقم 121",
//                Description = "وصف مشروع رقم 121",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 2, 10),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 70500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 122,
//                Name = "مشروع رقم 122",
//                Description = "وصف مشروع رقم 122",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 3, 11),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 71000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 123,
//                Name = "مشروع رقم 123",
//                Description = "وصف مشروع رقم 123",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 4, 12),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 71500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 124,
//                Name = "مشروع رقم 124",
//                Description = "وصف مشروع رقم 124",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 5, 13),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 72000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 125,
//                Name = "مشروع رقم 125",
//                Description = "وصف مشروع رقم 125",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 6, 14),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 72500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 126,
//                Name = "مشروع رقم 126",
//                Description = "وصف مشروع رقم 126",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 7, 15),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 73000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 127,
//                Name = "مشروع رقم 127",
//                Description = "وصف مشروع رقم 127",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 8, 16),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 73500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 128,
//                Name = "مشروع رقم 128",
//                Description = "وصف مشروع رقم 128",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 9, 17),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 74000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 129,
//                Name = "مشروع رقم 129",
//                Description = "وصف مشروع رقم 129",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 10, 18),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 74500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 130,
//                Name = "مشروع رقم 130",
//                Description = "وصف مشروع رقم 130",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 11, 19),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 75000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 131,
//                Name = "مشروع رقم 131",
//                Description = "وصف مشروع رقم 131",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 12, 20),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 75500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 132,
//                Name = "مشروع رقم 132",
//                Description = "وصف مشروع رقم 132",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 1, 21),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 76000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 133,
//                Name = "مشروع رقم 133",
//                Description = "وصف مشروع رقم 133",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 2, 22),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 76500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 134,
//                Name = "مشروع رقم 134",
//                Description = "وصف مشروع رقم 134",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 3, 23),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 77000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 135,
//                Name = "مشروع رقم 135",
//                Description = "وصف مشروع رقم 135",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 4, 24),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 77500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 136,
//                Name = "مشروع رقم 136",
//                Description = "وصف مشروع رقم 136",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 5, 25),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 78000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 137,
//                Name = "مشروع رقم 137",
//                Description = "وصف مشروع رقم 137",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 6, 26),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 78500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 138,
//                Name = "مشروع رقم 138",
//                Description = "وصف مشروع رقم 138",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 7, 27),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 79000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 139,
//                Name = "مشروع رقم 139",
//                Description = "وصف مشروع رقم 139",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 8, 28),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 79500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 140,
//                Name = "مشروع رقم 140",
//                Description = "وصف مشروع رقم 140",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 9, 1),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 80000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 141,
//                Name = "مشروع رقم 141",
//                Description = "وصف مشروع رقم 141",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 10, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 80500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 142,
//                Name = "مشروع رقم 142",
//                Description = "وصف مشروع رقم 142",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 11, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 81000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 143,
//                Name = "مشروع رقم 143",
//                Description = "وصف مشروع رقم 143",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 12, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 81500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 144,
//                Name = "مشروع رقم 144",
//                Description = "وصف مشروع رقم 144",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 1, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 82000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 145,
//                Name = "مشروع رقم 145",
//                Description = "وصف مشروع رقم 145",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 2, 6),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 82500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 146,
//                Name = "مشروع رقم 146",
//                Description = "وصف مشروع رقم 146",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 3, 7),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 83000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 147,
//                Name = "مشروع رقم 147",
//                Description = "وصف مشروع رقم 147",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 4, 8),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 83500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 148,
//                Name = "مشروع رقم 148",
//                Description = "وصف مشروع رقم 148",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 5, 9),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 84000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 149,
//                Name = "مشروع رقم 149",
//                Description = "وصف مشروع رقم 149",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 6, 10),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 84500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 150,
//                Name = "مشروع رقم 150",
//                Description = "وصف مشروع رقم 150",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 7, 11),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 85000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 151,
//                Name = "مشروع رقم 151",
//                Description = "وصف مشروع رقم 151",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 8, 12),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 85500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 152,
//                Name = "مشروع رقم 152",
//                Description = "وصف مشروع رقم 152",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 9, 13),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 86000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 153,
//                Name = "مشروع رقم 153",
//                Description = "وصف مشروع رقم 153",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 10, 14),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 86500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 154,
//                Name = "مشروع رقم 154",
//                Description = "وصف مشروع رقم 154",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 11, 15),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 87000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 155,
//                Name = "مشروع رقم 155",
//                Description = "وصف مشروع رقم 155",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 12, 16),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 87500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 156,
//                Name = "مشروع رقم 156",
//                Description = "وصف مشروع رقم 156",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 1, 17),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 88000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 157,
//                Name = "مشروع رقم 157",
//                Description = "وصف مشروع رقم 157",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 2, 18),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 88500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 158,
//                Name = "مشروع رقم 158",
//                Description = "وصف مشروع رقم 158",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 3, 19),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 89000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 159,
//                Name = "مشروع رقم 159",
//                Description = "وصف مشروع رقم 159",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 4, 20),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 89500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 160,
//                Name = "مشروع رقم 160",
//                Description = "وصف مشروع رقم 160",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 5, 21),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 90000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 161,
//                Name = "مشروع رقم 161",
//                Description = "وصف مشروع رقم 161",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 6, 22),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 90500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 162,
//                Name = "مشروع رقم 162",
//                Description = "وصف مشروع رقم 162",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 7, 23),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 91000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 163,
//                Name = "مشروع رقم 163",
//                Description = "وصف مشروع رقم 163",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 8, 24),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 91500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 164,
//                Name = "مشروع رقم 164",
//                Description = "وصف مشروع رقم 164",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 9, 25),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 92000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 165,
//                Name = "مشروع رقم 165",
//                Description = "وصف مشروع رقم 165",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 10, 26),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 92500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 166,
//                Name = "مشروع رقم 166",
//                Description = "وصف مشروع رقم 166",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 11, 27),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 93000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 167,
//                Name = "مشروع رقم 167",
//                Description = "وصف مشروع رقم 167",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 12, 28),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 93500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 168,
//                Name = "مشروع رقم 168",
//                Description = "وصف مشروع رقم 168",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 1, 1),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 94000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 169,
//                Name = "مشروع رقم 169",
//                Description = "وصف مشروع رقم 169",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 2, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 94500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 170,
//                Name = "مشروع رقم 170",
//                Description = "وصف مشروع رقم 170",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 3, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 95000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 171,
//                Name = "مشروع رقم 171",
//                Description = "وصف مشروع رقم 171",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 4, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 95500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 172,
//                Name = "مشروع رقم 172",
//                Description = "وصف مشروع رقم 172",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 5, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 96000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 173,
//                Name = "مشروع رقم 173",
//                Description = "وصف مشروع رقم 173",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 6, 6),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 96500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 174,
//                Name = "مشروع رقم 174",
//                Description = "وصف مشروع رقم 174",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 7, 7),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 97000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 175,
//                Name = "مشروع رقم 175",
//                Description = "وصف مشروع رقم 175",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 8, 8),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 97500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 176,
//                Name = "مشروع رقم 176",
//                Description = "وصف مشروع رقم 176",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 9, 9),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 98000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 177,
//                Name = "مشروع رقم 177",
//                Description = "وصف مشروع رقم 177",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 10, 10),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 98500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 178,
//                Name = "مشروع رقم 178",
//                Description = "وصف مشروع رقم 178",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 11, 11),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 99000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 179,
//                Name = "مشروع رقم 179",
//                Description = "وصف مشروع رقم 179",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 12, 12),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 99500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 180,
//                Name = "مشروع رقم 180",
//                Description = "وصف مشروع رقم 180",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 1, 13),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 100000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 181,
//                Name = "مشروع رقم 181",
//                Description = "وصف مشروع رقم 181",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 2, 14),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 100500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 182,
//                Name = "مشروع رقم 182",
//                Description = "وصف مشروع رقم 182",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 3, 15),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 101000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 183,
//                Name = "مشروع رقم 183",
//                Description = "وصف مشروع رقم 183",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 4, 16),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 101500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 184,
//                Name = "مشروع رقم 184",
//                Description = "وصف مشروع رقم 184",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 5, 17),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 102000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 185,
//                Name = "مشروع رقم 185",
//                Description = "وصف مشروع رقم 185",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 6, 18),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 102500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 186,
//                Name = "مشروع رقم 186",
//                Description = "وصف مشروع رقم 186",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 7, 19),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 103000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 187,
//                Name = "مشروع رقم 187",
//                Description = "وصف مشروع رقم 187",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 8, 20),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 103500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 188,
//                Name = "مشروع رقم 188",
//                Description = "وصف مشروع رقم 188",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 9, 21),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 104000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 189,
//                Name = "مشروع رقم 189",
//                Description = "وصف مشروع رقم 189",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 10, 22),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 104500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 190,
//                Name = "مشروع رقم 190",
//                Description = "وصف مشروع رقم 190",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 11, 23),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 105000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 191,
//                Name = "مشروع رقم 191",
//                Description = "وصف مشروع رقم 191",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 12, 24),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 105500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 192,
//                Name = "مشروع رقم 192",
//                Description = "وصف مشروع رقم 192",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 1, 25),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 106000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 193,
//                Name = "مشروع رقم 193",
//                Description = "وصف مشروع رقم 193",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 2, 26),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 106500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 194,
//                Name = "مشروع رقم 194",
//                Description = "وصف مشروع رقم 194",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 3, 27),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 107000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 195,
//                Name = "مشروع رقم 195",
//                Description = "وصف مشروع رقم 195",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 4, 28),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 107500,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 196,
//                Name = "مشروع رقم 196",
//                Description = "وصف مشروع رقم 196",
//                ProjectCatogoryId = 1,
//                StartDate = new DateTime(2023, 5, 1),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 108000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 197,
//                Name = "مشروع رقم 197",
//                Description = "وصف مشروع رقم 197",
//                ProjectCatogoryId = 2,
//                StartDate = new DateTime(2023, 6, 2),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 108500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 198,
//                Name = "مشروع رقم 198",
//                Description = "وصف مشروع رقم 198",
//                ProjectCatogoryId = 3,
//                StartDate = new DateTime(2023, 7, 3),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 109000,
//                ProjectPriority = ProjectPriority.High,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 199,
//                Name = "مشروع رقم 199",
//                Description = "وصف مشروع رقم 199",
//                ProjectCatogoryId = 4,
//                StartDate = new DateTime(2023, 8, 4),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.Completed,
//                Budget = 109500,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            },
//            new Project
//            {
//                Id = 200,
//                Name = "مشروع رقم 200",
//                Description = "وصف مشروع رقم 200",
//                ProjectCatogoryId = 5,
//                StartDate = new DateTime(2023, 9, 5),
//                EndDate = null,
//                ProjectStatus = ProjectStatus.InProgress,
//                Budget = 110000,
//                ProjectPriority = ProjectPriority.Medium,
//                ManagerId = null
//            }
//        );
//    }
//}
