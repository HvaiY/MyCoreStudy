using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Entities
{
    /// <summary>
    /// 对实例Material的对应表约束
    /// </summary>
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);//不为null 长度限制为50
           builder.HasOne(x => x.Product).WithMany(x => x.Materials).HasForeignKey(x => x.ProductId) .OnDelete(DeleteBehavior.Cascade);//创建外键关联并级联删除
        }
    }
}
