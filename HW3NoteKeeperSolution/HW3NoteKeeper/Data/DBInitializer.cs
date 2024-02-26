using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using HW3NoteKeeper.note; // Importing the namespace for the Note entity.

namespace HW3NoteKeeper.Data // Defining the namespace for data-related classes.
{
    public class DBInitializer // Defining a class named DBInitializer.
    {
        /// <summary>
        /// Initializes (seeds) the database with data
        /// </summary>
        /// <remarks>Step 7</remarks>
        public class DbInitializer // Defining a nested class named DbInitializer.
        {
            /// <summary>
            /// Initializes the specified context with data
            /// </summary>
            /// <param name="context">The context.</param>
            public static void Initialize(MyDatabaseContext context) // Method to initialize the database with data.
            {
                // Check to see if there is any data in the note table
                if (context.Note.Any()) // Checking if the note table already contains data.
                {
                    // Note table has data, nothing to do here
                    return; // Exiting the method as the database is already initialized.
                }

                // Create some data
                Note[] notes = // Creating an array of Note objects with sample data.
                [
                    new Note { Summary = "Running grocery list", Details = "Milk, Eggs, Oranges", DateCreated = DateTimeOffset.UtcNow}, // Updating Note object to use DateTimeOffset.
                    new Note { Summary = "Gift supplies notes", Details = "Tape & Wrapping Paper", DateCreated = DateTimeOffset.UtcNow}, // Updating Note object to use DateTimeOffset.
                    new Note { Summary = "Valentine's Day gift ideas", Details = "Chocolate, Diamonds, New Car", DateCreated = DateTimeOffset.UtcNow}, // Updating Note object to use DateTimeOffset.
                    new Note { Summary = "Azure tips", Details = "portal.azure.com is a quick way to get to the portal. Remember double underscore for linux and colon for windows", DateCreated = DateTimeOffset.UtcNow} // Updating Note object to use DateTimeOffset.
                ];

                // Add the data to the in Database
                foreach (Note note in notes) // Iterating through the array of Note objects.
                {
                    context.Note.Add(note); // Adding each Note object to the database context.
                }

                // Commit the changes to the database
                context.SaveChanges(); // Saving changes to the database.

                // The Notes added now are populated with their Ids
                Console.WriteLine("Notes Added:"); // Displaying a message indicating that notes have been added.
                foreach (Note Note in context.Note) // Iterating through the notes in the database context.
                {
                    Console.WriteLine($"\t Id: {Note.ID} Summary: {Note.Summary}"); // Displaying the ID and summary of each note.
                }
            }
        }
    }

    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadFileAsync(string containerName, string fileName, Stream content)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = "application/octet-stream" });
        }
    }

}
