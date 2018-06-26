using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackendApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackendApi.Controllers
{
    [Route("api/product")]
    public class MaterialsController : Controller
    {
        [Route("{productId}/Materials")]
        public IActionResult GetMaterials(int productId)
        {
            var product = ProductService.Current.Products.SingleOrDefault(p => p.Id == productId);
            if (product == null) return NotFound();
            return Ok(product.Materials);
        }
        [Route("{productId}/Materials/{id}")]//请求地址 ： http://localhost:53095/api/Product/2/materials/3
        public IActionResult GetMaterials(int productId, int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(p => p.Id == productId);
            if (product == null) return NotFound();
            var materials = product.Materials.SingleOrDefault(m => m.Id == id);
            if (materials == null) return NotFound();
            return Ok(materials);
        }
    }
}