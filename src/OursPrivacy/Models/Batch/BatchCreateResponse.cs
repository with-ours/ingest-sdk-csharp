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

    public required long Failed
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("failed");
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

    public required bool Success
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<bool>("success");
        }
        init { this._rawData.Set("success", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Accepted;
        _ = this.Failed;
        foreach (var item in this.Results)
        {
            item.Validate();
        }
        _ = this.Success;
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

[JsonConverter(typeof(ResultConverter))]
public record class Result : ModelBase
{
    public object? Value { get; } = null;

    JsonElement? _element = null;

    public JsonElement Json
    {
        get
        {
            return this._element ??= JsonSerializer.SerializeToElement(
                this.Value,
                ModelBase.SerializerOptions
            );
        }
    }

    public long Index
    {
        get { return Match(unionMember0: (x) => x.Index, unionMember1: (x) => x.Index); }
    }

    public Result(UnionMember0 value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Result(UnionMember1 value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Result(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="UnionMember0"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember0(out var value)) {
    ///     // `value` is of type `UnionMember0`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember0([NotNullWhen(true)] out UnionMember0? value)
    {
        value = this.Value as UnionMember0;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="UnionMember1"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember1(out var value)) {
    ///     // `value` is of type `UnionMember1`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember1([NotNullWhen(true)] out UnionMember1? value)
    {
        value = this.Value as UnionMember1;
        return value != null;
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Match"/>
    /// if you need your function parameters to return something.</para>
    ///
    /// <exception cref="OursPrivacyInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// instance.Switch(
    ///     (UnionMember0 value) =&gt; {...},
    ///     (UnionMember1 value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<UnionMember0> unionMember0, Action<UnionMember1> unionMember1)
    {
        switch (this.Value)
        {
            case UnionMember0 value:
                unionMember0(value);
                break;
            case UnionMember1 value:
                unionMember1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of Result"
                );
        }
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with and
    /// returns its result.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Switch"/>
    /// if you don't need your function parameters to return a value.</para>
    ///
    /// <exception cref="OursPrivacyInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// var result = instance.Match(
    ///     (UnionMember0 value) =&gt; {...},
    ///     (UnionMember1 value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<UnionMember0, T> unionMember0, Func<UnionMember1, T> unionMember1)
    {
        return this.Value switch
        {
            UnionMember0 value => unionMember0(value),
            UnionMember1 value => unionMember1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of Result"
            ),
        };
    }

    public static implicit operator Result(UnionMember0 value) => new(value);

    public static implicit operator Result(UnionMember1 value) => new(value);

    /// <summary>
    /// Validates that the instance was constructed with a known variant and that this variant is valid
    /// (based on its own <c>Validate</c> method).
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="OursPrivacyInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public override void Validate()
    {
        if (this.Value == null)
        {
            throw new OursPrivacyInvalidDataException("Data did not match any variant of Result");
        }
        this.Switch(
            (unionMember0) => unionMember0.Validate(),
            (unionMember1) => unionMember1.Validate()
        );
    }

    public virtual bool Equals(Result? other) =>
        other != null
        && this.VariantIndex() == other.VariantIndex()
        && JsonElement.DeepEquals(this.Json, other.Json);

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(this.Json),
            ModelBase.ToStringSerializerOptions
        );

    int VariantIndex()
    {
        return this.Value switch
        {
            UnionMember0 _ => 0,
            UnionMember1 _ => 1,
            _ => -1,
        };
    }
}

sealed class ResultConverter : JsonConverter<Result>
{
    public override Result? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<UnionMember0>(element, options);
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<UnionMember1>(element, options);
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

[JsonConverter(typeof(JsonModelConverter<UnionMember0, UnionMember0FromRaw>))]
public sealed record class UnionMember0 : JsonModel
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

    public UnionMember0() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public UnionMember0(UnionMember0 unionMember0)
        : base(unionMember0) { }
#pragma warning restore CS8618

    public UnionMember0(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    UnionMember0(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="UnionMember0FromRaw.FromRawUnchecked"/>
    public static UnionMember0 FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class UnionMember0FromRaw : IFromRawJson<UnionMember0>
{
    /// <inheritdoc/>
    public UnionMember0 FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        UnionMember0.FromRawUnchecked(rawData);
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

[JsonConverter(typeof(JsonModelConverter<UnionMember1, UnionMember1FromRaw>))]
public sealed record class UnionMember1 : JsonModel
{
    public required ApiEnum<string, Code> Code
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ApiEnum<string, Code>>("code");
        }
        init { this._rawData.Set("code", value); }
    }

    public required long Index
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("index");
        }
        init { this._rawData.Set("index", value); }
    }

    public required string Message
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("message");
        }
        init { this._rawData.Set("message", value); }
    }

    public required ApiEnum<bool, UnionMember1Success> Success
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ApiEnum<bool, UnionMember1Success>>("success");
        }
        init { this._rawData.Set("success", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Code.Validate();
        _ = this.Index;
        _ = this.Message;
        this.Success.Validate();
    }

    public UnionMember1() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public UnionMember1(UnionMember1 unionMember1)
        : base(unionMember1) { }
#pragma warning restore CS8618

    public UnionMember1(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    UnionMember1(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="UnionMember1FromRaw.FromRawUnchecked"/>
    public static UnionMember1 FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class UnionMember1FromRaw : IFromRawJson<UnionMember1>
{
    /// <inheritdoc/>
    public UnionMember1 FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        UnionMember1.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(CodeConverter))]
public enum Code
{
    InvalidEvent,
    QueueFailed,
}

sealed class CodeConverter : JsonConverter<Code>
{
    public override Code Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "invalid_event" => Code.InvalidEvent,
            "queue_failed" => Code.QueueFailed,
            _ => (Code)(-1),
        };
    }

    public override void Write(Utf8JsonWriter writer, Code value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                Code.InvalidEvent => "invalid_event",
                Code.QueueFailed => "queue_failed",
                _ => throw new OursPrivacyInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}

[JsonConverter(typeof(UnionMember1SuccessConverter))]
public enum UnionMember1Success
{
    False,
}

sealed class UnionMember1SuccessConverter : JsonConverter<UnionMember1Success>
{
    public override UnionMember1Success Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<bool>(ref reader, options) switch
        {
            false => UnionMember1Success.False,
            _ => (UnionMember1Success)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember1Success value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                UnionMember1Success.False => false,
                _ => throw new OursPrivacyInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
