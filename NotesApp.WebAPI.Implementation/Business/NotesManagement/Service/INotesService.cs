using NotesApp.WebAPI.Implementation.Business.NotesManagement.Dto;

namespace NotesApp.WebAPI.Implementation.Business.NotesManagement.Service
{
    /// <summary>
    /// Note management interface
    /// </summary>
    public interface INotesService
    {
        /// <summary>
        /// Fetches all the stored notes in the database
        /// </summary>
        /// <returns><see cref="IList{NoteDto}"/></returns>
        Task<IList<NoteDto>> GetAllNotes();

        /// <summary>
        /// Gets one note object by id
        /// </summary>
        /// <param name="noteId">Id of note to be found</param>
        /// <returns><see cref="NoteDto"/></returns>
        Task<NoteDto> GetNoteById(int noteId);

        /// <summary>
        /// Creates a new note
        /// </summary>
        /// <param name="noteToCreate">The note to create</param>
        /// <returns><see cref="NoteResponseDto"/></returns>
        Task<NoteResponseDto> CreateNote(NoteDto noteToCreate);

        /// <summary>
        /// Updates an existing note
        /// </summary>
        /// <param name="noteToUpdate">The note to update</param>
        /// <returns><see cref="NoteResponseDto"/></returns>
        Task<NoteResponseDto> UpdateNote(NoteDto noteToUpdate);

        /// <summary>
        /// Deletes a note by id
        /// </summary>
        /// <param name="noteId">The id of the note to be deleted</param>
        /// <returns><see cref="NoteResponseDto"/></returns>
        Task<NoteResponseDto> DeleteNote(int noteId);

        /// <summary>
        /// Retrieves all notes which have the flag: Important
        /// </summary>
        /// <returns><see cref="IList{NoteDto}"/> of important notes</returns>
        Task<IList<NoteDto>> GetNotesMarkedAsImportant();
    }
}