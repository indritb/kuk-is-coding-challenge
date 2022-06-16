using Microsoft.EntityFrameworkCore;

namespace NotesApp.Lib.UnitOfWork.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
        private TContext Context { get; }
        private IServiceProvider ServiceProvider { get; }

        public UnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            Context = context ?? throw new InvalidOperationException(nameof(context));
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Returns the repository class
        /// </summary>
        /// <typeparam name="T">Inherited repository class</typeparam>
        /// <returns>The initialized repository class</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Repository<T>() where T : class
        {
            var repository = ServiceProvider.GetService(typeof(T));

            if (repository is null)
                throw new InvalidOperationException($"Repository {typeof(T).Name} was not found.");

            return repository as T;
        }
    }
}