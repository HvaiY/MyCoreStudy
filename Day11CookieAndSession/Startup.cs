using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day11CookieAndSession.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day11CookieAndSession
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
            // DI 容器中加入session服务 在加入之前我们将告知session服务  将session存放在内存中(每一个状态一块缓存）
            services.AddDistributedMemoryCache();
#if true
          
            services.AddSession(options =>
            {
                //下面可以没有  但是为了安全Session安全 
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; //在使用https请求情况下才能使用session  
                options.Cookie.Name = "mywebsite";//默认session名称改为 ******
                options.IdleTimeout = TimeSpan.FromMinutes(5);//设置时间间隔 不互动就过期session
            });
#endif
#if true
            #region 注入Session的封装
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//注入HttpContext上下文 在session封装中可以用到
            services.AddSingleton<ISessionWapper, SessionWapper>();//注入session的封装
            #endregion  
#endif
          //  services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Cookie 存储和删除方式 app.run() 
#if false
            app.Run(async (context) =>
             {
                 string message;
                 if (!context.Request.Cookies.TryGetValue("Sample", out message))
                 {
                     message = "Sample cookie没有数据 ，现在开始保存数据";
                 }
                 context.Response.Cookies.Append("Sample", "名字叫Sample的cookie保存了本文本");

                 //删除cookie 
                 //  context.Response.Cookies.Delete("Sample"); 

                 await context.Response.WriteAsync($"{message}"); //注意中文可能会出现乱码  具体看浏览器的编码格式  
             }); 
#endif
            #endregion
            app.UseSession();//如果不加到管道中将无法使用Session 
#if false
            #region Session 存储
            //SessionMiddleWare 加入 PipeLine  将session 中间件加入到管道中
            app.UseSession();
            app.Run(async context =>
            {
                context.Session.SetString("Samples", "这是一个Session");
                string message = context.Session.GetString("Samples");
                await context.Response.WriteAsync($"{message}");

            });

            #endregion  
#endif

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
