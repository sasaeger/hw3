namespace HW3NoteKeeper.CustomSettings // Defining the namespace for custom settings.
{
    public class NoteLimits // Defining a class named NoteLimits.
    {
        /// 
        /// <summary>
        /// Gets or sets the maximum number of notes.
        /// </summary>
        /// <value>
        /// The maximum number of notes.
        /// </value>
        /// <remarks>Setting a default max notes if non provided in config to 10</remarks>
        public int MaxNotes { get; set; } = 10; // Declaring a property to store the maximum number of notes with a default value of 10.
    }
}
