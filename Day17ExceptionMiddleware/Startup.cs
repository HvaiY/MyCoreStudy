using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Day17ExceptionMiddleware.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day17ExceptionMiddleware
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
            //一般全局的异常过滤就在这里注册(Exception Filter) 而ExceptionFilter 所获取的异常通常是Action中的 ，那么 异常自己身和其它异常处理就困难。
            //而Exception MiddleWare 是在Filter的外层，如果再把Exception MiddleWare注册在所有的MiddleWare的最外层（代码的第一行）,就可以成为全局的ExceptionHandler
            //这里使用自定义的异常中间件(类)
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //异常中间件注册在所有的Middleware 外围 既可以捕获所有异常
            //需要看到效果 后面的异常使用注释掉 否者覆盖掉这里的异常处理返回的数据
            app.UseMiddleware<ExceptionMiddleware>();


#if true
            //webapi 异常处理
            app.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                ExceptionHandler = async context =>
                {
                    bool isApi = Regex.IsMatch(context.Request.Path.Value, "^/api/", RegexOptions.IgnoreCase);
                    if (isApi)
                    {
                        context.Response.ContentType = "application/json";
                        var json = @"{ ""Message"": ""Internal Server Error"" }";
                        await context.Response.WriteAsync(json);
                        return;
                    }
                    context.Response.Redirect("/WebAPIError");
                }
            });
#endif

#if true

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                //使用开发人员的异常页面
             //  app.UseDeveloperExceptionPage();
            }
            else
            {
                //Core 的异常中间件 给到普通用户查看 MVC的异常助手
               // app.UseExceptionHandler("/Home/Error");
            } 
#endif

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
