using Newtonsoft.Json;

namespace AndonApp.Model
{
    public class AppError
    {
        // Andon Errors
        [JsonProperty("errorType")]
        public string ErrorType { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        // Spring Errors
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("status")]
        public long? Status { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

    }
}
