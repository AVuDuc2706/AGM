using AuthService.Models;

namespace AuthService.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
