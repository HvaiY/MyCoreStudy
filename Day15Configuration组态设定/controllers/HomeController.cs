using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day15Configuration组态设定.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Day15Configuration组态设定.controllers
{
    public class HomeController : Controller
    {
        #region 使用弱类型的方式读取配置文件

#if false
        //配置文件的读取
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }
        public string Index()
        {
            var defaultCulture = _config["SupportedCultures:1"];//配置文件中数组对象索引获取依然是0开始
            var subProperty1 = _config["CustomObject:Property:SubProperty1"];
            var subProperty2 = _config["CustomObject:Property:SubProperty2"];
            var subProperty3 = _config["CustomObject:Property:SubProperty3"];

            return $"defaultCulture({defaultCulture.GetType()}): {defaultCulture}\r\n"
                   + $"subProperty1({subProperty1.GetType()}): {subProperty1}\r\n"
                   + $"subProperty2({subProperty2.GetType()}): {subProperty2}\r\n"
                   + $"subProperty3({subProperty3.GetType()}): {subProperty3}\r\n";
        } 
#endif

        #endregion

        #region 使用强类似的方式读取配置文件
#if false

        private readonly Settings _settings;

        public HomeController(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }
        //访问地址：http://localhost:58933/Home/index
        public string Index()
        {
            var defaultCulture = _settings.SupportedCultures[1];
            var subProperty1 = _settings.CustomObject.Property.SubProperty1;
            var subProperty2 = _settings.CustomObject.Property.SubProperty2;
            var subProperty3 = _settings.CustomObject.Property.SubProperty3;

            return $"defaultCulture({defaultCulture.GetType()}): {defaultCulture}\r\n"
                   + $"subProperty1({subProperty1.GetType()}): {subProperty1}\r\n"
                   + $"subProperty2({subProperty2.GetType()}): {subProperty2}\r\n"
                   + $"subProperty3({subProperty3.GetType()}): {subProperty3}\r\n";
        } 
#endif
        #endregion

        #region 读取命令行/环境变量/读取内存变量/自定义方式读取配置
#if true
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }
        //访问地址： http://localhost:58933/home/index 读取命令行的参数作为配置
        public string Index()
        {
            var siteName = _config["SiteName"];
            var domain = _config["Domain"];

            return $"SiteName({siteName.GetType()}): {siteName}\r\n"
                   + $"Domain({domain.GetType()}): {domain}\r\n";
        }
        public string Eve()
        {
            var sample = _config["Sample"];
            return $"sample({sample.GetType()}): {sample}\r\n";
        }
        public string Memory()
        {
            var siteName = _config["Site:Name"];
            var domain = _config["Site:Domain"];

            return $"Site.Name({siteName.GetType()}): {siteName}\r\n"
                   + $"Site.Domain({domain.GetType()}): {domain}\r\n";
        }
        public string Custom()
        {
            var siteName = _config["Custom:Site:Name"];
            var domain = _config["Custom:Site:Domain"];

            return $"Custom.Site.Name({siteName.GetType()}): {siteName}\r\n"
                   + $"Custom.Site.Domain({domain.GetType()}): {domain}\r\n";
        }
#endif
        #endregion
    }
}