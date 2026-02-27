using AuthService.Models.DTOs;

namespace AuthService.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(RegistrationRequestDto registrationRequestDto);
    }
}
