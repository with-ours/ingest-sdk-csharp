using System.Collections.Generic;
using System.Text.Json;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;
using OursPrivacy.Models.Batch;

namespace OursPrivacy.Tests.Models.Batch;

public class BatchCreateResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new BatchCreateResponse
        {
            Accepted = 0,
            Failed = Failed.V0,
            Results = [new() { Index = 0, Success = Success.True }],
            Success = BatchCreateResponseSuccess.True,
        };

        long expectedAccepted = 0;
        ApiEnum<double, Failed> expectedFailed = Failed.V0;
        List<Result> expectedResults = [new() { Index = 0, Success = Success.True }];
        ApiEnum<bool, BatchCreateResponseSuccess> expectedSuccess = BatchCreateResponseSuccess.True;

        Assert.Equal(expectedAccepted, model.Accepted);
        Assert.Equal(expectedFailed, model.Failed);
        Assert.Equal(expectedResults.Count, model.Results.Count);
        for (int i = 0; i < expectedResults.Count; i++)
        {
            Assert.Equal(expectedResults[i], model.Results[i]);
        }
        Assert.Equal(expectedSuccess, model.Success);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new BatchCreateResponse
        {
            Accepted = 0,
            Failed = Failed.V0,
            Results = [new() { Index = 0, Success = Success.True }],
            Success = BatchCreateResponseSuccess.True,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BatchCreateResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new BatchCreateResponse
        {
            Accepted = 0,
            Failed = Failed.V0,
            Results = [new() { Index = 0, Success = Success.True }],
            Success = BatchCreateResponseSuccess.True,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BatchCreateResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedAccepted = 0;
        ApiEnum<double, Failed> expectedFailed = Failed.V0;
        List<Result> expectedResults = [new() { Index = 0, Success = Success.True }];
        ApiEnum<bool, BatchCreateResponseSuccess> expectedSuccess = BatchCreateResponseSuccess.True;

        Assert.Equal(expectedAccepted, deserialized.Accepted);
        Assert.Equal(expectedFailed, deserialized.Failed);
        Assert.Equal(expectedResults.Count, deserialized.Results.Count);
        for (int i = 0; i < expectedResults.Count; i++)
        {
            Assert.Equal(expectedResults[i], deserialized.Results[i]);
        }
        Assert.Equal(expectedSuccess, deserialized.Success);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new BatchCreateResponse
        {
            Accepted = 0,
            Failed = Failed.V0,
            Results = [new() { Index = 0, Success = Success.True }],
            Success = BatchCreateResponseSuccess.True,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new BatchCreateResponse
        {
            Accepted = 0,
            Failed = Failed.V0,
            Results = [new() { Index = 0, Success = Success.True }],
            Success = BatchCreateResponseSuccess.True,
        };

        BatchCreateResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class FailedTest : TestBase
{
    [Theory]
    [InlineData(Failed.V0)]
    public void Validation_Works(Failed rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<double, Failed> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<double, Failed>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(Failed.V0)]
    public void SerializationRoundtrip_Works(Failed rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<double, Failed> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<double, Failed>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<double, Failed>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<double, Failed>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class ResultTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Result { Index = 0, Success = Success.True };

        long expectedIndex = 0;
        ApiEnum<bool, Success> expectedSuccess = Success.True;

        Assert.Equal(expectedIndex, model.Index);
        Assert.Equal(expectedSuccess, model.Success);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Result { Index = 0, Success = Success.True };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Result>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Result { Index = 0, Success = Success.True };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Result>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        long expectedIndex = 0;
        ApiEnum<bool, Success> expectedSuccess = Success.True;

        Assert.Equal(expectedIndex, deserialized.Index);
        Assert.Equal(expectedSuccess, deserialized.Success);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Result { Index = 0, Success = Success.True };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Result { Index = 0, Success = Success.True };

        Result copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class SuccessTest : TestBase
{
    [Theory]
    [InlineData(Success.True)]
    public void Validation_Works(Success rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, Success> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, Success>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(Success.True)]
    public void SerializationRoundtrip_Works(Success rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, Success> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, Success>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, Success>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, Success>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class BatchCreateResponseSuccessTest : TestBase
{
    [Theory]
    [InlineData(BatchCreateResponseSuccess.True)]
    public void Validation_Works(BatchCreateResponseSuccess rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, BatchCreateResponseSuccess> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, BatchCreateResponseSuccess>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(BatchCreateResponseSuccess.True)]
    public void SerializationRoundtrip_Works(BatchCreateResponseSuccess rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, BatchCreateResponseSuccess> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, BatchCreateResponseSuccess>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, BatchCreateResponseSuccess>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, BatchCreateResponseSuccess>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
