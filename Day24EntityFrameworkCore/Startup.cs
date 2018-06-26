using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day24EntityFrameworkCore.Models;
using Day24EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Day24EntityFrameworkCore
{
    public class Startup
    {

        /// <summary>
        /// EF Core ��һ����� EF Core ��dotnet add package Microsoft.EntityFrameworkCore
        /// 2������DBContext�� �̳�DBContext 
        /// </summary>
        /// <param name="configuration"></param>

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //EF  ע����� 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //ע�������ķ���
            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnEFCore"));
            });

            //ע���Լ���װ����ɾ�Ĳ�
            services.AddScoped<IRepository<UserModel, int>, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,MyContext dBContext)
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
           dBContext.Database.EnsureCreated();// �������ݿ�  
            app.UseMvcWithDefaultRoute();
        }
    }
}
