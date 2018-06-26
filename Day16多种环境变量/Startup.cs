using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day16多种环境变量
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
#if true
            //环境变量名字可以自己来指点任一字符串，系统默认提供了三种
            //Development：开发环境
            //Staging：暫存環境(測試環境)
            //Production：正式環境
            // 1.环境变量名可以在系统环境中设置 Key : ASPNETCORE_ENVIRONMENT  Value: Development或者Production 这随意 
            //windows中可以用指令的方式也可以 ： SETX ASPNETCORE_ENVIRONMENT "Production" /M
            //2.iis中设定
            /*
             * <aspNetCore processPath="dotnet" arguments=".\MyWebsite.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout">
                 <environmentVariables>
                 <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
                 </environmentVariables>
              </aspNetCore>
           </system.webServer>
             */
            //3.VS 2017中项目属性-环境变量连可以设置
            //4.Properties\launchSettings.json 
            /*
             *{
                 // ...
                 "profiles": {
                     // ...
                     "MyWebsite": {
                         "commandName": "Project",
                         "launchBrowser": true,
                         "environmentVariables": {
                             "ASPNETCORE_ENVIRONMENT": "Local"
                         },
                         "applicationUrl": "http://localhost:5000/"
                     }
                          */
            //这些名字可以用来选择要使用的配置文件 这就重要了
            //默认命名假设为 Settings.json ,那么对于的几个环境建议为 Setting.Development.json/Setting.Staging.json/Setting.Production.json
            // 如下设定了为开发环境，默认是开发环境
            env.EnvironmentName = EnvironmentName.Development;
#endif

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
#if false
            app.Run(async (context) =>
             {
                 await context.Response.WriteAsync(
                     $"EnvironmentName: {env.EnvironmentName}\r\n"
                     + $"IsDevelopment: {env.IsDevelopment()}"
                 );
             }); 
#endif
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
