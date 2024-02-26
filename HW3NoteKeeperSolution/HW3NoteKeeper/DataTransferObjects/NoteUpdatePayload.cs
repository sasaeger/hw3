using System.ComponentModel.DataAnnotations;


namespace HW3NoteKeeper.DataTransferObjects
{
    public class NoteUpdatePayload
    {
        /// <summary>
        /// Gets the Note Id.
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

        ///// <summary>
        ///// Gets or sets the Notes Date Created.
        ///// </summary>
        ///// <value>The Summary.</value>
        //public required DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Notes Date Modify.
        /// </summary>
        /// <value>The Summary.</value>
        [Required]
        public DateTime DateModify { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// 

        public override string ToString()
        {
            return $"Id=[{ID}] Summary=[{Summary}], Details=[{Details}],  DateModify=[{DateModify}]";
            //return $"Id=[{ID}] Summary=[{Summary}], Details=[{Details}], DateCreated=[{DateCreated}],  DateModify=[{DateModify}]";
        }
    }
}
