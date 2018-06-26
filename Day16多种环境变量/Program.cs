using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Day16多种环境变量
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var env = hostContext.HostingEnvironment;
                    config.SetBasePath(Path.Combine(env.ContentRootPath, "Configuration"))
                        .AddJsonFile(path: "settings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile(path: $"settings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);//环境变量名可以确定载入的配置文件，后面的Key与前面的Key相同时会覆盖掉前面key中的值，那么也就达到了环境变量不一样使用的配置文件就不一样了
                })
                .UseStartup<Startup>()
                .Build();
    }
}
