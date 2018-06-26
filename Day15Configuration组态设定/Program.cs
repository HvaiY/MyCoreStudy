using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Day15Configuration组态设定.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Day15Configuration组态设定
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    #region 1. 使用配置文件方式读取配置文件（可以在注册服务之后使用强类型的方式获取配置）
                    var env = hostContext.HostingEnvironment;
                    //Console.WriteLine(Path.Combine(env.ContentRootPath, "Configuration"));
                    //Console.ReadKey();
                    /**AddJsonFile：
                     path：組態檔案的路徑位置。
                     optional：如果是必要的組態檔，optional 就要設定為 false，當檔案不存在就會拋出 FileNotFoundException。
                     reloadOnChange：如果檔案被更新，就同步更新 IConfiguration 實例的值。
                     */
                    config.SetBasePath(Path.Combine(env.ContentRootPath, "Configuration")) //设置配置文件所在地址(项目中)
                        .AddJsonFile(path: "settings.json", optional: false, reloadOnChange: true);
                    #endregion

                    #region 2.读取命令行的方式   程序启动：dotnet run SiteName="John Wu's Blog" Domain="blog.johnwu.cc"
                    config.AddCommandLine(args);

                    #endregion
                    #region 3.读取环境变量的方式
                    //使用命令的方式设置环境变量  ： SETX Sample "This is environment variable sample." /M
                    //这里直接在桌面操作设置环境变量
                    config.AddEnvironmentVariables();
                    #endregion
                    #region 4.读取内存中的数据作为配置的方式  在这里设置内存中的数据
                    var dictionary = new Dictionary<string, string>
                    {
                        { "Site:Name", "John Wu's Blog" },
                        { "Site:Domain", "blog.johnwu.cc" }
                    };
                    config.AddInMemoryCollection(dictionary);
                    #endregion
                    #region 自定义的方式
                    config.Add(new CustomConfigurationSource());//自定义的类 实例化对象 存储了配置数据
                    #endregion
                })
              .UseStartup<Startup>()
              .Build();
        }

    }
}
