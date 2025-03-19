using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TransportApp.Server.JWT_Helper
{
    public class JwtHelper : IJwtHelper
    {
        private readonly string _secretKey;

        public JwtHelper(IConfiguration configuration) // ✅ Inject ONLY IConfiguration
        {
            _secretKey = configuration["JwtSettings:SecretKey"];
        }

        public ClaimsPrincipal ValidateToken(string token, out bool isExpired)
        {
            isExpired = false;

            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                isExpired = true;
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
