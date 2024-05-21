using DachaMentat.DTO;
using DachaMentat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DachaMentat.Controllers
{
    /// <summary>
    /// Sensor Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

        private readonly IUserAuthService _authService;

        public AdminController(ILogger<AdminController> logger, IUserAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet("/admin/check")]
        [Authorize]
        public async Task<BaseResponse> CheckAuth()
        {
            return new SimpleResponse("Auth success");
        }

        [HttpGet("/admin/cors")]
        public async Task<BaseResponse> CheckCors()
        {
            var a = Request.Headers;//.TryGetValue(, out var headerValue);
            return new SimpleResponse("Auth success");
        }

        [HttpPost("/admin/login")]
        public async Task<BaseResponse> Login([FromBody]UserAuthDto userAuth)
        {
            try
            {
                var token = _authService.CreateToken(userAuth.UserName, userAuth.Password);

                return new TokenResponse(token);
            }
            catch (Exception ex)
            {
                return new ErrorResponse("Auth data is incorrect");
            }
        }
    }
}