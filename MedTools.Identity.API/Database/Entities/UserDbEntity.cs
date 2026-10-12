namespace MedTools.Identity.API.Database.Entities;

public sealed class UserDbEntity
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public bool IsActive { get; set; }

    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Email { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }
    public string? ContactPhone { get; set; }

    public OrganizationDbEntity Organization { get; set; } = null!;
    public ICollection<RoleDbEntity> Roles { get; set; } = [];
}
