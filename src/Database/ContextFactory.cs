using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database
{
    internal class ContextFactory: IDesignTimeDbContextFactory<ShowroomContext>
    {
        public ShowroomContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder();
            string connectionString = GetConnectionString();

            ConfigureDbContextOptionsBuilder(builder, connectionString);

            return new ShowroomContext(builder.Options);
        }

        private static string GetConnectionString()
        {
            Console.WriteLine(AppContext.BaseDirectory);
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "..\\..\\..\\..\\WebApp")
                .AddJsonFile("appsettings.json", false, true);

            var config = builder.Build();

            return config.GetConnectionString(DbHelper.ConnectionStringName);
        }

        private static DbContextOptionsBuilder ConfigureDbContextOptionsBuilder(DbContextOptionsBuilder builder,
            string connectionString)
        {
            return builder
                .UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        }
    }
}