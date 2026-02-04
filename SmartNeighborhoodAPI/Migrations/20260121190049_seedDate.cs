using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae56d344-5f2b-442d-b83a-f34388faea7a", "AQAAAAIAAYagAAAAEFccTjeo4SYJhvWUrehWyjsncLnd+2czL2KH3uF0q0m6chW6c7+sO0KuLy1QTxOZIA==", "ea2fa83e-90f7-43a7-8479-49bb6384e480" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44def285-5b27-48df-9711-46d0891f0f6e", "AQAAAAIAAYagAAAAEAhQpLMEHdMBcC3ErOahlyGM0ux5B44GjdNuKeCrSE8548BvPc01FXJNjIywvevMZA==", "7a9a8937-e487-4161-8d24-781ed1bdf475" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e13fc60d-8de2-4025-a4f9-15404099d1a2", "AQAAAAIAAYagAAAAEMfw7cDygYmwoOB4eIuO/b4Be1ZIDqtDcEOnPOxsZlJB01MEUO2uN8LoLglPpW5Zpg==", "6f35395d-0ad5-428c-866b-e66a091a5197" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a36dc126-d4a8-481a-93c5-6c1264b49db4", "AQAAAAIAAYagAAAAEONV1etOw0BjCHxd7fsnoQsoYk8UPmQ14M4MUysrL3ps9j5ihvOOLCgZPJgcNlJh9g==", "49aabed9-9f6b-4017-a8a8-56b1394e81cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "859863f6-7e6d-48ac-b8b6-558c3e1c6f50", "AQAAAAIAAYagAAAAEILEu5NRzIFl+ZkrDfFEx2OvkLmXkj7F4TyeBY2+6ZkuyxchPod+rCalmQtSqsqZQA==", "7e34b86f-ee7d-40fc-8bde-33031528f477" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "769b8975-4682-49f3-a3fe-93426751d90f", "AQAAAAIAAYagAAAAEC15xOowdoV7X4bRlgD3D8lH7V4av2z1k+ky5OhmIJK32V9AmsaffTwe1HsnhBefQQ==", "febccb30-bee7-49ad-af7b-c16b5de5a358" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3597713-2a02-4875-b4d2-0a12cd59cd43", "AQAAAAIAAYagAAAAEGBzeWAPChvaWxgKbkHfApTDRByM00L4GSVuRFv+uO9/+xzfgT61Yv60Dlsw43G/mQ==", "b99cbe23-0e66-4039-a21d-6194c8712e95" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f390ea2-f797-4560-acff-aad9b364bb10", "AQAAAAIAAYagAAAAEFNo08W31vw68pwIX82PeNbP4L2UrBhN3khMWMxDnJfLHQjThEw13OIWGF0B1f7MUA==", "8414c430-a991-4797-acf4-57944192feeb" });

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 1,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa   ");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 2,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 3,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 4,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 5,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 6,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 7,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 8,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 9,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 10,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 11,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 12,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 13,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 14,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 15,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 16,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 17,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 18,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 19,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 20,
                column: "NeighborhoodManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "ResidentialUnits",
                keyColumn: "Id",
                keyValue: 1,
                column: "UnitManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b975235-892c-42c9-a02c-9c8ac5e4f6c8", "AQAAAAIAAYagAAAAEB8StXqFvEBNo8psW4XuY1oU1E6kSr7MGUOtuRQOCNw4M1sRvdvPVc6IiEzhWewhzw==", "0214d37f-5408-4ee0-b09b-13da602c6d00" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66134406-1776-4d0a-9758-2a3e995cd9cd", "AQAAAAIAAYagAAAAEPaiEFEKUj6IWnEuj75CfkE7aX6SaNHmUdUDMZRUk0mQ1s5Cjb2B+56KPPeJpqn2rg==", "c64debf4-e694-488a-bdcc-fe4f47a23c63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c218d40-08e8-4cb9-9753-8ba8ae08db6b", "AQAAAAIAAYagAAAAEO0t8TEAnUVo6hQwofacwvFDjR3k1NmnXaDgLmu1WJR2Rq+MWdK45gWC0DnGMygysg==", "21f67657-3430-463e-8eda-e8bd9470cc54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1442da9c-ddd8-4716-a936-a8604f832d0d", "AQAAAAIAAYagAAAAEJqTmyx/hAAclS5eSH8EmcoeFlnj4v9WSjI8r8lFVnbIM8QD4iY2WPWjZrACEDORfw==", "1e5160bc-467d-46a1-8aae-69a2fdd456af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7423f4c-687b-4fe0-bbc8-9f85f2028fff", "AQAAAAIAAYagAAAAEGl+z75O3QaRaN1ixqN9hgjtiTKqcHdTD+owHVmEm/RZULzC2sILgrP+MKAlpspdvw==", "e368055f-1aee-4936-8cf8-3c7bbcdc29de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ad46d1e-22e0-4bfc-a0bc-615b20aed10d", "AQAAAAIAAYagAAAAELsIwVcUC2RE97y0aDpF6dOjVPT8ypmxOSJd7nKchIvE4bOu/55HIHHVlrrmPDDCdg==", "da0c9f82-031e-4e0e-9b2c-0d92bfcdd593" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86e12aec-d41b-4b18-b06a-14576028f95e", "AQAAAAIAAYagAAAAEE/Tn7JwNz6tn3wqQRP14YusT4uOCFb21L72GOJhRiJdFE0SVBM21sCDfQIUAz4G3A==", "174487f8-006c-466d-8b84-938472ec2bb3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70582f4f-eae3-4378-a7e0-0486998eb00a", "AQAAAAIAAYagAAAAECFC1wI2NnT2sp9s2wHwf3EhPtSm5fK7hecYFNjqTx9zbqHYRs0GtehHkji8ijCe/Q==", "732d6401-ab1f-4eac-a3bf-4f9bc15b6a22" });

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 1,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 2,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 3,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 4,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 5,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 6,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 7,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 8,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 9,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 10,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 11,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 12,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 13,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 14,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 15,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 16,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 17,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 18,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 19,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialNeighborhoods",
                keyColumn: "Id",
                keyValue: 20,
                column: "NeighborhoodManagerId",
                value: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.UpdateData(
                table: "ResidentialUnits",
                keyColumn: "Id",
                keyValue: 1,
                column: "UnitManagerId",
                value: "cccccccc-cccc-cccc-cccc-cccccccccccc");
        }
    }
}
