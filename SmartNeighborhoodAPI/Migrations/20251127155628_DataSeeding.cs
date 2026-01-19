
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_People_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "PassportApplications",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Medicines",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "IDCardApplications",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0126101f-fb85-438d-b109-01df27e2d0d3", "AQAAAAIAAYagAAAAEJ/+Xft517onKGRROAsEq0X4rUhqDPNcWbKTu3Nk4X+JZ/vwm4vyT3XYz6x9bhbMJw==", "7cff8cad-f229-4eff-8fa4-cdd91e4786e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e338bac-0bd3-4599-b590-aad0fd8fa65a", "AQAAAAIAAYagAAAAEPSZULw+of1VaUU9TbHPIRUgAU6s19OOY25hKPGlmqhvGOV3096VHhMFfbBQ2uY9/w==", "37456c4a-6da6-4dec-bce3-d31dc53ddcac" });



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
                table: "Vehicles",
                columns: new[] { "Id", "LicenseExpiryDate", "Model", "OwnerResidentId", "PlateNumber", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toyota Camry", 1, "1234A", 1 },
                    { 2, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda Motorcycle", 2, "8891M", 2 },
                    { 3, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nissan Patrol", 3, "C9981", 1 },
                    { 4, new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kia Sportage", 4, "D1122", 1 },
                    { 5, new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda Civic", 5, "M7788", 1 },
                    { 6, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda Motorcycle", 6, "X1010", 2 },
                    { 7, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suzuki Motorcycle", 7, "P2211", 2 },
                    { 8, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford Ranger", 8, "T5544", 3 },
                    { 9, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Isuzu D-Max", 9, "V9090", 3 },
                    { 10, null, "Unknown Motorcycle", 10, null, 2 },
                    { 11, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toyota Corolla", 11, "U3401", 1 },
                    { 12, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hyundai Elantra", 12, "K4412", 1 },
                    { 13, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mazda 6", 13, "Q9971", 1 },
                    { 14, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chevrolet Spark", 14, "L5582", 1 },
                    { 15, new DateTime(2024, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toyota Yaris", 15, "H8812", 1 },
                    { 16, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Honda CRV", 16, "S9420", 1 },
                    { 17, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toyota Hilux", 17, "J3391", 3 },
                    { 18, new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suzuki Alto", 18, "R7310", 1 },
                    { 19, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "BMW 320i", 19, "W4410", 1 },
                    { 20, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mercedes C200", 20, "Z5501", 1 },
                    { 21, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toyota Camry", 1, "A1234", 1 },
                    { 22, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hyundai Tucson", 2, "B5678", 1 }
                });

            migrationBuilder.InsertData(
                table: "MedicineBatches",
                columns: new[] { "Id", "ArrivalDate", "ExpireDate", "MedicineId", "Provider", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Health Association A", 120 },
                    { 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Medical Charity B", 200 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerResidentId",
                table: "Vehicles",
                column: "OwnerResidentId");

        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerResidentId",
                table: "Vehicles");

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "IDCardApplications",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MedicineBatches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicineBatches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PassportApplications",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "PassportApplications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdate",
                table: "IDCardApplications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22c37920-1b67-4dd4-9c0a-2f589e1fd926", "AQAAAAIAAYagAAAAEKXFvu9ubD0HOcf5pUo1YvxgrVIqPTpnaf6SUVeCkR4JaTrRlmCWl3RV9V6bL6i4xA==", "5d0504b9-492d-48b6-9a30-7c17fde0b9e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d28f1ba-ea4c-4581-99a6-3a4c5c3c1fcb", "AQAAAAIAAYagAAAAEJ92QVTVNEJyNuJuaynphUieEo8g4VOwja7fkw+KyukE5AV87RQQNWdGLa3FwPYPkQ==", "5071b641-b51e-48f9-a99c-a0e5dee9d869" });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_People_OwnerId",
                table: "Vehicles",
                column: "OwnerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
