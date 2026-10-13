namespace MedTools.Identity.API.Common.Exceptions.Models;

public sealed class DuplicateEntityException(
    string message, 
    Exception exception) : Exception(message, exception);
