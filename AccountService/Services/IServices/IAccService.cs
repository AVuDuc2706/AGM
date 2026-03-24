using AccountService.Models;
using AccountService.Models.DTOs;

namespace AccountService.Services.IServices
{
    public interface IAccService
    {
        IEnumerable<Account> GetAll();
        IEnumerable<Account> Search(string key);
        Account Get(Guid Id);
        Account Create(AccountDto account);
        Account Update(AccountDto account);
        Account Delete(Guid Id);
    }
}
