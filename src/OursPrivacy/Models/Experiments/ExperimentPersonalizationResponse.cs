using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;

namespace OursPrivacy.Models.Experiments;

[JsonConverter(
    typeof(JsonModelConverter<
        ExperimentPersonalizationResponse,
        ExperimentPersonalizationResponseFromRaw
    >)
)]
public sealed record class ExperimentPersonalizationResponse : JsonModel
{
    public required IReadOnlyList<Personalization> Personalizations
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<Personalization>>(
                "personalizations"
            );
        }
        init
        {
            this._rawData.Set<ImmutableArray<Personalization>>(
                "personalizations",
                ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    public required ApiEnum<bool, ExperimentPersonalizationResponseSuccess> Success
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<
                ApiEnum<bool, ExperimentPersonalizationResponseSuccess>
            >("success");
        }
        init { this._rawData.Set("success", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        foreach (var item in this.Personalizations)
        {
            item.Validate();
        }
        this.Success.Validate();
    }

    public ExperimentPersonalizationResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public ExperimentPersonalizationResponse(
        ExperimentPersonalizationResponse experimentPersonalizationResponse
    )
        : base(experimentPersonalizationResponse) { }
#pragma warning restore CS8618

    public ExperimentPersonalizationResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    ExperimentPersonalizationResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ExperimentPersonalizationResponseFromRaw.FromRawUnchecked"/>
    public static ExperimentPersonalizationResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ExperimentPersonalizationResponseFromRaw : IFromRawJson<ExperimentPersonalizationResponse>
{
    /// <inheritdoc/>
    public ExperimentPersonalizationResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => ExperimentPersonalizationResponse.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(JsonModelConverter<Personalization, PersonalizationFromRaw>))]
public sealed record class Personalization : JsonModel
{
    public required double AssignedAt
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<double>("assigned_at");
        }
        init { this._rawData.Set("assigned_at", value); }
    }

    public required string ExperimentID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("experiment_id");
        }
        init { this._rawData.Set("experiment_id", value); }
    }

    public required string VariantID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("variant_id");
        }
        init { this._rawData.Set("variant_id", value); }
    }

    public string? ExperimentKey
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("experiment_key");
        }
        init { this._rawData.Set("experiment_key", value); }
    }

    public string? ExperimentName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("experiment_name");
        }
        init { this._rawData.Set("experiment_name", value); }
    }

    public string? VariantName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("variant_name");
        }
        init { this._rawData.Set("variant_name", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.AssignedAt;
        _ = this.ExperimentID;
        _ = this.VariantID;
        _ = this.ExperimentKey;
        _ = this.ExperimentName;
        _ = this.VariantName;
    }

    public Personalization() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Personalization(Personalization personalization)
        : base(personalization) { }
#pragma warning restore CS8618

    public Personalization(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Personalization(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="PersonalizationFromRaw.FromRawUnchecked"/>
    public static Personalization FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class PersonalizationFromRaw : IFromRawJson<Personalization>
{
    /// <inheritdoc/>
    public Personalization FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Personalization.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(ExperimentPersonalizationResponseSuccessConverter))]
public enum ExperimentPersonalizationResponseSuccess
{
    True,
}

sealed class ExperimentPersonalizationResponseSuccessConverter
    : JsonConverter<ExperimentPersonalizationResponseSuccess>
{
    public override ExperimentPersonalizationResponseSuccess Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<bool>(ref reader, options) switch
        {
            true => ExperimentPersonalizationResponseSuccess.True,
            _ => (ExperimentPersonalizationResponseSuccess)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        ExperimentPersonalizationResponseSuccess value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                ExperimentPersonalizationResponseSuccess.True => true,
                _ => throw new OursPrivacyInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
