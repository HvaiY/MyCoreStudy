using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Day19NLogAndLog4net.Common;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Day19NLogAndLog4net
{
    public class Program
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
#if false
            //使用 NLog 需要安装NLog 套件和Nlog.Web.AspNetCore套件
            //dotnet add package NLog -v 4.5.0-rc02
            // dotnet add package NLog.Web.AspNetCore -v 4.5.0-rc2
            //增加配置文件后 注入NLog服务 这就完成了 
            NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger(); 
#endif

#if true
            #region 使用Log4Net
            // //dotnet add package log4net
            #region 原始使用
            //LoadLog4netConfig();
            //_log.Info("Application Start");//写一次info 级别的log  
            #endregion
            #endregion
#endif
            BuildWebHost(args).Run();
        }
       
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => {
                    //注册的方式使用封装好的Log4net
                    logging.AddProvider(new Log4netProvider("log4net.config"));
                })
                .UseStartup<Startup>()
                .Build();
#if false
        #region 原始使用log4net
        private static void LoadLog4netConfig()
        {
            var repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)
            );
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }
        #endregion  
#endif
    }
}
