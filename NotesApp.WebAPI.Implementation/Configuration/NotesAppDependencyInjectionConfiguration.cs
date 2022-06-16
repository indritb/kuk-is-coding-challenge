using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Lib.UnitOfWork.Configuration;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Service;
using NotesApp.WebAPI.Implementation.Domain;
using NotesApp.WebAPI.Implementation.Domain.Repositories;
using NotesApp.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace NotesApp.WebAPI.Implementation.Configuration
{
    public static class NotesAppDependencyInjectionConfiguration
    {
        public static void SetupNotesDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            SetupDatabase(services, configuration);
            SetupRepositories(services);
            SetupServices(services);
        }

        private static void SetupRepositories(IServiceCollection services)
        {
            services.AddTransient<INotesRepository, NotesRepository>();
        }

        private static void SetupServices(IServiceCollection services)
        {
            services.AddTransient<INotesService, NotesService>();
        }

        /// <summary>
        /// Setup of database connection
        /// </summary>
        /// <param name="services">Dependency injection container</param>
        /// <param name="configuration">Configuration data from appsettings</param>
        private static void SetupDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.SetupDatabase<NotesContext>(configuration, "Notes", ServiceLifetime.Scoped).ConfigureAwait(false);
        }
    }
}