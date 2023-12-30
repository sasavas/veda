using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Veda.Api.Configurations;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Api.Helpers;

public class JwtProvider(IOptions<JwtConfiguration> jwtConfig)
{
    public string Generate(Customer customer)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtConfig.Value.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer =  jwtConfig.Value.Issuer,
            Audience = jwtConfig.Value.Audience,
            Subject = new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.NameIdentifier, customer.TCKimlikNo.Value), // Replace with actual username
                new Claim(ClaimTypes.Email, customer.EmailAddress.Value),
                new Claim(ClaimTypes.Role, customer.ActiveMembership?.MembershipStatus?.Id.ToString() ?? "")
            }),
            Expires = DateTime.UtcNow.AddHours(2), // Token expiry
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature), 
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}