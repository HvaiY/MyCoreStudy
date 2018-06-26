using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Day11CookieAndSession.Common
{
    /// <summary>
    /// 该扩展使得Session可以存储对象实例
    /// </summary>
    public static class SessionExtension
    {
        //由于在Core 中 session 是无法将一个对象存储在session中   但是可以自己实现Session 的扩展方法 自己序列化对象
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));//使用json序列化对象 序列化对象为字符串 之后再存储值Session 中
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value); //取出session中的值之后再反序列化 然后返回对象 
        }
    }
}
