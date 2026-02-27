using AccountService.DBContext;
using AccountService.Models;
using AccountService.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;
        private IMapper _mapper;
        private ResponseDto _responseDto;
        public AccountController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Account> result = _dbContext.Accounts.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<AccountDto>>(result);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{AccountId:Guid}")]
        public ResponseDto Get(Guid id)
        {
            try
            {
                Account result = _dbContext.Accounts.Where(s => s.AccountId == id).FirstOrDefault();
                _responseDto.Result = _mapper.Map<IEnumerable<AccountDto>>(result);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        public ResponseDto CreateAccountItem(Account account)
        {
            try
            {
                Account obj = _mapper.Map<Account>(account);
                _dbContext.Accounts.Add(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<AccountDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        public ResponseDto UpdateAccountItem(Account account)
        {
            try
            {
                Account obj = _mapper.Map<Account>(account);
                _dbContext.Accounts.Update(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<AccountDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete]
        public ResponseDto DeleteAccountItem(Guid id)
        {
            try
            {
                Account obj = _dbContext.Accounts.Where(s => s.AccountId == id).FirstOrDefault();
                _dbContext.Accounts.Remove(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<AccountDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
