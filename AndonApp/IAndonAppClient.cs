using AndonApp.Model;
using System.Threading.Tasks;

namespace AndonApp
{

    /// <summary>
    /// Client interface for making requests to Andon. See <see cref="AndonAppClient"/> for implementation.
    /// </summary>
    public interface IAndonAppClient
    {

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
        Task ReportDataAsync(ReportDataRequest request);

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
        Task UpdateStationStatusAsync(UpdateStationStatusRequest request);

    }
}
