// Import necessary namespaces for Azure Blob Storage operations, logging, system I/O, and asynchronous programming.
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;


// Define the namespace this class belongs to, encapsulating it within a specific project structure.
namespace AzureBlobManagedIdentity.Repositories
{
    // Define the StorageRepository class that provides functionalities to interact with Azure Blob Storage.
    public class StorageRepository
    {
        // Declare a BlobServiceClient instance to interact with the Azure Blob Storage service.
        private readonly BlobServiceClient _blobServiceClient;
        
        // Declare an ILogger instance for logging purposes within the repository.
        private readonly ILogger<StorageRepository> _logger;

        // Constructor for StorageRepository with dependency injection for BlobServiceClient and ILogger.
        public StorageRepository(BlobServiceClient blobServiceClient, ILogger<StorageRepository> logger)
        {
            // Initialize the BlobServiceClient with the injected instance.
            _blobServiceClient = blobServiceClient;
            
            // Initialize the ILogger with the injected instance.
            _logger = logger;
        }

        // Asynchronous method to upload a file to a specified container and blob within Azure Blob Storage.
        public async Task UploadFile(string containerName, string fileName, Stream fileStream, string contentType)
        {
            // Ensure the container exists or create a new one, and get a reference to it.
            var containerClient = GetOrCreateContainer(containerName);
           
            // Get a reference to a blob object within the container.
            var blobClient = containerClient.GetBlobClient(fileName);
            
            // Upload the file stream to the blob with specified content type headers.
            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
        }

        // Asynchronous method to download a blob's content from a specified container and blob name.
        public async Task<(MemoryStream, string)> GetFileAsync(string containerName, string fileName)
        {
            // Retrieve a reference to the specified container.
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            
            // Retrieve a reference to the specified blob within the container.
            var blobClient = containerClient.GetBlobClient(fileName);
           
            // Download the blob's content.
            var downloadInfo = await blobClient.DownloadAsync();

            // Create a new memory stream to hold the downloaded content.
            var memoryStream = new MemoryStream();
            
            // Copy the downloaded content to the memory stream.
            await downloadInfo.Value.Content.CopyToAsync(memoryStream);
            
            // Reset the memory stream's position for subsequent read operations.
            memoryStream.Position = 0;

            // Return the memory stream containing the blob's content and the content type of the blob.
            return (memoryStream, downloadInfo.Value.ContentType);
        }

        // Asynchronous method to delete a specified blob within a specified container.
        public async Task DeleteFile(string containerName, string fileName)
        {
            // Retrieve a reference to the specified container.
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            
            // Retrieve a reference to the specified blob within the container.
            var blobClient = containerClient.GetBlobClient(fileName);
            
            // Delete the blob if it exists.
            await blobClient.DeleteIfExistsAsync();
        }

        // Asynchronous method to list all blob names within a specified container.
        public async Task<IEnumerable<string>> GetListOfBlobs(string containerName)
        {
            // Retrieve a reference to the specified container.
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            
            // Asynchronously retrieve all blobs within the container.
            var blobs = containerClient.GetBlobsAsync();
            
            // Initialize a list to hold the names of all blobs.
            var blobNames = new List<string>();

            // Iterate over each blob in the container.
            await foreach (var blobItem in blobs)
            {
                // Add the name of each blob to the list.
                blobNames.Add(blobItem.Name);
            }

            // Return the list containing all blob names within the container.
            return blobNames;
        }

        // Private helper method to get or create a container by name, ensuring it exists before performing blob operations.
        private BlobContainerClient GetOrCreateContainer(string containerName)
        {
            // Retrieve a reference to the specified container.
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            
            // Create the container if it does not exist, with public access set to Blob level.
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            
            // Return the container client reference.
            return containerClient;
        }
    }
}
