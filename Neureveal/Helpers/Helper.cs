using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Neureveal.Helpers
{
    public static class Helper
    {
        public static IConfiguration? Configuration { get; set; }
        public static SymmetricSecurityKey getSecurityKey()
        {
            var secretKey = Configuration.GetValue<string>("SecretKey");
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(secretKeyBytes);
            return securityKey;
        }
        public static string generateToken(IList<Claim> userClaims)
        {
            var securityKey = getSecurityKey();

            var methodToGenerateToken = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: methodToGenerateToken,
                issuer: "bassantServer",
                audience: "apiOne");

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(Jwt);

            return token;
        }
    }
}
