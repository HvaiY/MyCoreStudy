using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Day07Route
{
    //控制器上定义路由特性，那么其优先于MapRoutes中的路由规则
    //当控制器设定了route 之后那么所有的action也需要设定route
    [Route("[controller]")]
    public class UserTestController : Controller
    {
        [Route("")] //该action由于route未指定地址，那么默认为控制器的名字访问 请求地址为http://localhost:xxx/UserTest
        public IActionResult UserInfo()
        {
            return Content("Userinfo Test");
        }
        [Route("CP")] //请求地址为 http://localhost:xxx/UserTest/CP
        public IActionResult ChangePassword()
        {
            return Content("修改密码");
        }
        [Route("[action]")] //明确它是一个action 那么访问地址为：http://localhost:xxx/UserTest/Other
        public IActionResult Other()
        {
            return Content("其它");
        }
    }
}
