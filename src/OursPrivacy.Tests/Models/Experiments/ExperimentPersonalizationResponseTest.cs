using System.Collections.Generic;
using System.Text.Json;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;
using OursPrivacy.Models.Experiments;

namespace OursPrivacy.Tests.Models.Experiments;

public class ExperimentPersonalizationResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Properties = new Dictionary<string, Property>() { { "foo", "string" } },
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        Dictionary<string, Property> expectedProperties = new() { { "foo", "string" } };
        ApiEnum<bool, ExperimentPersonalizationResponseSuccess> expectedSuccess =
            ExperimentPersonalizationResponseSuccess.True;

        Assert.Equal(expectedProperties.Count, model.Properties.Count);
        foreach (var item in expectedProperties)
        {
            Assert.True(model.Properties.TryGetValue(item.Key, out var value));

            Assert.Equal(value, model.Properties[item.Key]);
        }
        Assert.Equal(expectedSuccess, model.Success);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Properties = new Dictionary<string, Property>() { { "foo", "string" } },
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ExperimentPersonalizationResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Properties = new Dictionary<string, Property>() { { "foo", "string" } },
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ExperimentPersonalizationResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        Dictionary<string, Property> expectedProperties = new() { { "foo", "string" } };
        ApiEnum<bool, ExperimentPersonalizationResponseSuccess> expectedSuccess =
            ExperimentPersonalizationResponseSuccess.True;

        Assert.Equal(expectedProperties.Count, deserialized.Properties.Count);
        foreach (var item in expectedProperties)
        {
            Assert.True(deserialized.Properties.TryGetValue(item.Key, out var value));

            Assert.Equal(value, deserialized.Properties[item.Key]);
        }
        Assert.Equal(expectedSuccess, deserialized.Success);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Properties = new Dictionary<string, Property>() { { "foo", "string" } },
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Properties = new Dictionary<string, Property>() { { "foo", "string" } },
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        ExperimentPersonalizationResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class PropertyTest : TestBase
{
    [Fact]
    public void StringValidationWorks()
    {
        Property value = "string";
        value.Validate();
    }

    [Fact]
    public void DoubleValidationWorks()
    {
        Property value = 0;
        value.Validate();
    }

    [Fact]
    public void BoolValidationWorks()
    {
        Property value = true;
        value.Validate();
    }

    [Fact]
    public void StringSerializationRoundtripWorks()
    {
        Property value = "string";
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Property>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void DoubleSerializationRoundtripWorks()
    {
        Property value = 0;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Property>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void BoolSerializationRoundtripWorks()
    {
        Property value = true;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Property>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class ExperimentPersonalizationResponseSuccessTest : TestBase
{
    [Theory]
    [InlineData(ExperimentPersonalizationResponseSuccess.True)]
    public void Validation_Works(ExperimentPersonalizationResponseSuccess rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, ExperimentPersonalizationResponseSuccess> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<
            ApiEnum<bool, ExperimentPersonalizationResponseSuccess>
        >(JsonSerializer.SerializeToElement("invalid value"), ModelBase.SerializerOptions);

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(ExperimentPersonalizationResponseSuccess.True)]
    public void SerializationRoundtrip_Works(ExperimentPersonalizationResponseSuccess rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, ExperimentPersonalizationResponseSuccess> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<
            ApiEnum<bool, ExperimentPersonalizationResponseSuccess>
        >(json, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<
            ApiEnum<bool, ExperimentPersonalizationResponseSuccess>
        >(JsonSerializer.SerializeToElement("invalid value"), ModelBase.SerializerOptions);
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<
            ApiEnum<bool, ExperimentPersonalizationResponseSuccess>
        >(json, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}
