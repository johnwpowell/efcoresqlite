using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Pwned.Core.Data;
using Pwned.Core.Services;

namespace Pwned.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //todo: take in operations in the args and do stuff
            // pwned import file.txt
            // pwned query abxdfddfddfd

            // get configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appSettings.json", false, true)
                .Build();

            var connectionString = configuration.GetConnectionString("Pwned");

            // setup DI
            var serviceCollection = new ServiceCollection()
                .AddEntityFrameworkSqlite()
                .AddDbContext<PwnedDbContext>(x => x.UseSqlite(connectionString))
                .AddLogging()
                .AddScoped<IPasswordService, PasswordService>()
                .BuildServiceProvider();

            var logger = serviceCollection
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug)
                .CreateLogger<Program>();

            logger.LogDebug("Starting...");
            logger.LogDebug($"Connection string: {connectionString}");
            
            // if you want to, you can create the database
            var context = serviceCollection.GetService<PwnedDbContext>();
            context.Database.EnsureCreated();

            //do the actual work here
            var service = serviceCollection.GetService<IPasswordService>();

            // if query do this
            var password = service.GetPasswordAsync("abcdefg").GetAwaiter().GetResult();
            logger.LogDebug($"Result: {password}");

            logger.LogDebug("All done!");
        }
    }
}
