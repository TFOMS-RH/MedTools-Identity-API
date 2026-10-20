namespace MedTools.Identity.API.Common.Models.Requests;

public sealed record LoginRequest(
    string UserName,
    string Password);
