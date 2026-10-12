using MedTools.Identity.API.Enums;

namespace MedTools.Identity.API.Database.Entities;

public sealed class OrganizationDbEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Name { get; set; }
    public OrganizationType Type { get; set; }

    public ICollection<UserDbEntity> Employees { get; set; } = [];
}
