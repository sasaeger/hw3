using HW3NoteKeeper.Data; // Importing the namespace for accessing the data-related classes.
using HW3NoteKeeper.note; // Importing the namespace for accessing the note-related classes.
using HW3NoteKeeper.CustomSettings; // Importing the namespace for accessing custom settings.
using Microsoft.AspNetCore.Mvc; // Importing the namespace for using ASP.NET Core MVC features.
using HW3NoteKeeper.DataTransferObjects; // Importing the namespace for accessing data transfer objects.
using System.Net; // Importing the namespace for using HTTP status codes.

// Defining the namespace for the controller
namespace HW3NoteKeeper.Controllers
{
    [Route("api/[controller]")] // Specifies the route pattern for the controller
    [ApiController] // Indicates that the controller is an API controller
    public class NotesController(MyDatabaseContext context, NoteLimits notelimits) : ControllerBase // Defining a class named NotesController which inherits from ControllerBase
    {
        private readonly MyDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context)); // Declaring a private field to hold an instance of MyDatabaseContext
        private readonly NoteLimits _notelimits = notelimits ?? throw new ArgumentNullException(nameof(notelimits)); // Declaring a private field to hold an instance of NoteLimits

        /// <summary>
        /// Returns all notes from the Azure SQL Database note table
        /// </summary>
        /// <returns>Gets all notes in the note DB</returns>
        [HttpGet] // Specifies that this method responds to HTTP GET requests
        [ProducesResponseType(typeof(List<Note>), 200)] // Specifies the response type and status code if successful
        [ProducesResponseType(typeof(string), 400)] // Specifies the response type and status code for bad requests
        [ProducesResponseType(typeof(void), 500)] // Specifies the response type and status code for server errors
        [Route("api/v2/Notes")] // Specifies the route pattern for this action
        public IActionResult GetAllNotes()
        {
            // Accessing the Note entities from the database context
            IQueryable<Note> query = _context.Note;
            List<Note> notes = [.. query]; // Retrieving all notes from the database

            return new ObjectResult(notes); // Returning the list of notes as an HTTP response
        }

        //Create
        /// <summary>
        /// Creates a new note from the Azure SQL Database note table
        /// </summary>
        /// <returns>Creates a new note from the Azure SQL Database note table</returns>
        [HttpPost] // Specifies that this method responds to HTTP POST requests
        [ProducesResponseType(typeof(Note), 201)] // Specifies the response type and status code if successful
        [ProducesResponseType(typeof(string), 400)] // Specifies the response type and status code for bad requests
        [ProducesResponseType(typeof(void), 403)] // Specifies the response type and status code for forbidden requests
        [ProducesResponseType(typeof(void), 500)] // Specifies the response type and status code for server errors
        [Route("api/v2/Notes")] // Specifies the route pattern for this action
        public IActionResult CreateNote([FromBody] Note noteInput) // Defines a method to create a new note
        {
            if (!ModelState.IsValid) // Checking if the model state is valid
            {
                return BadRequest(ModelState); // Returning a bad request response with the model state if it's not valid
            }

            // Check if the maximum number of notes has been reached
            if (_context.Note.Count() >= _notelimits.MaxNotes)
            {
                return StatusCode(403, "Maximum number of notes reached."); // Returning a forbidden response if the maximum number of notes has been reached
            }

            // Creating a new Note object from the input model
            Note newNote = new()
            {
                Summary = noteInput.Summary,
                Details = noteInput.Details,
                DateCreated = DateTimeOffset.UtcNow, // Using DateTimeOffset
                DateModify= DateTimeOffset.UtcNow  // Using DateTimeOffset
            };

            // Adding the new note to the database
            _context.Note.Add(newNote);
            _context.SaveChanges(); // Saving changes to the database

            // Returning the newly created note
            return CreatedAtAction(nameof(GetAllNotes), newNote); // Returning an HTTP 201 response with the newly created note
        }

        /// <summary>
        /// Updates a note from the Azure SQL Database note table.
        /// </summary>
        /// <param name="id">The identifier of the note.</param>
        /// <param name="noteUpdatePayload">The note payload.</param>
        /// <returns>
        /// An IActionResult indicating HTTP 204 No Content if the update is successful,
        /// HTTP 201 Created if a new note is created,
        /// or BadRequest if the input is not valid.
        /// </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)] // Specifies the response status code if successful
        [ProducesResponseType((int)HttpStatusCode.BadRequest)] // Specifies the response status code for bad requests
        [ProducesResponseType((int)HttpStatusCode.NotFound)] // Specifies the response status code if the resource is not found
        [ProducesResponseType(typeof(void), 500)] // Specifies the response type and status code for server errors
        [Route("api/v2/Notes/{id}")] // Specifies the route pattern for this action
        [HttpPatch] // Specifies that this method responds to HTTP PATCH requests
        public IActionResult UpdateNote(Guid id, [FromBody] NoteUpdatePayload noteUpdatePayload) // Defines a method to update a note
        {
            if (!ModelState.IsValid) // Checking if the model state is valid
            {
                return BadRequest(ModelState); // Returning a bad request response with the model state if it's not valid
            }

            // Finding the note entity by its GUID
            Note noteEntity = _context.Note.FirstOrDefault(n => n.ID == id);
            if (noteEntity == null)
            {
                return NotFound(); // Or handle the null case appropriately
            }

            // Updating the existing note entity
            noteEntity.Summary = noteUpdatePayload.Summary;
            noteEntity.Details = noteUpdatePayload.Details;
            noteEntity.DateModify = DateTimeOffset.UtcNow; // Updating with DateTimeOffset

            // Saving the changes to the note DB
            _context.SaveChanges(); // Saving changes to the database

            // Returning No Content to indicate successful update
            return NoContent(); // Returning an HTTP 204 response indicating success with no content
        }

        /// <summary>
        /// Deletes a note by the ID from the Azure SQL Database note table.
        /// </summary>
        /// <param name="id">The identifier of the note to be deleted.</param>
        /// <returns>
        /// An IActionResult indicating HTTP 204 No Content if the deletion is successful,
        /// or HTTP 404 Not Found if the note could not be found.
        /// </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)] // Specifies the response status code if successful
        [ProducesResponseType((int)HttpStatusCode.NotFound)] // Specifies the response status code if the resource is not found
        [ProducesResponseType(typeof(void), 500)] // Specifies the response type and status code for server errors
        [Route("api/v2/Notes/{id}")] // Specifies the route pattern for this action
        [HttpDelete] // Specifies that this method responds to HTTP DELETE requests
        public IActionResult DeleteNote(Guid id) // Defines a method to delete a note
        {
            // Finding the note entity by its GUID
            Note noteEntity = _context.Note.FirstOrDefault(n => n.ID == id);

            // If the note entity was not found, return Not Found
            if (noteEntity == null)
            {
                return NotFound(); // Returning a not found response
            }

            // Removing the note from the context
            _context.Note.Remove(noteEntity);
            _context.SaveChanges(); // Saving changes to the database

            // Returning No Content to indicate successful deletion
            return NoContent(); // Returning an HTTP 204 response indicating success with no content
        }

        /// <summary>
        /// Retrieves a note by the ID from the Azure SQL Database note table.
        /// </summary>
        /// <param name="noteId">The identifier of the note to retrieve.</param>
        /// <returns>
        /// An IActionResult containing the note if found,
        /// or HTTP 404 Not Found if the note could not be found.
        /// </returns>
        [ProducesResponseType(typeof(Note), (int)HttpStatusCode.OK)] // Specifies the response type and status code if successful
        [ProducesResponseType((int)HttpStatusCode.NotFound)] // Specifies the response status code if the resource is not found
        [ProducesResponseType(typeof(void), 500)] // Specifies the response type and status code for server errors
        [HttpGet] // Specifies that this method responds to HTTP GET requests
        [Route("api/v2/Notes/{noteId}")] // Specifies the route pattern for this action
        public IActionResult GetNoteById(Guid noteId) // Defines a method to retrieve a note by its ID
        {
            // Finding the note entity by its ID
            Note noteEntity = _context.Note.FirstOrDefault(n => n.ID == noteId);

            // If the note entity was not found, return Not Found
            if (noteEntity == null)
            {
                return NotFound(); // Returning a not found response
            }

            // Returning the note entity
            return Ok(noteEntity); // Returning an HTTP 200 response with the retrieved note
        }
    }
}
