using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEMESYS.Migrations
{
    public partial class addedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Investigations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19e2d6a8-f9aa-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46d8c3de-ab03-48a5-ad0c-f42b8774e15c", "AQAAAAEAACcQAAAAEAkt8DnC1TAYH+RiCQ4qGTIbMnrInL7Ei5Z7Yhqc2XJ20ByaE5L/bUuwBEuqDZzxyw==", "d6f56b5a-770a-4a0d-8f01-909200928c97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e0a2010-f9aa-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85d10382-8377-4c43-b3e4-f66dac5f0e9e", "AQAAAAEAACcQAAAAEM8vPBZSaETaNvoirJbrTYOZm62rsbFBP18/uEL6TApqT1qZZbp8LZU8CY16UlS0KQ==", "a5f14ed2-1825-404a-a318-2b4e977f9891" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f2e610c-f9ab-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54bb7dfd-ca56-43e0-8672-c3cd72a3df9a", "AQAAAAEAACcQAAAAEK31DhfZiruNEI0YZYbq9OVIuQuEag75GvJ2BfGttEEkzFj9tqFrO2j3iOicjkq2og==", "6f7839a2-056f-440d-ad38-1c5ae9d0c020" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Closed" },
                    { 4, "Not Solved" }
                });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StatusId", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 18, 47, 12, 250, DateTimeKind.Local).AddTicks(7818), 1, new DateTime(2023, 5, 29, 16, 47, 12, 250, DateTimeKind.Utc).AddTicks(7852) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "StatusId", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 18, 47, 12, 250, DateTimeKind.Local).AddTicks(7854), 2, new DateTime(2023, 5, 28, 16, 47, 12, 250, DateTimeKind.Utc).AddTicks(7856) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "StatusId", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 18, 47, 12, 250, DateTimeKind.Local).AddTicks(7859), 3, new DateTime(2023, 5, 27, 16, 47, 12, 250, DateTimeKind.Utc).AddTicks(7861) });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_StatusId",
                table: "Reports",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Investigations_UserId",
                table: "Investigations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Investigations_AspNetUsers_UserId",
                table: "Investigations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Statuses_StatusId",
                table: "Reports",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investigations_AspNetUsers_UserId",
                table: "Investigations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Statuses_StatusId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Reports_StatusId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Investigations_UserId",
                table: "Investigations");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Investigations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19e2d6a8-f9aa-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "747e8b40-3bfc-4b96-b4e8-0cb2b05e5713", "AQAAAAEAACcQAAAAEPv0g6ZRLIbVwaXKsQBiP/FbFbwNb9IpLScXYUeNqj/fkp3kr2vFmPRbuLAgNTg5UA==", "e530e6ee-8dd8-44cd-8f14-4b5eb61f2eb8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e0a2010-f9aa-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b607fbf8-7668-42b1-9eaf-fd88d46d5f4a", "AQAAAAEAACcQAAAAEDb1z4oIWk0Ge+pilvGQWCdQz3D2c4nfh+wfpoVePbj+tB22z76u00NR3CXyc+Ysqw==", "0f74ea13-2a61-438d-85bc-5034d2827b68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f2e610c-f9ab-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e7ad8d6-ab17-45fb-92c5-a1a1dde34464", "AQAAAAEAACcQAAAAEN5cv6wfFohfLwhUzcTzqv4CUQNZtjIMpZrODMy6cgMuyOpxyZl+hxErjqOnICO4aA==", "a7a327ce-7319-4d6c-b281-d11c7e86e815" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4877), new DateTime(2023, 5, 29, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4912), new DateTime(2023, 5, 28, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4914) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4970), new DateTime(2023, 5, 27, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4972) });
        }
    }
}
