using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Day30KestrelWebServer
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
                    options.AddServerHeader = false;//Response的header是否带上Server的信息，为安全考虑设置为false 默认是true
                    options.Limits.KeepAliveTimeout=TimeSpan.FromMinutes(1);//设置限制(limits) Http持久连线时间为1分钟，默认2分钟
                    options.Limits.MaxConcurrentConnections = 100;//设置当前最大的同时连接数为100  默认是无限制
                    options.Limits.MaxConcurrentUpgradedConnections = 100;//设置当前最大的同时连接数为100 (包含WebSockets ,其它非http连接方式)
                    options.Limits.MaxRequestBodySize = 10 * 1024;//Response的封包限制 10K 默认(30000000Bytes)
                    //请求超时 设置当传送速率低于100byte/S c持续时间10秒以上则为超时 //默认5秒低于240字节/s
                    options.Limits.MinRequestBodyDataRate =
                        new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                    //响应超时 设置当传送速率低于100byte/S c持续时间10秒以上则为超时 //默认5秒低于240字节/s
                    options.Limits.MinResponseDataRate =
                        new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(5);//Servcer 处理一个封包最长时间  设置为5秒。默认为30秒

                    //使用ssl 安全请求
                    // http://localhost:5000/
                    options.Listen(IPAddress.Loopback, 5000); //监听常规http地址
                    // https://localhost:5443/
                    options.Listen(IPAddress.Loopback, 5443, listenOptions =>
                    {
                        listenOptions.UseHttps("localhost.pfx", "MyPassword");//Https了  监听
                    });
                })
                .UseUrls("http://localhost:5443")
                .Build();


        //Kestrel 要使用 HTTPS 
        /*  
         * 创建 localhost.conf
         * 安装windows  OpenSSL  下载地址： https://slproweb.com/products/Win32OpenSSL.html
         *命令1： 产生私有key ： openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout localhost.key -out localhost.crt -config localhost.conf -passin pass:MyPassword
         * 命令二 ：通过私有key生成*.pfx：openssl pkcs12 -export -out localhost.pfx -inkey localhost.key -in localhost.crt
         *设定如上40行  一般*.pfx 申请免费的比较少，多数购买的。
         */
    }
}
