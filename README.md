# AndonApp .NET Client

.NET client library for reporting data to [Andon](https://www.andonapp.com/)

## Install

```
Install-Package AndonApp
```

## Usage

In order to programmatically connect to Andon's APIs you must first generate an API token. This is done by logging into your Andon account, navigating to the [API settings page](https://portal.andonapp.com/settings/tokens), and generating a new token.  Make sure to record the token, and keep it secret.

Reference Andon's [getting started guide](https://drive.google.com/file/d/0B5cQI3VvgCT8UllmaENIazlwbGc/view) and [API guide](https://drive.google.com/file/d/0B5cQI3VvgCT8enNIZGN2QVo0STg/view) for complete details on these prerequisites

### Setting up the Client

Now that you have a token, create a client as follows:

```csharp
var andonClient = new AndonAppClient(orgName, apiToken);
```

Additionally, you can pass the constructor a preconfigured HttpMessageHandler.

### Reporting Data

Here's an example of using the client to report a success:

```csharp
andonClient.ReportDataAsync(new ReportDataRequest
    {
        LineName = "line 1",
        StationName = "station 1",
        PassResult = "PASS",
        ProcessTimeSeconds = 120
    });
```

And a failure:

```csharp
andonClient.ReportDataAsync(new ReportDataRequest
    {
        LineName = "line 1",
        StationName = "station 1",
        PassResult = "FAIL",
        FailReason = "Test Failure",
        FailNotes = "notes",
        ProcessTimeSeconds = 120
    });
```

### Updating a Station Status

Here's an example of flipping a station to Red:

```csharp
andonClient.UpdateStationStatusAsync(new UpdateStationStatusRequest
    {
        LineName = "line 1",
        StationName = "station 1",
        StatusColor = "RED",
        StatusReason = "Missing parts",
        StatusNotes = "notes"
    });
```

And back to Green:

```csharp
andonClient.UpdateStationStatusAsync(new UpdateStationStatusRequest
    {
        LineName = "line 1",
        StationName = "station 1",
        StatusColor = "GREEN"
    });
```

## License

[Licensed under the MIT license](LICENSE).
