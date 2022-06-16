using NotesApp.Lib.Shared;
using NotesApp.Lib.UnitOfWork.Service;
using NotesApp.Lib.UnitOfWork.UnitOfWork;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Converter;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Dto;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Exceptions;
using NotesApp.WebAPI.Implementation.Domain;
using NotesApp.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace NotesApp.WebAPI.Implementation.Business.NotesManagement.Service
{
    public class NotesService : Service<NotesContext>, INotesService
    {
        private readonly INotesRepository _notesRepository;

        public NotesService(IUnitOfWork<NotesContext> uoW) : base(uoW)
        {
            _notesRepository = uoW.Repository<INotesRepository>();
        }

        public async Task<NoteResponseDto> CreateNote(NoteDto noteToCreate)
        {
            try
            {
                var created = await _notesRepository.Create(noteToCreate.DtoToEntity());
                return new NoteResponseDto { Id = created.Id, Message = "Create OK" };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<NoteResponseDto> DeleteNote(int noteId)
        {
            try
            {
                var noteToDelete = await _notesRepository.GetFirstOrDefault(x => x.Id.Equals(noteId)).ConfigureAwait(false);

                if (noteToDelete == null)
                    throw new NoteNotFoundException($"Could not find note with id {noteId}");

                var deleted = await _notesRepository.Delete(noteToDelete).ConfigureAwait(false);
                return new NoteResponseDto { Id = deleted.Id, Message = "Delete OK" };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IList<NoteDto>> GetAllNotes()
        {
            try
            {
                var result = new List<NoteDto>();

                var storedNotes = await _notesRepository.Get().ConfigureAwait(false);

                foreach (var item in storedNotes)
                {
                    result.Add(item.EntityToDto());
                }

                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<NoteDto> GetNoteById(int noteId)
        {
            try
            {
                var note = await _notesRepository.GetFirstOrDefault(x => x.Id.Equals(noteId)).ConfigureAwait(false);
                if (note == null)
                    throw new NoteNotFoundException($"Could not find note with id {noteId}");

                return note.EntityToDto();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IList<NoteDto>> GetNotesMarkedAsImportant()
        {
            try
            {
                var result = new List<NoteDto>();

                var storedNotes = await _notesRepository.Get(x => x.Type.Equals(NoteType.Important)).ConfigureAwait(false);

                foreach (var item in storedNotes)
                {
                    result.Add(item.EntityToDto());
                }

                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<NoteResponseDto> UpdateNote(NoteDto noteToUpdate)
        {
            try
            {
                var storedNote = await _notesRepository.GetFirstOrDefault(x => x.Id.Equals(noteToUpdate.Id)).ConfigureAwait(false);
                if (storedNote == null)
                    throw new ArgumentException($"Could not find note with id {noteToUpdate.Id}");

                storedNote.Title = noteToUpdate.Title;
                storedNote.Description = noteToUpdate.Description;
                storedNote.Type = (NoteType)Enum.Parse(typeof(NoteType), noteToUpdate.Type, true);

                var updated = await _notesRepository.Update(storedNote);

                return new NoteResponseDto { Id = updated.Id, Message = "Update OK" };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}