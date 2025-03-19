namespace TransportApp.Server.JWT_Helper
{
    public interface IJwtService
    {
        string GenerateJwtToken(string username);

    }
}
