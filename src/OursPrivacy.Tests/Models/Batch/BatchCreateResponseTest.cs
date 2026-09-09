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
            Failed = 0,
            Results = [new UnionMember0() { Index = 0, Success = Success.True }],
            Success = true,
        };

        long expectedAccepted = 0;
        long expectedFailed = 0;
        List<Result> expectedResults = [new UnionMember0() { Index = 0, Success = Success.True }];
        bool expectedSuccess = true;

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
            Failed = 0,
            Results = [new UnionMember0() { Index = 0, Success = Success.True }],
            Success = true,
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
            Failed = 0,
            Results = [new UnionMember0() { Index = 0, Success = Success.True }],
            Success = true,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BatchCreateResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedAccepted = 0;
        long expectedFailed = 0;
        List<Result> expectedResults = [new UnionMember0() { Index = 0, Success = Success.True }];
        bool expectedSuccess = true;

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
            Failed = 0,
            Results = [new UnionMember0() { Index = 0, Success = Success.True }],
            Success = true,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new BatchCreateResponse
        {
            Accepted = 0,
            Failed = 0,
            Results = [new UnionMember0() { Index = 0, Success = Success.True }],
            Success = true,
        };

        BatchCreateResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class ResultTest : TestBase
{
    [Fact]
    public void UnionMember0ValidationWorks()
    {
        Result value = new UnionMember0() { Index = 0, Success = Success.True };
        value.Validate();
    }

    [Fact]
    public void UnionMember1ValidationWorks()
    {
        Result value = new UnionMember1()
        {
            Code = Code.InvalidEvent,
            Index = 0,
            Message = "message",
            Success = UnionMember1Success.False,
        };
        value.Validate();
    }

    [Fact]
    public void UnionMember0SerializationRoundtripWorks()
    {
        Result value = new UnionMember0() { Index = 0, Success = Success.True };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Result>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void UnionMember1SerializationRoundtripWorks()
    {
        Result value = new UnionMember1()
        {
            Code = Code.InvalidEvent,
            Index = 0,
            Message = "message",
            Success = UnionMember1Success.False,
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Result>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}

public class UnionMember0Test : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new UnionMember0 { Index = 0, Success = Success.True };

        long expectedIndex = 0;
        ApiEnum<bool, Success> expectedSuccess = Success.True;

        Assert.Equal(expectedIndex, model.Index);
        Assert.Equal(expectedSuccess, model.Success);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new UnionMember0 { Index = 0, Success = Success.True };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UnionMember0>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new UnionMember0 { Index = 0, Success = Success.True };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UnionMember0>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedIndex = 0;
        ApiEnum<bool, Success> expectedSuccess = Success.True;

        Assert.Equal(expectedIndex, deserialized.Index);
        Assert.Equal(expectedSuccess, deserialized.Success);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new UnionMember0 { Index = 0, Success = Success.True };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new UnionMember0 { Index = 0, Success = Success.True };

        UnionMember0 copied = new(model);

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

public class UnionMember1Test : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new UnionMember1
        {
            Code = Code.InvalidEvent,
            Index = 0,
            Message = "message",
            Success = UnionMember1Success.False,
        };

        ApiEnum<string, Code> expectedCode = Code.InvalidEvent;
        long expectedIndex = 0;
        string expectedMessage = "message";
        ApiEnum<bool, UnionMember1Success> expectedSuccess = UnionMember1Success.False;

        Assert.Equal(expectedCode, model.Code);
        Assert.Equal(expectedIndex, model.Index);
        Assert.Equal(expectedMessage, model.Message);
        Assert.Equal(expectedSuccess, model.Success);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new UnionMember1
        {
            Code = Code.InvalidEvent,
            Index = 0,
            Message = "message",
            Success = UnionMember1Success.False,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UnionMember1>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new UnionMember1
        {
            Code = Code.InvalidEvent,
            Index = 0,
            Message = "message",
            Success = UnionMember1Success.False,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UnionMember1>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<string, Code> expectedCode = Code.InvalidEvent;
        long expectedIndex = 0;
        string expectedMessage = "message";
        ApiEnum<bool, UnionMember1Success> expectedSuccess = UnionMember1Success.False;

        Assert.Equal(expectedCode, deserialized.Code);
        Assert.Equal(expectedIndex, deserialized.Index);
        Assert.Equal(expectedMessage, deserialized.Message);
        Assert.Equal(expectedSuccess, deserialized.Success);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new UnionMember1
        {
            Code = Code.InvalidEvent,
            Index = 0,
            Message = "message",
            Success = UnionMember1Success.False,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new UnionMember1
        {
            Code = Code.InvalidEvent,
            Index = 0,
            Message = "message",
            Success = UnionMember1Success.False,
        };

        UnionMember1 copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class CodeTest : TestBase
{
    [Theory]
    [InlineData(Code.InvalidEvent)]
    [InlineData(Code.QueueFailed)]
    public void Validation_Works(Code rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Code> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Code>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(Code.InvalidEvent)]
    [InlineData(Code.QueueFailed)]
    public void SerializationRoundtrip_Works(Code rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Code> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Code>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Code>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Code>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class UnionMember1SuccessTest : TestBase
{
    [Theory]
    [InlineData(UnionMember1Success.False)]
    public void Validation_Works(UnionMember1Success rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, UnionMember1Success> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1Success>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(UnionMember1Success.False)]
    public void SerializationRoundtrip_Works(UnionMember1Success rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, UnionMember1Success> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1Success>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1Success>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1Success>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
