using AccountService.DBContext;
using AccountService.Models;
using AccountService.Models.DTOs;
using AccountService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Services
{
    public class AccService : IAccService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        public AccService(AppDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUser = currentUserService;
            _mapper = mapper;
        }
        public Account Get(Guid Id)
        {
            return _dbContext.Accounts.Include(s=>s.ApplicationType).Where(s => s.AccountId == Id).FirstOrDefault();
        }

        public IEnumerable<Account> GetAll()
        {
            return _dbContext.Accounts.Include(s=>s.ApplicationType).Where(s => s.UserId == _currentUser.UserId);
        }

        public IEnumerable<Account> Search(string key)
        {
            var result = _dbContext.Accounts.Include(s=>s.ApplicationType).Where(s => s.UserId == _currentUser.UserId);
            if (!string.IsNullOrEmpty(key) && result != null)
            {
                key = key.ToLower();
                result = result.Include(s => s.ApplicationType).Where(s => s.UserName.ToLower().Contains(key)
                                                                    || s.DisplayName.ToLower().Contains(key)
                                                                    || s.ApplicationType.Name.ToLower().Contains(key));
            }

            return result;
        }

        public Account Create(AccountDto account)
        {
            Account acc = _mapper.Map<Account>(account);
            acc.CreateDate = DateTime.Now;
            acc.UserId = _currentUser.UserId.Value;
            _dbContext.Accounts.Add(acc);
            _dbContext.SaveChanges();
            return acc;
        }

        public Account Update(AccountDto account)
        {
            Account acc = _dbContext.Accounts.Where(s => s.AccountId == account.AccountId).FirstOrDefault();
            if (acc != null) {
                _mapper.Map(account, acc);
                acc.UpdateDate = DateTime.Now;
                acc.UserId = _currentUser.UserId.Value;
                _dbContext.Accounts.Update(acc);
                _dbContext.SaveChanges();
            }
            return acc;
        }

        public Account Delete(Guid Id)
        {
            Account acc = _dbContext.Accounts.Where(s => s.AccountId == Id).FirstOrDefault();
            _dbContext.Accounts.Remove(acc);
            _dbContext.SaveChanges();
            return acc;
        }
    }
}
