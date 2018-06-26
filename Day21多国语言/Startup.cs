using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day21多国语言
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //多国语言支持需要建档 
        // Controllers\HomeController.cs 要用的 en-GB 語系檔名稱：
        // Resources\Controllers\HomeController.en-GB.resx
        //   或 Resources\Controllers.HomeController.en-GB.resx
        // Views\Home\Index.cshtml 要用的 zh-TW 語系檔名稱：
        // Resources\Views\Home\Index.zh-TW.resx
        // 或 Resources\Views.Home.Index.zh-TW.resx

        //安装套件 Core 2.0以上已包含
        //dotnet add package Microsoft.AspNetCore.Localization
        // dotnet add package Microsoft.AspNetCore.Routing

        public void ConfigureServices(IServiceCollection services)
        {
            //注册服务 
            services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
            services.AddMvc()
                .AddViewLocalization()//View 中使用多国语言
                .AddDataAnnotationsLocalization(); //model中使用多国语言
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{culture=en-GB}/{controller=Home}/{action=Index}/{id?}"); //{culture=en-GB} 用来判定语言
            });
        }
    }
}
