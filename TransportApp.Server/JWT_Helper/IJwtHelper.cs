using System.Security.Claims;

namespace TransportApp.Server.JWT_Helper
{
    public interface IJwtHelper
    {
        ClaimsPrincipal ValidateToken(string token, out bool isExpired);

    }
}
