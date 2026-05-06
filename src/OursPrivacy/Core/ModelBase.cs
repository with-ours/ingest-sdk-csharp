using System.Text.Json;
using OursPrivacy.Exceptions;
using OursPrivacy.Models.Track;
using Batch = OursPrivacy.Models.Batch;
using Visitor = OursPrivacy.Models.Visitor;

namespace OursPrivacy.Core;

/// <summary>
/// The base class for all API objects with properties.
///
/// <para>API objects such as enums do not inherit from this class.</para>
/// </summary>
public abstract record class ModelBase
{
    protected ModelBase(ModelBase modelBase)
    {
        // Nothing to copy. Just so that subclasses can define copy constructors.
    }

    internal static readonly JsonSerializerOptions SerializerOptions = new()
    {
        Converters =
        {
            new FrozenDictionaryConverterFactory(),
            new ApiEnumConverter<bool, Success>(),
            new ApiEnumConverter<bool, Visitor::Success>(),
            new ApiEnumConverter<double, Batch::Failed>(),
            new ApiEnumConverter<bool, Batch::Success>(),
            new ApiEnumConverter<bool, Batch::BatchCreateResponseSuccess>(),
        },
    };

    internal static readonly JsonSerializerOptions ToStringSerializerOptions = new(
        SerializerOptions
    )
    {
        WriteIndented = true,
    };

    /// <summary>
    /// Validates that all required fields are set and that each field's value is of the expected type.
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="OursPrivacyInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public abstract void Validate();
}
