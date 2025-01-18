using Microsoft.AspNetCore.WebUtilities;

namespace LinkLobby.Models
{
    public class Error
    {
        public int status { get; set; }
        public string? message { get; set; }
        public Error(int statusCode)
        { 
            status = statusCode;
            message = ReasonPhrases.GetReasonPhrase(statusCode);
        }
        public Error(int statusCode, string message)
        {
            status = statusCode;
            this.message = message;
        }
    }
}
