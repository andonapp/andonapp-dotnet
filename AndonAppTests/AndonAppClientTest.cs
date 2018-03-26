using AndonApp;
using AndonApp.Exceptions;
using AndonApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AndonAppClientTests
{
    [TestClass]
    public class AndonAppClientTest
    {

        private const string _endpoint = "https://portal.andonapp.com/public/api/v1/";
        private const string _reportDataPath = "data/report";
        private const string _updateStatusPath = "station/update";

        private IAndonAppClient _andonClient;
        private string _orgName = "Test Org";
        private string _apiToken = "api-token";
        private MockHttpMessageHandler _mockHttp;

        [TestInitialize]
        public void Initialize()
        {
            _mockHttp = new MockHttpMessageHandler();
            _mockHttp.Fallback.Throw(new InvalidOperationException("No matching mock handler"));
            _andonClient = new AndonAppClient(_orgName, _apiToken, _mockHttp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionWhenOrgNameNull()
        {
            new AndonAppClient(null, _apiToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionWhenApiTokenNull()
        {
            new AndonAppClient(_orgName, null);
        }

        [TestMethod]
        public async Task ShouldReportDataWhenValidPassRequest()
        {
            var request = new ReportDataRequest
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.OK, new { });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        public async Task ShouldReportDataWhenValidFailRequest()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "FAIL",
                ProcessTimeSeconds = 200,
                FailReason = "Test Failure",
                FailNotes = "notes"
            };

            ExpectReportDataRequest(request, HttpStatusCode.OK, new { });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonInvalidRequestException))]
        public async Task ShouldThrowExceptionWhenReportDataMissingLineName()
        {
            var request = new ReportDataRequest()
            {
                StationName = "station 1",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.BadRequest, new AppError {
                ErrorType = "INVALID_REQUEST",
                ErrorMessage ="lineName may not be empty"
            });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonResourceNotFoundException))]
        public async Task ShouldThrowExceptionWhenReportDataStationNotFound()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 2",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.BadRequest, new AppError
            {
                ErrorType = "RESOURCE_NOT_FOUND",
                ErrorMessage = "Station not found"
            });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonInvalidRequestException))]
        public async Task ShouldThrowExceptionWhenReportDataInvalidPassResult()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "PAS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.BadRequest, new AppError
            {
                ErrorType = "INVALID_REQUEST",
                ErrorMessage = "'PAS' is not a valid pass result."
            });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonUnauthorizedRequestException))]
        public async Task ShouldThrowExceptionWhenReportDataUnauthorized()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.Unauthorized, new AppError
            {
                Timestamp = "2018-03-07T16:15:19.033+0000",
                Status = 401,
                Error = "Unauthorized",
                Message = "Unauthorized",
                Path = "/public/api/v1/data/report"
            });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonBadRequestException))]
        public async Task ShouldThrowExceptionWhenSpring400Error()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.BadRequest, new AppError
            {
                Timestamp = "2018-03-07T16:15:19.033+0000",
                Status = 400,
                Error = "error",
                Message = "message",
                Path = "/public/api/v1/data/report"
            });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonInternalErrorException))]
        public async Task ShouldThrowExceptionWhenSpring500Error()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.InternalServerError, new AppError
            {
                Timestamp = "2018-03-07T16:15:19.033+0000",
                Status = 500,
                Error = "error",
                Message = "message",
                Path = "/public/api/v1/data/report"
            });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonInternalErrorException))]
        public async Task ShouldThrowExceptionWhenInternalErrorResponse()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.BadRequest, new AppError {
                ErrorType = "INTERNAL_ERROR",
                ErrorMessage = "something broke"
            });

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        [ExpectedException(typeof(AndonAppException))]
        public async Task ShouldThrowExceptionWhenUnknownFailResponse()
        {
            var request = new ReportDataRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                PassResult = "PASS",
                ProcessTimeSeconds = 100
            };

            ExpectReportDataRequest(request, HttpStatusCode.BadRequest, new {});

            await _andonClient.ReportDataAsync(request);
        }

        [TestMethod]
        public async Task ShouldUpdateStatusToYellowWhenValidRequest()
        {
            var request = new UpdateStationStatusRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                StatusColor = "YELLOW",
                StatusReason = "Missing parts",
                StatusNotes = "notes"
            };

            ExpectUpdateStationStatusRequest(request, HttpStatusCode.OK, new { });

            await _andonClient.UpdateStationStatusAsync(request);
        }

        [TestMethod]
        public async Task ShouldUpdateStatusToGreenWhenValidRequest()
        {
            var request = new UpdateStationStatusRequest()
            {
                LineName = "line 1",
                StationName = "station 1",
                StatusColor = "GREEN"
            };

            ExpectUpdateStationStatusRequest(request, HttpStatusCode.OK, new { });

            await _andonClient.UpdateStationStatusAsync(request);
        }

        private void ExpectReportDataRequest(ReportDataRequest request, HttpStatusCode responseCode, object response)
        {
            ExpectRequest(_reportDataPath, new
            {
                lineName = request.LineName,
                stationName = request.StationName,
                passResult = request.PassResult,
                processTimeSeconds = request.ProcessTimeSeconds,
                failReason = request.FailReason,
                failNotes = request.FailNotes,
                orgName = _orgName
            }, responseCode, response);
        }

        private void ExpectUpdateStationStatusRequest(UpdateStationStatusRequest request, HttpStatusCode responseCode, object response)
        {
            ExpectRequest(_updateStatusPath, new
            {
                lineName = request.LineName,
                stationName = request.StationName,
                statusColor = request.StatusColor,
                statusReason = request.StatusReason,
                statusNotes = request.StatusNotes,
                orgName = _orgName
            }, responseCode, response);
        }

        private void ExpectRequest(string path, object request,
            HttpStatusCode responseCode, object response)
        {
            _mockHttp.When(_endpoint + path)
                .WithHeaders("Authorization", $"Bearer {_apiToken}")
                .WithHeaders("Content-Type", "application/json; charset=utf-8")
                .WithContent(JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .Respond(responseCode, "application/json", JsonConvert.SerializeObject(response));
        }
    }
}
