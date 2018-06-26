using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day27Response快取
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
#if false
            #region 客户端快取 (缓存）
		    //ResponseCache 除了直接在action上加上特性的方式还可以在configservices中注册 如下
            //action上调用方式  [ResponseCache(CacheProfileName = "MyCachepoliy")]
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("MyCachepoliy", new CacheProfile()
                {
                    Duration = 60,
                    Location = ResponseCacheLocation.Client
                });
            });  
            #endregion
#endif
            #region 服务端缓存
            /*回傳的狀態必須是 HTTP Status 200 (OK)。
              Request 的 HTTP Methods 必須是 GET 或 HEAD。
              不能有其他的 Middleware 在加工 ResponseCachingMiddleware 之前異動 Response。
              HTTP Header 不能用 Authorization。
              HTTP Header 的 Cache-Control 值必須是 public。
              (F5 刷新頁面不會帶 Cache-Control，所以使用 Server 快取條件不成立)
              HTTP Header 不能用 Set-Cookie。
              HTTP Header 的 Vary 值不能為 *。
              不能使用 IHttpSendFileFeature。
              不能設定 no-store。
              單一回傳快取不能大於 MaximumBodySize。
              總體快取不能大於 SizeLimit。*/
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = false;//URL 是否區分大小寫為不同的 Response 快取。  (預設為 true)
                options.MaximumBodySize = 1024;//單個 Response 快取的大小限制(單位是 Bytes)。 (預設為 64 MB)
                options.SizeLimit = 1024 * 1024 * 100;//Response 快取的總大小限制(單位是 Bytes)。 (預設為 100 MB)
            });
            #endregion
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
