using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories
{
    /// <summary>
    /// product 查询的实现
    /// </summary>
    public class ProductRespository : IProductRepository
    {
        private readonly MyContext _myContext;

        public ProductRespository(MyContext context)
        {
            _myContext = context;
        }

        public Material GetMaterialForProduct(int productId, int materialId)
        {
            return _myContext.Materials.FirstOrDefault(x => x.ProductId == productId && x.Id == materialId);
        }

        public IEnumerable<Material> GetMaterialsForProduct(int productId)
        {
            return _myContext.Materials.Where(x => x.ProductId == productId).ToList();
        }

        /// <summary>
        /// 如果包含配料表信息 一起返回
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="includeMaterials"></param>
        /// <returns></returns>
        public Product GetProduct(int productId, bool includeMaterials)
        {
            if (includeMaterials)
            {
                return _myContext.Products.Include(x => x.Materials).FirstOrDefault(x => x.Id == productId);
            }

            return _myContext.Products.Find(productId);
        }

        public Product GetProduct(int productId)
        {
            return _myContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _myContext.Products.OrderBy(x => x.Name).ToList();
        }

        public bool ProductExist(int productId)
        {
            return _myContext.Products.Any(x => x.Id == productId);
        }
        public void AddProduct(Product product)
        {
            _myContext.Products.Add(product);
        }
        public void DeleteProduct(Product product)
        {
            _myContext.Products.Remove(product);
        }
        public bool Save()
        {
            return _myContext.SaveChanges() >= 0;
        }
    }
}
