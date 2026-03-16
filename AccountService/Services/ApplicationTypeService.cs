using AccountService.DBContext;
using AccountService.Models;
using AccountService.Models.DTOs;
using AccountService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace AccountService.Services
{
    public class ApplicationTypeService : IApplicationTypeService
    {
        private readonly AppDbContext _dbContext;
        private IMapper _mapper;
        private ICurrentUserService _currentUser;
        private readonly IMemoryCache _memoryCache;

        public ApplicationTypeService(AppDbContext dbContext, IMapper mapper, ICurrentUserService currentUser, IMemoryCache memoryCache) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUser = currentUser;
            _memoryCache = memoryCache;
        }
        public IEnumerable<ApplicationType> Get()
        {
            if(!_memoryCache.TryGetValue("application_type", out IEnumerable<ApplicationType> data))
            {
                data = _dbContext.ApplicationTypes.AsNoTracking().Where(s => s.UserId == _currentUser.UserId).ToList();

                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set("application_type", data);
            }
            return data;
        }

        public ApplicationType Get(Guid Id)
        {
            return _dbContext.ApplicationTypes.Where(s => s.ApplicationId == Id).FirstOrDefault();
        }

        public IEnumerable<ApplicationType> Search(string key)
        {
            var data = _memoryCache.Get<IEnumerable<ApplicationType>>("application_type");
            if (data == null)
            {
                data = Get();
            }

            //IEnumerable<ApplicationType> result = _dbContext.ApplicationTypes;
            if (!string.IsNullOrEmpty(key))
            {
                data = data.Where(s => s.UserId == _currentUser.UserId &&
                                 (s.Name.ToLower().Contains(key.ToLower()) || s.Description.ToLower().Contains(key.ToLower())));
            }
            return data;
        }

        public ApplicationType Create(ApplicationTypeDto obj)
        {
            ApplicationType result = _mapper.Map<ApplicationType>(obj);
            result.CreateDate = DateTime.Now;
            result.UserId = _currentUser.UserId.Value;
            _dbContext.ApplicationTypes.Add(result);
            _dbContext.SaveChanges();

            _memoryCache.Remove("application_type");
            return result;
        }

        public ApplicationType Update(ApplicationTypeDto obj)
        {
            ApplicationType result = _dbContext.ApplicationTypes.Where(s => s.ApplicationId == obj.ApplicationId).FirstOrDefault();
            _mapper.Map(obj, result);
            result.UpdateDate = DateTime.Now;
            result.UserId = _currentUser.UserId.Value;
            _dbContext.ApplicationTypes.Update(result);
            _dbContext.SaveChanges();

            _memoryCache.Remove("application_type");

            return result;
        }

        public ApplicationType Delete(Guid Id)
        {
            ApplicationType obj = _dbContext.ApplicationTypes.Where(s => s.ApplicationId == Id && s.UserId == _currentUser.UserId).FirstOrDefault();
            _dbContext.ApplicationTypes.Remove(obj);
            _dbContext.SaveChanges();

            _memoryCache.Remove("application_type");
            return obj;
        }
        
    }
}
