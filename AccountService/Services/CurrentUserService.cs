using AccountService.Services.IServices;
using System.Security.Claims;

namespace AccountService.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                return Guid.Parse(_contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        }

        string? ICurrentUserService.Email
        {
            get
            {
                return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            }
        }

       
    }
}
