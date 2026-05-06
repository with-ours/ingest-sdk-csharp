using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;

namespace OursPrivacy.Models.Batch;

[JsonConverter(typeof(JsonModelConverter<BatchCreateResponse, BatchCreateResponseFromRaw>))]
public sealed record class BatchCreateResponse : JsonModel
{
    public required long Accepted
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("accepted");
        }
        init { this._rawData.Set("accepted", value); }
    }

    public required ApiEnum<double, Failed> Failed
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ApiEnum<double, Failed>>("failed");
        }
        init { this._rawData.Set("failed", value); }
    }

    public required IReadOnlyList<Result> Results
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<Result>>("results");
        }
        init
        {
            this._rawData.Set<ImmutableArray<Result>>(
                "results",
                ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    public required ApiEnum<bool, BatchCreateResponseSuccess> Success
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ApiEnum<bool, BatchCreateResponseSuccess>>(
                "success"
            );
        }
        init { this._rawData.Set("success", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Accepted;
        this.Failed.Validate();
        foreach (var item in this.Results)
        {
            item.Validate();
        }
        this.Success.Validate();
    }

    public BatchCreateResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public BatchCreateResponse(BatchCreateResponse batchCreateResponse)
        : base(batchCreateResponse) { }
#pragma warning restore CS8618

    public BatchCreateResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BatchCreateResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="BatchCreateResponseFromRaw.FromRawUnchecked"/>
    public static BatchCreateResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BatchCreateResponseFromRaw : IFromRawJson<BatchCreateResponse>
{
    /// <inheritdoc/>
    public BatchCreateResponse FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        BatchCreateResponse.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(FailedConverter))]
public enum Failed
{
    V0,
}

sealed class FailedConverter : JsonConverter<Failed>
{
    public override Failed Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<double>(ref reader, options) switch
        {
            0 => Failed.V0,
            _ => (Failed)(-1),
        };
    }

    public override void Write(Utf8JsonWriter writer, Failed value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                Failed.V0 => 0,
                _ => throw new OursPrivacyInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}

[JsonConverter(typeof(JsonModelConverter<Result, ResultFromRaw>))]
public sealed record class Result : JsonModel
{
    public required long Index
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("index");
        }
        init { this._rawData.Set("index", value); }
    }

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
        _ = this.Index;
        this.Success.Validate();
    }

    public Result() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Result(Result result)
        : base(result) { }
#pragma warning restore CS8618

    public Result(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Result(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ResultFromRaw.FromRawUnchecked"/>
    public static Result FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ResultFromRaw : IFromRawJson<Result>
{
    /// <inheritdoc/>
    public Result FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Result.FromRawUnchecked(rawData);
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

[JsonConverter(typeof(BatchCreateResponseSuccessConverter))]
public enum BatchCreateResponseSuccess
{
    True,
}

sealed class BatchCreateResponseSuccessConverter : JsonConverter<BatchCreateResponseSuccess>
{
    public override BatchCreateResponseSuccess Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<bool>(ref reader, options) switch
        {
            true => BatchCreateResponseSuccess.True,
            _ => (BatchCreateResponseSuccess)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        BatchCreateResponseSuccess value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                BatchCreateResponseSuccess.True => true,
                _ => throw new OursPrivacyInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
