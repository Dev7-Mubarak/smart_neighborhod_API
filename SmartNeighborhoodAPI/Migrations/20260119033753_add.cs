using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "ConfilctCases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "ConfilctCases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "630a2666-9c20-45fd-84be-8563095d20b3", "AQAAAAIAAYagAAAAEPYwj3ubFjMTqbea91am1ulqcMO3dXgihrx6ZlOj5qr8Dk17xqfNbsLSGFF2kaXMcA==", "24924f21-5885-4c6d-acd1-ff5eaed0df3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a43fde4-f1d4-42e1-bdee-fb1c2e3d99f3", "AQAAAAIAAYagAAAAEO79bxK8HITbU2K2dt5wyRaAQ/8VX1lBbTt3xujAdmGhleIRf2P8AyLPpt9Zv/JWcw==", "89efec93-1748-4608-b375-116ef1b4e6ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02e7328f-023a-42e0-aaf8-a4cbb690069b", "AQAAAAIAAYagAAAAEN45HTZBbTnAqSDwjPPvHY8Se8kWsN27oEI8VsmicC0sbBSv2OI70ivXa6Fn2bwxgQ==", "6e7e007b-2706-4e19-bbb3-9f197ffde15f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64ebd0d1-a750-43ae-bd1b-3f4e95b1fb3f", "AQAAAAIAAYagAAAAEGKSXsOeR56GzuO2EWnxgkK0rOdAz3P6umfd2aeoHav4q8ekM4bsc7J7k5Iq+1Bk8w==", "8e518d08-ebe2-4207-a3eb-657592b88ef1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcff18ff-b7cc-4ca6-bcbc-9f06a0d35db6", "AQAAAAIAAYagAAAAELBgFYk4K4YgjVycfkfLxhualqub5gO/ePjNGXhuga6XIXrvGLK0dXSI4Yac1wsEKg==", "beac4d88-4471-4f85-af77-c7f497a56e4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "175653d7-4db1-4199-b34f-9c566429ecda", "AQAAAAIAAYagAAAAECTg9Y9tQetQKr4L6HI/XJ57kIZrATbF1RlA1BrzVS15Z+DW94UO/exrd5SH6eEPqw==", "ee6de8a2-cb51-46e2-8db5-58b7eaef8405" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50b033be-e9d1-4ca3-abc6-6e12f091ab57", "AQAAAAIAAYagAAAAEJWrYH86yC/psx8YlnEp7E3gD+7SBbHWXZu1ZGxV+fMFugt5XVCSNujM5rXfYL7IMQ==", "a0500c63-82d8-4989-8506-cf05b9b3a0bc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5626388-5584-4171-814e-e228ec41d5dc", "AQAAAAIAAYagAAAAELyF9jXVqhS9huWkTZY8zY9V1GhFQLPv3ETXQFptzD2IB9w1pitE5cGdU/h+ujpAWA==", "c062d7a5-da78-4c8f-b66e-62e1b8b9c82e" });
        }
    }
}
