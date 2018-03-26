using Newtonsoft.Json;

namespace AndonApp.Model
{

    /// <summary>
    /// Request object for update a station status.
    /// </summary>
    public class UpdateStationStatusRequest : BaseRequest
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
        /// Must be 'GREEN', 'YELLOW', or 'RED'.
        /// </summary>
        [JsonProperty("statusColor")]
        public string StatusColor { get; set; }

        /// <summary>
        /// Reason for the change. May be null.
        /// </summary>
        [JsonProperty("statusReason")]
        public string StatusReason { get; set; }

        /// <summary>
        /// Notes on the change. May be null, depending on org settings.
        /// </summary>
        [JsonProperty("statusNotes")]
        public string StatusNotes { get; set; }

    }
}
