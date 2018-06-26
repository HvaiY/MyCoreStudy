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
            //mvc·�� (��Ը��ӵ�·�ɹ���)
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
                //��Asp.net mvc·�ɹ���д��һ�� ��ͬ���߱������Ч��
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                    ); 
#endif
            });
#endif

#if true
            //��·�ɹ���1
            //���ʵ�ַ�� http://localhost:49555/first ���ص���First.
            app.Map("/first", mapApp =>

            {
                mapApp.Run(async context =>
                {
                    await context.Response.WriteAsync("First.\r\n");
                });
            });
            //��·�ɹ���2  �����첽 �ȴ�����
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
