using AuthService.Models.DTOs;
using AuthService.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        protected ResponseDto _responseDto;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
            _responseDto = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationRequestDto register)
        {
            var result = await _authService.Register(register);
            if (!string.IsNullOrEmpty(result))
            {
                _responseDto.Success = false;
                _responseDto.Message = result;
                return Ok(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto login)
        {
            var result = await _authService.Login(login);
            if (result.User == null)
            {
                _responseDto.Success = false;
                _responseDto.Message = "Login fail";
                return Ok(_responseDto);
            }

            _responseDto.Result = result;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegistrationRequestDto model)
        {
            var result = await _authService.AssignRole(model);
            if (!result)
            {
                _responseDto.Result = false;
                _responseDto.Message = "Assign role fail";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
