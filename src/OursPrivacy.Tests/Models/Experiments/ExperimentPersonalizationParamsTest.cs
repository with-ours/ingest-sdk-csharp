using System;
using OursPrivacy.Models.Experiments;

namespace OursPrivacy.Tests.Models.Experiments;

public class ExperimentPersonalizationParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new ExperimentPersonalizationParams { Token = "token", VisitorID = "x" };

        string expectedToken = "token";
        string expectedVisitorID = "x";

        Assert.Equal(expectedToken, parameters.Token);
        Assert.Equal(expectedVisitorID, parameters.VisitorID);
    }

    [Fact]
    public void Url_Works()
    {
        ExperimentPersonalizationParams parameters = new() { Token = "token", VisitorID = "x" };

        var url = parameters.Url(new() { });

        Assert.True(
            TestBase.UrisEqual(
                new Uri("https://api.oursprivacy.com/api/v1/experiments/personalization"),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new ExperimentPersonalizationParams { Token = "token", VisitorID = "x" };

        ExperimentPersonalizationParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
