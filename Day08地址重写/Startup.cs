using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day08地址重写
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
            //地址重写
            var rewrite = new RewriteOptions()
                .AddRewrite("about.aspx", "about", skipRemainingRules: true) //地址重写 浏览器地址不变还是about.aspx 返回的是about页面的数据， 第三个参数表示找到地址之后略过后面的检查
                .AddRedirect("first", "index"); //地址跳转 地址会变动为index ,第三个参数为状态码
            app.UseRewriter(rewrite);//注入地址重写规则

#if false
            //正则表达式方式
            var rewrites = new RewriteOptions()
                    .AddRedirect("products.aspx?id=(\\W+)", "prosucts/$1", 301)
                    .AddRedirect("api/(.*)/(.*)/(.*)", "api?p1=$1&p2=$2&p3=$3", 301);
            app.UseRewriter(rewrites);
#endif

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

            app.UseMvc();
        }
    }
}
