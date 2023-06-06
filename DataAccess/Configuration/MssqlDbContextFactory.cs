using DataAccess.Concrete.Mssql.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class MssqlDbContextFactory : IDesignTimeDbContextFactory<ExampleDbContext>
    {
        public ExampleDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("dBSettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<ExampleDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new ExampleDbContext(optionsBuilder.Options);
        }
    }
}
