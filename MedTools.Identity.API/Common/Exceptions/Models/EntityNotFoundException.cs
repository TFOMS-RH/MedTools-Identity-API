namespace MedTools.Identity.API.Common.Exceptions.Models;

public sealed class EntityNotFoundException(
    string message) : Exception(message);
