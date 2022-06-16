using System.Text.Json.Serialization;

namespace NotesApp.WebAPI.Implementation.Business.NotesManagement.Dto
{
    public class NoteResponseDto
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
    }
}