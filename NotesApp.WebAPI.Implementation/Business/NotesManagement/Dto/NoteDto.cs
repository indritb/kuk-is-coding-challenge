using NotesApp.Lib.Shared;
using System.Text.Json.Serialization;

namespace NotesApp.WebAPI.Implementation.Business.NotesManagement.Dto
{
    public class NoteDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}