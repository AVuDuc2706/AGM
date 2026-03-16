using AccountService.DBContext;
using AccountService.Models;
using AccountService.Models.DTOs;
using AccountService.Services.IServices;
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

        private IApplicationTypeService _applicationTypeService;
        private ICurrentUserService _currentUser;
        public ApplicationTypeController(AppDbContext dbContext, IMapper mapper, ICurrentUserService currentUser, IApplicationTypeService applicationTypeService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _currentUser = currentUser;
            _applicationTypeService = applicationTypeService;
        }

        [Authorize]
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                //IEnumerable<ApplicationType> result = _dbContext.ApplicationTypes.Where(s => s.UserId == _currentUser.UserId);
                IEnumerable<ApplicationType> result = _applicationTypeService.Get();
                _responseDto.Result = _mapper.Map<IEnumerable<ApplicationTypeDto>>(result.ToList());
            }
            catch (Exception ex)
            {
                _responseDto.Success = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{ApplicationId:Guid}")]
        public ResponseDto Get(Guid id)
        {
            try
            {
                ApplicationType result = _applicationTypeService.Get(id);
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

                //IEnumerable<ApplicationType> result = _dbContext.ApplicationTypes;

                //if (!string.IsNullOrEmpty(keyWord))
                //{
                //    result = result.Where(s => s.UserId == _currentUser.UserId &&
                //                               (s.Name.ToLower().Contains(keyWord.ToLower()) || s.Description.ToLower().Contains(keyWord.ToLower())));
                //}

                IEnumerable<ApplicationType> result = _applicationTypeService.Search(keyWord);

                //result = result.OrderBy(s => s.UpdateDate)
                //                                    .Skip((pageIndex - 1) * pageSize)
                //                                    .Take(pageSize);

                _responseDto.Result = _mapper.Map<IEnumerable<ApplicationTypeDto>>(result.ToList());
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
        public ResponseDto CreateApplicationType(ApplicationTypeDto applicationType)
        {
            try
            {
                if (_currentUser.UserId.HasValue)
                {
                    //ApplicationType obj = _mapper.Map<ApplicationType>(applicationType);
                    //obj.CreateDate = DateTime.Now;
                    //obj.UserId = _currentUser.UserId.Value;
                    //_dbContext.ApplicationTypes.Add(obj);
                    //_dbContext.SaveChanges();

                    ApplicationType obj = _applicationTypeService.Create(applicationType);
                    _responseDto.Result = _mapper.Map<ApplicationTypeDto>(obj);
                }
                else
                {
                    _responseDto.Success = false;
                    _responseDto.Message = "User null";
                }

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
        public ResponseDto UpdateApplicationType(ApplicationTypeDto applicationType)
        {
            try
            {
                //ApplicationType obj = _dbContext.ApplicationTypes.Where(s => s.ApplicationId == applicationType.ApplicationId).FirstOrDefault();
                //_mapper.Map(applicationType, obj);
                //obj.UpdateDate = DateTime.Now;
                //obj.UserId = _currentUser.UserId.Value;
                //_dbContext.ApplicationTypes.Update(obj);
                //_dbContext.SaveChanges();

                ApplicationType obj = _applicationTypeService.Update(applicationType);
                _responseDto.Result = _mapper.Map<ApplicationTypeDto>(obj);
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
        public ResponseDto DeleteApplicationType(Guid id)
        {
            try
            {
                //ApplicationType obj = _dbContext.ApplicationTypes.Where(s => s.ApplicationId == id && s.UserId == _currentUser.UserId).FirstOrDefault();
                //_dbContext.ApplicationTypes.Remove(obj);
                //_dbContext.SaveChanges();

                ApplicationType obj = _applicationTypeService.Delete(id);
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
