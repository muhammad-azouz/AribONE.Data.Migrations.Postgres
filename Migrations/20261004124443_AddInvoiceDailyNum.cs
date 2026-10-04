using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Data.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceDailyNum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyNum",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DailyNumDate",
                table: "Invoices",
                type: "date",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BranchId_DailyNumDate_DailyNum",
                table: "Invoices",
                columns: new[] { "BranchId", "DailyNumDate", "DailyNum" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_BranchId_DailyNumDate_DailyNum",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DailyNum",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DailyNumDate",
                table: "Invoices");
        }
    }
}
