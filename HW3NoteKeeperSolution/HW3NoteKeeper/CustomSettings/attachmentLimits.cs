namespace HW3NoteKeeper.CustomSettings
{
    public class AttachmentLimits // Defining a class named MaxAttachments.
    {
        /// 
        /// <summary>
        /// Gets or sets the maximum number of Attachments.
        /// </summary>
        /// <value>
        /// The maximum number of notes.
        /// </value>
        /// <remarks>Setting a default Max Attachments if non provided in config to 3</remarks>
        public int MaxAttachments { get; set; } = 3; // Declaring a property to store the maximum number of Attachments with a default value of 3.
    }
}
