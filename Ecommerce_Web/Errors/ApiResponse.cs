namespace Ecommerce_Web.Errors
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            StatusCode = statusCode;
        }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            string message = string.Empty; 
            switch (statusCode)
            {
                case 400:
                    message = "A Bad Request You Have Made";
                break;

                case 401:
                    message = "You Are Not Authorized";
                break;

                case 404:
                    message = "Resource Not Found";
                break;

                case 500:
                    message = "UnExpected Error";
                break; 
            }
            return message;
        }
    }
}
