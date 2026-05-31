using System.Text.Json;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;
using OursPrivacy.Models.Experiments;

namespace OursPrivacy.Tests.Models.Experiments;

public class ExperimentAssignmentResponseTest : TestBase
{
    [Fact]
    public void UnionMember0ValidationWorks()
    {
        ExperimentAssignmentResponse value = new UnionMember0()
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            IsControl = true,
            Type = "type",
            VariantName = "variant_name",
        };
        value.Validate();
    }

    [Fact]
    public void UnionMember1ValidationWorks()
    {
        ExperimentAssignmentResponse value = new UnionMember1()
        {
            InExperiment = UnionMember1InExperiment.False,
            Success = UnionMember1Success.True,
        };
        value.Validate();
    }

    [Fact]
    public void UnionMember0SerializationRoundtripWorks()
    {
        ExperimentAssignmentResponse value = new UnionMember0()
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            IsControl = true,
            Type = "type",
            VariantName = "variant_name",
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ExperimentAssignmentResponse>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void UnionMember1SerializationRoundtripWorks()
    {
        ExperimentAssignmentResponse value = new UnionMember1()
        {
            InExperiment = UnionMember1InExperiment.False,
            Success = UnionMember1Success.True,
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ExperimentAssignmentResponse>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class UnionMember0Test : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            IsControl = true,
            Type = "type",
            VariantName = "variant_name",
        };

        string expectedExperimentID = "experiment_id";
        ApiEnum<bool, InExperiment> expectedInExperiment = InExperiment.True;
        ApiEnum<bool, Success> expectedSuccess = Success.True;
        string expectedVariantID = "variant_id";
        string expectedExperimentKey = "experiment_key";
        string expectedExperimentName = "experiment_name";
        bool expectedIsControl = true;
        string expectedType = "type";
        string expectedVariantName = "variant_name";

        Assert.Equal(expectedExperimentID, model.ExperimentID);
        Assert.Equal(expectedInExperiment, model.InExperiment);
        Assert.Equal(expectedSuccess, model.Success);
        Assert.Equal(expectedVariantID, model.VariantID);
        Assert.Equal(expectedExperimentKey, model.ExperimentKey);
        Assert.Equal(expectedExperimentName, model.ExperimentName);
        Assert.Equal(expectedIsControl, model.IsControl);
        Assert.Equal(expectedType, model.Type);
        Assert.Equal(expectedVariantName, model.VariantName);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            IsControl = true,
            Type = "type",
            VariantName = "variant_name",
        };

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
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            IsControl = true,
            Type = "type",
            VariantName = "variant_name",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UnionMember0>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedExperimentID = "experiment_id";
        ApiEnum<bool, InExperiment> expectedInExperiment = InExperiment.True;
        ApiEnum<bool, Success> expectedSuccess = Success.True;
        string expectedVariantID = "variant_id";
        string expectedExperimentKey = "experiment_key";
        string expectedExperimentName = "experiment_name";
        bool expectedIsControl = true;
        string expectedType = "type";
        string expectedVariantName = "variant_name";

        Assert.Equal(expectedExperimentID, deserialized.ExperimentID);
        Assert.Equal(expectedInExperiment, deserialized.InExperiment);
        Assert.Equal(expectedSuccess, deserialized.Success);
        Assert.Equal(expectedVariantID, deserialized.VariantID);
        Assert.Equal(expectedExperimentKey, deserialized.ExperimentKey);
        Assert.Equal(expectedExperimentName, deserialized.ExperimentName);
        Assert.Equal(expectedIsControl, deserialized.IsControl);
        Assert.Equal(expectedType, deserialized.Type);
        Assert.Equal(expectedVariantName, deserialized.VariantName);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            IsControl = true,
            Type = "type",
            VariantName = "variant_name",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
        };

        Assert.Null(model.ExperimentKey);
        Assert.False(model.RawData.ContainsKey("experiment_key"));
        Assert.Null(model.ExperimentName);
        Assert.False(model.RawData.ContainsKey("experiment_name"));
        Assert.Null(model.IsControl);
        Assert.False(model.RawData.ContainsKey("is_control"));
        Assert.Null(model.Type);
        Assert.False(model.RawData.ContainsKey("type"));
        Assert.Null(model.VariantName);
        Assert.False(model.RawData.ContainsKey("variant_name"));
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetValidation_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullAreSetToNull_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",

            ExperimentKey = null,
            ExperimentName = null,
            IsControl = null,
            Type = null,
            VariantName = null,
        };

        Assert.Null(model.ExperimentKey);
        Assert.True(model.RawData.ContainsKey("experiment_key"));
        Assert.Null(model.ExperimentName);
        Assert.True(model.RawData.ContainsKey("experiment_name"));
        Assert.Null(model.IsControl);
        Assert.True(model.RawData.ContainsKey("is_control"));
        Assert.Null(model.Type);
        Assert.True(model.RawData.ContainsKey("type"));
        Assert.Null(model.VariantName);
        Assert.True(model.RawData.ContainsKey("variant_name"));
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullValidation_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",

            ExperimentKey = null,
            ExperimentName = null,
            IsControl = null,
            Type = null,
            VariantName = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new UnionMember0
        {
            ExperimentID = "experiment_id",
            InExperiment = InExperiment.True,
            Success = Success.True,
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            IsControl = true,
            Type = "type",
            VariantName = "variant_name",
        };

        UnionMember0 copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class InExperimentTest : TestBase
{
    [Theory]
    [InlineData(InExperiment.True)]
    public void Validation_Works(InExperiment rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, InExperiment> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, InExperiment>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(InExperiment.True)]
    public void SerializationRoundtrip_Works(InExperiment rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, InExperiment> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, InExperiment>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, InExperiment>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, InExperiment>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
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
            InExperiment = UnionMember1InExperiment.False,
            Success = UnionMember1Success.True,
        };

        ApiEnum<bool, UnionMember1InExperiment> expectedInExperiment =
            UnionMember1InExperiment.False;
        ApiEnum<bool, UnionMember1Success> expectedSuccess = UnionMember1Success.True;

        Assert.Equal(expectedInExperiment, model.InExperiment);
        Assert.Equal(expectedSuccess, model.Success);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new UnionMember1
        {
            InExperiment = UnionMember1InExperiment.False,
            Success = UnionMember1Success.True,
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
            InExperiment = UnionMember1InExperiment.False,
            Success = UnionMember1Success.True,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UnionMember1>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<bool, UnionMember1InExperiment> expectedInExperiment =
            UnionMember1InExperiment.False;
        ApiEnum<bool, UnionMember1Success> expectedSuccess = UnionMember1Success.True;

        Assert.Equal(expectedInExperiment, deserialized.InExperiment);
        Assert.Equal(expectedSuccess, deserialized.Success);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new UnionMember1
        {
            InExperiment = UnionMember1InExperiment.False,
            Success = UnionMember1Success.True,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new UnionMember1
        {
            InExperiment = UnionMember1InExperiment.False,
            Success = UnionMember1Success.True,
        };

        UnionMember1 copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class UnionMember1InExperimentTest : TestBase
{
    [Theory]
    [InlineData(UnionMember1InExperiment.False)]
    public void Validation_Works(UnionMember1InExperiment rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, UnionMember1InExperiment> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1InExperiment>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<OursPrivacyInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(UnionMember1InExperiment.False)]
    public void SerializationRoundtrip_Works(UnionMember1InExperiment rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<bool, UnionMember1InExperiment> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1InExperiment>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1InExperiment>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<bool, UnionMember1InExperiment>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class UnionMember1SuccessTest : TestBase
{
    [Theory]
    [InlineData(UnionMember1Success.True)]
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
    [InlineData(UnionMember1Success.True)]
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
