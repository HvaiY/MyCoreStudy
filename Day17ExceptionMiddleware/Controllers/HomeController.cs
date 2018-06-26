using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day17ExceptionMiddleware.Models;
using Microsoft.Extensions.Logging;

namespace Day17ExceptionMiddleware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            throw new Exception("抛出了一个异常Exception");
           // return View();
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

        #region Test  Webapi Exception
        [Route("/api/test")]
        public string Test()
        {
            throw new System.Exception("This is exception sample from Test().");
        }

        [Route("/error")]
        public IActionResult WebAPIError()
        {
            return View();
        }
        #endregion


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //测试log
        public string Logger()
        {
            _logger.LogTrace("日记级别0 开发阶段使用 不适合正式环境");
            _logger.LogDebug("日志级别1 這類型的 Log 是為了在正式環境除錯使用，但平常不應該開啟，避免 Log 量太大，反而會造成正式環境的問題。 (預設不會輸出)");
            _logger.LogInformation("日志级别2 常見的 Log 類型，主要是紀錄程試運行的流程。");
            _logger.LogWarning("Log Level = 3)紀錄可預期的錯誤或者效能不佳的事件；不改不會死，但改了會更好的問題");
            _logger.LogError("Log Level = 4) 紀錄非預期的錯誤，不該發生但卻發生，應該要避免重複發生的錯誤事件。This error log from Home.Index()");
            _logger.LogCritical("Log Level = 5)只要發生就準備見上帝的錯誤事件，例如會導致網站重啟，系統崩潰的事件。This critical log from Home.Index()");
            return "Home.Index()";
        }
    }
}
