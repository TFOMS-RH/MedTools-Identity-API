using MedTools.Identity.API.Enums;

namespace MedTools.Identity.API.Models;

public sealed record CreateOrganizationRequest(
    string Code,
    OrganizationType Type,
    string? Name);
