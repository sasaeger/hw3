namespace HW3NoteKeeper.Exceptions // Defining the namespace for exception handling.
{
    [Serializable] // Attribute indicating that the class is serializable.
    public class EntityNotFoundException : Exception // Defining a class named EntityNotFoundException which inherits from Exception.
    {
        public string Id { get; } // Declaring a property to store the ID of the entity not found.

        public EntityNotFoundException(string id, string message) : base(message) // Constructor with parameters to initialize the ID and message.
        {
            Id = id; // Assigning the provided ID to the property.
        }
        public EntityNotFoundException() { } // Default constructor.
        public EntityNotFoundException(string message) : base(message) { } // Constructor with message parameter.
        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { } // Constructor with message and inner exception parameters.
        protected EntityNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { } // Constructor for serialization.
    }
}
