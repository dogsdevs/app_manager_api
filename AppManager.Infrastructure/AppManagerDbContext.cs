using System.Reflection;
using AppManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure;

public class AppManagerDbContext : DbContext
{
    public AppManagerDbContext(DbContextOptions<AppManagerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("admin");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        
        var provider = Database.ProviderName; 

        if (provider == "Microsoft.EntityFrameworkCore.SqlServer")
        {
            modelBuilder.Entity<Tenant>()
                .Property(x => x.NormalizedNameAndSlug)
                .HasComputedColumnSql(
                    "UPPER(COALESCE([Name], '') + ' ' + COALESCE([Slug], ''))",
                    stored: true);
        }
        else if (provider == "Npgsql.EntityFrameworkCore.PostgreSQL")
        {
            modelBuilder.Entity<Tenant>()
                .Property(x => x.NormalizedNameAndSlug)
                .HasComputedColumnSql(
                    "upper(coalesce(\"Name\", '') || ' ' || coalesce(\"Slug\", ''))",
                    stored: true);
        }

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
}