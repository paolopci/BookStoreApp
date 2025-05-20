using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRole2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62676900-72A8-407E-8E9F-250E7AAC1113");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94ed5dd5-ee95-4861-9a36-71a0bbefdf8e", "AQAAAAIAAYagAAAAEOZRb/WVKD0ooisl9sBt1rLkQpYpDaeEiF1GEIlYwAh7ro/Ty3wSgoqcMHKfPFBOcQ==", "0c12da38-9fb5-4e62-b45f-2eb05e9ffa2c" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "29EC137D-88AF-4437-A719-28B3A65F287D", 0, "f88edf68-df23-47a2-aa50-0845d25ace6f", "admin@bookstore.com", false, "Paolo", "Paci", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEP50ABKfMeO3tVOSu8/ePwp06Au1sjSYkOBjmWyaLDZuNrgFDH8XgrWpSVimcukmsQ==", null, false, "bb4234c0-a1d9-42b3-a4bb-2c60076a40bf", false, "admin@bookstore.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29EC137D-88AF-4437-A719-28B3A65F287D");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04fbe294-b25b-48b0-bb69-bfe38b5c4cbb", "AQAAAAIAAYagAAAAELQdK0XRmCfWC6tXC2ZPYquZjZLJP+2H1qOWFkowr0aHrE1nzA34/QMHqB3/pclvww==", "59584af9-40d1-495b-8255-57742cbae78e" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "62676900-72A8-407E-8E9F-250E7AAC1113", 0, "a7faa588-6b87-412e-89b6-3b351848f4bd", "admin@bookstore.com", false, "Paolo", "Paci", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEK5li4V8legi4lFaPo1MY8B282L/xxFNFyCMARxRprc5LmxotkWTDVqciqFbKxYEMA==", null, false, "6882411d-0ce3-453f-af64-c49479228406", false, "admin@bookstore.com" });
        }
    }
}
