using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day26跨域请求
{
    /// <summary>
    /// 关于跨域请求问题
    ///  需要注册服务  先添加： dotnet add package Microsoft.AspNetCore.Cors
    /// ASP.NET Core 中使用 CORS 只要在 Startup.ConfigureServices 呼叫 AddCors，就能註冊 CORS 的 Policy 規則
    /// </summary>
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
            //注册服务 跨域请求限制
            services.AddCors(options =>
            {
              options.AddPolicy("MyCorsPolicy", policy =>
              {
                  policy.WithOrigins("http://212.37.89.51:8080", "http://localhost:3000")
                      .AllowAnyHeader() //WithHeaders 做限制 ，允许多个头用‘，’隔开
                      .AllowAnyMethod()//WithMethods限制请求方法 同上
                      .AllowCredentials();
              });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //全局套用一个跨域限制
            app.UseCors("MyCorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
