namespace MedTools.Identity.API.Database.Entities;

public sealed class RoleDbEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<UserDbEntity> Users { get; set; } = [];
    public ICollection<PermissionDbEntity> Permissions { get; set; } = [];
}
