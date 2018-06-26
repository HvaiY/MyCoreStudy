using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GetAppsettingValues.json
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("开始获取配置文件中的值");

            var builder = new ConfigurationBuilder() //实例化配置文件对象
                .SetBasePath(Directory.GetCurrentDirectory()) //设置文件所在路径
                .AddJsonFile("appsetting.json"); //指定配置文件

            Configuration = builder.Build();

            Console.WriteLine($"option1 = {Configuration["Option1"]}");
            Console.WriteLine($"option2 = {Configuration["option2"]}");
            Console.WriteLine(
                $"suboption1 = {Configuration["subsection:suboption1"]}");
            Console.WriteLine();

            Console.WriteLine("Wizards:");
            Console.Write($"{Configuration["wizards:0:Name"]}, ");//配置文件中的wizards 属性中的第一个对象  的Name
            Console.WriteLine($"age {Configuration["wizards:0:Age"]}");
            Console.Write($"{Configuration["wizards:1:Name"]}, ");//配置文件中的wizards 属性中的第二个对象  的Name
            Console.WriteLine($"age {Configuration["wizards:1:Age"]}");
            Console.WriteLine();

            Console.WriteLine("Press a key...");

            Console.ReadKey();

            // BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
