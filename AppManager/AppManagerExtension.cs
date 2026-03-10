using AppManager.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppManager;

public static class AppManagerExtension
{
    private const string PostgreSql = "postgresql";
    private const string SqlServer = "sqlserver";

    public static void AddAppManager(this WebApplicationBuilder builder)
    {
        builder.Services.AddAppManager();

        var databaseProvider = builder.Configuration["DB_PROVIDER"] ?? "sqlserver";

        if (string.IsNullOrEmpty(databaseProvider))
            throw new NotSupportedException("Database provider es requerido.");

        builder.Services
            .AddDbContext<AppManagerDbContext>(options => _ = databaseProvider switch
            {
                PostgreSql => options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DataContextPostgreSql"),
                    x => x.MigrationsAssembly("AppManager.Migrations.Postgresql")),

                SqlServer => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DataContextSqlServer"),
                    x => x.MigrationsAssembly("AppManager.Migrations.SqlServer")),

                _ => throw new NotSupportedException($"Database provider sin configuración: {databaseProvider}")
            })
            .AddOptions();
    }

    public static void ApplyDbMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppManagerDbContext>();
        context.Database.Migrate();
    }
}