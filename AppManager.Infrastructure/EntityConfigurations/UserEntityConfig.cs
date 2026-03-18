using AppManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.EntityConfigurations;

public class UserEntityConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.IdentityKey).IsRequired();
        builder.HasIndex(x => x.IdentityKey).IsUnique();

        builder.HasMany(x => x.Roles).WithMany(x => x.Users).UsingEntity<UserRole>();
        builder.HasMany(x => x.Permissions).WithMany(x => x.Users).UsingEntity<UserPermission>();
    }
}