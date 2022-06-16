using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Dto;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Exceptions;
using NotesApp.WebAPI.Implementation.Business.NotesManagement.Service;

namespace NotesApp.WebAPI.Implementation.Business.NotesManagement.Controllers
{
    /// <summary>
    /// Notes management controller
    /// </summary>
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// Instance declaration of <see cref="INotesService"/>
        /// </summary>
        private readonly INotesService _notesService;

        /// <summary>
        /// Initializes a new instance of <see cref="NotesController"/>
        /// </summary>
        /// <param name="notesService"></param>
        public NotesController(INotesService notesService)
        {
            _notesService = notesService ?? throw new ArgumentNullException(nameof(notesService));
        }

        [HttpGet]
        [Route("api/services/rest/notes/list")]
        [ProducesResponseType(typeof(IList<NoteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var notes = await _notesService.GetAllNotes().ConfigureAwait(false);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(message);
            }
        }

        [HttpGet]
        [Route("api/services/rest/notes/important")]
        [ProducesResponseType(typeof(IList<NoteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetImportantNotes()
        {
            try
            {
                var notes = await _notesService.GetNotesMarkedAsImportant().ConfigureAwait(false);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BadRequest(message);
            }
        }

        [HttpGet]
        [Route("api/services/rest/notes/{id:int}")]
        [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNote(int id)
        {
            try
            {
                var note = await _notesService.GetNoteById(id).ConfigureAwait(false);
                return Ok(note);
            }
            catch (NoteNotFoundException nex)
            {
                return NotFound(nex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/services/rest/notes/create")]
        [ProducesResponseType(typeof(NoteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNote([FromBody] NoteDto noteDto)
        {
            try
            {
                var note = await _notesService.CreateNote(noteDto).ConfigureAwait(false);
                return CreatedAtAction(nameof(GetNote), new { id = note.Id}, note);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/services/rest/notes/update")]
        [ProducesResponseType(typeof(NoteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNote([FromBody] NoteDto noteDto)
        {
            try
            {
                var updatedNote = await _notesService.UpdateNote(noteDto).ConfigureAwait(false);
                return Ok(updatedNote);
            }
            catch (NoteNotFoundException nex)
            {
                return NotFound(nex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/services/rest/notes/remove/{id:int}")]
        [ProducesResponseType(typeof(NoteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                var deletedNote = await _notesService.DeleteNote(id).ConfigureAwait(false);
                return Ok(deletedNote);
            }
            catch (NoteNotFoundException nex)
            {
                return NotFound(nex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}