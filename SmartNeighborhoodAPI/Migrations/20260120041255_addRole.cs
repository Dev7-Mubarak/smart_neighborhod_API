using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class addRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44444444-4444-4444-4444-444444444444", null, "ResidentialNeighborhoodManager", "RESIDENTIALNEIGHBORHOODMANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "060b3ad0-d96d-4870-afd2-24e0f35a7319", "AQAAAAIAAYagAAAAEEmpaikg+gsNCxhslU4uyg52wHrVAS0FsZPvbjAkcUXM9b33MfWXOWjC+5p7pNuoWQ==", "f0399d39-a5be-4b30-923d-6ced626184fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30059f73-3b8d-4ade-a9be-6b94dbb74a7a", "AQAAAAIAAYagAAAAEK4VtXkgiZELClLRERNwpECBtlZLNrCV2o8f/IeggmcW/M0RhWQxDql9gNJ9oGng2g==", "b41411ff-f03c-4104-a114-f14aa3d56dc7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13cbc3f9-57b5-457c-96c8-0aa978d521d0", "AQAAAAIAAYagAAAAEOWSwkRSIvCuGMAiydMi6cQqRuRWiJCScL1+0UVtaC87cgZ8tn0pmHDRXnucNB2dxg==", "afce7774-eae2-4343-bfa6-bc02b75c2b13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b983a2a-65e5-4941-b529-e2c03352cabc", "AQAAAAIAAYagAAAAEERbovDVWv+SdJr17oJiLSHN9lwbeFvXBkJkO6omChadmAIsY4xcicICoPDQNu7W1g==", "b32ce32e-55fc-4399-946a-7d96c16a59e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e22b832-f59c-4de8-a44c-23882065fb4f", "AQAAAAIAAYagAAAAEFoo07HEVuSOsHWl6OV2M/ylPXb1pqKpZY+LDPaQNDeWd/KH8SFdcxIx7q7Flkdpiw==", "78cf0d60-fd2a-415c-a564-307df82973c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb0bac59-0eb5-4698-9af4-83e75fc6d8fc", "AQAAAAIAAYagAAAAEB8q+g1u2YQbCI5Zuy1cZKDmyDFM8EyrdoI2Bh+HPWnQEl+JEbxVwX/Hc/rT5mrulg==", "fcea23e8-fe17-47d3-b65e-aa356cc3fa26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b57ba11e-1b30-4510-a4f3-f49b425d3843", "AQAAAAIAAYagAAAAEO0wVW4YZYApme3kyHk7cOPpP7tK/eeuju/n5GlGLasNqTjv91fk+hqIBPRJWAISzw==", "0807bb9e-c04d-4ebb-a174-8c1c22c65ba3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55b5631a-de10-400e-8a3a-9672d0489679", "AQAAAAIAAYagAAAAEKgp9ssp8plMf6nw7aCLCwwSMy9m5Qaeb7ejmRs+/uF1Mfjg6FHoT3FAmadd3AG3YQ==", "f510ff53-97ad-487f-9c82-0b5e6ededdf6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44444444-4444-4444-4444-444444444444");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54b0ab7e-7a55-4e93-b1d9-22d76f0260cd", "AQAAAAIAAYagAAAAEKbWOVGLvD88Fj0f2XZqsQoRDGlmrd/eKzN8KKmaz6DtKTuYlUJRTrREdE9WBxHHwg==", "39e79629-157a-428e-b3d3-d8926c7a9c54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "724d449b-10b0-4951-91d6-ae389bad772b", "AQAAAAIAAYagAAAAEMWIyMTc7JNFl6FHwmZD3GP/w4ZYMJfhhVYAg8Crcj7L0r5GnXAtftyUIVGLPfqymg==", "cf0af6b6-a7e2-462a-acd7-e19d3ea8b05d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e288a0e-9ee5-4399-833d-26d938f27b86", "AQAAAAIAAYagAAAAEJz4MWfXDjpVe8oU8ZwZRb0hCxVELuzZiFcqf9GwSz2Nf5bi/QKrtjqi2SYZG2zVLg==", "62fa69ed-6ffb-4663-9bbc-2a629b1fab0b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36528ce3-bcc6-4354-82a8-214866970d2c", "AQAAAAIAAYagAAAAEJ+gAFkPhTFJx/6k4RJlnau39cqraw/RAiuXpbaztOymXiKMF2XIqPn0BIIMiMARsQ==", "13bc30e9-fb18-4b40-9efc-8fe7b871d10c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bda5a852-e618-4009-9443-7bee357c389f", "AQAAAAIAAYagAAAAEGNRNYX0tgyq52T9rUlcu/2JTTXJPuHhQ9EwQNXtMbRy0tdZXGLgno4h9z2sHLPjeA==", "3211be04-dd83-426d-9c80-a33a43b6ffc7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c6d5755-8a72-41ac-87dd-7092d74bb4b6", "AQAAAAIAAYagAAAAEG5TC7F9+XYwoekd2JJdFOknD6S+icg+Eb6noW91/0czDoHuKN0oH8m87iVSTXyI+g==", "6fc9c08c-9f83-4638-b11a-03631b7e0147" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da594c77-6f42-4e93-a493-279d89a6eec8", "AQAAAAIAAYagAAAAEEC86RyWmP9PGRe0o3IZ+ZRK4cMn4x61nzwoiAu0v2D4RRnM363qQ9C3eDbs37HZfA==", "3b0eefb8-c75b-492c-b0c9-3335e1697dab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89fb5fc9-7b43-4848-991d-9d1959644118", "AQAAAAIAAYagAAAAENZarRP66xRgwcv+9+qCmk0+yeD/dbylxQWNhyfjZUsmwW6lBJd77h9VFfx2h8LscQ==", "091a587a-4b79-4168-b098-e61ea2dd1c2c" });
        }
    }
}
