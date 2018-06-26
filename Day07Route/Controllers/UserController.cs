using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Day07Route.Controllers
{
    //控制器上定义路由特性，那么其优先于MapRoutes中的路由规则
    //当控制器设定了route 之后那么所有的action也需要设定route
    [Route("[controller]")]
    public class UserController : Controller
    {
        [Route("")] //该action由于route未指定地址，那么默认为控制器的名字访问 请求地址为http://localhost:xxx/User
        public IActionResult UserInfo()
        {
            return Content("Userinfo");
        }
        [Route("CP")] //请求地址为 http://localhost:xxx/User/CP
        public IActionResult ChangePassword()
        {
            return Content("修改密码");
        }
        [Route("[action]")] //明确它是一个action 那么访问地址为：http://localhost:xxx/User/Other
        public IActionResult Other()
        {
            return Content("其它");
        }
    }
}