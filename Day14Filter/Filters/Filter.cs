using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Day14Filter.Filters
{
    public class Filter
    {

    }
    /*
         * 关于过滤器 与ASP.NET 一样  同样保留五种过滤器（使用：定义一个类实现对应的接口即可）
         * Filters 分别为：Authorization Filter、Resource Filter、 Action Filter、Exception Filter、Result Filter
         * Authorization Filter
         *Authorization 在五种 Filter 中优先级最高的，通常用于验证Requert 合不合法，不合法后面就直接跳过。
         *Resource Filter
         *Resource 是第二优先，会在 Authorization 之后，Model Binding 之前执行。通常会是需要对 Model 加工处理才用。
         *Action Filter
         *最容易使用的 Filter，封包进出都需要使用它，使用上沒什么需要特別注意的。跟 Resource Filter 很类似，但并不会经过 Model Binding。
         *Exception Filter
         *异常处理的 Exception。
         *Result Filter
         *当 Action 完成后，最终会经过的 Filter。
      */

    /// <summary>
    /// AuthorizationFilter 同步过滤 
    /// </summary>
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //  context.HttpContext.Session.GetString("user")... 可用于 用户校验
            context.HttpContext.Response.WriteAsync($"{GetType().Name} in.\r\n");

        }
    }

    #region 同一类型的特性更改执行顺序  只要实现的IOrderedFilter  即可,使用时候指定name以及Order order值越小 优先级越高（先进后出）
    public class TestActionFilter : Attribute, IActionFilter, IOrderedFilter
    {
        public string Name { get; set; }
        public int Order { get; set; } = 0;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} {Name}  out. \r\n");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} {Name} in. \r\n");
        }
    }
    #endregion

    /// <summary>
    /// 异步 AsyncAuthorizationFilter
    /// </summary>
    public class AsyncAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
        }
    }
    /// <summary>
    /// ResourceFilter 资源过滤
    /// </summary>
    public class ResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} out. \r\n");
        }
    }
    /// <summary>
    /// 异步资源过滤  AsyncResourceFilter
    /// </summary>
    public class AsyncResourceFilter : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            await context.HttpContext.Response.WriteAsync($"{GetType().Name} in .\r\n");
            await next();
            await context.HttpContext.Response.WriteAsync($"{GetType().Name} out .\r\n");
        }
    }
    /// <summary>
    /// ActionFilter action 过滤
    /// </summary>
    public class ActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} in .\r\n");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} out .\r\n");
        }
    }
    /// <summary>
    /// 异步action 过滤
    /// </summary>
    public class AsyncActionFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await context.HttpContext.Response.WriteAsync($"{GetType().Name} in.\r\n");
            await next();
            await context.HttpContext.Response.WriteAsync($"{GetType().Name} out \r\n");
        }
    }
    /// <summary>
    /// 执行结果过滤  ResultFilter
    /// </summary>
    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} out. \r\n");
        }
        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
        }
    }
    /// <summary>
    /// 异步执行结果过滤  AsyncResultFilter
    /// </summary>
    public class AsyncResultFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await context.HttpContext.Response.WriteAsync($"{GetType().Name} in.\r\n");
            await next();
            await context.HttpContext.Response.WriteAsync($"{GetType().Name} out .\r\n");
        }
    }
    /// <summary>
    /// 异常过滤  写日志专属
    /// </summary>
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
        }
    }
    /// <summary>
    /// 异步异常过滤
    /// </summary>
    public class AsyncExceptionFilter : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
            return Task.CompletedTask;
        }
    }
}
