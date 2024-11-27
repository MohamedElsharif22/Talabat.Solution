using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contracts;

namespace Talabat.Services.Auth_Service
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            // Private Claims
            var privateClaims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.DisplayName),
                new(ClaimTypes.Email, user.Email),
            };

            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                foreach (var role in userRoles)
                    privateClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // set Security Key
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty));

            // Generate JWT token
            var token = new JwtSecurityToken
                (
                    // Registered Claims
                    audience: _configuration["JWT:ValidAudience"],
                    issuer: _configuration["JWT:ValidIssuer"],
                    expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
                    // private claims
                    claims: privateClaims,
                    // security Algorithem 
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
