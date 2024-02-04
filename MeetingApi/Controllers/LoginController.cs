using MeetingApi.Business.Authentication;
using MeetingApi.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var result = _authenticationService.Login(username, password);
            if (result != null)
            {
                var token = _authenticationService.GenerateToken(result);
                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpPost]

        public IActionResult Register(User user)
        {
            var result = _authenticationService.Register(user);
            if (result != null)
            {
                var token = _authenticationService.GenerateToken(result);
                return Ok(token);
            }
            return Unauthorized();
        }

    }
}
