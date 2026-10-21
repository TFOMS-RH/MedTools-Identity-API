using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MedTools.Identity.API.Common.Options;
using MedTools.Identity.API.Database.Entities;

namespace MedTools.Identity.API.Services;

public sealed class JwtTokenGenerationService(
    IOptions<JwtOptions> options,
    SigningCredentials signingCredentials)
{
    public string GenerateAccessToken(
        UserDbEntity user,
        IReadOnlyCollection<string> permissions)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new("first_name", user.FirstName),
            new("last_name", user.LastName),
            new("organization_id", user.OrganizationId.ToString()),
            new("organization_code", user.Organization.Code),
            new("organization_type", user.Organization.Type.ToString())
        };

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            claims.Add(
                new(
                    JwtRegisteredClaimNames.Email,
                    user.Email));
        }

        claims.AddRange([.. permissions.Select(
            permission => new Claim(
                "permission",
                permission))]);

        var now = DateTime.UtcNow;

        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(options.Value.AccessTokenExpirationTimeInMinutes),
            signingCredentials: signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}
