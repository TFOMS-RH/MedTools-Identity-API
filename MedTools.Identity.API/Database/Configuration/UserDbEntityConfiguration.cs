using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Database.Configuration;

public sealed class UserDbEntityConfiguration : IEntityTypeConfiguration<UserDbEntity>
{
    public void Configure(EntityTypeBuilder<UserDbEntity> builder)
    {
        builder.ToTable("medtools_users").HasKey(entity => entity.Id);

        builder.Property(prop => prop.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(prop => prop.OrganizationId)
            .HasColumnName("organization_id")
            .IsRequired(true);

        builder.Property(prop => prop.IsActive)
            .HasColumnName("is_active")
            .IsRequired(true)
            .HasDefaultValue(true);

        builder.Property(prop => prop.UserName)
            .HasColumnName("username")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.Property(prop => prop.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.Property(prop => prop.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(prop => prop.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.Property(prop => prop.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(255)
            .IsRequired(true);

        builder.Property(prop => prop.MiddleName)
            .HasColumnName("middle_name")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(prop => prop.BirthDate)
            .HasColumnName("birth_date")
            .IsRequired(false);

        builder.Property(prop => prop.ContactPhone)
            .HasColumnName("contact_phone")
            .HasMaxLength(255)
            .IsRequired(false);


        builder.HasOne(user => user.Organization)
            .WithMany(organization => organization.Employees)
            .HasForeignKey(user => user.OrganizationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity("user_roles");

        builder.HasIndex(user => user.UserName).IsUnique();
    }
}
