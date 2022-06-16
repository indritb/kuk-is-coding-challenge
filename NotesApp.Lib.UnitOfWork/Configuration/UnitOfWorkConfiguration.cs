using Microsoft.Extensions.DependencyInjection;
using NotesApp.Lib.UnitOfWork.Repository;
using NotesApp.Lib.UnitOfWork.UnitOfWork;

namespace NotesApp.Lib.UnitOfWork.Configuration
{
    public static class UnitOfWorkConfiguration
    {
        /// <summary>
        /// Add the services to the dependency injection container
        /// </summary>
        /// <param name="services"></param>
        public static void SetupUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}