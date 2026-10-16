namespace MedTools.Identity.API.Common.Options;

public sealed class JwtOptions
{
    public const string SectionName = "JwtOptions";

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationTimeInMinutes { get; set; }
    public string PrivateKeyPemFileName { get; set; } = string.Empty;
}
