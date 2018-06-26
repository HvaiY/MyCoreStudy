using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFCore.Dtos;
using EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.controllers
{
    [Route("api/product")] // 和主Model的Controller前缀一样
    public class MaterialController : Controller
    {
        private readonly IProductRepository _productRepository;
        public MaterialController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{productId}/materials")]
        public IActionResult GetMaterials(int productId)
        {
            //先检查是否存在product
            var product = _productRepository.ProductExist(productId);
            if (!product)
            {
                return NotFound();
            }
            var materials = _productRepository.GetMaterialsForProduct(productId);
#if false
            var results = materials.Select(material => new MaterialDto
            {
                Id = material.Id,
                Name = material.Name
            })
                .ToList(); 
#endif
            var results = Mapper.Map<ProductDto>(materials);
            return Ok(results);
        }

        [HttpGet("{productId}/materials/{id}")]
        public IActionResult GetMaterial(int productId, int id)
        {
            //先检查是否存在
            var product = _productRepository.ProductExist(productId);
            if (!product)
            {
                return NotFound();
            }

            var material = _productRepository.GetMaterialForProduct(productId, id);
            if (material == null)
            {
                return NotFound();
            }
#if false //映射前
            var result = new MaterialDto
            {
                Id = material.Id,
                Name = material.Name
            }; 
#endif
            var result = Mapper.Map<MaterialDto>(material);
            return Ok(result);
        }



    }
}