using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using game2048.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace game2048.Server.entities
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddXmlFile("app.config").Build();
            var connectionString = configuration["connectionStrings:add:MyKey:connectionString"];
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlServer(connectionString);
            return new Context(builder.Options);
        }
    }
}
