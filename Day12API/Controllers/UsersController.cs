using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day12API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day12API.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private List<UserModel> list = new List<UserModel>();

        public UsersController()
        {
            for (int i = 0; i < 5; i++)
            {
                list.Add(new UserModel() { Id = 100 + i, Name = $"大海{i}", Email = $"123{i}@qq.com", PhoneNumber = $"111{i}", Address = $"XXX{i}区" });
            }
        }

        [HttpGet("{id}")] //请求地址：http://localhost:51737/api/Users/1
        public UserModel Get(int id)
        {
            return new UserModel() { Id = id, Name = "大海" };
        }
        [HttpGet] //请求地址：http://localhost:51737/api/Users
         //  [Route("Getlist")]   //可以使用特性来指定请求
        public List<UserModel> Getlist()
        {
            return list;
        }
        [HttpPost]
        public List<UserModel> Post([FromBody]UserModel user)
        {
            list.Add(user);
            return list;
        }
        [HttpPut("{id}")]
        public List<UserModel> Put(int id, [FromBody] UserModel user)
        {
           //处理修改
            return list;
        }

        [HttpDelete("{id}")]
        public List<UserModel> Delete(int id)
        {
           //处理删除
            
            return list;
        }
    }
}