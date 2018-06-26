using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackendApi.Dtos;
using CoreBackendApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreBackendApi.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        //日志Core 已注入 这里使用 
        private readonly ILogger<ProductController> _logger;
        private readonly IMailService _mailService;//自定义的服务

        public ProductController(ILogger<ProductController> logger,IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }


        #region 请求方式 约定
        /*
      * Get, 查询, Attribute: HttpGet, 例如: '/api/product', '/api/product/1'
        POST, 创建, HttpPost, '/api/product'
        PUT 整体修改更新 HttpPut, '/api/product/1'
        PATCH 部分更新, HttpPatch, '/api/product/1'
        DELETE 删除, HttpDelete, '/api/product/1
     */
        #endregion
        #region 状态码（response）
        //200: OK

        //201: Created, 创建了新的资源

        //204: 无内容 No Content, 例如删除成功

        //400: Bad Request, 指的是客户端的请求错误.

        //401: 未授权 Unauthorized.

        //403: 禁止操作 Forbidden. 验证成功, 但是没法访问相应的资源

        //404: Not Found 

        //409: 有冲突 Conflict.

        //500: Internal Server Error, 服务器发生了错误.
        #endregion
#if false

        [HttpGet("All")]
        public JsonResult GetProducts()
        {
            return new JsonResult(ProductService.Current.Products) {StatusCode = 200};//设定状态为200 但是依然不是很方便
        }
        [HttpGet("{id}")]
        public JsonResult GetProducts(int id)
        {
            return new JsonResult(ProductService.Current.Products.SingleOrDefault(p => p.Id == id));
        } 
#endif

        #region IActionResult 被ActionResult 实现  而JsonResult继承了ActionResult 上面的升级使用Core 内置的方法
        [HttpGet("All")]
        public IActionResult GetProducts()
        {
            return Ok(ProductService.Current.Products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProducts(int id)
        {
            var temp = ProductService.Current.Products.SingleOrDefault(p => p.Id == id);
            if (temp == null)
            {
                //使用注入的自己写的服务 
                _mailService.Send("GetProducts",$"{id}为Id的商品未找到！");
                return NotFound();
            }
            return Ok(temp);
        }
        #endregion

        [Route("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreation product) //[FromBody] , 请求的body里面包含着方法需要的实体数据, 方法需要把这个数据Deserialize成ProductCreation, [FromBody]就是干这些活的.
        {
            if (product == null)
            {
                return BadRequest();
            }
            //对请求过来的数据进行验证(与模型上的一样可以在ModelState 验证)
            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            //模型验证问题 模型规则已在模型特性上加上 如果没通过那么返回BadRequest
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);//ModelState: 是一个Dictionary, 它里面是请求提交到Action的Name和Value的对们, 一个name对应着model的一个属性, 它也包含了一个针对每个提交的属性的错误信息的集合.
            }

            var maxId = ProductService.Current.Products.Max(x => x.Id);
            var newProduct = new Product
            {
                Id = ++maxId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description

            };
            ProductService.Current.Products.Add(newProduct);

            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);//返回指定名字路由 第二个参数为对应方法参数 ，第三个参数为实体数据
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductModification product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            model.Name = product.Name;
            model.Price = product.Price;
            model.Description = product.Description;

            // return Ok(model);
            return NoContent();
        }
        /*
         *Patch可以实现  替换, 复制, 移除 参数格式
         *[
         *  {
         *   "op":"replace",
         *   "path":"/name",
         *   "value":"大海"
         *  }
         *]
         * 参数格式为上面的格式  主要看参数 op 是上面操作 可以是replace  remove 和copy 
         *
         */

        [HttpPatch("{id}")] //部分更新
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<ProductModification> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            var toPatch = new ProductModification
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
            patchDoc.ApplyTo(toPatch, ModelState); //部分更改  使用该方法就可以更新需要更新的属性

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (toPatch.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }
            TryValidateModel(toPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Name = toPatch.Name;
            model.Description = toPatch.Description;
            model.Price = toPatch.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            ProductService.Current.Products.Remove(model);
            return NoContent();
        }
    }
}