using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IndentityLogic.Services
{
    public class TokenService
    {
        private readonly IOptions<Security> _config;

        public TokenService(IOptions<Security> config)
        {
            _config = config;
        }

        public string CreateToken(ApplicationUser user)
        {
            CreateClaims(user, out var claims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.SymmetricSecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            CreateTokenDescriptor(claims, credentials, out var tokenDescriptor);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void CreateClaims(ApplicationUser user, out List<Claim> claims)
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
        }

        public void CreateTokenDescriptor(List<Claim> claims, SigningCredentials credentials, out SecurityTokenDescriptor tokenDescriptor)
        {
            tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };
        }
    }
}
