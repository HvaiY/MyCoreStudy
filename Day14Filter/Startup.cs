using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day14Filter.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day14Filter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /*
         * 关于过滤器 与ASP.NET 一样  同样保留五种过滤器（使用：定义一个类实现对应的接口即可）
         * Filters 分别为：Authorization Filter、Resource Filter、 Action Filter、Exception Filter、Result Filter
         * Authorization Filter
         *Authorization 是五種 Filter 中優先序最高的，通常用於驗證 Requert 合不合法，不合法後面就直接跳過。
         *Resource Filter
         *Resource 是第二優先，會在 Authorization 之後，Model Binding 之前執行。通常會是需要對 Model 加工處裡才用。
         *Action Filter
         *最容易使用的 Filter，封包進出都會經過它，使用上沒捨麼需要特別注意的。跟 Resource Filter 很類似，但並不會經過 Model Binding。
         *Exception Filter
         *異常處理的 Exception。
         *Result Filter
         *當 Action 完成後，最終會經過的 Filter。
         */
        public void ConfigureServices(IServiceCollection services)
        {
            //过滤分为全局注册和区域注册  
            //建议 AuthorizationFilter 是区域注册 可以在某个action /control上加上特性 AuthorizationFilter(该类已实现特性(Attribute)可以直接使用)
            //资源 异常 结果可以放在 这里进行全局注册
#if false
            services.AddMvc(
           config =>
       {
           config.Filters.Add(new ResourceFilter());
           config.Filters.Add(new ResultFilter());
           config.Filters.Add(new ExceptionFilter());
                //。。。增加更多过滤
            }

      ); 
#endif
            services.AddMvc(config =>
            {
                config.Filters.Add(new ResultFilter());
                config.Filters.Add(new ExceptionFilter());
                config.Filters.Add(new ResourceFilter());
                config.Filters.Add(new TestActionFilter() {Name = "Global", Order = 1});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
