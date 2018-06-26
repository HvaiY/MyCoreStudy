using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreBackendApi.Entities
{
    public class MyDbconetxt : DbContext
    {


        #region 配置文件 方法覆写的方式注册
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=YUANLONGHAI\\MSSQLSERVER2014;DataBase=ProductDB;uid=sa;pwd=hai");
        //    base.OnConfiguring(optionsBuilder);
        //}

        #endregion
        //构造函数注册 链接字符串配置由服务注册 
        public MyDbconetxt(DbContextOptions<MyDbconetxt> options) : base(options)
        {
            //Database.EnsureCreated();//依赖注入时会被调用(MyDbcontext)  而EnsureCreated 作用是对数据库的创建(如果存在数据那么就不创建了)
            ////model 变更的话使用 Migration(迁移)

            //迁移做法  打开程序包控制台  做个迁移命令： Add-Migration xxx_productDbInfo名字随便吧 
            //然后 修改 Database.Migrate();
            //然后  Update-Database  后面加上  -verbose 可以看到明细
            // 增加一个model或者多model进行修改  那么久再执行 Add-Migration xxx_xxx  然后  Update-Database -verbose 完成就可以了(二次迁移)
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
#if false //表映射的的主键 长度 数据类型 设置到单独一个类中，不在用下面的方法
            //在model上加特性一样可以 这里使用的 fiuent api
            //第一行表示设置Id为主键（其实我们并不需要这么做）。然后Name属性是必填的，而且最大长度是50。最后Price的精度是8，2，数据库里的类型为decimal。
            // 设置方法 api : https://docs.microsoft.com/en-us/ef/core/modeling/ 
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(50);//设置属性验证最大长度为50
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(8,2)"); 
#endif
            modelBuilder.ApplyConfiguration(new ProductConfiguration()); //与上面一样的效果，
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());

        }

        public DbSet<Product> Products { get; set; } //数据库创建时会创建Products表
        public DbSet<Material> Materials { get; set; }
    }
}
