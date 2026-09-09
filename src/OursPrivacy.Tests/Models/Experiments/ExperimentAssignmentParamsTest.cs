using System;
using System.Text.Json;
using OursPrivacy.Core;
using OursPrivacy.Models.Experiments;

namespace OursPrivacy.Tests.Models.Experiments;

public class ExperimentAssignmentParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new ExperimentAssignmentParams
        {
            ExperimentKey = "experiment_key",
            Token = "token",
            VisitorID = "x",
            Context = new() { Search = "search", Url = "url" },
            TrackImpression = true,
        };

        string expectedExperimentKey = "experiment_key";
        string expectedToken = "token";
        string expectedVisitorID = "x";
        Context expectedContext = new() { Search = "search", Url = "url" };
        bool expectedTrackImpression = true;

        Assert.Equal(expectedExperimentKey, parameters.ExperimentKey);
        Assert.Equal(expectedToken, parameters.Token);
        Assert.Equal(expectedVisitorID, parameters.VisitorID);
        Assert.Equal(expectedContext, parameters.Context);
        Assert.Equal(expectedTrackImpression, parameters.TrackImpression);
    }

    [Fact]
    public void OptionalNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new ExperimentAssignmentParams
        {
            ExperimentKey = "experiment_key",
            Token = "token",
            VisitorID = "x",
        };

        Assert.Null(parameters.Context);
        Assert.False(parameters.RawBodyData.ContainsKey("context"));
        Assert.Null(parameters.TrackImpression);
        Assert.False(parameters.RawBodyData.ContainsKey("track_impression"));
    }

    [Fact]
    public void OptionalNullableParamsSetToNullAreSetToNull_Works()
    {
        var parameters = new ExperimentAssignmentParams
        {
            ExperimentKey = "experiment_key",
            Token = "token",
            VisitorID = "x",

            Context = null,
            TrackImpression = null,
        };

        Assert.Null(parameters.Context);
        Assert.True(parameters.RawBodyData.ContainsKey("context"));
        Assert.Null(parameters.TrackImpression);
        Assert.True(parameters.RawBodyData.ContainsKey("track_impression"));
    }

    [Fact]
    public void Url_Works()
    {
        ExperimentAssignmentParams parameters = new()
        {
            ExperimentKey = "experiment_key",
            Token = "token",
            VisitorID = "x",
        };

        var url = parameters.Url(new() { });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://api.oursprivacy.com/api/v1/experiments/assignments/experiment_key"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new ExperimentAssignmentParams
        {
            ExperimentKey = "experiment_key",
            Token = "token",
            VisitorID = "x",
            Context = new() { Search = "search", Url = "url" },
            TrackImpression = true,
        };

        ExperimentAssignmentParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}

public class ContextTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Context { Search = "search", Url = "url" };

        string expectedSearch = "search";
        string expectedUrl = "url";

        Assert.Equal(expectedSearch, model.Search);
        Assert.Equal(expectedUrl, model.Url);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Context { Search = "search", Url = "url" };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Context>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Context { Search = "search", Url = "url" };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Context>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedSearch = "search";
        string expectedUrl = "url";

        Assert.Equal(expectedSearch, deserialized.Search);
        Assert.Equal(expectedUrl, deserialized.Url);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Context { Search = "search", Url = "url" };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Context { };

        Assert.Null(model.Search);
        Assert.False(model.RawData.ContainsKey("search"));
        Assert.Null(model.Url);
        Assert.False(model.RawData.ContainsKey("url"));
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetValidation_Works()
    {
        var model = new Context { };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullAreSetToNull_Works()
    {
        var model = new Context { Search = null, Url = null };

        Assert.Null(model.Search);
        Assert.True(model.RawData.ContainsKey("search"));
        Assert.Null(model.Url);
        Assert.True(model.RawData.ContainsKey("url"));
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Context { Search = null, Url = null };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Context { Search = "search", Url = "url" };

        Context copied = new(model);

        Assert.Equal(model, copied);
    }
}
