using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using TransportApp.Server.DataModels;
using TransportApp.Server.Dtos;
using TransportApp.Server.JWT_Helper;

namespace TransportApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "testuser", Password = "password" }
        };

        [HttpPost("login")]
        [AllowAnonymous]
        public CustomResponse<ResponseData> Login([FromBody] UserDto userDto)
        {
            var user = _users.FirstOrDefault(u => u.Username == userDto.Username && u.Password == userDto.Password);

            if (user == null)
            {
                return new CustomResponse<ResponseData>
                {
                    status = 400,
                    message = "Invalid username or password",
                    errorDetails = "Authentication failed",
                    data = null
                };
            }

            var token = _jwtService.GenerateJwtToken(user.Username);

            var responseData = new ResponseData
            {
                token = token
            };

            return new CustomResponse<ResponseData>
            {
                status = 200,
                message = "Login successful",
                errorDetails = null,
                data = responseData
            };
        }

    }
}
