using MedTools.Identity.API.Common.Enums;

namespace MedTools.Identity.API.Common.Models.Dtos;

public sealed record OrganizationDto(
    Guid Id,
    OrganizationType Type,
    string Code,
    string? Name);
