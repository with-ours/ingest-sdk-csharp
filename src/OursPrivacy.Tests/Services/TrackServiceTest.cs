using System.Threading.Tasks;

namespace OursPrivacy.Tests.Services;

public class TrackServiceTest : TestBase
{
    [Fact(Skip = "Prism tests are disabled")]
    public async Task Event_Works()
    {
        var response = await this.client.Track.Event(
            new() { Token = "x", Event = "x" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }
}
