namespace ExceptionHandling.DataTransferObjects
{
    /// <summary>
    /// Defines the public facing error response
    /// </summary>
    public class PublicErrorResponse
    {
        /// <summary>
        /// The error number associated with the error
        /// </summary>
        /// <value>
        /// The error number associated with the error
        /// </value>
        public int Number { get; set; }

        /// <summary>
        /// The description of the error
        /// </summary>
        /// <value>
        /// The description of the error
        /// </value>
        public string? Description { get; set; }
    }
}
