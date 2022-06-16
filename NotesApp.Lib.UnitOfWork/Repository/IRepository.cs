using System.Linq.Expressions;

namespace NotesApp.Lib.UnitOfWork.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<IList<T>> Get(Expression<Func<T, bool>> predicate = null);
    }
}