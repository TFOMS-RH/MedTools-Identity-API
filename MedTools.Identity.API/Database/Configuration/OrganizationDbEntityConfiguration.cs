using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Database.Configuration;

public sealed class OrganizationDbEntityConfiguration : IEntityTypeConfiguration<OrganizationDbEntity>
{
    public void Configure(EntityTypeBuilder<OrganizationDbEntity> builder)
    {
        builder.ToTable("organizations").HasKey(entity => entity.Id);

        builder.Property(prop => prop.Id)
            .HasColumnName("id")
            .IsRequired(true)
            .ValueGeneratedOnAdd();

        builder.Property(prop => prop.Code)
            .HasColumnName("code")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.Property(prop => prop.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(prop => prop.Type)
            .HasColumnName("type")
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(organization => organization.Code).IsUnique();

    }
}
