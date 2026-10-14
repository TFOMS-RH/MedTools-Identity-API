namespace MedTools.Identity.API.Database.Entities;

public sealed class PermissionDbEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<RoleDbEntity> Roles { get; set; } = [];
}
