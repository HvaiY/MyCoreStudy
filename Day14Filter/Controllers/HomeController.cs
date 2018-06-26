using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Day14Filter.Filters;
using Microsoft.AspNetCore.Mvc;
using Day14Filter.Models;
using Microsoft.AspNetCore.Http;

namespace Day14Filter.Controllers
{
    [AuthorizationFilter] //可以用来校验登录情况
    [TestActionFilter(Name = "control",Order = 2)]
    public class HomeController : Controller
    {
        [ActionFilter]
        [TestActionFilter(Name = "action", Order = 3)]
        public void Index()
        {
            // return View();
            Response.WriteAsync("Hello Hvai \r\n");
        }
         [TestActionFilter(Name = "action",Order = 3)]
        public void About()
        {
            Response.WriteAsync("关于");
        }
        // [ActionFilter]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        // [ActionFilter]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
