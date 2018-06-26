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
            // DI �����м���session���� �ڼ���֮ǰ���ǽ���֪session����  ��session������ڴ���(ÿһ��״̬һ�黺�棩
            services.AddDistributedMemoryCache();
#if true
          
            services.AddSession(options =>
            {
                //�������û��  ����Ϊ�˰�ȫSession��ȫ 
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; //��ʹ��https��������²���ʹ��session  
                options.Cookie.Name = "mywebsite";//Ĭ��session���Ƹ�Ϊ ******
                options.IdleTimeout = TimeSpan.FromMinutes(5);//����ʱ���� �������͹���session
            });
#endif
#if true
            #region ע��Session�ķ�װ
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//ע��HttpContext������ ��session��װ�п����õ�
            services.AddSingleton<ISessionWapper, SessionWapper>();//ע��session�ķ�װ
            #endregion  
#endif
          //  services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Cookie �洢��ɾ����ʽ app.run() 
#if false
            app.Run(async (context) =>
             {
                 string message;
                 if (!context.Request.Cookies.TryGetValue("Sample", out message))
                 {
                     message = "Sample cookieû������ �����ڿ�ʼ��������";
                 }
                 context.Response.Cookies.Append("Sample", "���ֽ�Sample��cookie�����˱��ı�");

                 //ɾ��cookie 
                 //  context.Response.Cookies.Delete("Sample"); 

                 await context.Response.WriteAsync($"{message}"); //ע�����Ŀ��ܻ��������  ���忴������ı����ʽ  
             }); 
#endif
            #endregion
            app.UseSession();//������ӵ��ܵ��н��޷�ʹ��Session 
#if false
            #region Session �洢
            //SessionMiddleWare ���� PipeLine  ��session �м�����뵽�ܵ���
            app.UseSession();
            app.Run(async context =>
            {
                context.Session.SetString("Samples", "����һ��Session");
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
