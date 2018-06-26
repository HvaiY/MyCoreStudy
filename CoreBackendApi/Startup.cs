using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackendApi.Controllers;
using CoreBackendApi.Entities;
using CoreBackendApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace CoreBackendApi
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration) //注入配置文件
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// Core 2.0中 基本方法调用顺序：Main -> ConfigureServices -> Configure
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()//注册mvc到Container
                .AddMvcOptions(options =>
                {
                    //添加格式 输出为xml 需要请求头设置Accept:application/xml
                    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()); //xml相对要少 用到 以往.net中webserver返回就是该格式
                })
                .AddJsonOptions(options =>
                {
                    if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                    {
                        resolver.NamingStrategy = null; //去除小驼峰命名规则(那么每个单词的第一个字母均为大写) 建议还是保持默认的
                    }

                });
#if false
                //设置指定json命名方式
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    //  options.SerializerSettings.ContractResolver = new DefaultContractResolver(); 帕斯卡命名方式  每个单词第一个都为大写

                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; //空值不显示null 那么返回的json可以更少更加节省流量

                });
#endif
            #region 自定义服务注入 依赖接口（抽象） 也可以直接注入一个类了

            //   services.AddTransient<IMailService,LocalMailService>();//这句话的意思就是，当需要IMailService的一个实现的时候，Container就会提供一个LocalMailService的实例。
            //按照调试方式来 启用注入的对象  Release   或者debug模式
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            #endregion


            ////使用DbContext 那么就注入一个
            //services.AddDbContext<MyDbconetxt>(); //方法注册时使用的 
            //关于链接字符串问了安全性最好放到系统环境变量中 ，key：connectionString:sqlconn value:Data Source=YUANLONGHAI\\MSSQLSERVER2014;DataBase=ProductDB;uid=sa;pwd=hai
            services.AddDbContext<MyDbconetxt>(options => { options.UseSqlServer(Configuration["connectionString:sqlconn"]); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory,MyDbconetxt myDbConetxt)
        {
            #region NLog日志注入 NuGet 获取NLog.Extensions.Logging --添加nlog.config 配置文件 

            loggerFactory.AddNLog();
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//开发者异常页面
            }
            else
            {
                app.UseExceptionHandler();//默认的异常页面 抛出状态 500
            }
            myDbConetxt.EnsureSeedDataForContext();//调用种子方法 实现自动创建数据库并插入数据

            //http://www.cnblogs.com/cgzl/tag/asp.net%20core%202.0/
#if false
            #region 返回简单的Hello World 测试抛出异常
            app.Run(async (context) =>
                 {

                     await context.Response.WriteAsync("Hello World!");
                  // throw new Exception("大海");
              });
            #endregion
#endif
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
