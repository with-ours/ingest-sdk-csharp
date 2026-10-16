using System.Threading.Tasks;

namespace OursPrivacy.Tests.Services;

public class TrackServiceTest : TestBase
{
    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Event_Works()
    {
        var response = await this.client.Track.Event(
            new() { Token = "P", Event = "P" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }
}
