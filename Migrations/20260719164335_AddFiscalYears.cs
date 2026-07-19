using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AribONE.Data.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddFiscalYears : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiscalYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ClosedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClosingRegNum = table.Column<Guid>(type: "uuid", nullable: true),
                    NetProfit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYears", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-7000-a000-000000000050"), "يمكنه إعداد وإعادة تشكيل تسلسل السنوات المالية", "ادارة السنوات المالية" },
                    { new Guid("00000003-0000-7000-a000-000000000051"), "يمكنه إغلاق سنة مالية", "اغلاق السنة المالية" },
                    { new Guid("00000003-0000-7000-a000-000000000052"), "يمكنه إعادة فتح آخر سنة مالية مغلقة", "اعادة فتح السنة المالية" }
                });

            migrationBuilder.InsertData(
                table: "PostingAccounts",
                columns: new[] { "Role", "AccountId", "LabelAr", "LabelEn" },
                values: new object[] { "RetainedEarnings", new Guid("00000001-0000-7000-a000-000000000129"), "أرباح (خسائر) مرحلة", "Retained Earnings" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-7000-a000-000000000080"), new Guid("00000003-0000-7000-a000-000000000050"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000081"), new Guid("00000003-0000-7000-a000-000000000051"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000082"), new Guid("00000003-0000-7000-a000-000000000052"), new Guid("00000002-0000-7000-a000-000000000001") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_StartDate_EndDate",
                table: "FiscalYears",
                columns: new[] { "StartDate", "EndDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_Status",
                table: "FiscalYears",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiscalYears");

            migrationBuilder.DeleteData(
                table: "PostingAccounts",
                keyColumn: "Role",
                keyValue: "RetainedEarnings");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000080"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000081"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000082"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000052"));
        }
    }
}
