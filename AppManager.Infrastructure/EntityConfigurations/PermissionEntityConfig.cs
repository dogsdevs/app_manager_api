using AppManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.EntityConfigurations;

public class PermissionEntityConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Action).HasMaxLength(100);

        builder.HasOne(x => x.Feature)
            .WithMany(x => x.Permissions)
            .HasForeignKey(f => f.FeatureId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}