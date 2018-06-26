using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day27Response快取.Models;
using Microsoft.Extensions.Logging;

namespace Day27Response快取.Controllers
{
    public class HomeController : Controller
    {
#if false
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]//客户端(浏览器)缓存时间是60秒过期 然后按照Cookie区分不同使用者
        public IActionResult Index()
        {
            return View();
        } 
#endif
#if false
        [ResponseCache(CacheProfileName = "MyCachepoliy")] //已在服务中注册， 直接用对应名称
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        } 
#endif
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 360)] //服务端缓存使用 时间为360秒
        public IActionResult Index()
        {
            var request = HttpContext.Request;
            _logger.LogDebug($"URL: {request.Host}{request.Path}{request.QueryString}");
            return View(model: DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
