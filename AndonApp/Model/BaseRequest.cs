using Newtonsoft.Json;

namespace AndonApp.Model
{
    public class BaseRequest
    {

        /// <summary>
        /// Name of the organization. Will be automatically supplied in the client.
        /// </summary>
        [JsonProperty("orgName")]
        public string OrgName { get; set; }

    }
}
