using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NotesApp.Lib.UnitOfWork.Configuration
{
    public static class DatabaseConfiguration
    {
        /// <summary>
        /// Set up the database configuration for the defined context
        /// </summary>
        /// <typeparam name="T">DbContext class</typeparam>
        /// <param name="services">IoC container with registered services</param>
        /// <param name="configuration">Configuration data from appsettings</param>
        /// <param name="connectionStringName">Name of connection string for the database</param>
        /// <param name="serviceLifetime"><see cref="ServiceLifetime.Singleton"/> or <see cref="ServiceLifetime.Scoped"/> or <see cref="ServiceLifetime.Transient"/></param>
        /// <returns>Async <see cref="Task"/></returns>
        /// <exception cref="ArgumentException">When connection string is not available in appsettings</exception>
        public static async Task SetupDatabase<T>(this IServiceCollection services, IConfiguration configuration, string connectionStringName,
            ServiceLifetime serviceLifetime) where T : DbContext
        {
            var connectionStringsSection = configuration.GetSection("ConnectionStrings").GetChildren();
            var connectionString = connectionStringsSection.FirstOrDefault(c => string.Equals(c.Key, connectionStringName, 
                StringComparison.CurrentCultureIgnoreCase));


            if (connectionString != null && !string.IsNullOrEmpty(connectionString.Value))
                services.AddDbContext<T>(options => options.UseSqlServer(connectionString.Value, null), serviceLifetime);
            else
                throw new ArgumentException($"The connection string was not provided. Please check your configuration file.");

            try
            {
                using var serviceProvider = services.BuildServiceProvider();

                if(serviceProvider != null)
                {
                    var context = serviceProvider.GetService(typeof(T));

                    if(context != null)
                    {
                        await (context as T)?.Database.MigrateAsync();
                        await serviceProvider.DisposeAsync().ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}