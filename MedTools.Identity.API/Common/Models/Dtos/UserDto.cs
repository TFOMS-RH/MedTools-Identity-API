namespace MedTools.Identity.API.Common.Models.Dtos;

public sealed record UserDto(
    Guid OrganizationId,
    string UserName,
    string FirstName,
    string LastName,
    string? MiddleName,
    string? ContactPhone,
    string? Email,
    DateOnly? BirthDate);
