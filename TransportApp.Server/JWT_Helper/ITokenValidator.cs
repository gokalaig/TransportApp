using TransportApp.Server.DataModels;

namespace TransportApp.Server.JWT_Helper
{
    public interface ITokenValidator
    {
        CustomResponse<T> ValidateToken<T>(string authToken);
    }
}
