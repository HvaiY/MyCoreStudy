using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day11CookieAndSession.Common;
using Day11CookieAndSession.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day11CookieAndSession.Controllers
{
    [Route("[controller]")]
    public class TestSessionController : Controller
    {
        [Route("[action]")]
        public IActionResult Index()
        {

            HttpContext.Session.SetObject("userinfo", new User() { Id = 123, Name = "大海", Emial = "123@qq.com", Phone = 1333333 });
            User user = HttpContext.Session.GetObject<User>("userinfo");

            return Json(user);
        }

        #region 封装后的Session获取和赋值 这里使用封装了的User

#if true
        private readonly ISessionWapper _sessionWapper;

        public TestSessionController(ISessionWapper sessionWapper)
        {
            //
            _sessionWapper = sessionWapper;
        }
        [Route("[action]")]
        public IActionResult SessTestSet()
        {
            _sessionWapper.User = new User() { Emial = "333@qq.com", Id = 111, Name = "周周", Phone = 133 };
            return Ok("OK");
        } 
#endif
        [Route("[action]")]
        public IActionResult SessTestGet()
        {
            //注意开启Https 加密的话必须在Https加密后才能访问
            var user = _sessionWapper.User;
            return Json(user);
            //User user = HttpContext.Session.GetObject<User>("userinfo");

            //return Json(user);
        }

        #endregion



        //[Route("[action]")]
        //public IActionResult SessionSetString(string str)
        //{

        //}
    }
}