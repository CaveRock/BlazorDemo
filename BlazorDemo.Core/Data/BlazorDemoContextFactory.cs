using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Server.Data {
    public class BlazorDemoContextFactory : IDesignTimeDbContextFactory<BlazorDemoContext> {

        public BlazorDemoContext CreateDbContext(string[] args)
        {
            var config = GetAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<BlazorDemoContext>();

            string connectionString = config.GetConnectionString("DefaultConnection");
            
            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("BlazorDemo.Core"));
            return new BlazorDemoContext(optionsBuilder.Options);

            
        }

        IConfiguration GetAppConfiguration()
        {
            var environmentName =
                      Environment.GetEnvironmentVariable(
                          "ASPNETCORE_ENVIRONMENT");
            Console.WriteLine(environmentName);
            var dir = Directory.GetParent(AppContext.BaseDirectory);

            if (Environments.Development.Equals(environmentName,
                StringComparison.OrdinalIgnoreCase))
            {
                var depth = 0;
                do
                    dir = dir.Parent;
                while (++depth < 5 && dir.Name != "bin");
                dir = dir.Parent;
            }
            var path = dir.FullName;

            Console.WriteLine(path);

            var builder = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                    .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
