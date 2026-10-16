namespace MedTools.Identity.API.Common.Models.Results;

public sealed record AuthenticationResult(
    string AccessToken,
    string TokenType);
