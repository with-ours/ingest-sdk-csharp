// Send one track event to Ours Privacy and print the result.
//
// Run from the repository root with your Source token:
//   OURS_PRIVACY_TOKEN=your-source-token dotnet run --project examples/TrackEvent
// Events go to production unless OURS_PRIVACY_BASE_URL is set.
// Sample values only. Do not put personal or health data in event properties.
using OursPrivacy;
using OursPrivacy.Models.Track;

var token = Environment.GetEnvironmentVariable("OURS_PRIVACY_TOKEN");
if (string.IsNullOrEmpty(token))
{
    Console.Error.WriteLine("Set OURS_PRIVACY_TOKEN to your Source token");
    return 1;
}

OursPrivacyClient client = new();

TrackEventParams parameters = new()
{
    Token = token,
    Event = "Example Signup", // must be allowed on your Source
    ExternalID = "example-user-123", // your own identifier for this visitor
    EventProperties = new Dictionary<string, EventProperty?>
    {
        ["plan"] = "starter",
        ["quantity"] = 1,
    },
};

var response = await client.Track.Event(parameters);
Console.WriteLine($"tracked: {response.Success}");
return 0;
