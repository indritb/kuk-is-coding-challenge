using NotesApp.Lib.Shared;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Dto;
using NotesApp.WebAPI.Implementation.Domain.Entities;

namespace NotesApp.WebAPI.Implementation.Business.NotesManagement.Converter
{
    public static class NotesConverter
    {
        public static Note DtoToEntity(this NoteDto noteDto)
        {
            return new Note
            {
                Title = noteDto.Title,
                Description = noteDto.Description,
                Type = (NoteType)Enum.Parse(typeof(NoteType), noteDto.Type, true)
            };
        }

        public static NoteDto EntityToDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                Type = note.Type.ToString()
            };
        }
    }
}
