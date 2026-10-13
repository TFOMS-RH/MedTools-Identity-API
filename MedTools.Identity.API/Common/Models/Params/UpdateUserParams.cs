namespace MedTools.Identity.API.Common.Models.Params;

public sealed record UpdateUserParams(
    PatchField<string> FirstName,
    PatchField<string> LastName,
    PatchField<string> MiddleName,
    PatchField<string> ContactPhone,
    PatchField<DateOnly?> BirthDate,
    PatchField<string> Email);
