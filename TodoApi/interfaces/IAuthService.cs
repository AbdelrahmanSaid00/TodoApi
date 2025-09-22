using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace TodoApi.interfaces
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateTokenAsync(IdentityUser user);
    }
}
