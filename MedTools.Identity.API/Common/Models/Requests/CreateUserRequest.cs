namespace MedTools.Identity.API.Common.Models.Requests;

public sealed record CreateUserRequest(
    string UserName,
    string Password,
    string LastName,
    string FirstName,
    string? MiddleName,
    string? Email,
    string? ContactPhone,
    Guid OrganizationId,
    DateOnly? BirthDate);
