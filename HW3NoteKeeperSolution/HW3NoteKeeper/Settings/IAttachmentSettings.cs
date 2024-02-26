namespace AzureBlobManagedIdentity.Settings
{
    /// <summary>
    /// Defines the Attachment settings 
    /// </summary>
    public interface IAttachmentSettings
    {
        /// <summary>
        /// The Attachment storage container name
        /// </summary>
        public string AttachmentContainerName { get; set; }
    }
}
