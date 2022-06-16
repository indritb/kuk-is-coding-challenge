using NotesApp.Lib.UnitOfWork.Repository;
using NotesApp.WebAPI.Implementation.Domain.Entities;
using NotesApp.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace NotesApp.WebAPI.Implementation.Domain.Repositories
{
    /// <summary>
    /// Database operations for managing notes implementation
    /// </summary>
    public class NotesRepository : Repository<Note>, INotesRepository
    {
        /// <summary>
        /// Initialized a new instance of <see cref="NotesRepository"/>
        /// </summary>
        /// <param name="context">DbContext inherited from base</param>
        public NotesRepository(NotesContext context) : base(context)
        {

        }
    }
}