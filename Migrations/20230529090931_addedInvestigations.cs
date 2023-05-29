using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEMESYS.Migrations
{
    public partial class addedInvestigations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvestigationId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Investigations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investigations", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Assault" },
                    { 5, "Equipment" },
                    { 6, "No Category" }
                });

            migrationBuilder.InsertData(
                table: "Investigations",
                columns: new[] { "Id", "Content", "CreatedDate", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, "LESA on the Way...", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "19e2d6a8-f9aa-11ed-be56-0242ac120002" },
                    { 2, "Spotted all of them and called pest removal...", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "19e2d6a8-f9aa-11ed-be56-0242ac120002" },
                    { 3, "Should have been cleaned last week however...", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2f2e610c-f9ab-11ed-be56-0242ac120002" }
                });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "InvestigationId", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4877), 1, new DateTime(2023, 5, 29, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "InvestigationId", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4912), 2, new DateTime(2023, 5, 28, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4914) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "InvestigationId", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 29, 11, 9, 31, 593, DateTimeKind.Local).AddTicks(4970), 3, new DateTime(2023, 5, 27, 9, 9, 31, 593, DateTimeKind.Utc).AddTicks(4972) });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_InvestigationId",
                table: "Reports",
                column: "InvestigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Investigations_InvestigationId",
                table: "Reports",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Investigations_InvestigationId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "Investigations");

            migrationBuilder.DropIndex(
                name: "IX_Reports_InvestigationId",
                table: "Reports");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "InvestigationId",
                table: "Reports");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19e2d6a8-f9aa-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d9e4cd0-42bf-4ffa-ae55-db32bffc8013", "AQAAAAEAACcQAAAAEIUurcQg9ZxwLqspNjSjGnLzvT4SRUemP0A7fjGc52NypThLqvoFC8W7/ZadAjLCtg==", "ef4bc627-7eac-41bb-9738-db9dee36c8f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e0a2010-f9aa-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "839b2e20-cbd5-436b-bbc5-4e774c65b200", "AQAAAAEAACcQAAAAEGAHrQk7WDumY/BHU/7LQpr8pphFEj+BLQmhpvp2DGFU3/Hm4jwxaIDv06StusVi3A==", "48992d1c-dea7-4df1-a70d-802788c47869" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f2e610c-f9ab-11ed-be56-0242ac120002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba68365a-d278-41ed-a30c-06bfc96fa305", "AQAAAAEAACcQAAAAEAQhhDSpHDgmvprQKzP2Sj6CNPGgCGr3TtP3ATOyASNMruJhZ+jgMOqSjEFhFz+ePg==", "027dcb04-a3f9-448e-84b8-4d28e0f64a6f" });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 26, 18, 7, 27, 236, DateTimeKind.Local).AddTicks(896), new DateTime(2023, 5, 26, 16, 7, 27, 236, DateTimeKind.Utc).AddTicks(931) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 26, 18, 7, 27, 236, DateTimeKind.Local).AddTicks(933), new DateTime(2023, 5, 25, 16, 7, 27, 236, DateTimeKind.Utc).AddTicks(935) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 26, 18, 7, 27, 236, DateTimeKind.Local).AddTicks(936), new DateTime(2023, 5, 24, 16, 7, 27, 236, DateTimeKind.Utc).AddTicks(938) });
        }
    }
}
