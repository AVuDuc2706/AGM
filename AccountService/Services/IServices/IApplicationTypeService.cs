using AccountService.Models;
using AccountService.Models.DTOs;

namespace AccountService.Services.IServices
{
    public interface IApplicationTypeService
    {
        IEnumerable<ApplicationType> Get();
        IEnumerable<ApplicationType> Search(string key);

        ApplicationType Get(Guid Id);
        ApplicationType Create(ApplicationTypeDto obj);
        ApplicationType Update(ApplicationTypeDto obj);
        ApplicationType Delete(Guid Id);
    }
}
