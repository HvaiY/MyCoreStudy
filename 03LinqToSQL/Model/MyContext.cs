using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace _03LinqToSQL.Model
{
    public class MyContext : DbContext
    {
        public MyContext() : base()
        {

            Database.EnsureCreated();

#if false
            this.Customers.AddRange(new List<Customer>()
            {
                new Customer(){Name = "大海"},
                new Customer(){Name = "周周"},
                new Customer(){Name = "小黄"},
                new Customer(){Name = "Tom Chen"},
                new Customer(){Name = "Vincent Ke"},
                new Customer(){Name = "Alan"}
            });

            this.CustomerInfos.AddRange(new List<CustomerInfo>()
            {
                new CustomerInfo(){Address = "上海", Cid = 1, Price = "18", Customer = new Customer(){ Name = "大海"}},
                new CustomerInfo(){Address = "上海", Cid = 1, Price = "19", Customer = new Customer(){ Name = "大海8"}}
            });
            this.CustomerMores.AddRange(new List<CustomerMore>()
            {
                new CustomerMore(){ Name = "虹桥", CIid = 1},
                new CustomerMore(){ Name = "黄埔", CIid = 2}
            });
            this.SaveChanges(); 
#endif
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //"Server=YUANLONGHAI\\MSSQLSERVER2014;DataBase=ProductDB;User ID=sa;Password=hai;"
            optionsBuilder.UseSqlServer("Server=YUANLONGHAI\\MSSQLSERVER2014;DataBase=EFCoreLinq;uid=sa;pwd=hai");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }
        public DbSet<CustomerMore> CustomerMores { get; set; }
    }
}
