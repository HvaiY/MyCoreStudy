using System.Threading.Tasks;
using Abp.Application.Services;
using MyFirstABPProject.Sessions.Dto;

namespace MyFirstABPProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
