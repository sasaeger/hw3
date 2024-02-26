using System.ComponentModel.DataAnnotations.Schema; // Importing the namespace for data annotations schema.
using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations.

// Defining the namespace for the Note entity
namespace HW3NoteKeeper.note
{
    /// <summary>
    /// The Note entity.
    /// </summary>
    /// <remarks> </remarks>
    public class Note // Defining a class named Note.
    {
        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key] // Specifies that this property is a primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies that the database generates a value for this property
        public Guid ID { get; set; } // Declaring a property to store the note identifier.

        /// <summary>
        /// Gets or sets the note summary.
        /// </summary>
        /// <value>The note summary.</value>
        [Required] // Specifies that this property is required
        [StringLength(60)] // Specifies the maximum length of the string
        public string Summary { get; set; } // Declaring a property to store the note summary.

        /// <summary>
        /// Gets or sets the notes details.
        /// </summary>
        /// <value>The note details.</value>
        [Required] // Specifies that this property is required
        [StringLength(1024)] // Specifies the maximum length of the string
        public string Details { get; set; } // Declaring a property to store the note details.

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies that the database generates a value for this property
        public DateTimeOffset DateCreated { get; set; } // Declaring a property to store the created date.

        /// <summary>
        /// Gets or sets the Modify date.
        /// </summary>
        /// <value>The Modify date.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies that the database generates a value for this property
        public DateTimeOffset? DateModify { get; set; } // Declaring a property to store the modify date.
    }
}
