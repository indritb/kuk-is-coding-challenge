using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NotesApp.Lib.UnitOfWork.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext DbContext { get; set; }

        /// <summary>
        /// Sets the object to be queried with the DbSet the repository is working with
        /// </summary>
        private IQueryable<T> Queryable => SetQuery<T>();

        /// <summary>
        /// Gets the <see cref="DbSet{TEntity}"/> for the entity without tracking
        /// </summary>
        /// <typeparam name="K">Type of entity for the DbSet</typeparam>
        /// <returns><see cref="DbSet{TEntity}"/></returns>
        private IQueryable<K> SetQuery<K>() where K : class
        {
            return DbContext.Set<K>().AsNoTracking();
        }

        /// <summary>
        /// Initialization class
        /// </summary>
        /// <param name="context">The database context</param>
        public Repository(DbContext context)
        {
            DbContext = context;
        }

        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">Entity to be created</param>
        /// <returns>The created entity</returns>
        public async Task<T> Create(T entity)
        {
            try
            {
                var result = await DbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
                result.State = EntityState.Added;
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets the <see cref="EntityState"/> of the entity to modified
        /// for the EF Core ChangeTracker to pickup
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>The updated entity</returns>
        public async Task<T> Update(T entity)
        {
            try
            {
                var result = DbContext.Set<T>().Update(entity);
                result.State = EntityState.Modified;
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        /// <returns>The deleted entity</returns>
        public async Task<T> Delete(T entity)
        {
            try
            {
                var result = DbContext.Set<T>().Remove(entity);
                result.State = EntityState.Deleted;
                await DbContext.SaveChangesAsync().ConfigureAwait(false);
                result.State = EntityState.Detached;
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a list of the queried entity without its relationships
        /// </summary>
        /// <param name="predicate">Method representation with search criteria</param>
        /// <returns><see cref="IList{T}"/> of queried entities</returns>
        public async Task<IList<T>> Get(Expression<Func<T, bool>> predicate = null)
        {
            return await GetQueryFromPredicate(predicate).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a single entity from the database based on predicate
        /// </summary>
        /// <param name="predicate">Method representation with search criteria</param>
        /// <returns>Entity which is queried</returns>
        public Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetQueryFromPredicate(predicate).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Builds the query with the DbSet based on the predicate
        /// </summary>
        /// <param name="predicate">Method representation with search criteria</param>
        /// <returns><see cref="IQueryable{T}"/></returns>
        private IQueryable<T> GetQueryFromPredicate(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? Queryable.Where(predicate) : Queryable;
        }
    }
}
