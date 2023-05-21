using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEMESYS.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorAlias",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "f6e0c528-da7a-48a9-b7f1-7c36809df8a2", "Reporter", "REP" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "45b7dd73-2964-426e-9f1e-b4c8462fe3f7", "Investigator", "INV" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AuthorAlias", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "55c3f668-f76e-11ed-b67e-0242ac120002", 0, "InvestigatorUser", "e531073c-f926-4d41-a1fd-36e456222076", "investigator@mail.com", true, false, null, "INVESTIGATOR@MAIL.COM", "INVESTIGATOR@MAIL.COM ", "AQAAAAEAACcQAAAAEJsyXxPIp6mb73X0gN12klVLyJDlYFa4eR4UVjR09Z9LfFtN7biTqwUzmjBiY0jGbw==", "", false, "84e78336-72b7-4d59-a0ec-d891f32b3582", false, "investigator@mail.com" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate", "UserId" },
                values: new object[] { new DateTime(2023, 5, 21, 2, 38, 4, 166, DateTimeKind.Local).AddTicks(9692), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "55c3f668-f76e-11ed-b67e-0242ac120002" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate", "UserId" },
                values: new object[] { new DateTime(2023, 5, 21, 2, 38, 4, 166, DateTimeKind.Local).AddTicks(9723), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "55c3f668-f76e-11ed-b67e-0242ac120002" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate", "UserId" },
                values: new object[] { new DateTime(2023, 5, 21, 2, 38, 4, 166, DateTimeKind.Local).AddTicks(9726), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "55c3f668-f76e-11ed-b67e-0242ac120002" });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_UserId",
                table: "Reports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_UserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId",
                table: "Reports");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "55c3f668-f76e-11ed-b67e-0242ac120002");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "AuthorAlias",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 19, 22, 27, 53, 959, DateTimeKind.Local).AddTicks(2529), new DateTime(2023, 5, 19, 20, 27, 53, 959, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 19, 22, 27, 53, 959, DateTimeKind.Local).AddTicks(2561), new DateTime(2023, 5, 18, 20, 27, 53, 959, DateTimeKind.Utc).AddTicks(2564) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 19, 22, 27, 53, 959, DateTimeKind.Local).AddTicks(2565), new DateTime(2023, 5, 17, 20, 27, 53, 959, DateTimeKind.Utc).AddTicks(2567) });
        }
    }
}
