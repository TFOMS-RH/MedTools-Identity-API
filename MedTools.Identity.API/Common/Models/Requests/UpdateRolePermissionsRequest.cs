namespace MedTools.Identity.API.Common.Models.Requests;

public sealed record UpdateRolePermissionsRequest(List<Guid> PermissionIds);
