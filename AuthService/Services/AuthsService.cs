using AuthService.DBContext;
using AuthService.Models;
using AuthService.Models.DTOs;
using AuthService.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Services
{
    public class AuthsService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthsService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email,
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _appDbContext.ApplicationUsers.First(s => s.UserName == registrationRequestDto.Email);
                    UserDto userDto = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Name = user.Name,
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error";
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var userLogin = _appDbContext.ApplicationUsers.FirstOrDefault(s => s.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(userLogin, loginRequestDto.Password);
            if (userLogin == null || !isValid)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            var roles = await _userManager.GetRolesAsync(userLogin);
            var token = _jwtTokenGenerator.GenerateToken(userLogin, roles);

            UserDto user = new UserDto
            {
                Id = userLogin.Id,
                Email = userLogin.Email,
                PhoneNumber = userLogin.PhoneNumber,
                Name = userLogin.Name,
            };

            LoginResponseDto res = new LoginResponseDto
            {
                User = user,
                Token = token,
            };

            return res;
        }

        public async Task<LoginResponseDto> Logout()
        {
            UserDto user = null;
            LoginResponseDto res = new LoginResponseDto
            {
                User = user,
                Token = string.Empty
            };
            return res;
        }
        public async Task<bool> AssignRole(RegistrationRequestDto registrationRequestDto)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(s => s.Email.ToLower() == registrationRequestDto.Email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(registrationRequestDto.Role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(registrationRequestDto.Role)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, registrationRequestDto.Role);
                return true;
            }
            return false;
        }

    }
}
