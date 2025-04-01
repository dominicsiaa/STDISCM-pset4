using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Authentication.Model;

namespace Authentication.Infrastructure
{
    public class TokenProvider
    {
        private readonly IConfiguration configuration;

        public TokenProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Token GenerateToken(User user)
        {
            string secretKey = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                    ]),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var accessToken = new JsonWebTokenHandler().CreateToken(tokenDescriptor: tokenDescriptor);

            return new Token { AccessToken = accessToken };
        }
    }

    public class Token
    {
        public string AccessToken { get; set; }
    }
}
