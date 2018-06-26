using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBackendApi.Entities
{
    /// <summary>
    /// 设置Prodct 的 配置 （主键  长度 数据类型等 ）
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //在model上加特性一样可以 这里使用的 fiuent api
            //第一行表示设置Id为主键（其实我们并不需要这么做）。然后Name属性是必填的，而且最大长度是50。最后Price的精度是8，2，数据库里的类型为decimal。
            // 设置方法 api : https://docs.microsoft.com/en-us/ef/core/modeling/ 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);//设置属性验证最大长度为50
            builder.Property(x => x.Price).HasColumnType("decimal(8,2)");
            builder.Property(x => x.Description).HasMaxLength(200);
        }
    }
}
