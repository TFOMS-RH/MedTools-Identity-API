using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Database.Configuration;

public sealed class PermissionDbEntityConfiguration : IEntityTypeConfiguration<PermissionDbEntity>
{
    public void Configure(EntityTypeBuilder<PermissionDbEntity> builder)
    {
        builder.ToTable("permissions").HasKey(entity => entity.Id);

        builder.Property(prop => prop.Id).HasColumnName("id").IsRequired(true);

        builder.Property(prop => prop.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.Property(prop => prop.Code)
            .HasColumnName("code")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.HasIndex(permission => permission.Code).IsUnique();
    }
}