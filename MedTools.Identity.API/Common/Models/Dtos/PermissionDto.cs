using Microsoft.Identity.Client;

namespace MedTools.Identity.API.Common.Models.Dtos;

public sealed record PermissionDto(
    Guid Id,
    string Code,
    string Name,
    string? Description);
