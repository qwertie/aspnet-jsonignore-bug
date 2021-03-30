using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApp
{
    public class ExampleChild
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Child!";
    }
    public class Example
    {
        public int Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string Ignored { get; set; } = "?!?!";
        public string Good { get; set; } = "Good!";

        public int? ChildId { get; set; }
        [ForeignKey(nameof(ChildId))]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ExampleChild Child { get; set; }
    }

    public class DbContextWithLogging : Microsoft.EntityFrameworkCore.DbContext
    {
        ILoggerFactory _loggerFactory;
        IConfiguration _configuration; // Provides access to appsettings.json

        public DbContextWithLogging(ILoggerFactory loggerFactory, IConfiguration configuration)
            => (_loggerFactory, _configuration) = (loggerFactory, configuration);

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(_loggerFactory);
            #if DEBUG
            builder.EnableSensitiveDataLogging();
            #endif
        }
    }
}
