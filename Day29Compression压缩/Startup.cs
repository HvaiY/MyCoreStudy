using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Day29Compression压缩.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day29Compression压缩
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
            //注入 封包压缩的服务  默认是不对图片压缩的
            //(預設的 MimeTypes 有：text/plain、text/css、application/javascript、text/html、application/xml、text/xml、application/json、text/json)
            services.AddResponseCompression();
#endif
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;//开启对https封包压缩
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/jpeg" });//将图片格式包含到压缩类型中
                options.Providers.Add<GzipCompressionProvider>();//注册Gzip压缩方式

                #region 研究深了再用吧 
                //  //自定义的压缩方式 
                //   options.Providers.Add<CustomCompressionProvider>(); 
                #endregion

            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;//压缩等级设置为最佳的 这是一个枚举
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();//使用压缩服务
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

         //   https://ithelp.ithome.com.tw/users/20107461/ironman/1372?page=3  学习 29

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
