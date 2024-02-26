namespace AzureBlobManagedIdentity.Settings
{
    public interface IStorageAccountSettings
    {
        /// <summary>
        /// Defines url for the container end point
        /// </summary>
        public string ContainerEndpoint { get; set; }

        /// <summary>
        /// The tenant id where the storage account resides
        /// </summary>
        public string TenantId { get; set; }
    }
}