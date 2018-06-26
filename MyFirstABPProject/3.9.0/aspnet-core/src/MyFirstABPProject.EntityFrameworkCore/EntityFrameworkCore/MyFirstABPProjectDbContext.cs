using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyFirstABPProject.Authorization.Roles;
using MyFirstABPProject.Authorization.Users;
using MyFirstABPProject.MultiTenancy;

namespace MyFirstABPProject.EntityFrameworkCore
{
    public class MyFirstABPProjectDbContext : AbpZeroDbContext<Tenant, Role, User, MyFirstABPProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MyFirstABPProjectDbContext(DbContextOptions<MyFirstABPProjectDbContext> options)
            : base(options)
        {
        }
    }
}
