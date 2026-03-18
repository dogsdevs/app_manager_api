using AppManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.EntityConfigurations;

public class UserPermissionEntityConfig : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("UserPermissions");
        
        builder.HasKey(x => new { x.UserId, x.PermissionId });
        
        builder.HasOne(x => x.Permission)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}