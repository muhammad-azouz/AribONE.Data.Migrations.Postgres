using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Data.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentAuditEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFulfillments_InvoiceLines_OrderLineId",
                table: "ReservationFulfillments");

            migrationBuilder.RenameColumn(
                name: "OrderLineId",
                table: "ReservationFulfillments",
                newName: "ReservationLineId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationFulfillments_OrderLineId",
                table: "ReservationFulfillments",
                newName: "IX_ReservationFulfillments_ReservationLineId");

            migrationBuilder.CreateTable(
                name: "DocumentAuditEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<int>(type: "integer", nullable: false),
                    Field = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    OldValue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NewValue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Reason = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Subject = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAuditEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAuditEntries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentAuditEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuditEntries_BranchId",
                table: "DocumentAuditEntries",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuditEntries_DocumentId_CreatedAt",
                table: "DocumentAuditEntries",
                columns: new[] { "DocumentId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuditEntries_ShiftId",
                table: "DocumentAuditEntries",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAuditEntries_UserId",
                table: "DocumentAuditEntries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFulfillments_InvoiceLines_ReservationLineId",
                table: "ReservationFulfillments",
                column: "ReservationLineId",
                principalTable: "InvoiceLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationFulfillments_InvoiceLines_ReservationLineId",
                table: "ReservationFulfillments");

            migrationBuilder.DropTable(
                name: "DocumentAuditEntries");

            migrationBuilder.RenameColumn(
                name: "ReservationLineId",
                table: "ReservationFulfillments",
                newName: "OrderLineId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationFulfillments_ReservationLineId",
                table: "ReservationFulfillments",
                newName: "IX_ReservationFulfillments_OrderLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationFulfillments_InvoiceLines_OrderLineId",
                table: "ReservationFulfillments",
                column: "OrderLineId",
                principalTable: "InvoiceLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
