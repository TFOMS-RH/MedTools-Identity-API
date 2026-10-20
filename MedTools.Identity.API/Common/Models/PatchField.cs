namespace MedTools.Identity.API.Common.Models;

public readonly record struct PatchField<T>(
    bool IsSpecified,
    T? Value);
