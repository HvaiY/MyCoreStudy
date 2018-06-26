using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyFirstABPProject.MultiTenancy.Dto;

namespace MyFirstABPProject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
