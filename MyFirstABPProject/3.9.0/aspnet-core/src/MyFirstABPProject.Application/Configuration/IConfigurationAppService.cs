using System.Threading.Tasks;
using MyFirstABPProject.Configuration.Dto;

namespace MyFirstABPProject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
