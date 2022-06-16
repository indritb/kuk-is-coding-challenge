using Microsoft.EntityFrameworkCore;

namespace NotesApp.Lib.UnitOfWork.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        T Repository<T>() where T : class;
    }
}