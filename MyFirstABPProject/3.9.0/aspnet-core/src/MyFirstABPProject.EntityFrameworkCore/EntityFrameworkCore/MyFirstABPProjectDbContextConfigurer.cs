using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyFirstABPProject.EntityFrameworkCore
{
    public static class MyFirstABPProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyFirstABPProjectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyFirstABPProjectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
