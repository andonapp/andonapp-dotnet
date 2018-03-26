using Newtonsoft.Json;

namespace AndonApp.Model
{

    /// <summary>
    /// Request object for reporting process data.
    /// </summary>
    public class ReportDataRequest : BaseRequest
    {

        /// <summary>
        /// Name of the line. Cannot be null.
        /// </summary>
        [JsonProperty("lineName")]
        public string LineName { get; set; }

        /// <summary>
        /// Name of the station. Cannot be null.
        /// </summary>
        [JsonProperty("stationName")]
        public string StationName { get; set; }

        /// <summary>
        /// Must be 'PASS' or 'FAIL'.
        /// </summary>
        [JsonProperty("passResult")]
        public string PassResult { get; set; }

        /// <summary>
        /// Time in seconds spent processing. Cannot be null.
        /// </summary>
        [JsonProperty("processTimeSeconds")]
        public long ProcessTimeSeconds { get; set; }

        /// <summary>
        /// Reason of failure. Null on success.
        /// </summary>
        [JsonProperty("failReason")]
        public string FailReason { get; set; }

        /// <summary>
        /// Freeform notes on failure. Maybe be null, depending on org settings.
        /// </summary>
        [JsonProperty("failNotes")]
        public string FailNotes { get; set; }

    }
}
