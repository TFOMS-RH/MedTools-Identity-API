namespace MedTools.Identity.API.Common.Models.Requests;

public sealed record CreatePermissionRequest(
    string Code,
    string Name,
    string? Description);
