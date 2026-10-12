using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Database.Configuration;

public sealed class RoleDbEntityConfiguration : IEntityTypeConfiguration<RoleDbEntity>
{
    public void Configure(EntityTypeBuilder<RoleDbEntity> builder)
    {
        builder.ToTable("roles").HasKey(entity => entity.Id);

        builder.Property(prop => prop.Id).HasColumnName("id").IsRequired(true);
        builder.Property(prop => prop.Name).HasColumnName("name");

        builder.HasMany(role => role.Permissions)
            .WithMany(permission => permission.Roles)
            .UsingEntity("role_permissions");
    }
}