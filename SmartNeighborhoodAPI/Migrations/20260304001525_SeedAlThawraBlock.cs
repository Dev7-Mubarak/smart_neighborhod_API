using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedAlThawraBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_ConfilctTypes_ConfilctTypeId",
                table: "ConfilctCases");

            migrationBuilder.DropForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ManagerId1",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "AdGroup");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_ConfilctTypeId",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ConfilctTypeId",
                table: "ConfilctCases");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GovernmentInstitutions",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Families",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Families",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FamilyNotes",
                table: "Families",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ConfilctTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blocks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AdsText",
                table: "Ads",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d1696d9-94f6-4b4c-abb0-7f7e300f5b5a", "AQAAAAIAAYagAAAAEDiXrMr7GD+bUsWi7dkYWVGlKsB5VSpYk4CqVeXirV3YUN7sDKXb090Sg4FUXDPLfg==", "9cc06796-303b-4a2c-a71d-4390ed41fb4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2afade91-bc41-4fef-ab68-647cfdb42757", "AQAAAAIAAYagAAAAEHksLfDFXSvLVVNnmBfQNKQerqk85clb2lEXzkwGE9U9Y+rlJ0QsOMO9q6RJl7buvA==", "a3f7042d-bcbf-436d-8eaf-2058c999ed9f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5f89e0d-de3e-4b0d-a7d5-b1abb516cc41", "AQAAAAIAAYagAAAAEGgRUES8D6Dx6Jfd4We04swrAwPBTs7x9schV3LyynO4GZIE2mCrTkh9rKhWektTnw==", "86379be0-dfd0-4b3c-b169-bce2de9e4d0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9167fa2c-3d76-4c9e-b1c3-89a9b6708efc", "AQAAAAIAAYagAAAAEJtMN4xXyzIInE0SWOblpC3i/826lhs4V7+i46uPOLK9Pxcfmfx0PrOcyHoda1JoeA==", "6cf4c11b-76bc-44b5-9e3d-021158d1d1cb" });

            migrationBuilder.InsertData(
                table: "ConfilctTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "صلح" },
                    { 2, "معاهدات" },
                    { 3, "اتفاقيات" }
                });

            migrationBuilder.InsertData(
                table: "GovernmentInstitutions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "محطة المياه والصرف الصحي" },
                    { 2, "مديرية الأمن" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "مجموعة الحي العام" },
                    { 2, "مجموعة الشباب" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BloodType", "ChronicDiseasesNotes", "DateOfBirth", "FirstName", "Gender", "HasChronicDiseases", "Image", "IsContactNumber", "IsWhatsapp", "Job", "LastName", "MaritalStatus", "NationalId", "OccupationStatus", "PhoneNumber", "ResidencyStatus", "SecondName", "ThirdName", "VehicleRegistrationNumber", "VehicleType", "personType" },
                values: new object[,]
                {
                    { 1, 6, null, null, "مبارك", 0, null, null, null, null, null, "الأمين", 1, null, 2, "0900000001", 0, "محمد", "علي", null, null, 3 },
                    { 2, 0, null, null, "خالد", 0, null, null, null, null, null, "العتيبي", 2, null, 2, "0900000002", 0, "عمر", "سالم", null, null, 1 },
                    { 3, 2, null, null, "عبدالله", 0, null, null, null, null, null, "المرواني", 2, null, 2, "0900000003", 0, "سعيد", "حسن", null, null, 2 },
                    { 4, 4, null, null, "يوسف", 0, null, null, null, null, null, "الزيدي", 2, null, 2, "0900000004", 0, "إبراهيم", "ناصر", null, null, 2 },
                    { 5, 6, null, null, "أحمد", 0, false, null, true, true, "مهندس", "الصالح", 2, null, 2, "0911100001", 0, "محمد", "صالح", null, null, 0 },
                    { 6, 0, null, null, "فاطمة", 1, false, null, false, true, "معلمة", "الصالح", 2, null, 2, "0911100002", 0, "علي", "حسن", null, null, 0 },
                    { 7, 6, null, null, "محمد", 0, false, null, null, null, null, "الصالح", 1, null, 1, null, 0, "أحمد", "محمد", null, null, 0 },
                    { 8, 0, null, null, "سارة", 1, false, null, null, null, null, "الصالح", 1, null, 1, null, 0, "أحمد", "محمد", null, null, 0 },
                    { 9, 2, null, null, "عمر", 0, false, null, true, true, "تاجر", "البدر", 2, null, 2, "0922200001", 0, "يوسف", "ناصر", null, null, 0 },
                    { 10, 4, "ضغط الدم", null, "سلمى", 1, true, null, true, false, null, "البدر", 2, null, 3, "0922200002", 0, "خالد", "نور", null, null, 0 },
                    { 11, 2, null, null, "يوسف", 0, false, null, null, null, null, "البدر", 1, null, 1, null, 0, "عمر", "يوسف", null, null, 0 },
                    { 12, 0, null, null, "حسن", 0, false, null, true, true, "محاسب", "الحمدان", 2, null, 2, "0933300001", 0, "علي", "ناصر", null, null, 0 },
                    { 13, 6, null, null, "نورة", 1, false, null, false, true, null, "الحمدان", 2, null, 3, "0933300002", 0, "محمد", "سعيد", null, null, 0 },
                    { 14, 0, null, null, "عبدالرحمن", 0, false, null, null, null, null, "الحمدان", 1, null, 1, null, 0, "حسن", "علي", null, null, 0 },
                    { 15, 6, null, null, "ليلى", 1, false, null, null, null, null, "الحمدان", 1, null, 1, null, 0, "حسن", "علي", null, null, 0 },
                    { 16, 2, "سكري", null, "إبراهيم", 0, true, null, true, true, "مقاول", "الرشيد", 2, null, 2, "0944400001", 0, "عبدالله", "فهد", null, null, 0 },
                    { 17, 1, null, null, "منى", 1, false, null, true, false, null, "الرشيد", 2, null, 3, "0944400002", 0, "يوسف", "كريم", null, null, 0 },
                    { 18, 2, null, null, "طارق", 0, false, null, null, null, null, "الرشيد", 1, null, 1, null, 0, "إبراهيم", "عبدالله", null, null, 0 },
                    { 19, 6, null, null, "سلطان", 0, false, null, true, true, "معلم", "النعيمي", 2, null, 2, "0955500001", 0, "ناصر", "راشد", null, null, 0 },
                    { 20, 5, null, null, "هيفاء", 1, false, null, true, true, null, "النعيمي", 2, null, 3, "0955500002", 0, "سالم", "حمد", null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Budget", "Description", "EndDate", "ManagerId", "Name", "ProjectCatogoryId", "ProjectPriority", "ProjectStatus", "StartDate" },
                values: new object[,]
                {
                    { 1, 100000m, "هذا مشروع تمهيدي", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "مشروع تجريبي", 1, 1, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 15000m, "تحسين النظافة والمرافق في المنطقة", new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "حملة نظافة الحي", 4, 2, 0, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 40000m, "إنشاء ملعب مصغر وتجهيزات رياضية", new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "تطوير ملعب الحي", 1, 1, 0, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ResidentialNeighborhoods",
                columns: new[] { "Id", "Name", "NeighborhoodManagerId" },
                values: new object[] { 1, "الحي الأول", "dddddddd-dddd-dddd-dddd-dddddddddddd" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "فريق تطوير الحي" },
                    { 2, "فريق حملة النظافة" }
                });

            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "Id", "AdsText", "CreatedTime", "GroupId" },
                values: new object[,]
                {
                    { 1, "إعلان: اجتماع عام لسكان حي الثوره يوم الجمعة القادم الساعة السابعة مساءً.", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "تذكير: الرجاء الحفاظ على نظافة الشوارع والمرافق العامة في الحي.", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "دعوة للشباب للمشاركة في مبادرة تطوير ملعب الحي — التسجيل مفتوح.", new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "GovernmentInstitutionContacts",
                columns: new[] { "Id", "GovernmentInstitutionId", "Job", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "مدير محطة المياه", "مهندس علي سالم", "0777001111" },
                    { 2, 1, "مسؤولة الصرف الصحي", "أمينة محمد", "0777002222" },
                    { 3, 2, "ضابط ارتباط", "نقيب خالد الشامي", "0777013333" }
                });

            migrationBuilder.InsertData(
                table: "ProjectTeams",
                columns: new[] { "Id", "ProjectId", "TeamId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "ResidentialUnits",
                columns: new[] { "Id", "Name", "ResidentialNeighborhoodId", "UnitManagerId" },
                values: new object[] { 1, "الوحدة الأولى", 1, "cccccccc-cccc-cccc-cccc-cccccccccccc" });

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "Id", "DateOfJoin", "PersonId", "TeamId", "TeamRoleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 1 },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1, 2 },
                    { 3, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 1, 3 },
                    { 4, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 1, 3 },
                    { 5, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 2, 1 },
                    { 6, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Blocks",
                columns: new[] { "Id", "BlockManagerId", "Name", "ResidentialUnitId" },
                values: new object[] { 1, "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", "حي الثوره", 1 });

            migrationBuilder.InsertData(
                table: "Families",
                columns: new[] { "Id", "BlockId", "FamilyCatgoryId", "FamilyNotes", "HousingType", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, null, 2, "الشارع الأول، حي الثوره", "عائلة الصالح" },
                    { 2, 1, 2, null, 1, "الشارع الثاني، حي الثوره", "عائلة البدر" },
                    { 3, 1, 1, null, 2, "الشارع الثالث، حي الثوره", "عائلة الحمدان" },
                    { 4, 1, 3, null, 1, "الشارع الرابع، حي الثوره", "عائلة الرشيد" },
                    { 5, 1, 2, null, 2, "الشارع الخامس، حي الثوره", "عائلة النعيمي" }
                });

            migrationBuilder.InsertData(
                table: "FamilyMembers",
                columns: new[] { "Id", "FamilyId", "MemberFamilyRoleId", "PersonId" },
                values: new object[,]
                {
                    { 1, 1, 1, 5 },
                    { 2, 1, 2, 6 },
                    { 3, 1, 3, 7 },
                    { 4, 1, 4, 8 },
                    { 5, 2, 1, 9 },
                    { 6, 2, 2, 10 },
                    { 7, 2, 3, 11 },
                    { 8, 3, 1, 12 },
                    { 9, 3, 2, 13 },
                    { 10, 3, 3, 14 },
                    { 11, 3, 4, 15 },
                    { 12, 4, 1, 16 },
                    { 13, 4, 2, 17 },
                    { 14, 4, 3, 18 },
                    { 15, 5, 1, 19 },
                    { 16, 5, 2, 20 }
                });

            migrationBuilder.InsertData(
                table: "ProjectFamilies",
                columns: new[] { "Id", "FamilyID", "ProjectID", "dateReceived" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 2, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 2, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, 2, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, 3, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, 3, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 5, 3, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_GroupId",
                table: "Ads",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Groups_GroupId",
                table: "Ads",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Groups_GroupId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families");

            migrationBuilder.DropIndex(
                name: "IX_Ads_GroupId",
                table: "Ads");

            migrationBuilder.DeleteData(
                table: "Ads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ads",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ConfilctTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ConfilctTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ConfilctTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "GovernmentInstitutionContacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GovernmentInstitutionContacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GovernmentInstitutionContacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProjectFamilies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProjectTeams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectTeams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GovernmentInstitutions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GovernmentInstitutions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ResidentialUnits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Budget",
                table: "Projects",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId1",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GovernmentInstitutions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Families",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Families",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "FamilyNotes",
                table: "Families",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ConfilctTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "ConfilctTypeId",
                table: "ConfilctCases",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AdsText",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ad37b45-83e0-4ea2-b41a-897b27f284b5", "AQAAAAIAAYagAAAAEIdileAA8jBYZOyEejJEcCdMaF3q+99vMqt8Try70QGjrXPcJ/xk8Y42Uk9M+392Hw==", "ea65ba2b-bede-4f81-8f6c-167d610ee116" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c31bb9d-f1f3-4868-b9f1-6af408d686cd", "AQAAAAIAAYagAAAAEGDZe6OmFCuNQ/1ItEgVpsRwy+Ab7i5peE3q/ZSctPdec/DbCuUMFbLTrH2Hx583DA==", "30dc0fdd-10df-4b9b-a6fc-6403a936e681" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d35c510c-dde6-45e2-9953-07c162dbee29", "AQAAAAIAAYagAAAAEOeHyzrAKNb8eHRaaAK8ZLPDBm+De1qczXmYI/CYTStl3S3406hZxVNGj53LXRizWw==", "309136a0-6d25-40bc-9952-ae4aad233df3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e83391e-8ba7-414e-a7a2-247206283ce2", "AQAAAAIAAYagAAAAEOaGTsreufe7/NlURhL7S1XryOXRcUpeQjykSMYD9iCejYyexhE18VTVwA0zWLxYZw==", "61252185-44e1-4890-8a25-b6ded3f13fc0" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId1",
                table: "Projects",
                column: "ManagerId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ConfilctTypeId",
                table: "ConfilctCases",
                column: "ConfilctTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdGroup_GroupsId",
                table: "AdGroup",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_ConfilctTypes_ConfilctTypeId",
                table: "ConfilctCases",
                column: "ConfilctTypeId",
                principalTable: "ConfilctTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ManagerId1",
                table: "Projects",
                column: "ManagerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
