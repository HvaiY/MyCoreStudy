using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day11CookieAndSession.Models;
using Microsoft.AspNetCore.Http;

namespace Day11CookieAndSession.Common
{
    //对Session的包装  使得Session有了强类型效果 需要在Satrtup 单例模式注入
    public interface ISessionWapper
    {
        //这里仅仅封装一个Session Key 与Value 封装User
        User User { get; set; }
    }


    public class SessionWapper : ISessionWapper
    {
        private static readonly string _userKey = "session.user";//session  的key
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionWapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private ISession Session
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session;
            }
        }

        public User User
        {
            get
            {
                return Session.GetObject<User>(_userKey);
            }
            set
            {
                Session.SetObject(_userKey,value);
            }
        }
    }
}
