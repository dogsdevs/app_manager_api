using AppManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppManager.Infrastructure.EntityConfigurations;

public class FeatureEntityConfig : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.ToTable("Features");

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Key).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ParentId);
        builder.Property(x => x.ShowInMenu).HasDefaultValue(false);
        builder.Property(x => x.MenuLabel).HasMaxLength(100);
        builder.Property(x => x.MenuPath).HasMaxLength(150);
        builder.Property(x => x.MenuIcon).HasMaxLength(50);
        builder.Property(x => x.MenuOrder).IsRequired();

        builder.HasOne(x => x.Parent);
        builder.HasMany(x => x.Permissions);
    }
}