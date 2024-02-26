namespace AzureBlobManagedIdentity.Settings
{
    /// <summary>
    /// Implements the Attachment settings 
    /// </summary>
    public class AttachmentSettings : IAttachmentSettings
    {
        /// <summary>
        /// The Attachment storage container name
        /// </summary>
        public required string AttachmentContainerName { get; set; }
    }
}
