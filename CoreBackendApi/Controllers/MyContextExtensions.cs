using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackendApi.Entities;

namespace CoreBackendApi.Controllers
{
    /// <summary>
    /// MyDbconetxt，用来初始化数据 也就是种子类  直接在Configure调用方法就能实现创建数据库(迁移）
    /// </summary>
    public static class MyContextExtensions
    {
        public static void EnsureSeedDataForContext(this MyDbconetxt context)
        {
            //表中存在任何元素久直接返回
            if (context.Products.Any())
            {
                return;
            }
            var products = new List<Product>
            {
                new Product
                {
                    Name = "牛奶",
                    Price = 2.5f,
                    Description = "这是牛奶啊"
                },
                new Product
                {
                    Name = "面包",
                    Price = 4.5f,
                    Description = "这是面包啊"
                },
                new Product
                {
                    Name = "啤酒",
                    Price = 7.5f,
                    Description = "这是啤酒啊"
                }
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        
    }
}
}
