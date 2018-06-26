using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Day23上传和下载
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .UseStartup<Startup>()
                 .UseKestrel(options =>
                {
                    // 1G 设置上传文件大小限制 //也可以在IIS上设置
                    options.Limits.MaxRequestBodySize = 1024 * 1024 * 1024;
                })
                .Build();
    }
}
