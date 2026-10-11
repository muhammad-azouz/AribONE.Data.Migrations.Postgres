using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Data.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUseAppMenuPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("00000003-0000-7000-a000-000000000061"), "يمكنه رؤية قائمة التطبيق في أعلى الشاشة (الإعدادات، قاعدة البيانات، بيانات الشركة والفرع، الاستيراد، حول)", "استخدام قائمة التطبيق" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { new Guid("00000004-0000-7000-a000-000000000089"), new Guid("00000003-0000-7000-a000-000000000061"), new Guid("00000002-0000-7000-a000-000000000001") });

            // Mirrors the SQL Server migration of the same name (byte-identical RolePermission ids).
            migrationBuilder.Sql("""
                INSERT INTO "RolePermissions" ("Id", "RoleId", "PermissionId")
                SELECT
                    (
                        substring(hashed.hex, 1, 8) || '-' ||
                        substring(hashed.hex, 9, 4) || '-' ||
                        substring(hashed.hex, 13, 4) || '-' ||
                        substring(hashed.hex, 17, 4) || '-' ||
                        substring(hashed.hex, 21, 12)
                    )::uuid,
                    r."Id",
                    p."Id"
                FROM "Roles" r
                CROSS JOIN "Permissions" p
                CROSS JOIN LATERAL (
                    SELECT md5(lower(r."Id"::text) || lower(p."Id"::text)) AS hex
                ) AS hashed
                WHERE p."Id" = '00000003-0000-7000-a000-000000000061'
                  AND NOT EXISTS (
                      SELECT 1 FROM "RolePermissions" rp
                      WHERE rp."RoleId" = r."Id" AND rp."PermissionId" = p."Id"
                  );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                WITH hashed AS (
                    SELECT
                        "Id" AS rp_id,
                        (
                            substring(h, 1, 8) || '-' ||
                            substring(h, 9, 4) || '-' ||
                            substring(h, 13, 4) || '-' ||
                            substring(h, 17, 4) || '-' ||
                            substring(h, 21, 12)
                        )::uuid AS expected_id
                    FROM "RolePermissions",
                    LATERAL (SELECT md5(lower("RoleId"::text) || lower("PermissionId"::text)) AS h) hh
                    WHERE "PermissionId" = '00000003-0000-7000-a000-000000000061'
                )
                DELETE FROM "RolePermissions" rp
                USING hashed
                WHERE rp."Id" = hashed.rp_id AND rp."Id" = hashed.expected_id;
                """);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000089"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000061"));
        }
    }
}
