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
                    personType = table.Column<int>(type: "int", nullable: false),
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
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectCatogoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectStatus = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: true),
                    ProjectPriority = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    ManagerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_ManagerId1",
                        column: x => x.ManagerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_ProjectCatogories_ProjectCatogoryId",
                        column: x => x.ProjectCatogoryId,
                        principalTable: "ProjectCatogories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialNeighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NeighborhoodManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialNeighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialNeighborhoods_AspNetUsers_NeighborhoodManagerId",
                        column: x => x.NeighborhoodManagerId,
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
                name: "ResidentialUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidentialNeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    UnitManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialUnits_AspNetUsers_UnitManagerId",
                        column: x => x.UnitManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResidentialUnits_ResidentialNeighborhoods_ResidentialNeighborhoodId",
                        column: x => x.ResidentialNeighborhoodId,
                        principalTable: "ResidentialNeighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidentialUnitId = table.Column<int>(type: "int", nullable: false),
                    BlockManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_AspNetUsers_BlockManagerId",
                        column: x => x.BlockManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blocks_ResidentialUnits_ResidentialUnitId",
                        column: x => x.ResidentialUnitId,
                        principalTable: "ResidentialUnits",
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
                name: "ProjectBlock",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBlock", x => new { x.ProjectId, x.BlockId });
                    table.ForeignKey(
                        name: "FK_ProjectBlock_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectBlock_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_FamilyMembers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_ProjectFamilies_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
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
                    ConflictTypeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConfilctTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfilctCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_ConfilctTypes_ConfilctTypeId",
                        column: x => x.ConfilctTypeId,
                        principalTable: "ConfilctTypes",
                        principalColumn: "Id");
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
                    { "33333333-3333-3333-3333-333333333333", null, "UnitManager", "UNITMANAGER" },
                    { "44444444-4444-4444-4444-444444444444", null, "ResidentialNeighborhoodManager", "RESIDENTIALNEIGHBORHOODMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", 0, "90f79cfa-938c-403e-b72c-07a1ad6b305d", "neighborhood.manager@test.com", null, null, true, true, false, null, "NEIGHBORHOOD.MANAGER@TEST.COM", "NEIGHBORHOODMANAGER1", "AQAAAAIAAYagAAAAELhPoGacZ/oN4j6DqtwsCICCjcki95H5XDMKPyjmpW0jPygAw+y2SPISNfeeeGvQ3w==", 2, null, false, "fc83a7c3-6217-421c-8c21-4df58a72416e", false, "NeighborhoodManager1" });

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
                columns: new[] { "Id", "BloodType", "DateOfBirth", "FirstName", "Gender", "Image", "IsContactNumber", "IsWhatsapp", "Job", "LastName", "MaritalStatus", "OccupationStatus", "PhoneNumber", "SecondName", "ThirdName", "personType" },
                values: new object[,]
                {
                    { 1, 5, null, "أحمد", 0, null, null, null, "مهندس", "الزهيري", 2, 2, "0791234567", "سعيد", "محمود", 3 },
                    { 3, 6, new DateTime(1982, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "سالم", 0, null, true, true, null, "العمري", 2, 2, "0771122334", "بن راشد", "عوض", 2 },
                    { 4, 0, new DateTime(1979, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "خالد", 0, null, true, true, null, "الغرمي", 2, 2, "0772233445", "بن حمد", "محمد", 1 },
                    { 5, 5, new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "يوسف", 0, null, true, true, null, "الملاح", 2, 2, "0773344556", "محمد", "سالم", 1 },
                    { 6, 4, new DateTime(1980, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "عبدالله", 0, null, true, true, null, "الهادي", 2, 2, "0774455667", "سالم", "محمد", 1 },
                    { 7, 2, new DateTime(1986, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "علي", 0, null, true, true, null, "الدفعي", 2, 2, "0775566778", "حسن", "سالم", 1 },
                    { 8, 7, new DateTime(1978, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ناصر", 0, null, true, true, null, "القحطاني", 2, 2, "0776677889", "عبد الرحمن", "بدر", 1 },
                    { 11, 0, new DateTime(1975, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "محمد", 0, null, null, null, "سائق", "الغانم", 2, 2, "0791110001", "سعيد", "صالح", 0 },
                    { 12, 2, new DateTime(1978, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "مريم", 1, null, null, null, null, "الغانم", 2, 3, "0791110002", "صالح", "محمد", 0 },
                    { 13, 0, new DateTime(2005, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "سعيد", 0, null, null, null, null, "الغانم", 1, 1, "0791110003", "محمد", "سعيد", 0 },
                    { 14, 6, new DateTime(1980, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "أحمد", 0, null, null, null, null, "الملاح", 2, 2, "0791122001", "عبد الله", "خليل", 0 },
                    { 15, 5, new DateTime(1982, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "سمية", 1, null, null, null, null, "الملاح", 2, 3, "0791122002", "خليل", "عبد الله", 0 },
                    { 16, 2, new DateTime(2008, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "هند", 1, null, null, null, null, "الملاح", 1, 1, "0791122003", "أحمد", "خليل", 0 },
                    { 17, 7, new DateTime(1970, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "عبد الولي", 0, null, null, null, null, "العكبري", 2, 3, "0791133001", "بن سالم", "خميس", 0 },
                    { 18, 1, new DateTime(1974, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "نورة", 1, null, null, null, null, "العكبري", 2, 3, "0791133002", "خميس", "بن سالم", 0 },
                    { 19, 2, new DateTime(2006, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "سارة", 1, null, null, null, null, "العكبري", 1, 1, "0791133003", "عبد الولي", "خميس", 0 },
                    { 20, 0, new DateTime(1983, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "خالد", 0, null, null, null, null, "العمري", 2, 2, "0791144001", "سعيد", "خالد", 0 },
                    { 21, 0, new DateTime(1985, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "رنا", 1, null, null, null, null, "العمري", 2, 2, "0791144002", "خالد", "سعيد", 0 },
                    { 22, 6, new DateTime(2010, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "مازن", 0, null, null, null, null, "العمري", 1, 1, "0791144003", "خالد", "سعيد", 0 },
                    { 23, 2, new DateTime(1976, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "سالم", 0, null, null, null, null, "الهاشمي", 2, 2, "0791155001", "المعطي", "بن", 0 },
                    { 24, 5, new DateTime(1979, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "هناء", 1, null, null, null, null, "الهاشمي", 2, 3, "0791155002", "سالم", "المعطي", 0 },
                    { 25, 7, new DateTime(2009, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ليلى", 1, null, null, null, null, "الهاشمي", 1, 1, "0791155003", "سالم", "المعطي", 0 },
                    { 26, 0, new DateTime(1988, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "هشام", 0, null, null, null, null, "الحمادي", 2, 2, "0791166001", "عبد الله", "بن", 0 },
                    { 27, 2, new DateTime(1990, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "نهى", 1, null, null, null, null, "الحمادي", 2, 2, "0791166002", "هشام", "عبد الله", 0 },
                    { 28, 2, new DateTime(2012, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "ريان", 0, null, null, null, null, "الحمادي", 1, 1, "0791166003", "هشام", "عبد الله", 0 },
                    { 29, 0, new DateTime(1972, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "عبد الكريم", 0, null, null, null, null, "الحدري", 2, 3, "0791177001", "حسن", "صالح", 0 },
                    { 30, 7, new DateTime(1975, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "فاطمة", 1, null, null, null, null, "الحدري", 2, 3, "0791177002", "عبد الكريم", "حسن", 0 },
                    { 31, 6, new DateTime(2007, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "ريان", 0, null, null, null, null, "الحدري", 1, 1, "0791177003", "عبد الكريم", "حسن", 0 },
                    { 32, 0, new DateTime(1986, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "يوسف", 0, null, null, null, null, "البساطي", 2, 2, "0791188001", "عبد الله", "علي", 0 },
                    { 33, 3, new DateTime(1988, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "سعاد", 1, null, null, null, null, "البساطي", 2, 3, "0791188002", "يوسف", "عبد الله", 0 },
                    { 34, 7, new DateTime(2011, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "عائشة", 1, null, null, null, null, "البساطي", 1, 1, "0791188003", "يوسف", "عبد الله", 0 },
                    { 35, 0, new DateTime(1979, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "سعد", 0, null, null, null, null, "المرزوقي", 2, 2, "0791199001", "محمد", "الشريف", 0 },
                    { 36, 2, new DateTime(1981, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "هناء", 1, null, null, null, null, "المرزوقي", 2, 3, "0791199002", "سعد", "محمد", 0 },
                    { 37, 6, new DateTime(1982, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "عمر", 0, null, null, null, null, "الزبيري", 2, 2, "0791199003", "سعيد", "الزبيري", 0 },
                    { 38, 5, new DateTime(1984, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "نجلاء", 1, null, null, null, null, "الزبيري", 2, 3, "0791199004", "عمر", "سعيد", 0 },
                    { 39, 2, new DateTime(1975, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "عبد الرحمن", 0, null, null, null, null, "الهاشمي", 2, 2, "0791199005", "محيي", "الهاشمي", 0 },
                    { 40, 7, new DateTime(1978, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "آمنة", 1, null, null, null, null, "الهاشمي", 2, 3, "0791199006", "عبد الرحمن", "محيي", 0 },
                    { 41, 0, new DateTime(1986, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "قاسم", 0, null, null, null, null, "العرادي", 2, 2, "0791199007", "علي", "العرادي", 0 },
                    { 42, 2, new DateTime(1987, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "سلوى", 1, null, null, null, null, "العرادي", 2, 3, "0791199008", "قاسم", "علي", 0 },
                    { 43, 7, new DateTime(1974, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ماهر", 0, null, null, null, null, "القاضي", 2, 3, "0791199009", "خالد", "القاضي", 0 },
                    { 44, 0, new DateTime(1976, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "مريم", 1, null, null, null, null, "القاضي", 2, 3, "0791199010", "ماهر", "خالد", 0 },
                    { 45, 2, new DateTime(1989, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "سيف", 0, null, null, null, null, "الربيعي", 2, 2, "0791199011", "عوض", "الربيعي", 0 },
                    { 46, 1, new DateTime(1990, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "هاجر", 1, null, null, null, null, "الربيعي", 2, 2, "0791199012", "سيف", "عوض", 0 },
                    { 47, 0, new DateTime(1973, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "عبد الله", 0, null, null, null, null, "الحاج", 2, 3, "0791199013", "محمود", "الحاج", 0 },
                    { 48, 2, new DateTime(1975, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "هدى", 1, null, null, null, null, "الحاج", 2, 3, "0791199014", "عبد الله", "محمود", 0 },
                    { 49, 6, new DateTime(1984, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "صالح", 0, null, null, null, null, "السقاف", 2, 2, "0791199015", "علي", "السقاف", 0 },
                    { 50, 0, new DateTime(1986, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "نجلاء", 1, null, null, null, null, "السقاف", 2, 3, "0791199016", "صالح", "علي", 0 },
                    { 51, 4, new DateTime(1988, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "هاني", 0, null, null, null, null, "العولقي", 2, 2, "0791199017", "سالم", "العولقي", 0 },
                    { 52, 2, new DateTime(1990, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "صفية", 1, null, null, null, null, "العولقي", 2, 3, "0791199018", "هاني", "سالم", 0 },
                    { 53, 7, new DateTime(1977, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "مختار", 0, null, null, null, null, "مختار", 2, 2, "0791199019", "سليم", "مختار", 0 },
                    { 54, 0, new DateTime(1979, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "أماني", 1, null, null, null, null, "مختار", 2, 3, "0791199020", "مختار", "سليم", 0 }
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
                    { "10101010-1010-1010-1010-101010101010", 0, "79271c9c-bc00-49c6-8b90-f81b6752cf33", "block.manager4@test.com", null, null, true, true, false, null, "BLOCK.MANAGER4@TEST.COM", "BLOCKMANAGER4", "AQAAAAIAAYagAAAAEKKDUuaqkx7GBefZuoFl4qNkOJzk+aZ8ZgUfLi6H+0KUMt0bXUT44ATdxmmALLkBng==", 7, null, false, "b49b1fda-1d4a-4a58-87a4-57c6acd308a7", false, "BlockManager4" },
                    { "20202020-2020-2020-2020-202020202020", 0, "027037f2-e438-4288-bb0c-25b73a510ce6", "block.manager5@test.com", null, null, true, true, false, null, "BLOCK.MANAGER5@TEST.COM", "BLOCKMANAGER5", "AQAAAAIAAYagAAAAEEB3Lsq6rPuA+5UpWMs5D8khu4ruRPZxsq+8mK0/WkBBOoNHaQF7tth9mJTMHGTavw==", 8, null, false, "7712931f-5636-49f6-aa64-f3bebf450eae", false, "BlockManager5" },
                    { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", 0, "695f4ae2-44ab-49f6-9393-d46880ba38fb", "sys.smartneighborhood@gmail.com", null, null, true, false, false, null, "SYS.SMARTNEIGHBORHOOD@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEBLKwpW6VFBYMLYUpZHwWvoq0K+nURGAqH8T7kEkk2CP0yUL0h0GRgsrlvj2mXlzjA==", 1, null, false, "d3a563f6-b5e4-44ff-b3a5-2b2a228b4868", false, "Admin" },
                    { "cccccccc-cccc-cccc-cccc-cccccccccccc", 0, "40052b47-71f9-47f9-b9ea-9cd426c470f2", "unit.manager@test.com", null, null, true, true, false, null, "UNIT.MANAGER@TEST.COM", "UNITMANAGER1", "AQAAAAIAAYagAAAAEKHPEJPxmgstoGQnv7bSIRDCcaZ1rQ7ZC1Wjy7usixEf/Zs+5AEf90i/qUb6yhVTBg==", 3, null, false, "54555566-0520-43f1-a664-e64aa74ed4ab", false, "UnitManager1" },
                    { "dddddddd-dddd-dddd-dddd-dddddddddddd", 0, "4a9db97c-08e3-4cc4-a5e0-4b0dc89d7dc5", "block.manager1@test.com", null, null, true, true, false, null, "BLOCK.MANAGER1@TEST.COM", "BLOCKMANAGER1", "AQAAAAIAAYagAAAAEBZA6QozvQF3EQPcpQc+6N14xr/vuKFzDm6ZpoeRUBcppuxtwAXrMjRSmUGEzskBoQ==", 4, null, false, "38a3641f-499c-4b9f-aa17-0a5ae0a13e6f", false, "BlockManager1" },
                    { "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", 0, "bba5b935-b583-4094-9d68-528516775fcf", "block.manager2@test.com", null, null, true, true, false, null, "BLOCK.MANAGER2@TEST.COM", "BLOCKMANAGER2", "AQAAAAIAAYagAAAAEOMu+qAePP2NJq6cetnCVk3tEkwgMlGAoaoUMEssvOy2xBQ7+7N5YHm5kYogkEnYkw==", 5, null, false, "a240b0ed-9050-4ef9-b8a2-832fae864ba0", false, "BlockManager2" },
                    { "ffffffff-ffff-ffff-ffff-ffffffffffff", 0, "f995e56e-f93b-44eb-8c8a-c2be26250ba2", "block.manager3@test.com", null, null, true, true, false, null, "BLOCK.MANAGER3@TEST.COM", "BLOCKMANAGER3", "AQAAAAIAAYagAAAAEH262/ag1Mn1cctPUIaWSfH4IwxpFYFFHNnP/1/LvtL2aWo3RSRfOdaCqx8jN0w2Nw==", 6, null, false, "58e685b4-569d-40fd-bfa3-c05943952990", false, "BlockManager3" }
                });

            migrationBuilder.InsertData(
                table: "ResidentialNeighborhoods",
                columns: new[] { "Id", "Name", "NeighborhoodManagerId" },
                values: new object[,]
                {
                    { 1, "حي 22 مايو", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 2, "حي أكتوبر", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 3, "حي ابن سيناء", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 4, "حي الثورة", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 5, "حي الحرشيات", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 6, "حي السلام", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 7, "حي الشهيد خالد", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 8, "حي الصديق", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 9, "حي الصيادين", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 10, "حي العمال", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 11, "حي العيص", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 12, "حي المتضررين", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 13, "حي النصر", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 14, "حي امبيخة", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 15, "حي بويش", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 16, "حي خلف", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 17, "حي روكب", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 18, "حي فوه القديمة", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 19, "حي نوفمبر", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                    { 20, "مربع واحد", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa" });

            migrationBuilder.InsertData(
                table: "ResidentialUnits",
                columns: new[] { "Id", "Name", "ResidentialNeighborhoodId", "UnitManagerId" },
                values: new object[] { 1, "الوحدة السكنية 1", 1, "cccccccc-cccc-cccc-cccc-cccccccccccc" });

            migrationBuilder.InsertData(
                table: "Blocks",
                columns: new[] { "Id", "BlockManagerId", "Name", "ResidentialUnitId" },
                values: new object[,]
                {
                    { 1, "dddddddd-dddd-dddd-dddd-dddddddddddd", "مبنى 1", 1 },
                    { 2, "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", "مبنى 2", 1 },
                    { 3, "ffffffff-ffff-ffff-ffff-ffffffffffff", "مبنى 3", 1 },
                    { 4, "10101010-1010-1010-1010-101010101010", "مبنى 4", 1 },
                    { 5, "20202020-2020-2020-2020-202020202020", "مبنى 5", 1 },
                    { 6, "dddddddd-dddd-dddd-dddd-dddddddddddd", "مبنى 6", 1 },
                    { 7, "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", "مبنى 7", 1 },
                    { 8, "ffffffff-ffff-ffff-ffff-ffffffffffff", "مبنى 8", 1 },
                    { 9, "10101010-1010-1010-1010-101010101010", "مبنى 9", 1 },
                    { 10, "20202020-2020-2020-2020-202020202020", "مبنى 10", 1 },
                    { 11, "dddddddd-dddd-dddd-dddd-dddddddddddd", "مبنى 11", 1 },
                    { 12, "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", "مبنى 12", 1 },
                    { 13, "ffffffff-ffff-ffff-ffff-ffffffffffff", "مبنى 13", 1 },
                    { 14, "10101010-1010-1010-1010-101010101010", "مبنى 14", 1 },
                    { 15, "20202020-2020-2020-2020-202020202020", "مبنى 15", 1 },
                    { 16, "dddddddd-dddd-dddd-dddd-dddddddddddd", "مبنى 16", 1 },
                    { 17, "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", "مبنى 17", 1 },
                    { 18, "ffffffff-ffff-ffff-ffff-ffffffffffff", "مبنى 18", 1 },
                    { 19, "10101010-1010-1010-1010-101010101010", "مبنى 19", 1 },
                    { 20, "20202020-2020-2020-2020-202020202020", "مبنى 20", 1 }
                });

            migrationBuilder.InsertData(
                table: "Families",
                columns: new[] { "Id", "BlockId", "FamilyCatgoryId", "FamilyNotes", "HousingType", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, "لا ملاحظات", 2, "البيت 1، الحي القديم", "عائلة الغانم" },
                    { 2, 1, 3, "تأجير طويل الأمد", 1, "البيت 2، شارع البحر", "عائلة الملاح" },
                    { 3, 2, 2, "عائلة مع العديد من الأولاد", 2, "الزاوية الجنوبية، مبنى 2", "عائلة العكبري" },
                    { 4, 2, 3, null, 1, "الطابق الأول، مبنى 2", "عائلة العمري" },
                    { 5, 3, 1, "تحتاج دعم صحّي", 2, "الحي الشمالي، مبنى 3", "عائلة الهاشمي" },
                    { 6, 3, 3, null, 1, "الطابق الثاني، مبنى 3", "عائلة الحمادي" },
                    { 7, 4, 2, "أحد أفراد الأسرة بحاجة لرعاية", 2, "الشارع العام، مبنى 4", "عائلة الحدري" },
                    { 8, 4, 3, null, 1, "الصفحة الشرقية، مبنى 4", "عائلة البساطي" },
                    { 9, 5, 1, "عائلة مستقرة", 2, "المنطقة الجنوبية، مبنى 5", "عائلة الملاح" },
                    { 10, 5, 3, null, 1, "الطابق الأرضي، مبنى 5", "عائلة القحطاني" },
                    { 11, 6, 1, null, 2, "الطابق الأول، مبنى 6", "عائلة الشريف" },
                    { 12, 7, 2, "تحتاج دعم تعليمي", 1, "البيت 2، مبنى 7", "عائلة الزبيري" },
                    { 13, 8, 3, null, 2, "الزاوية الغربية، مبنى 8", "عائلة الهاشمي" },
                    { 14, 9, 3, null, 1, "الشقة 3، مبنى 9", "عائلة العرادي" },
                    { 15, 10, 1, "أم عازبة", 2, "الطابق العلوي، مبنى 10", "عائلة القاضي" },
                    { 16, 11, 3, null, 1, "مبنى 11، شارع السوق", "عائلة الربيعي" },
                    { 17, 12, 2, "عائلة كبيرة", 2, "المنطقة 12، مبنى 12", "عائلة الحاج" },
                    { 18, 13, 3, null, 1, "الطابق الثاني، مبنى 13", "عائلة السقاف" },
                    { 19, 14, 3, null, 2, "منزل صغير، مبنى 14", "عائلة العولقي" },
                    { 20, 15, 2, null, 1, "الطابق الأرضي، مبنى 15", "عائلة مختار" }
                });

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");


            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ResidentialUnitId",
                table: "Blocks",
                column: "ResidentialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ConfilctTypeId",
                table: "ConfilctCases",
                column: "ConfilctTypeId");

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
                name: "IX_ProjectBlock_BlockId",
                table: "ProjectBlock",
                column: "BlockId");

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
                name: "IX_Projects_ManagerId1",
                table: "Projects",
                column: "ManagerId1");

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
                name: "IX_ResidentialNeighborhoods_Name",
                table: "ResidentialNeighborhoods",
                column: "Name",
                unique: true);



            migrationBuilder.CreateIndex(
                name: "IX_ResidentialUnits_ResidentialNeighborhoodId",
                table: "ResidentialUnits",
                column: "ResidentialNeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialUnits_UnitManagerId",
                table: "ResidentialUnits",
                column: "UnitManagerId",
                unique: true);

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
                name: "ProjectBlock");

            migrationBuilder.DropTable(
                name: "ProjectFamilies");

            migrationBuilder.DropTable(
                name: "ProjectTeams");

            migrationBuilder.DropTable(
                name: "TeamMembers");

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
                name: "ResidentialUnits");

            migrationBuilder.DropTable(
                name: "ResidentialNeighborhoods");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
