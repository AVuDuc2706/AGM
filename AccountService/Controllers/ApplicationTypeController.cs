using AccountService.DBContext;
using AccountService.Models;
using AccountService.Models.DTOs;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTypeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private IMapper _mapper;    
        private ResponseDto _responseDto;
        public ApplicationTypeController(AppDbContext dbContext, IMapper mapper) { 
            _dbContext = dbContext;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [Authorize]
        [HttpGet]
        public ResponseDto Get() {
            try
            {
                IEnumerable<ApplicationType> result = _dbContext.ApplicationTypes;
                _responseDto.Result = _mapper.Map<IEnumerable<ApplicationTypeDto>>(result.ToList());
            }
            catch(Exception ex) {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{ApplicationId:Guid}")]
        public ResponseDto Get(Guid id) {
            try
            {
                ApplicationType result = _dbContext.ApplicationTypes.Where(s => s.ApplicationId == id).FirstOrDefault();
                _responseDto.Result = _mapper.Map<IEnumerable<ApplicationTypeDto>>(result);
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
        public ResponseDto Filter(string? keyWord, int pageIndex = 1, int pageSize = 5)
        {
            try
            {
                if (pageIndex < 1)
                    pageIndex = 1;

                if (pageSize < 5)
                    pageSize = 5;

                IEnumerable<ApplicationType> result = _dbContext.ApplicationTypes;

                if (!string.IsNullOrEmpty(keyWord))
                {
                    result = result.Where(s => s.Name.ToLower().Contains(keyWord.ToLower()) || s.Description.ToLower().Contains(keyWord.ToLower()));
                }

                result = result.OrderBy(s => s.UpdateDate)
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize);

                _responseDto.Result = _mapper.Map<IEnumerable<ApplicationTypeDto>>(result.ToList());
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        public ResponseDto CreateApplicationType(ApplicationType applicationType) {
            try
            {
                ApplicationType obj = _mapper.Map<ApplicationType>(applicationType);
                obj.CreateDate = DateTime.Now;
                _dbContext.ApplicationTypes.Add(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<ApplicationTypeDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        public ResponseDto UpdateApplicationType(ApplicationType applicationType) {
            try
            {
                ApplicationType obj = _mapper.Map<ApplicationType>(applicationType);
                obj.UpdateDate = DateTime.Now;
                _dbContext.ApplicationTypes.Update(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<ApplicationTypeDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public ResponseDto DeleteApplicationType(Guid id) {
            try
            {
                ApplicationType obj = _dbContext.ApplicationTypes.Where(s=>s.ApplicationId == id).FirstOrDefault();
                _dbContext.ApplicationTypes.Remove(obj);
                _dbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<ApplicationTypeDto>(obj);
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
