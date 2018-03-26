using AndonApp.Exceptions;
using AndonApp.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AndonApp
{

    /// <summary>
    /// Client for making requests to Andon. In order to use the client you must generate
    /// an API token on the org settings page within Andon.
    /// </summary>
    /// <example>
    /// <code>
    /// var andonClient = new AndonAppClient(orgName, apiToken);
    /// andonClient.ReportDataAsync(new ReportDataRequest
    ///     {
    ///         LineName = "line 1",
    ///         StationName = "station 1",
    ///         PassResult = "PASS",
    ///         ProcessTimeSeconds = 120
    ///     });
    /// </code>
    /// </example>
    public class AndonAppClient : IAndonAppClient
    {

        private const string _defaultEndpoint = "https://portal.andonapp.com/public/api/v1/";

        private const string _reportDataPath = "data/report";
        private const string _updateStatusPath = "station/update";

        private readonly HttpClient _httpClient;

        private readonly string _orgName;

        private readonly JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Construct a new AndonAppClient for a specific organization.
        /// </summary>
        /// <param name="orgName">Name of the organization</param>
        /// <param name="apiToken">API token for the organziation</param>
        /// <param name="handler">Optional parameter for providing an <see cref="HttpMessageHandler"/></param>
        public AndonAppClient(string orgName, string apiToken, HttpMessageHandler handler = null)
        {
            _orgName = CheckNotNull(orgName, "orgName cannot be null");
            CheckNotNull(apiToken, "apitToken cannot be null");

            _httpClient = new HttpClient(handler ?? new HttpClientHandler());
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiToken);
            _httpClient.BaseAddress = new Uri(_defaultEndpoint);

            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        }

        /// <summary>
        /// Changes the endpoint that requests are made to.
        /// </summary>
        /// <param name="endpoint">Andon endpoint to connect to</param>
        public void Endpoint(string endpoint)
        {
            CheckNotNull(endpoint, "endpoint cannot be null");
            _httpClient.BaseAddress = new Uri(endpoint);
        }


        /// <summary>
        /// Reports the outcome of a process at a station to Andon.
        /// </summary>
        /// <example> 
        /// <code>
        /// andonClient.ReportDataAsync(new ReportDataRequest
        ///     {
        ///         LineName = "line 1",
        ///         StationName = "station 1",
        ///         PassResult = "FAIL",
        ///         FailReason = "Test Failure",
        ///         FailNotes = "notes",
        ///         ProcessTimeSeconds = 120
        ///     });
        /// </code>
        /// </example>
        /// <param name="request">The data to report</param>
        /// <exception cref="AndonApp.Exceptions.AndonAppException">If there is a general request failure</exception>
        /// <exception cref="AndonApp.Exceptions.AndonBadRequestException">If there is something wrong with the request</exception>
        /// <exception cref="AndonApp.Exceptions.AndonInternalErrorException">If there is a failure within Andon</exception>
        /// <exception cref="AndonApp.Exceptions.AndonInvalidRequestException">If there are invalid request arguments</exception>
        /// <exception cref="AndonApp.Exceptions.AndonResourceNotFoundException">If the referenced station cannot be found</exception>
        /// <exception cref="AndonApp.Exceptions.AndonUnauthorizedRequestException">If authorization fails</exception>
        public async Task ReportDataAsync(ReportDataRequest request)
        {
            await ExcecuteRequest(request, _reportDataPath).ConfigureAwait(false);
        }

        /// <summary>
        /// Changes the status of a station in Andon.
        /// </summary>
        /// <example> 
        /// <code>
        /// andonClient.UpdateStationStatusAsync(new UpdateStationStatusRequest
        ///     {
        ///         LineName = "line 1",
        ///         StationName = "station 1",
        ///         StatusColor = "YELLOW",
        ///         StatusReason = "Missing parts",
        ///         StatusNotes = "notes"
        ///     });
        /// </code>
        /// </example>
        /// <param name="request">The data to report</param>
        /// <exception cref="AndonApp.Exceptions.AndonAppException">If there is a general request failure</exception>
        /// <exception cref="AndonApp.Exceptions.AndonBadRequestException">If there is something wrong with the request</exception>
        /// <exception cref="AndonApp.Exceptions.AndonInternalErrorException">If there is a failure within Andon</exception>
        /// <exception cref="AndonApp.Exceptions.AndonInvalidRequestException">If there are invalid request arguments</exception>
        /// <exception cref="AndonApp.Exceptions.AndonResourceNotFoundException">If the referenced station cannot be found</exception>
        /// <exception cref="AndonApp.Exceptions.AndonUnauthorizedRequestException">If authorization fails</exception>
        public async Task UpdateStationStatusAsync(UpdateStationStatusRequest request)
        {
            await ExcecuteRequest(request, _updateStatusPath).ConfigureAwait(false);
        }

        private async Task ExcecuteRequest<T>(T request, string path) where T : BaseRequest
        {
            CheckNotNull(request, "request cannot be null");

            request.OrgName = _orgName;

            var requestStr = new StringContent(
                JsonConvert.SerializeObject(request, _serializerSettings),
                Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(path, requestStr).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response).ConfigureAwait(false);
            }
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            var responseStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            AppError appError = JsonConvert.DeserializeObject<AppError>(responseStr);

            if (appError != null)
            {
                if (appError.ErrorType != null)
                {
                    var errorType = appError.ErrorType;
                    var errorMessage = appError.ErrorMessage;

                    switch (errorType)
                    {
                        case "BAD_REQUEST":
                            throw new AndonBadRequestException(errorMessage);
                        case "INVALID_REQUEST":
                            throw new AndonInvalidRequestException(errorMessage);
                        case "RESOURCE_NOT_FOUND":
                            throw new AndonResourceNotFoundException(errorMessage);
                        case "UNAUTHORIZED_REQUEST":
                            throw new AndonUnauthorizedRequestException(errorMessage);
                        case "INTERNAL_ERROR":
                            throw new AndonInternalErrorException(errorMessage);
                        default:
                            throw new AndonAppException(errorMessage);
                    }
                }
                else if (appError.Status.HasValue)
                {
                    var status = appError.Status;
                    var errorMessage = appError.Message;

                    if (status == 401)
                    {
                        throw new AndonUnauthorizedRequestException(errorMessage);
                    }
                    else if (status >= 400 && status < 500)
                    {
                        throw new AndonBadRequestException(errorMessage);
                    }
                    else
                    {
                        throw new AndonInternalErrorException(errorMessage);
                    }
                }
            }

            throw new AndonAppException($"Status {response.StatusCode}: {responseStr}");
        }

        private T CheckNotNull<T>(T value, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(message);
            }
            return value;
        }
    }
}
