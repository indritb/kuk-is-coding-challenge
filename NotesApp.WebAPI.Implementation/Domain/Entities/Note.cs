using NotesApp.Lib.Shared;
using NotesApp.WebAPI.Implementation.Domain.Interfaces;

namespace NotesApp.WebAPI.Implementation.Domain.Entities
{
    public class Note : ITrackable
    {
        private DateTime _createdAt;
        private DateTime _lastUpdatedAt;
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public NoteType Type { get; set; }
        public DateTime CreatedAt => _createdAt;
        public DateTime LastUpdatedAt => _lastUpdatedAt;

        public bool IsTrackable()
        {
            return true;
        }
    }
}
