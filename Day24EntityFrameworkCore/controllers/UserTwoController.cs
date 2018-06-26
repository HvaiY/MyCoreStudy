using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Day24EntityFrameworkCore.Models;
using Day24EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day24EntityFrameworkCore.controllers
{
    [Produces("application/json")]
    [Route("api/UserTwo")]
    public class UserTwoController : Controller
    {
        private readonly IRepository<UserModel, int> _repository;

        public UserTwoController(IRepository<UserModel, int> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public ResultModel Get(string q)
        {
            var result = new ResultModel
            {
                Data = _repository.Find(u =>
                    string.IsNullOrEmpty(q) || Regex.IsMatch(u.Name, q, RegexOptions.IgnoreCase)),
                Message = "获取成功",
                IsSuccess = true
            };
            return result;

        }
        [HttpGet("{id}")]
        public ResultModel Get(int id)
        {
            var result = new ResultModel();
            result.Data = _repository.FindById(id);
            result.IsSuccess = true;
            return result;
        }

        [HttpPost]
        public ResultModel Post([FromBody]UserModel user)
        {
            var result = new ResultModel();
            _repository.Create(user);
            result.Data = user.Id;
            result.IsSuccess = true;
            return result;
        }

        [HttpPut("{id}")]
        public ResultModel Put(int id, [FromBody]UserModel user)
        {
            var result = new ResultModel();
            try
            {
                user.Id = id;
                _repository.Update(user);
                result.IsSuccess = true;
            }
            catch
            {
                // ...
            }
            return result;
        }

        [HttpDelete("{id}")]
        public ResultModel Delete(int id)
        {
            var result = new ResultModel();
            try
            {
                _repository.Delete(id);
                result.IsSuccess = true;
            }
            catch
            {
                // ...
            }
            return result;
        }
    }
}