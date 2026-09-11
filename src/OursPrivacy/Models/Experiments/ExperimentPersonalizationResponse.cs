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

    /// <summary>
    /// The visitor traits accumulated by your personalization property rules, keyed
    /// by property key. Values are always scalars — a string, number, or boolean,
    /// or null when the captured field was itself empty. Empty for a visitor who
    /// has not matched any rule yet. These same values are delivered to the visitor's
    /// browser and are readable by anyone who knows the visitor_id, so never accumulate
    /// secrets, credentials, PHI, or confidential data into a property.
    /// </summary>
    public required IReadOnlyDictionary<string, Property> Properties
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<FrozenDictionary<string, Property>>("properties");
        }
        init
        {
            this._rawData.Set<FrozenDictionary<string, Property>>(
                "properties",
                FrozenDictionary.ToFrozenDictionary(value)
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
        foreach (var item in this.Properties.Values)
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

[JsonConverter(typeof(PropertyConverter))]
public record class Property : ModelBase
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

    public Property(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Property(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Property(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Property(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="string"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickString(out var value)) {
    ///     // `value` is of type `string`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickString([NotNullWhen(true)] out string? value)
    {
        value = this.Value as string;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="double"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickDouble(out var value)) {
    ///     // `value` is of type `double`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickDouble([NotNullWhen(true)] out double? value)
    {
        value = this.Value as double?;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="bool"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickBool(out var value)) {
    ///     // `value` is of type `bool`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickBool([NotNullWhen(true)] out bool? value)
    {
        value = this.Value as bool?;
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
    ///     (string value) =&gt; {...},
    ///     (double value) =&gt; {...},
    ///     (bool value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<string> @string, Action<double> @double, Action<bool> @bool)
    {
        switch (this.Value)
        {
            case string value:
                @string(value);
                break;
            case double value:
                @double(value);
                break;
            case bool value:
                @bool(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of Property"
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
    ///     (string value) =&gt; {...},
    ///     (double value) =&gt; {...},
    ///     (bool value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<string, T> @string, Func<double, T> @double, Func<bool, T> @bool)
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of Property"
            ),
        };
    }

    public static implicit operator Property(string value) => new(value);

    public static implicit operator Property(double value) => new(value);

    public static implicit operator Property(bool value) => new(value);

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
            throw new OursPrivacyInvalidDataException("Data did not match any variant of Property");
        }
    }

    public virtual bool Equals(Property? other) =>
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
            string _ => 0,
            double _ => 1,
            bool _ => 2,
            _ => -1,
        };
    }
}

sealed class PropertyConverter : JsonConverter<Property>
{
    public override Property? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<string>(element, options);
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            return new(JsonSerializer.Deserialize<double>(element, options), element);
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            return new(JsonSerializer.Deserialize<bool>(element, options), element);
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(Utf8JsonWriter writer, Property value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
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
