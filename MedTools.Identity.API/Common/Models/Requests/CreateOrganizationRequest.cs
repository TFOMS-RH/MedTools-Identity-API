using MedTools.Identity.API.Common.Enums;

namespace MedTools.Identity.API.Common.Models.Requests;

public sealed record CreateOrganizationRequest(
    string Code,
    OrganizationType Type,
    string? Name);
