using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MyFirstABPProject.Configuration.Dto;

namespace MyFirstABPProject.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MyFirstABPProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
