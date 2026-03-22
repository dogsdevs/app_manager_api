using AppManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.EntityConfigurations;

public class TenantEntityConfig : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.HasIndex(x => x.NormalizedNameAndSlug);
        
        builder.HasMany(x => x.Users).WithOne(x => x.Tenant).HasForeignKey(x => x.TenantId);
        builder.HasMany(x => x.Roles).WithOne(x => x.Tenant).HasForeignKey(x => x.TenantId);
        builder.HasMany(x => x.AuditLogs).WithOne(x => x.Tenant).HasForeignKey(x => x.TenantId);
    }
}