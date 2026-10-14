namespace MedTools.Identity.API.Common.Models.Requests;

public sealed class UpdateUserStatusRequest
{
    public required bool IsActive { get; init; }
};
