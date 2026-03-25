using AccountService.DBContext;
using AccountService.Models;
using AccountService.Models.DTOs;
using AccountService.Services.IServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;
        private IMapper _mapper;
        private readonly IAccService _accService;
        private ResponseDto _responseDto;
        public AccountController(IMapper mapper,IAccService accService, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _accService = accService;
            _responseDto = new ResponseDto();
        }

        [Authorize]
        [HttpGet]
        public ResponseDto Get(int? pageSize, int? pageIndex)
        {
            try
            {
                int totalItems = 0;
                IEnumerable<Account> result = _accService.GetAll();

                if(pageSize != null && pageIndex != null && result != null)
                {
                    totalItems = result.Count();
                    result = result.OrderByDescending(s => s.CreateDate)
                                    .Skip((pageIndex.Value - 1) * pageSize.Value)
                                    .Take(pageSize.Value);
                }
                
                //_responseDto.Result = _mapper.Map<IEnumerable<AccountDto>>(result.ToList());
                _responseDto.Result = new
                {
                    Data = _mapper.Map<IEnumerable<AccountDto>>(result.ToList()),
                    Total = totalItems
                };
                //_responseDto.Result = _accService.GetAll();
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [Authorize]
        [HttpGet]
        [Route("{AccountId:Guid}")]
        public ResponseDto Get(Guid id)
        {
            try
            {
                Account result = _accService.Get(id);
                _responseDto.Result = _mapper.Map<AccountDto>(result);
                //_responseDto.Result = _accService.Get(id);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<ResponseDto> Search(string? keyword, int pageIndex = 1, int pageSize = 5) 
        {
            try
            {
                int totalItems = 0;
                var sw = Stopwatch.StartNew();
                IEnumerable<Account> result = _accService.Search(keyword);

                if (result.Count() > 0)
                {
                    totalItems = result.Count();
                    result = result.OrderByDescending(s => s.CreateDate)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize);

                    //sw.Stop();
                    Console.WriteLine($"DB time AccController: {sw.ElapsedMilliseconds}");
                }
                sw.Restart();

                //_responseDto.Result = _mapper.Map<IEnumerable<AccountDto>>(result.ToList());
                _responseDto.Result = new
                {
                    Data = _mapper.Map<IEnumerable<AccountDto>>(result.ToList()),
                    Total = totalItems
                };
                Console.WriteLine($"Map: {sw.ElapsedMilliseconds}");

            }
            catch (Exception ex) 
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [Authorize]
        [HttpPost]
        public ResponseDto CreateAccountItem(AccountDto account)
        {
            try
            {
                //Account obj = _mapper.Map<Account>(account);
                //_dbContext.Accounts.Add(obj);
                //_dbContext.SaveChanges();
                Account obj = _accService.Create(account);
                _responseDto.Result = _mapper.Map<AccountDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [Authorize]
        [HttpPut]
        public ResponseDto UpdateAccountItem(AccountDto account)
        {
            try
            {
                //Account obj = _mapper.Map<Account>(account);
                //_dbContext.Accounts.Update(obj);
                //_dbContext.SaveChanges();
                Account obj = _accService.Update(account);
                _responseDto.Result = _mapper.Map<AccountDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [Authorize]
        [HttpDelete]
        [Route("{id:Guid}")]
        public ResponseDto DeleteAccountItem(Guid id)
        {
            try
            {
                //Account obj = _dbContext.Accounts.Where(s => s.AccountId == id).FirstOrDefault();
                //_dbContext.Accounts.Remove(obj);
                //_dbContext.SaveChanges();
                Account obj = _accService.Delete(id);
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
