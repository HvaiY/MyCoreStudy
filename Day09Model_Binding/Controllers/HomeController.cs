using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day09Model_Binding.Models;
using Microsoft.Extensions.Logging;

namespace Day09Model_Binding.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       [HttpPost]
        public IActionResult Index(int id)
        {
            //通过POST请求就可以获取对应传递过来的id的值 这就是数据绑定  ，浏览器地址中也是一样可以显示或者隐士的传递 与mvc不无二致
            //该三种方式为预设方式，Core提供绑定特性的方式冲request提取其它的数据绑定  需要在mvc服务中加入 XmlSerializerFormatters 也就是为了序列化
            return Content($"id:{id}");
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
        public IActionResult FirstSample(
            [FromHeader]string header,
            [FromForm]string form,
            [FromRoute]string id,
            [FromQuery]string query)
        {
            return Content($"header: {header}, form: {form}, id: {id}, query: {query}");
        }

        public IActionResult DiSample([FromServices] ILogger<HomeController> logger)
        {
            return Content($"logger is null? {logger == null}");
        }

        public IActionResult BodySample([FromBody]UserModel model)
        {
            return Ok(model);
        }
    }
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
