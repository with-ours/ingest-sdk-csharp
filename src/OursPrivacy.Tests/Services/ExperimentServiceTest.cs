using System.Threading.Tasks;

namespace OursPrivacy.Tests.Services;

public class ExperimentServiceTest : TestBase
{
    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Assignment_Works()
    {
        var response = await this.client.Experiments.Assignment(
            "experiment_key",
            new() { Token = "token", VisitorID = "x" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Personalization_Works()
    {
        var response = await this.client.Experiments.Personalization(
            new() { Token = "token", VisitorID = "x" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }
}
