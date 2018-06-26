using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Day24EntityFrameworkCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day24EntityFrameworkCore.controllers
{
    [Route("api/[controller]s")]
    public class UserController : Controller
    {
        private readonly MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ResultModel Get(string q)
        {
            var result = new ResultModel();
            result.Data = _context.Users.Where(x => string.IsNullOrEmpty(q)
                                                 || Regex.IsMatch(x.Name, q, RegexOptions.IgnoreCase));
            result.IsSuccess = true;
            return result;
        }

        [HttpGet("{id}")]
        public ResultModel Get(int id)
        {
            var result = new ResultModel();
            result.Data = _context.Users.SingleOrDefault(x => x.Id == id);
            result.IsSuccess = true;
            return result;
        }

        [HttpPost]
        public ResultModel Post([FromBody]UserModel user)
        {
            var result = new ResultModel();
          
            _context.Users.Add(user);
            _context.SaveChanges();
            result.Data = user.Id;
            result.IsSuccess = true;
            return result;
        }

        [HttpPut("{id}")]
        public ResultModel Put([FromBody]UserModel user)
        {
            var result = new ResultModel();
            var oriUser = _context.Users.SingleOrDefault(x => x.Id == user.Id);
            if (oriUser != null)
            {
                //修改oriUser 为user
                _context.Entry(oriUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
                result.IsSuccess = true;
            }
            return result;
        }

        [HttpDelete("{id}")]
        public ResultModel Delete(int id)
        {
            var result = new ResultModel();
            var oriUser = _context.Users.SingleOrDefault(x => x.Id == id);
            if (oriUser != null)
            {
                _context.Users.Remove(oriUser);
                _context.SaveChanges();
                result.IsSuccess = true;
            }
            return result;
        }
    }
}