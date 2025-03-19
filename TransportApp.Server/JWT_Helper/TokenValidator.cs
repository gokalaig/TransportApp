using TransportApp.Server.DataModels;

namespace TransportApp.Server.JWT_Helper
{
    public class TokenValidator : ITokenValidator
    {
        private readonly IJwtHelper _jwtHelper;

        public TokenValidator(IJwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        public CustomResponse<T> ValidateToken<T>(string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                return new CustomResponse<T>
                {
                    status = 401,
                    message = "Missing authorization token",
                    data = default
                };
            }

            if (authToken.StartsWith("Bearer "))
            {
                authToken = authToken.Substring(7);
            }

            bool isExpired;
            var principal = _jwtHelper.ValidateToken(authToken, out isExpired);

            if (principal == null)
            {
                return new CustomResponse<T>
                {
                    status = 401,
                    message = isExpired ? "Token has expired" : "Invalid token",
                    data = default
                };
            }

            return null; // Token is valid
        }
    }
}
