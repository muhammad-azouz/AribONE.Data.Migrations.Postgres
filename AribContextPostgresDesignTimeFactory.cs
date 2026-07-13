using System;
using AribONE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AribONE.Data.Migrations.Postgres;

/// <summary>
/// Design-time factory for the PostgreSQL migration set. <c>dotnet ef</c> scans
/// the startup assembly's own types for a factory, so this lives here (not in
/// AribONE.Data, which carries the SQL Server factory) and wires Npgsql with this
/// project as the migrations assembly. Generate/apply with this project as both
/// the migrations and startup project:
/// <code>dotnet ef migrations add &lt;Name&gt; \
///   --project AribONE.Data.Migrations.Postgres \
///   --startup-project AribONE.Data.Migrations.Postgres</code>
/// Set <c>ARIB_DESIGN_CS</c> to target a real database when applying migrations;
/// <c>add</c> works against the default placeholder.
/// </summary>
public sealed class AribContextPostgresDesignTimeFactory : IDesignTimeDbContextFactory<AribContext>
{
    public AribContext CreateDbContext(string[] args)
    {
        var cs = Environment.GetEnvironmentVariable("ARIB_DESIGN_CS")
                 ?? "Host=localhost;Database=arib_designtime;Username=postgres;Password=postgres";
        var opts = new DbContextOptionsBuilder<AribContext>()
            .UseNpgsql(cs, npg => npg.MigrationsAssembly("AribONE.Data.Migrations.Postgres"))
            .Options;
        return new AribContext(opts);
    }
}
