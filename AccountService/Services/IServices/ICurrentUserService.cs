namespace AccountService.Services.IServices
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
    }
}
