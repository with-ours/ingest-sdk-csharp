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
            Personalizations =
            [
                new()
                {
                    AssignedAt = 0,
                    ExperimentID = "experiment_id",
                    VariantID = "variant_id",
                    ExperimentKey = "experiment_key",
                    ExperimentName = "experiment_name",
                    VariantName = "variant_name",
                },
            ],
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        List<Personalization> expectedPersonalizations =
        [
            new()
            {
                AssignedAt = 0,
                ExperimentID = "experiment_id",
                VariantID = "variant_id",
                ExperimentKey = "experiment_key",
                ExperimentName = "experiment_name",
                VariantName = "variant_name",
            },
        ];
        ApiEnum<bool, ExperimentPersonalizationResponseSuccess> expectedSuccess =
            ExperimentPersonalizationResponseSuccess.True;

        Assert.Equal(expectedPersonalizations.Count, model.Personalizations.Count);
        for (int i = 0; i < expectedPersonalizations.Count; i++)
        {
            Assert.Equal(expectedPersonalizations[i], model.Personalizations[i]);
        }
        Assert.Equal(expectedSuccess, model.Success);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Personalizations =
            [
                new()
                {
                    AssignedAt = 0,
                    ExperimentID = "experiment_id",
                    VariantID = "variant_id",
                    ExperimentKey = "experiment_key",
                    ExperimentName = "experiment_name",
                    VariantName = "variant_name",
                },
            ],
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
            Personalizations =
            [
                new()
                {
                    AssignedAt = 0,
                    ExperimentID = "experiment_id",
                    VariantID = "variant_id",
                    ExperimentKey = "experiment_key",
                    ExperimentName = "experiment_name",
                    VariantName = "variant_name",
                },
            ],
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ExperimentPersonalizationResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        List<Personalization> expectedPersonalizations =
        [
            new()
            {
                AssignedAt = 0,
                ExperimentID = "experiment_id",
                VariantID = "variant_id",
                ExperimentKey = "experiment_key",
                ExperimentName = "experiment_name",
                VariantName = "variant_name",
            },
        ];
        ApiEnum<bool, ExperimentPersonalizationResponseSuccess> expectedSuccess =
            ExperimentPersonalizationResponseSuccess.True;

        Assert.Equal(expectedPersonalizations.Count, deserialized.Personalizations.Count);
        for (int i = 0; i < expectedPersonalizations.Count; i++)
        {
            Assert.Equal(expectedPersonalizations[i], deserialized.Personalizations[i]);
        }
        Assert.Equal(expectedSuccess, deserialized.Success);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Personalizations =
            [
                new()
                {
                    AssignedAt = 0,
                    ExperimentID = "experiment_id",
                    VariantID = "variant_id",
                    ExperimentKey = "experiment_key",
                    ExperimentName = "experiment_name",
                    VariantName = "variant_name",
                },
            ],
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new ExperimentPersonalizationResponse
        {
            Personalizations =
            [
                new()
                {
                    AssignedAt = 0,
                    ExperimentID = "experiment_id",
                    VariantID = "variant_id",
                    ExperimentKey = "experiment_key",
                    ExperimentName = "experiment_name",
                    VariantName = "variant_name",
                },
            ],
            Success = ExperimentPersonalizationResponseSuccess.True,
        };

        ExperimentPersonalizationResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class PersonalizationTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            VariantName = "variant_name",
        };

        double expectedAssignedAt = 0;
        string expectedExperimentID = "experiment_id";
        string expectedVariantID = "variant_id";
        string expectedExperimentKey = "experiment_key";
        string expectedExperimentName = "experiment_name";
        string expectedVariantName = "variant_name";

        Assert.Equal(expectedAssignedAt, model.AssignedAt);
        Assert.Equal(expectedExperimentID, model.ExperimentID);
        Assert.Equal(expectedVariantID, model.VariantID);
        Assert.Equal(expectedExperimentKey, model.ExperimentKey);
        Assert.Equal(expectedExperimentName, model.ExperimentName);
        Assert.Equal(expectedVariantName, model.VariantName);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            VariantName = "variant_name",
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Personalization>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            VariantName = "variant_name",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Personalization>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        double expectedAssignedAt = 0;
        string expectedExperimentID = "experiment_id";
        string expectedVariantID = "variant_id";
        string expectedExperimentKey = "experiment_key";
        string expectedExperimentName = "experiment_name";
        string expectedVariantName = "variant_name";

        Assert.Equal(expectedAssignedAt, deserialized.AssignedAt);
        Assert.Equal(expectedExperimentID, deserialized.ExperimentID);
        Assert.Equal(expectedVariantID, deserialized.VariantID);
        Assert.Equal(expectedExperimentKey, deserialized.ExperimentKey);
        Assert.Equal(expectedExperimentName, deserialized.ExperimentName);
        Assert.Equal(expectedVariantName, deserialized.VariantName);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            VariantName = "variant_name",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",
        };

        Assert.Null(model.ExperimentKey);
        Assert.False(model.RawData.ContainsKey("experiment_key"));
        Assert.Null(model.ExperimentName);
        Assert.False(model.RawData.ContainsKey("experiment_name"));
        Assert.Null(model.VariantName);
        Assert.False(model.RawData.ContainsKey("variant_name"));
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetValidation_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullAreSetToNull_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",

            ExperimentKey = null,
            ExperimentName = null,
            VariantName = null,
        };

        Assert.Null(model.ExperimentKey);
        Assert.True(model.RawData.ContainsKey("experiment_key"));
        Assert.Null(model.ExperimentName);
        Assert.True(model.RawData.ContainsKey("experiment_name"));
        Assert.Null(model.VariantName);
        Assert.True(model.RawData.ContainsKey("variant_name"));
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",

            ExperimentKey = null,
            ExperimentName = null,
            VariantName = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Personalization
        {
            AssignedAt = 0,
            ExperimentID = "experiment_id",
            VariantID = "variant_id",
            ExperimentKey = "experiment_key",
            ExperimentName = "experiment_name",
            VariantName = "variant_name",
        };

        Personalization copied = new(model);

        Assert.Equal(model, copied);
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
