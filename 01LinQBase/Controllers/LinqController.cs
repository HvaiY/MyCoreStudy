using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace _01LinQBase.Controllers
{
    [Route("api/[controller]")]
    public class LinqController : Controller
    {

        [Route("get")]
        public IActionResult Index()
        {
            #region LINQ的两种查询方式
      string[] names = {"Tom", "Dick", "Harry", "Mary", "Jay"};
            //查询表达式
            var query = from n in names
                where n.Contains("r")
                orderby n.Length
                select n.ToUpper();
            //更多表达式关键字：Where、Select、SelectMany、OrderBy、ThenBy、OrderByDescending、ThenByDescending、GroupBy、Join、GroupJoin。
            //表达式关键字不足以使用的情况下可以使用方法查询
            //方法查询

            var query2 = names
                .Where(n => n.Contains("r"))
                .OrderBy(n => n.Length)
                .Select(n => n.ToLower());

            //   return Json(query.Concat(query2));
            #endregion

            #region LINQ查询选择
       //表达式查询
            int matchs = (from n in names where n.Contains("a") select n).Count();
            string str = (from n in names orderby n select n).First();
        //方法查询简单使用方法查询更方便
            int matchs2 = (names.Where(n => n.Contains("a"))).Count();
            string str2 = names.OrderBy(n => n).First();

           return Json(matchs + " " + str + ":" + matchs2 + " " + str2);
            #endregion

 

        }
    }
}