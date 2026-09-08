using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AribONE.Data.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerAccessPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-7000-a000-000000000058"), "يمكنه رؤية العملاء في قوائم اختيار العملاء والموردين", "التعامل مع العملاء" },
                    { new Guid("00000003-0000-7000-a000-000000000059"), "يمكنه رؤية الموردين في قوائم اختيار العملاء والموردين", "التعامل مع الموردين" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-7000-a000-000000000086"), new Guid("00000003-0000-7000-a000-000000000058"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000087"), new Guid("00000003-0000-7000-a000-000000000059"), new Guid("00000002-0000-7000-a000-000000000001") }
                });

            // Mirrors the SQL Server migration of the same name in AribONE.Data — see its
            // comment for the full rationale (Tier A ServerWins collision risk, the
            // FiscalYear precedent, why HasData alone can't reach custom roles). This must
            // produce byte-identical RolePermission.Id values to the SQL Server side for
            // the same (RoleId, PermissionId) pair: Postgres central DBs receive existing
            // tenants' already-synced custom Roles, so both engines independently compute
            // the same grant row instead of racing to invent two different ones.
            //
            // Id = md5(RoleId text || PermissionId text), reformatted as a uuid. Postgres's
            // uuid::text is already lower-case and md5() already returns lower-case hex,
            // but both are lower-cased explicitly anyway to stay byte-identical with the
            // SQL Server side regardless of either engine's default casing.
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
                WHERE p."Id" IN ('00000003-0000-7000-a000-000000000058', '00000003-0000-7000-a000-000000000059')
                  AND NOT EXISTS (
                      SELECT 1 FROM "RolePermissions" rp
                      WHERE rp."RoleId" = r."Id" AND rp."PermissionId" = p."Id"
                  );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Undo the backfill first (before the Permission rows it references are
            // deleted below). Only deletes rows whose Id still matches the deterministic
            // formula recomputed from that row's own (RoleId, PermissionId) — a role
            // whose permission was later toggled off/on by hand through the UI gets a
            // fresh GuidV7 id there and is correctly left alone.
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
                    WHERE "PermissionId" IN ('00000003-0000-7000-a000-000000000058', '00000003-0000-7000-a000-000000000059')
                )
                DELETE FROM "RolePermissions" rp
                USING hashed
                WHERE rp."Id" = hashed.rp_id AND rp."Id" = hashed.expected_id;
                """);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000086"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000087"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000059"));
        }
    }
}
