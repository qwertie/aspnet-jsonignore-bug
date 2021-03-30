using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApp
{
    // Migration creation command:
    //     dotnet ef migrations add <NAME> --project MvcWebApp --context ExampleDbContext
    public class ExampleDbContext : DbContextWithLogging
    {
        public ExampleDbContext(ILoggerFactory loggerFactory, IConfiguration configuration) : base(loggerFactory, configuration) { }

        public DbSet<Example> Examples { get; set; }
        public DbSet<ExampleChild> ExampleChildren { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseSqlite("Data Source=example-sqlite.db;");
            builder.UseLazyLoadingProxies();
        }
    }
}
