using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace MvcWebApp
{
    public class Entity {
        public object Config { get; set; }
    }
    public class ConfigBase {
        public int UniversalSetting { get; set; }
    }
    public class ConfigDerived : ConfigBase {
        public int DerivedSetting { get; set; }


    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //var entity = new Entity {
            //    Config = new ConfigDerived { UniversalSetting = 123, DerivedSetting = 456 }
            //};
            //var json = JsonSerializer.Serialize<object>(entity);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
