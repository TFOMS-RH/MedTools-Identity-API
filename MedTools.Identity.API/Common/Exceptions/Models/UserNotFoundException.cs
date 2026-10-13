namespace MedTools.Identity.API.Common.Exceptions.Models;

public sealed class UserNotFoundException(
    string message) : Exception(message);
