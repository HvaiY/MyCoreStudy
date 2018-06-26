using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day20快取机制和RedisSession
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
            // services.AddMemoryCache();//使用本机快取得方式就是注入IMemoryCache //所有请求通用的 
            //分散式的快取  每一个请求状态使用一个独立的内存 比如session //这种放手导致了数据存储是字节数组或者字符串，存储对象需要做处理  home/ index
            // services.AddDistributedMemoryCache();
            //使用RedisCache （分布式）使用和上面的一样
            //安装套件  dotnet add package Microsoft.Extensions.Caching.Redis.Core
            services.AddDistributedRedisCache(options =>
            {
                // Redis Server 的 IP 跟 Port
                //已安装 redis 先启动 redis :cmd>redis-server.exe
                options.Configuration = "127.0.0.1:6379";
            });
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
