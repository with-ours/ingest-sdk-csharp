using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;

namespace OursPrivacy.Models.Track;

[JsonConverter(typeof(JsonModelConverter<TrackEventResponse, TrackEventResponseFromRaw>))]
public sealed record class TrackEventResponse : JsonModel
{
    public required ApiEnum<bool, Success> Success
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ApiEnum<bool, Success>>("success");
        }
        init { this._rawData.Set("success", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Success.Validate();
    }

    public TrackEventResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public TrackEventResponse(TrackEventResponse trackEventResponse)
        : base(trackEventResponse) { }
#pragma warning restore CS8618

    public TrackEventResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    TrackEventResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="TrackEventResponseFromRaw.FromRawUnchecked"/>
    public static TrackEventResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public TrackEventResponse(ApiEnum<bool, Success> success)
        : this()
    {
        this.Success = success;
    }
}

class TrackEventResponseFromRaw : IFromRawJson<TrackEventResponse>
{
    /// <inheritdoc/>
    public TrackEventResponse FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        TrackEventResponse.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(SuccessConverter))]
public enum Success
{
    True,
}

sealed class SuccessConverter : JsonConverter<Success>
{
    public override Success Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<bool>(ref reader, options) switch
        {
            true => Success.True,
            _ => (Success)(-1),
        };
    }

    public override void Write(Utf8JsonWriter writer, Success value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                Success.True => true,
                _ => throw new OursPrivacyInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
