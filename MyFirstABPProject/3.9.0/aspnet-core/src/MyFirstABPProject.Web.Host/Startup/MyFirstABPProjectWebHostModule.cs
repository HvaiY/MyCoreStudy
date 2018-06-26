using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyFirstABPProject.Configuration;

namespace MyFirstABPProject.Web.Host.Startup
{
    [DependsOn(
       typeof(MyFirstABPProjectWebCoreModule))]
    public class MyFirstABPProjectWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MyFirstABPProjectWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyFirstABPProjectWebHostModule).GetAssembly());
        }
    }
}
