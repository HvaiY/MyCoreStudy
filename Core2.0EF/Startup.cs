using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Dtos;
using EFCore.Entities;
using EFCore.Repositories;
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

namespace EFCore
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    //添加格式 输出为xml 需要请求头设置Accept:application/xml
                    options.OutputFormatters.Add(
                        new XmlDataContractSerializerOutputFormatter()); //xml相对要少 用到 以往.net中webserver返回就是该格式
                })
                .AddJsonOptions(options =>
                {
                    if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                    {
                        resolver.NamingStrategy = null; //去除小驼峰命名规则(那么每个单词的第一个字母均为大写) 建议还是保持默认的
                    }

                });
            services.AddDbContext<MyContext>(options =>
                options.UseSqlServer(Configuration["connectionString:conn"]));
            services.AddScoped<IProductRepository, ProductRespository>(); //每一个请求生成一个实例(注册自定义服务?注册的是Product与materials的创储)

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, MyContext context, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();//NLog 记录日志
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.EnsureSeedDataForContext();//触发数据库插入(种子) MyContext context,
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");  
            //});
            app.UseStatusCodePages();

            //注册自动映射 AutoMapper（使用该程序集需要NuGet）
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductWithoutMaterialDto>(); //注册映射关系 对应属性
                cfg.CreateMap<Product, ProductDto>();

                cfg.CreateMap<Material, MaterialDto>();
                cfg.CreateMap<ProductModification, Product>();
                cfg.CreateMap<Product, ProductModification>();

            });

            app.UseMvc();
        }
    }
}
