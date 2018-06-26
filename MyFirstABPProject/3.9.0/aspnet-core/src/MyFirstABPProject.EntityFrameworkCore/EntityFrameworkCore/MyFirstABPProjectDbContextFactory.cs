using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyFirstABPProject.Configuration;
using MyFirstABPProject.Web;

namespace MyFirstABPProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MyFirstABPProjectDbContextFactory : IDesignTimeDbContextFactory<MyFirstABPProjectDbContext>
    {
        public MyFirstABPProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyFirstABPProjectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MyFirstABPProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MyFirstABPProjectConsts.ConnectionStringName));

            return new MyFirstABPProjectDbContext(builder.Options);
        }
    }
}
