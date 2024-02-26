namespace AzureBlobManagedIdentity.Settings
{
    public class StorageAccountSettings : IStorageAccountSettings
    {
        /// <summary>
        /// Defines url for the container end point
        /// </summary>
        public string ContainerEndpoint { get; set; }

        /// <summary>
        /// The tenant id where the storage account resides
        /// </summary>
        public required string TenantId { get; set; }

    }
}
