using ExceptionHandling.DataTransferObjects; // Importing the namespace for data transfer objects related to exception handling.
using HW3NoteKeeper.Exceptions;
using Microsoft.AspNetCore.Mvc; // Importing the namespace for ASP.NET Core MVC.
using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations.

namespace ExceptionHandling.Controllers // Defining the namespace for controllers handling exceptions.
{
    [Route("api/[controller]")] // Attribute to specify the route for the controller.
    [ApiController] // Attribute to specify that the class is an API controller.
    public class ExceptionController : Controller // Defining a class named ExceptionController which inherits from Controller.
    {
        /// <summary>
        /// Exception handling
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The ID is returned</returns>
        /// <remarks>
        /// 0 Causes a System.Exception
        /// 1 Causes a Argument Exception
        /// 2 Causes an EntityNotFoundException
        /// 3 Causes an BadRequest String Result
        /// 4 Causes an NotFound String result
        /// 5 Causes a NotFound with PublicErrorResponse Result
        /// > 5 Success and id entered is returned back
        /// </remarks>
        [HttpGet("{id}")] // Attribute to specify the HTTP GET method and route.
        public IActionResult Get(int id) // Method to handle HTTP GET requests.
        {
            if (id == 0) // Checking if the ID is 0.
            {
                throw new System.Exception(); // Throwing a generic exception.
            }

            if (id == 1) // Checking if the ID is 1.
            {
                throw new System.ArgumentException("Valid values are 10, 11 and 12", nameof(id)); // Throwing an argument exception.
            }

            if (id == 2) // Checking if the ID is 2.
            {
                throw new EntityNotFoundException(id.ToString(), "Could not find the entity!"); // Throwing an entity not found exception.
            }

            if (id == 3) // Checking if the ID is 3.
            {
                return BadRequest("Bad request error from controller using BadRequest() method"); // Returning a bad request response.
            }

            if (id == 4) // Checking if the ID is 4.
            {
                return NotFound("Not found from error from controller using NotFound() method"); // Returning a not found response.
            }

            if (id == 5) // Checking if the ID is 5.
            {
                PublicErrorResponse publicErrorResponse = new() // Creating a new PublicErrorResponse object.
                {
                    Number = ErrorNumberConstants.BadParameterError, // Setting the error number.
                    Description = "BadParameterError: Handled in controller" // Setting the error description.
                };
                return BadRequest(publicErrorResponse); // Returning a bad request response with the public error response.
            }

            return new ObjectResult(id); // Returning an object result with the provided ID.
        }

        /// <summary>
        /// Input validation. The Max Length Supported is 10
        /// </summary>
        /// <param name="inputPayload">The input payload</param>
        /// <returns>The input payload</returns>
        [HttpPost] // Attribute to specify the HTTP POST method.
        public IActionResult Post([FromBody] InputPayload inputPayload) // Method to handle HTTP POST requests.
        {
            return this.StatusCode(StatusCodes.Status201Created, inputPayload); // Returning a status code 201 (Created) with the input payload.
        }

        public class InputPayload // Defining a nested class named InputPayload.
        {
            [MaxLength(10)] // Specifying the maximum length of the string property.
            public string? Summary { get; set; } // Declaring a property to store the summary with optional nullability.
        }
    }
}
