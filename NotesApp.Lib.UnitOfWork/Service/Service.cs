using Microsoft.EntityFrameworkCore;
using NotesApp.Lib.UnitOfWork.UnitOfWork;

namespace NotesApp.Lib.UnitOfWork.Service
{
    public class Service<TContext> : IService where TContext : DbContext
    {
        public IUnitOfWork<TContext> UoW { get; }

        public Service(IUnitOfWork<TContext> uoW)
        {
            UoW = uoW;
        }
    }
}