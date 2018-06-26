using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Day17ExceptionMiddleware.Common
{
    //异常中间件
    public class ExceptionMiddleware
    {
     
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //完全可以写日志文件
                await context.Response
                    .WriteAsync($"{GetType().Name} catch exception. Message: {ex.Message}");
            }
        }
    }
}
