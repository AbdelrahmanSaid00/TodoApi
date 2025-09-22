using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApi.interfaces;

namespace TodoApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration configuration;
        public AuthService (UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<JwtSecurityToken> CreateTokenAsync(IdentityUser user)
        {
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            Claims.Add(new Claim(ClaimTypes.Email, user.Email));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityKey securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));

            SigningCredentials creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return token;
        }
    }
}
