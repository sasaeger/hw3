using System.ComponentModel.DataAnnotations;

namespace HW3NoteKeeper.DataTransferObjects
{
    public class NoteCreatePayload
    {
        /// <summary>
        /// Gets or sets the Note Id.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the Note Summary.
        /// </summary>
        /// <value>The email Note.</value>
        public required string Summary { get; set; }

        /// <summary>
        /// Gets or sets the Notes Details.
        /// </summary>
        /// <value>The Summary.</value>
        public required String Details { get; set; }

        /// <summary>
        /// Gets or sets the Notes Details.
        /// </summary>
        /// <value>The Summary.</value>
        public required DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Notes Details.
        /// </summary>
        /// <value>The Summary.</value>
        [Required]
        public DateTimeOffset DateModify { get; set; }

        public override string ToString()
        {
            return $"Id=[{ID}] Summary=[{Summary}], Details=[{Details}], DateCreated=[{DateCreated}],  DateModify=[{DateModify}]";
        }
    }

}
