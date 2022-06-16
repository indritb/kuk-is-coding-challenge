using NotesApp.Lib.UnitOfWork.Repository;
using NotesApp.WebAPI.Implementation.Domain.Entities;

namespace NotesApp.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// Database operations interface for managing notes
    /// </summary>
    public interface INotesRepository : IRepository<Note>
    {

    }
}