namespace NotesApp.WebAPI.Implementation.Business.NotesManagement.Exceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(string message) : base(message)
        {

        }
    }
}
