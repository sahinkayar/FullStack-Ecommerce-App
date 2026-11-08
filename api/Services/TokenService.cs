using api.Interfaces;
using api.models;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config=config;
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Id!),
                  new Claim(ClaimTypes.GivenName, user.UserName!),
                  new Claim(ClaimTypes.Email, user.Email!)
              
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
