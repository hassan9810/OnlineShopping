
namespace Store.Helper
{
    public class ResponseHelper
    {
        public ResponseDto RetrievedSuccessfully(dynamic result, string message)
        {
            var ResponseDto = new ResponseDto()
            {
                Result = result,
                Message = message,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
            return ResponseDto;
        }
        
        /// <summary>
        /// Creates a response for a "Not Found" scenario.
        /// </summary>
        /// <param name="message">The message indicating the reason for not finding the object.</param>
        /// <returns>A response object indicating a failure to find the object with the provided message.</returns>
        public ResponseDto NotFound(string message)
        {
            var ResponseDto = new ResponseDto
            {
                Message = message,
                StatusCode = System.Net.HttpStatusCode.NotFound,
                IsSuccess = false,
            };
            return ResponseDto;
        }

        /// <summary>
        /// Creates a response for a "Already Exists" scenario.
        /// </summary>
        /// <param name="message">The message indicating the existence of the object before.</param>
        /// <returns>A response object indicating the existence of the object before with the provided message.</returns>
        public ResponseDto AlreadyExists(string message)
        {
            var ResponseDto = new ResponseDto
            {
                Message = message,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                IsSuccess = false,

            };
            return ResponseDto;
        }

        /// <summary>
        /// Creates a success response where an operation saved successfully.
        /// </summary>
        /// <param name="message">The message indicating the success of the operation.</param>
        /// <returns>A response object indicating a successful save operation with the provided message.</returns>
        public ResponseDto SavedSuccessfully(string message)
        {
            var ResponseDto = new ResponseDto
            {
                Message = message,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true

            };
            return ResponseDto;
        }

        /// <summary>
        /// Creates a success response with data where an operation saved successfully.
        /// </summary>
        /// <param name="result">The data to include in the response.</param>
        /// <param name="message">The message indicating the success of the operation.</param>
        /// <returns>A response object indicating a successful save operation with the provided message.</returns>
        public ResponseDto SavedSuccessfully(dynamic result, string message)
        {
            var ResponseDto = new ResponseDto
            {
                Result = result,
                Message = message,
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true
            };
            return ResponseDto;
        }

        /// <summary>
        /// Creates a response for a scenario where an operation failed to save changes.
        /// </summary>
        /// <param name="message">The message indicating the reason for the failed save operation.</param>
        /// <returns>A response object indicating a failure to save changes with the provided message.</returns>
        public ResponseDto FailedToSave(string message)
        {
            var ResponseDto = new ResponseDto
            {
                Message = message,
                StatusCode = System.Net.HttpStatusCode.ExpectationFailed,
                IsSuccess = false
            };
            return ResponseDto;
        }

        /// <summary>
        /// Creates a response for an exception scenario.
        /// </summary>
        /// <returns>A response object indicating an exception scenario with a predefined error message.</returns>
        public ResponseDto Exception()
        {
            var ResponseDto = new ResponseDto
            {
                StatusCode = System.Net.HttpStatusCode.ExpectationFailed,
                Message = "An error occurred. Please contact the system administrator.",
                IsSuccess = false

            };
            return ResponseDto;
        }
    }
}
