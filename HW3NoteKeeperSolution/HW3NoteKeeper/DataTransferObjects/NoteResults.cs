

namespace HW3NoteKeeper.DataTransferObjects
{
    public class NoteResults
    {

        /// <summary>
        /// Gets the Note Id.
        /// </summary>
        /// <value>The Id.</value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the Summary
        /// </summary>
        /// <value>Summary.</value>
        public string? Summary { get; set; }

        /// <summary>
        /// Gets or sets the Details
        /// </summary>
        /// <value>Details.</value>
        public string? Details { get; set; }

        /// <summary>
        /// Gets or sets the Date Created 
        /// </summary>
        /// <value>Date Created.</value>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Date Modified 
        /// </summary>
        /// <value>Date Modified.</value>
        public DateTime? DateModify { get; set; }

        public override string ToString()
        {
            return $"Id=[{ID}] Summary=[{Summary}], Details=[{Details}], DateCreated=[{DateCreated}],  DateModify=[{DateModify}]";
        }
    }
}
