using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62676900-72A8-407E-8E9F-250E7AAC1113", null, "User", "USER" },
                    { "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "62676900-72A8-407E-8E9F-250E7AAC1113", 0, "a7faa588-6b87-412e-89b6-3b351848f4bd", "admin@bookstore.com", false, "Paolo", "Paci", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEK5li4V8legi4lFaPo1MY8B282L/xxFNFyCMARxRprc5LmxotkWTDVqciqFbKxYEMA==", null, false, "6882411d-0ce3-453f-af64-c49479228406", false, "admin@bookstore.com" },
                    { "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69", 0, "04fbe294-b25b-48b0-bb69-bfe38b5c4cbb", "user@bookstore.com", false, "User1", "User2", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAELQdK0XRmCfWC6tXC2ZPYquZjZLJP+2H1qOWFkowr0aHrE1nzA34/QMHqB3/pclvww==", null, false, "59584af9-40d1-495b-8255-57742cbae78e", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", "62676900-72A8-407E-8E9F-250E7AAC1113" },
                    { "62676900-72A8-407E-8E9F-250E7AAC1113", "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124", "62676900-72A8-407E-8E9F-250E7AAC1113" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "62676900-72A8-407E-8E9F-250E7AAC1113", "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62676900-72A8-407E-8E9F-250E7AAC1113");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F7F8FE30-6BAC-43DB-AFEE-0546FE8F5124");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62676900-72A8-407E-8E9F-250E7AAC1113");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8B22FC70-F6E8-48CD-8506-22F1DE1BCD69");
        }
    }
}
