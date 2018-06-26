using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day07Route
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
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            //app.UseMvc();
#if true
            //mvc路由 (相对复杂的路由规则)
            app.UseMvc(routes => 
            {
                routes.MapRoute(
                    name:"about",
                    template:"about",
                    defaults:new {controller="Home",action="About"}
                    );
                routes.MapRoute(
                     name:"default",
                    template:"{controller=Home}/{action=Index}/{id?}"
                    );

#if false
                //与Asp.net mvc路由规则写法一样 ，同样具备上面的效果
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                    ); 
#endif
            });
#endif

#if true
            //简单路由规则1
            //访问地址： http://localhost:49555/first 返回的是First.
            app.Map("/first", mapApp =>

            {
                mapApp.Run(async context =>
                {
                    await context.Response.WriteAsync("First.\r\n");
                });
            });
            //简单路由规则2  都是异步 等待方法
            app.Map("/Second", mapApp =>
            {
                mapApp.Run(async context =>
                {
                    await context.Response.WriteAsync("Second.\r\n");
                });
            }); 
#endif
        }
    }
}
