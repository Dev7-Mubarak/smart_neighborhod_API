using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdsText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfilctTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfilctTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyCatgories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyCatgories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GovernmentInstitutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentInstitutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberFamilyRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFamilyRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ThirdName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsWhatsapp = table.Column<bool>(type: "bit", nullable: true),
                    IsContactNumber = table.Column<bool>(type: "bit", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", maxLength: 100, nullable: true),
                    Gender = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    OccupationStatus = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCatogories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCatogories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GovernmentInstitutionContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GovernmentInstitutionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentInstitutionContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GovernmentInstitutionContacts_GovernmentInstitutions_GovernmentInstitutionId",
                        column: x => x.GovernmentInstitutionId,
                        principalTable: "GovernmentInstitutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdGroup",
                columns: table => new
                {
                    AdsId = table.Column<int>(type: "int", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdGroup", x => new { x.AdsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_AdGroup_Ads_AdsId",
                        column: x => x.AdsId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineBatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineBatches_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    EmailConfirmationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EmailConfirmationCodeExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDCardApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDCardApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDCardApplications_People_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassportApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassportApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassportApplications_People_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OwnerResidentId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LicenseExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    ProjectCatogoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectStatus = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProjectPriority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_People_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectCatogories_ProjectCatogoryId",
                        column: x => x.ProjectCatogoryId,
                        principalTable: "ProjectCatogories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMembers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_TeamRoles_TeamRoleId",
                        column: x => x.TeamRoleId,
                        principalTable: "TeamRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTeams_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyCatgoryId = table.Column<int>(type: "int", nullable: false),
                    HousingType = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Families_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Families_FamilyCatgories_FamilyCatgoryId",
                        column: x => x.FamilyCatgoryId,
                        principalTable: "FamilyCatgories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    FamilyId = table.Column<int>(type: "int", nullable: false),
                    MemberFamilyRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_MemberFamilyRoles_MemberFamilyRoleId",
                        column: x => x.MemberFamilyRoleId,
                        principalTable: "MemberFamilyRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                });

            migrationBuilder.CreateTable(
                name: "ProjectFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    FamilyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFamilies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFamilies_Families_FamilyID",
                        column: x => x.FamilyID,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
      
                });

            migrationBuilder.CreateTable(
                name: "ConfilctCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPartyId = table.Column<int>(type: "int", nullable: false),
                    SecondPartyId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConflictTypeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfilctCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_ConfilctTypes_ConflictTypeId",
                        column: x => x.ConflictTypeId,
                        principalTable: "ConfilctTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_FamilyMembers_FirstPartyId",
                        column: x => x.FirstPartyId,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_FamilyMembers_SecondPartyId",
                        column: x => x.SecondPartyId,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", null, "Admin", "ADMIN" },
                    { "22222222-2222-2222-2222-222222222222", null, "BlockManager", "BLOCKMANAGER" },
                    { "33333333-3333-3333-3333-333333333333", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "FamilyCatgories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "A" },
                    { 2, "B" },
                    { 3, "C" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "For diabetic patients", "Insulin", 2 },
                    { 2, "Blood pressure treatment", "Amlodipine", 3 },
                    { 3, null, "Amlodipine", 3 },
                    { 4, null, "Losartan", 3 },
                    { 5, null, "Antibiotic A", 4 },
                    { 6, null, "Painkiller A", 5 },
                    { 7, null, "Dolo 500", 5 },
                    { 8, null, "Cold Relief", 1 },
                    { 9, null, "Vitamin C", 1 },
                    { 10, null, "Iron Supplement", 1 },
                    { 11, null, "Lisinopril", 3 },
                    { 12, null, "Glibenclamide", 2 },
                    { 13, null, "Amoxicillin", 4 },
                    { 14, null, "Ibuprofen", 5 },
                    { 15, null, "Acetaminophen", 5 },
                    { 16, null, "Multivitamins", 1 },
                    { 17, null, "Magnesium Tablets", 1 },
                    { 18, null, "Zinc Supplements", 1 },
                    { 19, null, "Cough Syrup", 1 },
                    { 20, null, "Stomach Relief", 1 },
                    { 21, null, "Insulin", 2 },
                    { 22, null, "Metformin", 2 }
                });

            migrationBuilder.InsertData(
                table: "MemberFamilyRoles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "أب" },
                    { 2, "أم" },
                    { 3, "ابن" },
                    { 4, "ابنة" },
                    { 5, "جد" },
                    { 6, "جدة" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BloodType", "DateOfBirth", "FirstName", "Gender", "Image", "IsContactNumber", "IsWhatsapp", "Job", "LastName", "MaritalStatus", "OccupationStatus", "PhoneNumber", "SecondName", "ThirdName" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(1990, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "أحمد", 0, null, true, true, "مهندس", "الزهيري", 2, 2, "0791234567", "سعيد", "محمود" },
                    { 2, 0, new DateTime(1998, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "فاطمة", 1, null, false, true, "طالبة", "الحسني", 1, 1, "0789876543", "خالد", "عبد الله" },
                    { 3, 7, new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "محمد", 0, null, true, true, "محاسب", "القيسي", 2, 2, "0795551122", "علي", "سليم" },
                    { 4, 2, new DateTime(1995, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "مريم", 1, null, true, false, null, "العنزي", 1, 3, "0784443322", "إبراهيم", "حسن" },
                    { 5, 0, new DateTime(2000, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "يوسف", 0, null, true, true, "طالب", "العجارمة", 1, 1, "0797778899", "صالح", "عبد الرحمن" },
                    { 6, 5, new DateTime(1992, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "ليلى", 1, null, false, true, "ربة منزل", "الطراونة", 2, 3, "0781122334", "موسى", "جميل" },
                    { 7, 3, new DateTime(1988, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "سلمان", 0, null, true, true, "فني كهرباء", "العوفي", 2, 2, "0793322110", "زيد", "أحمد" },
                    { 8, 5, new DateTime(1999, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "نهى", 1, null, true, false, "طالبة", "الخطيب", 1, 1, "0786677889", "راشد", "سامي" },
                    { 9, 6, new DateTime(1983, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "حسن", 0, null, true, true, "سائق", "البقاعي", 2, 2, "0799988776", "محمود", "فوزي" },
                    { 10, 0, new DateTime(2001, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "نور", 1, null, false, true, "طالبة", "الغزاوي", 1, 1, "0789988223", "جميل", "عمر" },
                    { 11, 2, new DateTime(1990, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "خالد", 0, null, true, true, "معلم", "المعايطه", 2, 2, "0792211334", "فهد", "سالم" },
                    { 12, 5, new DateTime(1997, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "سارة", 1, null, true, false, "مصممة", "الشروف", 1, 2, "0785544332", "منصور", "أمين" },
                    { 13, 4, new DateTime(1986, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "رائد", 0, null, true, true, "ممرض", "عواد", 2, 2, "0798877665", "خليل", "يوسف" },
                    { 14, 3, new DateTime(1993, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "هناء", 1, null, false, true, "ربة منزل", "السالم", 2, 3, "0784433221", "زيد", "عبدالله" },
                    { 15, 6, new DateTime(1982, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "عبدالله", 0, null, true, true, "تاجر", "العلاونة", 2, 2, "0791122556", "محمد", "نادر" },
                    { 16, 0, new DateTime(1996, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "دلال", 1, null, true, false, "طالبة", "الزيود", 1, 1, "0782211445", "سالم", "هاشم" },
                    { 17, 2, new DateTime(1989, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "إيهاب", 0, null, true, true, "مبرمج", "العتوم", 2, 2, "0796655443", "سامي", "سعيد" },
                    { 18, 6, new DateTime(2002, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "جنان", 1, null, false, true, "طالبة", "الطويل", 1, 1, "0787766554", "طه", "رياض" },
                    { 19, 5, new DateTime(1984, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "سليمان", 0, null, true, true, "مقاول", "الحوري", 2, 2, "0794433556", "أكرم", "منير" },
                    { 20, 2, new DateTime(1999, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "تقى", 1, null, true, false, "سكرتيرة", "الصمادي", 1, 2, "0789988771", "نائل", "فارس" }
                });

            migrationBuilder.InsertData(
                table: "ProjectCatogories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "مشاريع سكنية", "سكني" },
                    { 2, "مشاريع تجارية", "تجاري" },
                    { 3, "مشاريع تعليمية", "تعليمي" },
                    { 4, "مشاريع مساعدات", "مساعدات" }
                });

            migrationBuilder.InsertData(
                table: "TeamRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "مدير المشروع" },
                    { 2, "النائب" },
                    { 3, "عضو" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", 0, "41812ea1-bd92-4260-ac24-d2ac5e112aa8", "sys.smartneighborhood@gmail.com", null, null, true, false, false, null, "sys.smartneighborhood@gmail.com", "ADMIN", "AQAAAAIAAYagAAAAEPfXKQe9Og0u2tIxllcdSAU6okauujYdu23/dzH+wyKWrN6cma6v/Q4rmMJUIFwIUQ==", 9, null, false, "ff6ca4d9-c9b7-4425-90a9-95fba2078b39", false, "Admin" },
                    { "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa", 0, "e10459f4-3df0-46a7-a404-31b4d9b652a2", "sys.smartneighborhood@gmail.com", null, null, true, false, false, null, "SYS.SMARTNEIGHBORHOOD@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAfdpDF/Bi0JWZQfHJacD0eEyOcAX6JqhZFljJdsOPvG/jq3Uq3RPZ418yhXwS21Ng==", 1, null, false, "f42fc8e1-5aee-4dcb-b1db-1bf6f8affbec", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "IDCardApplications",
                columns: new[] { "Id", "ApplicationNumber", "Notes", "ResidentId", "Status" },
                values: new object[,]
                {
                    { 1, "IDC-5001", "Initial application submitted", 1, 0 },
                    { 2, "IDC-5002", "Biometric verification pending", 3, 1 },
                    { 3, "IDC2003", null, 3, 0 },
                    { 4, "IDC2004", "Missing ID copy", 4, 3 },
                    { 5, "IDC2005", null, 5, 2 },
                    { 6, "IDC2006", null, 6, 0 },
                    { 7, "IDC2007", null, 7, 1 },
                    { 8, "IDC2008", null, 8, 2 },
                    { 9, "IDC2009", null, 9, 0 },
                    { 10, "IDC2010", null, 10, 2 },
                    { 11, "IDC2011", null, 11, 3 },
                    { 12, "IDC2012", null, 12, 1 },
                    { 13, "IDC2013", null, 13, 2 },
                    { 14, "IDC2014", null, 14, 0 },
                    { 15, "IDC2015", null, 15, 1 },
                    { 16, "IDC2016", null, 16, 2 },
                    { 17, "IDC2017", null, 17, 0 },
                    { 18, "IDC2018", null, 18, 2 },
                    { 19, "IDC2019", null, 19, 3 },
                    { 20, "IDC2020", null, 20, 1 },
                    { 21, "IDC2001", null, 1, 2 },
                    { 22, "IDC2002", null, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "MedicineBatches",
                columns: new[] { "Id", "ArrivalDate", "ExpireDate", "MedicineId", "Provider", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Health Association A", 120 },
                    { 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Medical Charity B", 200 }
                });

            migrationBuilder.InsertData(
                table: "PassportApplications",
                columns: new[] { "Id", "ApplicationNumber", "LastUpdate", "Notes", "ResidentId", "Status" },
                values: new object[,]
                {
                    { 1, "PAS-1001", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8078), "Under review", 1, 1 },
                    { 2, "PAS-1002", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8085), "Ready for pickup", 2, 2 },
                    { 3, "PAS1003", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8109), "Ready", 3, 2 },
                    { 4, "PAS1004", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8111), "Missing documents", 4, 3 },
                    { 5, "PAS1005", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8113), null, 5, 2 },
                    { 6, "PAS1006", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8114), null, 6, 0 },
                    { 7, "PAS1007", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8116), null, 7, 1 },
                    { 8, "PAS1008", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8118), null, 8, 0 },
                    { 9, "PAS1009", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8120), null, 9, 1 },
                    { 10, "PAS1010", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8121), null, 10, 2 },
                    { 11, "PAS1011", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8123), null, 11, 0 },
                    { 12, "PAS1012", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8125), null, 12, 2 },
                    { 13, "PAS1013", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8126), null, 13, 1 },
                    { 14, "PAS1014", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8128), null, 14, 0 },
                    { 15, "PAS1015", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8129), null, 15, 2 },
                    { 16, "PAS1016", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8131), "Invalid photo", 16, 3 },
                    { 17, "PAS1017", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8133), null, 17, 1 },
                    { 18, "PAS1018", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8134), null, 18, 0 },
                    { 19, "PAS1019", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8136), null, 19, 2 },
                    { 20, "PAS1020", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8138), null, 20, 1 },
                    { 21, "PAS1001", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8087), "Waiting", 1, 0 },
                    { 22, "PAS1002", new DateTime(2025, 11, 27, 16, 53, 28, 946, DateTimeKind.Utc).AddTicks(8088), "Processing", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Budget", "Description", "EndDate", "ManagerId", "Name", "ProjectCatogoryId", "ProjectPriority", "ProjectStatus", "StartDate" },
                values: new object[] { 1, 100000m, "هذا مشروع تمهيدي", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "مشروع تجريبي", 1, 1, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa" });

            migrationBuilder.CreateIndex(
                name: "IX_AdGroup_GroupsId",
                table: "AdGroup",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers",
                column: "PersonId");


            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ManagerId",
                table: "Blocks",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ConflictTypeId",
                table: "ConfilctCases",
                column: "ConflictTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_FirstPartyId",
                table: "ConfilctCases",
                column: "FirstPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ManagerId",
                table: "ConfilctCases",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_SecondPartyId",
                table: "ConfilctCases",
                column: "SecondPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_BlockId",
                table: "Families",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_FamilyCatgoryId",
                table: "Families",
                column: "FamilyCatgoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_FamilyId",
                table: "FamilyMembers",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_MemberFamilyRoleId",
                table: "FamilyMembers",
                column: "MemberFamilyRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_PersonId",
                table: "FamilyMembers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernmentInstitutionContacts_GovernmentInstitutionId",
                table: "GovernmentInstitutionContacts",
                column: "GovernmentInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_IDCardApplications_ResidentId",
                table: "IDCardApplications",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineBatches_MedicineId",
                table: "MedicineBatches",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PassportApplications_ResidentId",
                table: "PassportApplications",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCatogories_Name",
                table: "ProjectCatogories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFamilies_FamilyID",
                table: "ProjectFamilies",
                column: "FamilyID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFamilies_ProjectID",
                table: "ProjectFamilies",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectCatogoryId",
                table: "Projects",
                column: "ProjectCatogoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeams_ProjectId",
                table: "ProjectTeams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeams_TeamId",
                table: "ProjectTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_PersonId",
                table: "TeamMembers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamRoleId",
                table: "TeamMembers",
                column: "TeamRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdGroup");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ConfilctCases");

            migrationBuilder.DropTable(
                name: "GovernmentInstitutionContacts");

            migrationBuilder.DropTable(
                name: "IDCardApplications");

            migrationBuilder.DropTable(
                name: "MedicineBatches");

            migrationBuilder.DropTable(
                name: "PassportApplications");

            migrationBuilder.DropTable(
                name: "ProjectFamilies");

            migrationBuilder.DropTable(
                name: "ProjectTeams");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ConfilctTypes");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropTable(
                name: "GovernmentInstitutions");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TeamRoles");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "MemberFamilyRoles");

            migrationBuilder.DropTable(
                name: "ProjectCatogories");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "FamilyCatgories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
