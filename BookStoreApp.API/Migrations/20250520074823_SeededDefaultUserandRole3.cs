using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRole3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", "62676900-72A8-407E-8E9F-250E7AAC1113" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", "29EC137D-88AF-4437-A719-28B3A65F287D" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29EC137D-88AF-4437-A719-28B3A65F287D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cda8d69-0549-4a54-9b8e-541da10a7989", "AQAAAAIAAYagAAAAEBWyzTyr07Hc4+jtx2oOpWM0cuw/b0ediChWLBrRKm8ICNjmIJqqf/PGPcsvTgVPfA==", "2f39bef3-57d2-4019-ab5c-87491cb578d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e9ea9a8-a470-4bec-9bb6-2e5c976b99dc", "AQAAAAIAAYagAAAAEBgXqH20pib327Ud/rGHqpkzS6sRo+kka+2SfHRabFDAjabGD22EbsZoQAejGH+7Kw==", "407ef7ce-f48d-494c-9bfd-8771267e68a6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", "29EC137D-88AF-4437-A719-28B3A65F287D" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", "62676900-72A8-407E-8E9F-250E7AAC1113" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29EC137D-88AF-4437-A719-28B3A65F287D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f88edf68-df23-47a2-aa50-0845d25ace6f", "AQAAAAIAAYagAAAAEP50ABKfMeO3tVOSu8/ePwp06Au1sjSYkOBjmWyaLDZuNrgFDH8XgrWpSVimcukmsQ==", "bb4234c0-a1d9-42b3-a4bb-2c60076a40bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94ed5dd5-ee95-4861-9a36-71a0bbefdf8e", "AQAAAAIAAYagAAAAEOZRb/WVKD0ooisl9sBt1rLkQpYpDaeEiF1GEIlYwAh7ro/Ty3wSgoqcMHKfPFBOcQ==", "0c12da38-9fb5-4e62-b45f-2eb05e9ffa2c" });
        }
    }
}
