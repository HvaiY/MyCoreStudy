using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Day21多国语言.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Day21多国语言.Models;
using Microsoft.Extensions.Localization;

namespace Day21多国语言.Controllers
{
    [MiddlewareFilter(typeof(CultureMiddleware))]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View(model: new SampleModel());
        }

        public IActionResult Content()
        {
            return Content($"CurrentCulture: {CultureInfo.CurrentCulture.Name}\r\n"
                           + $"CurrentUICulture: {CultureInfo.CurrentUICulture.Name}\r\n"
                           + $"{_localizer["Hello"]}");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

      

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
