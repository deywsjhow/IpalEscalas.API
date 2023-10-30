using Microsoft.Extensions.Configuration;
using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Common.Models.Utils;
using PortalIpalEscalas.Infraestructure.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

namespace PortalIpalEscalas.Domain
{
    public class Token : IToken
    {
        private readonly IConfiguration configuration;

        public Token(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddToken(AuthResponse authResponse, string user, string pass)
        {
            DateTime dateNow = DateTime.Now;

            var expiresToken = configuration.GetSection("TokenExpires");
            var jwtKey = configuration.GetSection("JwtKey");

            var sessionExpires = dateNow.AddMinutes(Convert.ToDouble(expiresToken.Value));

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityClaim = new SecurityClaim
            {
                authResponse = authResponse,
                user = user,
                pass = Crypto.EncryptStringAES(pass, "8D@#$!%DSAf7Km")
            };

            var serialize = Serializer.Serialize(securityClaim);

            var claimsAES = Crypto.EncryptStringAES(serialize, "9LK339M6-04JD-981T-951G572108A4");

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, claimsAES )
            });

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtKey.Value));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = tokenHandler.CreateJwtSecurityToken(
                       issuer: "https://apiescalasipal-client.cpm.br",
                       audience: "https://apiescalasipal-client.con.br",
                       subject: claimsIdentity,
                       notBefore: dateNow,
                       expires: sessionExpires,
                       signingCredentials: signingCredentials);

            var tokenResponse = tokenHandler.WriteToken(token);

            return tokenResponse;
            
        }
    }
}
