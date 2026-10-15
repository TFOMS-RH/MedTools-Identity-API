namespace MedTools.Identity.API.Common.Models.Requests;

public sealed record UpdateUserRolesRequest(List<Guid> RoleIds);
