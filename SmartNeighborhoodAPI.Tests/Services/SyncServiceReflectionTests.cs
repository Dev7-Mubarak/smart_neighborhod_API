using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Sync;
using SmartNeighborhoodAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartNeighborhoodAPI.Tests.Services
{
    /// <summary>
    /// Unit tests for SyncService with Reflection-based PushChangesAsync method.
    /// Tests prove that the dynamic entity processing correctly handles:
    /// 1. Entity discovery from DTO names
    /// 2. ID type detection (int vs Guid)
    /// 3. Dynamic method invocation
    /// </summary>
    public class SyncServiceReflectionTests : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<ILogger<SyncService>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly SyncService _syncService;

        public SyncServiceReflectionTests()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _dbContext = new ApplicationDbContext(options);

            // Setup mock logger
            _mockLogger = new Mock<ILogger<SyncService>>();

            // Setup AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Person mappings
                cfg.CreateMap<PersonChangeDto, Person>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.Gender) ? (SmartNeighborhoodAPI.Entites.Enums.Gender?)null : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.Gender>(src.Gender)))
                    .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.BloodType) ? default(SmartNeighborhoodAPI.Entites.Enums.BloodType) : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.BloodType>(src.BloodType)))
                    .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.MaritalStatus) ? default(SmartNeighborhoodAPI.Entites.Enums.MaritalStatus) : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.MaritalStatus>(src.MaritalStatus)))
                    .ForMember(dest => dest.OccupationStatus, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.OccupationStatus) ? default(SmartNeighborhoodAPI.Entites.Enums.OccupationStatus) : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.OccupationStatus>(src.OccupationStatus)))
                    .ForMember(dest => dest.personType, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.PersonType) ? default(SmartNeighborhoodAPI.Entites.Enums.PersonType) : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.PersonType>(src.PersonType)))
                    .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.VehicleType) ? (SmartNeighborhoodAPI.Entites.Enums.VehicleType?)null : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.VehicleType>(src.VehicleType)))
                    .ForMember(dest => dest.ResidencyStatus, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.ResidencyStatus) ? (SmartNeighborhoodAPI.Entites.Enums.ResidencyStatus?)null : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.ResidencyStatus>(src.ResidencyStatus)))
                    .ForMember(dest => dest.FamilyMembers, opt => opt.Ignore())
                    .ForMember(dest => dest.TeamMemberships, opt => opt.Ignore());

                // Issue mappings
                cfg.CreateMap<IssueChangeDto, Issue>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                    .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.Priority) ? default(SmartNeighborhoodAPI.Entites.Enums.IssuePriority) : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.IssuePriority>(src.Priority)))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.Status) ? default(SmartNeighborhoodAPI.Entites.Enums.IssueStatus) : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.IssueStatus>(src.Status)))
                    .ForMember(dest => dest.Reporter, opt => opt.Ignore())
                    .ForMember(dest => dest.Assignee, opt => opt.Ignore())
                    .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                    .ForMember(dest => dest.ResolvedAt, opt => opt.Ignore());

                // Family mappings
                cfg.CreateMap<FamilyChangeDto, Family>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.FamilyCatgoryId, opt => opt.MapFrom(src => int.Parse(src.FamilyCategoryId)))
                    .ForMember(dest => dest.BlockId, opt => opt.MapFrom(src => int.Parse(src.BlockId)))
                    .ForMember(dest => dest.HousingType, opt => opt.MapFrom(src => 
                        string.IsNullOrEmpty(src.HousingType) ? default(SmartNeighborhoodAPI.Entites.Enums.HousingType) : Enum.Parse<SmartNeighborhoodAPI.Entites.Enums.HousingType>(src.HousingType)))
                    .ForMember(dest => dest.FamilyMembers, opt => opt.Ignore())
                    .ForMember(dest => dest.FamilyCatgory, opt => opt.Ignore())
                    .ForMember(dest => dest.Block, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectFamilies, opt => opt.Ignore());
            });

            _mapper = mapperConfig.CreateMapper();

            // Create SyncService
            _syncService = new SyncService(_dbContext, _mockLogger.Object, _mapper);
        }

        /// <summary>
        /// Test: Verify that Person entities (with int ID) are processed correctly via reflection
        /// </summary>
        [Fact]
        public async Task PushChangesAsync_WithPersonChanges_ProcessesIntEntitiesDynamically()
        {
            // Arrange
            var request = new SyncPushRequest
            {
                ClientId = "test-client-001",
                RequestedAt = DateTime.UtcNow,
                Changes = new SyncChanges
                {
                    Persons = new EntityChanges<PersonChangeDto>
                    {
                        Created = new List<PersonChangeDto>
                        {
                            new PersonChangeDto
                            {
                                ClientId = "offline-person-001",
                                FirstName = "Ahmed",
                                SecondName = "Mohammed",
                                ThirdName = "Ali",
                                LastName = "Hassan",
                                PhoneNumber = "0912345678",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    }
                }
            };

            // Act
            var response = await _syncService.PushChangesAsync(request, "test-user-001");

            // Assert
            Assert.NotNull(response);
            Assert.Equal("success", response.SyncStatus);
            Assert.True(response.Results.ContainsKey("persons"));
            Assert.Equal(1, response.Results["persons"].Created.Count);
            Assert.Equal(1, response.TotalProcessed);
            Assert.Equal(0, response.TotalConflicts);

            // Verify entity was created in database
            var person = await _dbContext.Set<Person>().FirstOrDefaultAsync();
            Assert.NotNull(person);
            Assert.Equal("Ahmed", person.FirstName);
            Assert.Equal("Hassan", person.LastName);
            Assert.Equal("offline-person-001", person.ClientId);

            // Verify logger was called
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Successfully processed Person entities")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        /// <summary>
        /// Test: Verify that Issue entities (with Guid ID) are processed correctly via reflection
        /// </summary>
        [Fact]
        public async Task PushChangesAsync_WithIssueChanges_ProcessesGuidEntitiesDynamically()
        {
            // Arrange
            var request = new SyncPushRequest
            {
                ClientId = "test-client-002",
                RequestedAt = DateTime.UtcNow,
                Changes = new SyncChanges
                {
                    Issues = new EntityChanges<IssueChangeDto>
                    {
                        Created = new List<IssueChangeDto>
                        {
                            new IssueChangeDto
                            {
                                ClientId = "offline-issue-001",
                                Title = "Road Issue",
                                Description = "Road needs repair",
                                Category = "Infrastructure",
                                Priority = "High",
                                Status = "Open",
                                ReporterId = "user-001",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    }
                }
            };

            // Act
            var response = await _syncService.PushChangesAsync(request, "test-user-001");

            // Assert
            Assert.NotNull(response);
            Assert.Equal("success", response.SyncStatus);
            Assert.True(response.Results.ContainsKey("issues"));
            Assert.Equal(1, response.Results["issues"].Created.Count);
            Assert.Equal(1, response.TotalProcessed);
            Assert.Equal(0, response.TotalConflicts);

            // Verify entity was created in database
            var issue = await _dbContext.Set<Issue>().FirstOrDefaultAsync();
            Assert.NotNull(issue);
            Assert.Equal("Road Issue", issue.Title);
            Assert.Equal("Road needs repair", issue.Description);
            Assert.Equal("offline-issue-001", issue.ClientId);
            Assert.NotEqual(Guid.Empty, issue.Id);

            // Verify logger was called
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Successfully processed Issue entities")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        /// <summary>
        /// Test: Verify that multiple entity types can be processed together via reflection
        /// </summary>
        [Fact]
        public async Task PushChangesAsync_WithMultipleEntityTypes_ProcessesAllDynamically()
        {
            // Arrange
            var request = new SyncPushRequest
            {
                ClientId = "test-client-003",
                RequestedAt = DateTime.UtcNow,
                Changes = new SyncChanges
                {
                    Persons = new EntityChanges<PersonChangeDto>
                    {
                        Created = new List<PersonChangeDto>
                        {
                            new PersonChangeDto
                            {
                                ClientId = "offline-person-002",
                                FirstName = "Fatima",
                                SecondName = "Ali",
                                ThirdName = "Ahmed",
                                LastName = "Khan",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    },
                    Issues = new EntityChanges<IssueChangeDto>
                    {
                        Created = new List<IssueChangeDto>
                        {
                            new IssueChangeDto
                            {
                                ClientId = "offline-issue-002",
                                Title = "Water Supply",
                                Description = "No water supply",
                                Category = "Utilities",
                                Priority = "High",
                                Status = "Open",
                                ReporterId = "user-002",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    },
                    Families = new EntityChanges<FamilyChangeDto>
                    {
                        Created = new List<FamilyChangeDto>
                        {
                            new FamilyChangeDto
                            {
                                ClientId = "offline-family-001",
                                Name = "Khan Family",
                                Location = "Block A",
                                FamilyCategoryId = "1",
                                HousingType = "Apartment",
                                BlockId = "1",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    }
                }
            };

            // Act
            var response = await _syncService.PushChangesAsync(request, "test-user-001");

            // Assert
            Assert.NotNull(response);
            Assert.Equal("success", response.SyncStatus);

            // Verify all entity types were processed
            Assert.True(response.Results.ContainsKey("persons"));
            Assert.True(response.Results.ContainsKey("issues"));
            Assert.True(response.Results.ContainsKey("families"));

            Assert.Equal(1, response.Results["persons"].Created.Count);
            Assert.Equal(1, response.Results["issues"].Created.Count);
            Assert.Equal(1, response.Results["families"].Created.Count);

            Assert.Equal(3, response.TotalProcessed);
            Assert.Equal(0, response.TotalConflicts);

            // Verify entities were created in database
            Assert.Equal(1, await _dbContext.Set<Person>().CountAsync());
            Assert.Equal(1, await _dbContext.Set<Issue>().CountAsync());
            Assert.Equal(1, await _dbContext.Set<Family>().CountAsync());
        }

        /// <summary>
        /// Test: Verify that null entity changes are skipped gracefully
        /// </summary>
        [Fact]
        public async Task PushChangesAsync_WithNullEntityChanges_SkipsProcessing()
        {
            // Arrange
            var request = new SyncPushRequest
            {
                ClientId = "test-client-004",
                RequestedAt = DateTime.UtcNow,
                Changes = new SyncChanges
                {
                    Persons = new EntityChanges<PersonChangeDto>
                    {
                        Created = new List<PersonChangeDto>
                        {
                            new PersonChangeDto
                            {
                                ClientId = "offline-person-003",
                                FirstName = "Test",
                                SecondName = "User",
                                ThirdName = "Middle",
                                LastName = "Name",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    },
                    Issues = null, // Null - should be skipped
                    Families = null // Null - should be skipped
                }
            };

            // Act
            var response = await _syncService.PushChangesAsync(request, "test-user-001");

            // Assert
            Assert.NotNull(response);
            Assert.Equal("success", response.SyncStatus);
            Assert.True(response.Results.ContainsKey("persons"));
            Assert.False(response.Results.ContainsKey("issues"));
            Assert.False(response.Results.ContainsKey("families"));
            Assert.Equal(1, response.TotalProcessed);
        }

        /// <summary>
        /// Test: Verify client-to-server ID mapping for created entities
        /// </summary>
        [Fact]
        public async Task PushChangesAsync_CreatedEntities_ReturnsClientIdToServerIdMapping()
        {
            // Arrange
            var clientId = "offline-person-mapping-test";
            var request = new SyncPushRequest
            {
                ClientId = "test-client-005",
                RequestedAt = DateTime.UtcNow,
                Changes = new SyncChanges
                {
                    Persons = new EntityChanges<PersonChangeDto>
                    {
                        Created = new List<PersonChangeDto>
                        {
                            new PersonChangeDto
                            {
                                ClientId = clientId,
                                FirstName = "Mapping",
                                SecondName = "Test",
                                ThirdName = "User",
                                LastName = "Person",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    }
                }
            };

            // Act
            var response = await _syncService.PushChangesAsync(request, "test-user-001");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Results["persons"].Created.Count);

            var createdItem = response.Results["persons"].Created[0] as dynamic;
            Assert.NotNull(createdItem);
            Assert.Equal(clientId, (string)createdItem.clientId);
            Assert.NotNull(createdItem.serverId);
            Assert.NotEmpty((string)createdItem.serverId);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
