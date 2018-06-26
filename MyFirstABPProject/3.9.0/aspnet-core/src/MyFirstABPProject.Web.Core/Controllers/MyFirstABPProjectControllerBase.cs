using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MyFirstABPProject.Controllers
{
    public abstract class MyFirstABPProjectControllerBase: AbpController
    {
        protected MyFirstABPProjectControllerBase()
        {
            LocalizationSourceName = MyFirstABPProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
