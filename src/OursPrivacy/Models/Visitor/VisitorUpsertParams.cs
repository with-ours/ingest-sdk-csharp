using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;

namespace OursPrivacy.Models.Visitor;

/// <summary>
/// Set or update properties on an existing visitor, or create a new visitor if no
/// match is found. This fires a $identify event, making the call visible in the event
/// stream. Identity resolution runs in priority order: userId (direct, no lookup)
/// → externalId → email → userProperties.phone_number. Each lookup runs only if
/// earlier identifiers did not resolve a visitor. Phone matching ignores common formatting
/// and an international + or 00 prefix, but does not infer a country code. When a
/// visitor is found, their Ours Visitor ID is used going forward so all future events
/// are attached to the same profile. For top-level visitor properties: null clears
/// the existing value, while undefined, omitted fields, and empty strings are ignored.
/// For entries inside custom_properties: null, undefined, and empty strings are all
/// ignored (custom_properties use merge semantics). See https://docs.oursprivacy.com/docs/data-types
/// for details and common pitfalls.
///
/// <para>NOTE: Do not inherit from this type outside the SDK unless you're okay with
/// breaking changes in non-major versions. We may add new methods in the future that
/// cause existing derived classes to break.</para>
/// </summary>
public record class VisitorUpsertParams : ParamsBase
{
    readonly JsonDictionary _rawBodyData = new();
    public IReadOnlyDictionary<string, JsonElement> RawBodyData
    {
        get { return this._rawBodyData.Freeze(); }
    }

    /// <summary>
    /// The token for your Source. You can find this in the dashboard. Leading and
    /// trailing whitespace is removed before validation.
    /// </summary>
    public required string Token
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNotNullClass<string>("token");
        }
        init { this._rawBodyData.Set("token", value); }
    }

    /// <summary>
    /// User properties to associate with this user. The existing user properties
    /// will be updated. And all future events will have these properties associated
    /// with them.
    /// </summary>
    public required UserProperties UserProperties
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNotNullClass<UserProperties>("userProperties");
        }
        init { this._rawBodyData.Set("userProperties", value); }
    }

    /// <summary>
    /// These properties are used throughout the Ours app to pass known values onto destinations
    /// </summary>
    public DefaultProperties? DefaultProperties
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<DefaultProperties>("defaultProperties");
        }
        init { this._rawBodyData.Set("defaultProperties", value); }
    }

    /// <summary>
    /// The email address of a user. When userId is absent and externalId does not
    /// resolve a visitor, we search your account for a visitor with this email. If
    /// no match is found, we try userProperties.phone_number before creating a new
    /// visitor. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Email
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<string>("email");
        }
        init { this._rawBodyData.Set("email", value); }
    }

    /// <summary>
    /// Your system's unique identifier for this user. When userId is absent, we
    /// search your account for an existing visitor with this externalId. If no match
    /// is found, we try email and then userProperties.phone_number before creating
    /// a new visitor. If you also have the userId from cookies or local storage,
    /// send both — it removes the lookup round-trip. Non-string input is converted
    /// to text before validation; clients should send a string. Leading and trailing
    /// whitespace is removed before validation.
    /// </summary>
    public string? ExternalID
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<string>("externalId");
        }
        init { this._rawBodyData.Set("externalId", value); }
    }

    /// <summary>
    /// End-user network context for server-side calls. Required for probabilistic
    /// identity resolution when the caller is a backend server rather than an end-user browser.
    /// </summary>
    public IdentityContext? IdentityContext
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<IdentityContext>("identityContext");
        }
        init { this._rawBodyData.Set("identityContext", value); }
    }

    /// <summary>
    /// The Ours Visitor ID stored in local storage and cookies on your web properties.
    /// When present, this is used directly — no lookup by externalId, email, or phone
    /// is performed. If you have both a userId and an externalId, send both so the
    /// event is attached to the right visitor without any lookup overhead. Leading
    /// and trailing whitespace is removed before validation.
    /// </summary>
    public string? UserID
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<string>("userId");
        }
        init { this._rawBodyData.Set("userId", value); }
    }

    public VisitorUpsertParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public VisitorUpsertParams(VisitorUpsertParams visitorUpsertParams)
        : base(visitorUpsertParams)
    {
        this._rawBodyData = new(visitorUpsertParams._rawBodyData);
    }
#pragma warning restore CS8618

    public VisitorUpsertParams(
        IReadOnlyDictionary<string, JsonElement> rawHeaderData,
        IReadOnlyDictionary<string, JsonElement> rawQueryData,
        IReadOnlyDictionary<string, JsonElement> rawBodyData
    )
    {
        this._rawHeaderData = new(rawHeaderData);
        this._rawQueryData = new(rawQueryData);
        this._rawBodyData = new(rawBodyData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    VisitorUpsertParams(
        FrozenDictionary<string, JsonElement> rawHeaderData,
        FrozenDictionary<string, JsonElement> rawQueryData,
        FrozenDictionary<string, JsonElement> rawBodyData
    )
    {
        this._rawHeaderData = new(rawHeaderData);
        this._rawQueryData = new(rawQueryData);
        this._rawBodyData = new(rawBodyData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="IFromRawJson{T}.FromRawUnchecked"/>
    public static VisitorUpsertParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawHeaderData,
        IReadOnlyDictionary<string, JsonElement> rawQueryData,
        IReadOnlyDictionary<string, JsonElement> rawBodyData
    )
    {
        return new(
            FrozenDictionary.ToFrozenDictionary(rawHeaderData),
            FrozenDictionary.ToFrozenDictionary(rawQueryData),
            FrozenDictionary.ToFrozenDictionary(rawBodyData)
        );
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(
                new Dictionary<string, JsonElement>()
                {
                    ["HeaderData"] = FriendlyJsonPrinter.PrintValue(
                        JsonSerializer.SerializeToElement(this._rawHeaderData.Freeze())
                    ),
                    ["QueryData"] = FriendlyJsonPrinter.PrintValue(
                        JsonSerializer.SerializeToElement(this._rawQueryData.Freeze())
                    ),
                    ["BodyData"] = FriendlyJsonPrinter.PrintValue(this._rawBodyData.Freeze()),
                }
            ),
            ModelBase.ToStringSerializerOptions
        );

    public virtual bool Equals(VisitorUpsertParams? other)
    {
        if (other == null)
        {
            return false;
        }
        return this._rawHeaderData.Equals(other._rawHeaderData)
            && this._rawQueryData.Equals(other._rawQueryData)
            && this._rawBodyData.Equals(other._rawBodyData);
    }

    public override Uri Url(ClientOptions options)
    {
        return new UriBuilder(options.BaseUrl.ToString().TrimEnd('/') + "/identify")
        {
            Query = this.QueryString(options),
        }.Uri;
    }

    internal override HttpContent? BodyContent()
    {
        return new StringContent(
            JsonSerializer.Serialize(this.RawBodyData, ModelBase.SerializerOptions),
            Encoding.UTF8,
            "application/json"
        );
    }

    internal override void AddHeadersToRequest(HttpRequestMessage request, ClientOptions options)
    {
        ParamsBase.AddDefaultHeaders(request, options);
        foreach (var item in this.RawHeaderData)
        {
            ParamsBase.AddHeaderElementToRequest(request, item.Key, item.Value);
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

/// <summary>
/// User properties to associate with this user. The existing user properties will
/// be updated. And all future events will have these properties associated with them.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<UserProperties, UserPropertiesFromRaw>))]
public sealed record class UserProperties : JsonModel
{
    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? _EfTransactionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("_ef_transaction_id");
        }
        init { this._rawData.Set("_ef_transaction_id", value); }
    }

    public string? AdID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ad_id");
        }
        init { this._rawData.Set("ad_id", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? AdmitadUid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("admitad_uid");
        }
        init { this._rawData.Set("admitad_uid", value); }
    }

    public string? AdsetID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("adset_id");
        }
        init { this._rawData.Set("adset_id", value); }
    }

    public string? Alart
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("alart");
        }
        init { this._rawData.Set("alart", value); }
    }

    public string? Aleid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("aleid");
        }
        init { this._rawData.Set("aleid", value); }
    }

    public string? Axwrt
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("axwrt");
        }
        init { this._rawData.Set("axwrt", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? BasisCid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("basis_cid");
        }
        init { this._rawData.Set("basis_cid", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? BeeswaxAuctionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("beeswax_auction_id");
        }
        init { this._rawData.Set("beeswax_auction_id", value); }
    }

    public string? CampaignID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("campaign_id");
        }
        init { this._rawData.Set("campaign_id", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? City
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("city");
        }
        init { this._rawData.Set("city", value); }
    }

    public string? Clickid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("clickid");
        }
        init { this._rawData.Set("clickid", value); }
    }

    public string? Clid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("clid");
        }
        init { this._rawData.Set("clid", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? CompanyName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("company_name");
        }
        init { this._rawData.Set("company_name", value); }
    }

    /// <summary>
    /// Use at most 200 nonempty property keys and five object levels. Array contents
    /// do not count toward the object-depth limit. Empty keys are removed recursively,
    /// arrays are truncated to 250 entries, and top-level numbers outside the safe
    /// integer range become strings. After normalization, the serialized JSON must
    /// be shorter than 1,048,576 UTF-16 code units.
    /// </summary>
    public IReadOnlyDictionary<string, Consent?>? Consent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<FrozenDictionary<string, Consent?>>("consent");
        }
        init
        {
            this._rawData.Set<FrozenDictionary<string, Consent?>?>(
                "consent",
                value == null ? null : FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Country
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("country");
        }
        init { this._rawData.Set("country", value); }
    }

    /// <summary>
    /// Use at most 200 nonempty property keys and five object levels. Array contents
    /// do not count toward the object-depth limit. Empty keys are removed recursively,
    /// arrays are truncated to 250 entries, and top-level numbers outside the safe
    /// integer range become strings. After normalization, the serialized JSON must
    /// be shorter than 1,048,576 UTF-16 code units.
    /// </summary>
    public IReadOnlyDictionary<string, CustomProperty?>? CustomProperties
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<FrozenDictionary<string, CustomProperty?>>(
                "custom_properties"
            );
        }
        init
        {
            this._rawData.Set<FrozenDictionary<string, CustomProperty?>?>(
                "custom_properties",
                value == null ? null : FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? DateOfBirth
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("date_of_birth");
        }
        init { this._rawData.Set("date_of_birth", value); }
    }

    public string? Dclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("dclid");
        }
        init { this._rawData.Set("dclid", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Email
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("email");
        }
        init { this._rawData.Set("email", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Epik
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("epik");
        }
        init { this._rawData.Set("epik", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? ExternalID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("external_id");
        }
        init { this._rawData.Set("external_id", value); }
    }

    public string? Fbc
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("fbc");
        }
        init { this._rawData.Set("fbc", value); }
    }

    public string? Fbclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("fbclid");
        }
        init { this._rawData.Set("fbclid", value); }
    }

    public string? Fbp
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("fbp");
        }
        init { this._rawData.Set("fbp", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? FirstName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("first_name");
        }
        init { this._rawData.Set("first_name", value); }
    }

    public string? GadSource
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gad_source");
        }
        init { this._rawData.Set("gad_source", value); }
    }

    public string? Gbraid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gbraid");
        }
        init { this._rawData.Set("gbraid", value); }
    }

    public string? Gclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gclid");
        }
        init { this._rawData.Set("gclid", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Gender
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gender");
        }
        init { this._rawData.Set("gender", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Graid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("graid");
        }
        init { this._rawData.Set("graid", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Grclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("grclid");
        }
        init { this._rawData.Set("grclid", value); }
    }

    public string? ImRef
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("im_ref");
        }
        init { this._rawData.Set("im_ref", value); }
    }

    /// <summary>
    /// The IP address of the user Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? IP
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ip");
        }
        init { this._rawData.Set("ip", value); }
    }

    public string? Irclickid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("irclickid");
        }
        init { this._rawData.Set("irclickid", value); }
    }

    /// <summary>
    /// Any JSON value: string, number, boolean, array, object, or null.
    /// </summary>
    public IsBot? IsBot
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<IsBot>("is_bot");
        }
        init { this._rawData.Set("is_bot", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? JobTitle
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("job_title");
        }
        init { this._rawData.Set("job_title", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? LastName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("last_name");
        }
        init { this._rawData.Set("last_name", value); }
    }

    public string? LiFatID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("li_fat_id");
        }
        init { this._rawData.Set("li_fat_id", value); }
    }

    public string? Msclkid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("msclkid");
        }
        init { this._rawData.Set("msclkid", value); }
    }

    public string? Ndclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ndclid");
        }
        init { this._rawData.Set("ndclid", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? ObClickID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ob_click_id");
        }
        init { this._rawData.Set("ob_click_id", value); }
    }

    public string? Oppref
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("oppref");
        }
        init { this._rawData.Set("oppref", value); }
    }

    /// <summary>
    /// Any JSON value: string, number, boolean, array, object, or null.
    /// </summary>
    public PhoneNumber? PhoneNumber
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<PhoneNumber>("phone_number");
        }
        init { this._rawData.Set("phone_number", value); }
    }

    public string? Qclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("qclid");
        }
        init { this._rawData.Set("qclid", value); }
    }

    public string? RdtCid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("rdt_cid");
        }
        init { this._rawData.Set("rdt_cid", value); }
    }

    public string? Referrer
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("referrer");
        }
        init { this._rawData.Set("referrer", value); }
    }

    public string? ReferringDomain
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("referring_domain");
        }
        init { this._rawData.Set("referring_domain", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Sacid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sacid");
        }
        init { this._rawData.Set("sacid", value); }
    }

    public string? Sccid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sccid");
        }
        init { this._rawData.Set("sccid", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string.
    /// </summary>
    public string? Sid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sid");
        }
        init { this._rawData.Set("sid", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? State
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("state");
        }
        init { this._rawData.Set("state", value); }
    }

    public string? Ttclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ttclid");
        }
        init { this._rawData.Set("ttclid", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? TtdImpressionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ttd_impression_id");
        }
        init { this._rawData.Set("ttd_impression_id", value); }
    }

    public string? Twclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("twclid");
        }
        init { this._rawData.Set("twclid", value); }
    }

    public string? UserAgent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("user_agent");
        }
        init { this._rawData.Set("user_agent", value); }
    }

    public string? UserAgentFullList
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("user_agent_full_list");
        }
        init { this._rawData.Set("user_agent_full_list", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string.
    /// </summary>
    public string? UtmCampaign
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_campaign");
        }
        init { this._rawData.Set("utm_campaign", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string.
    /// </summary>
    public string? UtmContent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_content");
        }
        init { this._rawData.Set("utm_content", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string.
    /// </summary>
    public string? UtmMedium
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_medium");
        }
        init { this._rawData.Set("utm_medium", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string.
    /// </summary>
    public string? UtmName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_name");
        }
        init { this._rawData.Set("utm_name", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string.
    /// </summary>
    public string? UtmSource
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_source");
        }
        init { this._rawData.Set("utm_source", value); }
    }

    /// <summary>
    /// Non-string input is converted to text before validation; clients should send
    /// a string.
    /// </summary>
    public string? UtmTerm
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_term");
        }
        init { this._rawData.Set("utm_term", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? ViantClickID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("viant_click_id");
        }
        init { this._rawData.Set("viant_click_id", value); }
    }

    /// <summary>
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? ViantImpressionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("viant_impression_id");
        }
        init { this._rawData.Set("viant_impression_id", value); }
    }

    public string? Wbraid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("wbraid");
        }
        init { this._rawData.Set("wbraid", value); }
    }

    public string? Ylpcid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ylpcid");
        }
        init { this._rawData.Set("ylpcid", value); }
    }

    /// <summary>
    /// Any JSON value: string, number, boolean, array, object, or null.
    /// </summary>
    public Zip? Zip
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<Zip>("zip");
        }
        init { this._rawData.Set("zip", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this._EfTransactionID;
        _ = this.AdID;
        _ = this.AdmitadUid;
        _ = this.AdsetID;
        _ = this.Alart;
        _ = this.Aleid;
        _ = this.Axwrt;
        _ = this.BasisCid;
        _ = this.BeeswaxAuctionID;
        _ = this.CampaignID;
        _ = this.City;
        _ = this.Clickid;
        _ = this.Clid;
        _ = this.CompanyName;
        if (this.Consent != null)
        {
            foreach (var item in this.Consent.Values)
            {
                item?.Validate();
            }
        }
        _ = this.Country;
        if (this.CustomProperties != null)
        {
            foreach (var item in this.CustomProperties.Values)
            {
                item?.Validate();
            }
        }
        _ = this.DateOfBirth;
        _ = this.Dclid;
        _ = this.Email;
        _ = this.Epik;
        _ = this.ExternalID;
        _ = this.Fbc;
        _ = this.Fbclid;
        _ = this.Fbp;
        _ = this.FirstName;
        _ = this.GadSource;
        _ = this.Gbraid;
        _ = this.Gclid;
        _ = this.Gender;
        _ = this.Graid;
        _ = this.Grclid;
        _ = this.ImRef;
        _ = this.IP;
        _ = this.Irclickid;
        this.IsBot?.Validate();
        _ = this.JobTitle;
        _ = this.LastName;
        _ = this.LiFatID;
        _ = this.Msclkid;
        _ = this.Ndclid;
        _ = this.ObClickID;
        _ = this.Oppref;
        this.PhoneNumber?.Validate();
        _ = this.Qclid;
        _ = this.RdtCid;
        _ = this.Referrer;
        _ = this.ReferringDomain;
        _ = this.Sacid;
        _ = this.Sccid;
        _ = this.Sid;
        _ = this.State;
        _ = this.Ttclid;
        _ = this.TtdImpressionID;
        _ = this.Twclid;
        _ = this.UserAgent;
        _ = this.UserAgentFullList;
        _ = this.UtmCampaign;
        _ = this.UtmContent;
        _ = this.UtmMedium;
        _ = this.UtmName;
        _ = this.UtmSource;
        _ = this.UtmTerm;
        _ = this.ViantClickID;
        _ = this.ViantImpressionID;
        _ = this.Wbraid;
        _ = this.Ylpcid;
        this.Zip?.Validate();
    }

    public UserProperties() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public UserProperties(UserProperties userProperties)
        : base(userProperties) { }
#pragma warning restore CS8618

    public UserProperties(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    UserProperties(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="UserPropertiesFromRaw.FromRawUnchecked"/>
    public static UserProperties FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class UserPropertiesFromRaw : IFromRawJson<UserProperties>
{
    /// <inheritdoc/>
    public UserProperties FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        UserProperties.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(ConsentConverter))]
public record class Consent : ModelBase
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

    public Consent(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Consent(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Consent(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Consent(IReadOnlyList<JsonValue?> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public Consent(
        IReadOnlyDictionary<string, UnionMember4Item?> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public Consent(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonValues([NotNullWhen(true)] out IReadOnlyList<JsonValue?>? value)
    {
        value = this.Value as IReadOnlyList<JsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>UnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, UnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4Items(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, UnionMember4Item?>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, UnionMember4Item?>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonValue?>> jsonValues,
        Action<IReadOnlyDictionary<string, UnionMember4Item?>> unionMember4Items
    )
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
            case IReadOnlyList<JsonValue?> value:
                jsonValues(value);
                break;
            case IReadOnlyDictionary<string, UnionMember4Item?> value:
                unionMember4Items(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of Consent"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonValue?>, T> jsonValues,
        Func<IReadOnlyDictionary<string, UnionMember4Item?>, T> unionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonValue?> value => jsonValues(value),
            IReadOnlyDictionary<string, UnionMember4Item?> value => unionMember4Items(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of Consent"
            ),
        };
    }

    public static implicit operator Consent(string value) => new(value);

    public static implicit operator Consent(double value) => new(value);

    public static implicit operator Consent(bool value) => new(value);

    public static implicit operator Consent(List<JsonValue?> value) =>
        new((IReadOnlyList<JsonValue?>)value);

    public static implicit operator Consent(Dictionary<string, UnionMember4Item?> value) =>
        new((IReadOnlyDictionary<string, UnionMember4Item?>)value);

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
            throw new OursPrivacyInvalidDataException("Data did not match any variant of Consent");
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (jsonValues) =>
            {
                foreach (var item in jsonValues)
                {
                    item?.Validate();
                }
            },
            (unionMember4Items) =>
            {
                foreach (var item in unionMember4Items.Values)
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(Consent? other) =>
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
            IReadOnlyList<JsonValue?> _ => 3,
            IReadOnlyDictionary<string, UnionMember4Item?> _ => 4,
            _ => -1,
        };
    }
}

sealed class ConsentConverter : JsonConverter<Consent?>
{
    public override Consent? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonValue?>>(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, UnionMember4Item?>>(
                element,
                options
            );
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(Utf8JsonWriter writer, Consent? value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(JsonValueConverter))]
public record class JsonValue : ModelBase
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

    public JsonValue(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public JsonValue(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public JsonValue(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public JsonValue(IReadOnlyList<JsonElement> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public JsonValue(IReadOnlyDictionary<string, JsonElement> value, JsonElement? element = null)
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public JsonValue(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of JsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of JsonValue"
            ),
        };
    }

    public static implicit operator JsonValue(string value) => new(value);

    public static implicit operator JsonValue(double value) => new(value);

    public static implicit operator JsonValue(bool value) => new(value);

    public static implicit operator JsonValue(List<JsonElement> value) =>
        new((IReadOnlyList<JsonElement>)value);

    public static implicit operator JsonValue(Dictionary<string, JsonElement> value) =>
        new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of JsonValue"
            );
        }
    }

    public virtual bool Equals(JsonValue? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class JsonValueConverter : JsonConverter<JsonValue?>
{
    public override JsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        JsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(UnionMember4ItemConverter))]
public record class UnionMember4Item : ModelBase
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

    public UnionMember4Item(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4Item(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4Item(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4Item(
        IReadOnlyList<UnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4Item(
        IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public UnionMember4Item(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>UnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;UnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4ItemJsonValues(
        [NotNullWhen(true)] out IReadOnlyList<UnionMember4ItemJsonValue?>? value
    )
    {
        value = this.Value as IReadOnlyList<UnionMember4ItemJsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>UnionMember4ItemUnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4ItemUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4ItemUnionMember4Items(
        [NotNullWhen(true)]
            out IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<UnionMember4ItemJsonValue?>> unionMember4ItemJsonValues,
        Action<
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?>
        > unionMember4ItemUnionMember4Items
    )
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
            case IReadOnlyList<UnionMember4ItemJsonValue?> value:
                unionMember4ItemJsonValues(value);
                break;
            case IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?> value:
                unionMember4ItemUnionMember4Items(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<UnionMember4ItemJsonValue?>, T> unionMember4ItemJsonValues,
        Func<
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?>,
            T
        > unionMember4ItemUnionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<UnionMember4ItemJsonValue?> value => unionMember4ItemJsonValues(value),
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?> value =>
                unionMember4ItemUnionMember4Items(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4Item"
            ),
        };
    }

    public static implicit operator UnionMember4Item(string value) => new(value);

    public static implicit operator UnionMember4Item(double value) => new(value);

    public static implicit operator UnionMember4Item(bool value) => new(value);

    public static implicit operator UnionMember4Item(List<UnionMember4ItemJsonValue?> value) =>
        new((IReadOnlyList<UnionMember4ItemJsonValue?>)value);

    public static implicit operator UnionMember4Item(
        Dictionary<string, UnionMember4ItemUnionMember4Item?> value
    ) => new((IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (unionMember4ItemJsonValues) =>
            {
                foreach (var item in unionMember4ItemJsonValues)
                {
                    item?.Validate();
                }
            },
            (unionMember4ItemUnionMember4Items) =>
            {
                foreach (var item in unionMember4ItemUnionMember4Items.Values)
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(UnionMember4Item? other) =>
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
            IReadOnlyList<UnionMember4ItemJsonValue?> _ => 3,
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4Item?> _ => 4,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemConverter : JsonConverter<UnionMember4Item?>
{
    public override UnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<UnionMember4ItemJsonValue?>>(
                element,
                options
            );
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                Dictionary<string, UnionMember4ItemUnionMember4Item?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(UnionMember4ItemJsonValueConverter))]
public record class UnionMember4ItemJsonValue : ModelBase
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

    public UnionMember4ItemJsonValue(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemJsonValue(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemJsonValue(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemJsonValue(IReadOnlyList<JsonElement> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public UnionMember4ItemJsonValue(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator UnionMember4ItemJsonValue(string value) => new(value);

    public static implicit operator UnionMember4ItemJsonValue(double value) => new(value);

    public static implicit operator UnionMember4ItemJsonValue(bool value) => new(value);

    public static implicit operator UnionMember4ItemJsonValue(List<JsonElement> value) =>
        new((IReadOnlyList<JsonElement>)value);

    public static implicit operator UnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(UnionMember4ItemJsonValue? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemJsonValueConverter : JsonConverter<UnionMember4ItemJsonValue?>
{
    public override UnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(UnionMember4ItemUnionMember4ItemConverter))]
public record class UnionMember4ItemUnionMember4Item : ModelBase
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

    public UnionMember4ItemUnionMember4Item(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4Item(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4Item(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4Item(
        IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4Item(
        IReadOnlyDictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4Item(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>UnionMember4ItemUnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4ItemUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4ItemUnionMember4ItemJsonValues(
        [NotNullWhen(true)] out IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?>? value
    )
    {
        value = this.Value as IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>UnionMember4ItemUnionMember4ItemUnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4ItemUnionMember4ItemUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4ItemUnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4ItemUnionMember4ItemUnionMember4Items(
        [NotNullWhen(true)]
            out IReadOnlyDictionary<
            string,
            UnionMember4ItemUnionMember4ItemUnionMember4Item?
        >? value
    )
    {
        value =
            this.Value
            as IReadOnlyDictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<
            IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?>
        > unionMember4ItemUnionMember4ItemJsonValues,
        Action<
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?>
        > unionMember4ItemUnionMember4ItemUnionMember4Items
    )
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
            case IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?> value:
                unionMember4ItemUnionMember4ItemJsonValues(value);
                break;
            case IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4Item?
            > value:
                unionMember4ItemUnionMember4ItemUnionMember4Items(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4ItemUnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<
            IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?>,
            T
        > unionMember4ItemUnionMember4ItemJsonValues,
        Func<
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?>,
            T
        > unionMember4ItemUnionMember4ItemUnionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?> value =>
                unionMember4ItemUnionMember4ItemJsonValues(value),
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?> value =>
                unionMember4ItemUnionMember4ItemUnionMember4Items(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4Item"
            ),
        };
    }

    public static implicit operator UnionMember4ItemUnionMember4Item(string value) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4Item(double value) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4Item(bool value) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4Item(
        List<UnionMember4ItemUnionMember4ItemJsonValue?> value
    ) => new((IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?>)value);

    public static implicit operator UnionMember4ItemUnionMember4Item(
        Dictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?> value
    ) => new((IReadOnlyDictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (unionMember4ItemUnionMember4ItemJsonValues) =>
            {
                foreach (var item in unionMember4ItemUnionMember4ItemJsonValues)
                {
                    item?.Validate();
                }
            },
            (unionMember4ItemUnionMember4ItemUnionMember4Items) =>
            {
                foreach (var item in unionMember4ItemUnionMember4ItemUnionMember4Items.Values)
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(UnionMember4ItemUnionMember4Item? other) =>
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
            IReadOnlyList<UnionMember4ItemUnionMember4ItemJsonValue?> _ => 3,
            IReadOnlyDictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?> _ => 4,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemUnionMember4ItemConverter
    : JsonConverter<UnionMember4ItemUnionMember4Item?>
{
    public override UnionMember4ItemUnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                List<UnionMember4ItemUnionMember4ItemJsonValue?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                Dictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4Item?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4ItemUnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(UnionMember4ItemUnionMember4ItemJsonValueConverter))]
public record class UnionMember4ItemUnionMember4ItemJsonValue : ModelBase
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

    public UnionMember4ItemUnionMember4ItemJsonValue(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemJsonValue(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemJsonValue(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyList<JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemJsonValue(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4ItemUnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator UnionMember4ItemUnionMember4ItemJsonValue(string value) =>
        new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemJsonValue(double value) =>
        new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemJsonValue(bool value) =>
        new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemJsonValue(
        List<JsonElement> value
    ) => new((IReadOnlyList<JsonElement>)value);

    public static implicit operator UnionMember4ItemUnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(UnionMember4ItemUnionMember4ItemJsonValue? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemUnionMember4ItemJsonValueConverter
    : JsonConverter<UnionMember4ItemUnionMember4ItemJsonValue?>
{
    public override UnionMember4ItemUnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4ItemUnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(UnionMember4ItemUnionMember4ItemUnionMember4ItemConverter))]
public record class UnionMember4ItemUnionMember4ItemUnionMember4Item : ModelBase
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

    public UnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4Item(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4Item(
        IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4Item(
        IReadOnlyDictionary<
            string,
            UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
        > value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4Item(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(
        [NotNullWhen(true)]
            out IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>? value
    )
    {
        value =
            this.Value as IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(
        [NotNullWhen(true)]
            out IReadOnlyDictionary<
            string,
            UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
        >? value
    )
    {
        value =
            this.Value
            as IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
        > unionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues,
        Action<
            IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >
        > unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items
    )
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
            case IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value:
                unionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(value);
                break;
            case IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > value:
                unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>,
            T
        > unionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues,
        Func<
            IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >,
            T
        > unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value =>
                unionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(value),
            IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > value => unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4Item"
            ),
        };
    }

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4Item(bool value) =>
        new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4Item(
        List<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value
    ) => new((IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>)value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4Item(
        Dictionary<string, UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?> value
    ) =>
        new(
            (IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >)
                value
        );

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (unionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues) =>
            {
                foreach (var item in unionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues)
                {
                    item?.Validate();
                }
            },
            (unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items) =>
            {
                foreach (
                    var item in unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items.Values
                )
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(UnionMember4ItemUnionMember4ItemUnionMember4Item? other) =>
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
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> _ => 3,
            IReadOnlyDictionary<
                string,
                UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > _ => 4,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemUnionMember4ItemUnionMember4ItemConverter
    : JsonConverter<UnionMember4ItemUnionMember4ItemUnionMember4Item?>
{
    public override UnionMember4ItemUnionMember4ItemUnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                List<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                Dictionary<
                    string,
                    UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
                >
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4ItemUnionMember4ItemUnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter))]
public record class UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue : ModelBase
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

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyList<JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        List<JsonElement> value
    ) => new((IReadOnlyList<JsonElement>)value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter
    : JsonConverter<UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
{
    public override UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemConverter))]
public record class UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item : ModelBase
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

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(
        [NotNullWhen(true)]
            out IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>? value
    )
    {
        value =
            this.Value
            as IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
        > unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
    )
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
            case IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value:
                unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>,
            T
        > unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value =>
                unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item"
            ),
        };
    }

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        bool value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        List<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value
    ) =>
        new(
            (IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>)
                value
        );

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues) =>
            {
                foreach (
                    var item in unionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
                )
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(
        UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item? other
    ) =>
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
            IReadOnlyList<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> _ =>
                3,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemConverter
    : JsonConverter<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?>
{
    public override UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                List<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(
    typeof(UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter)
)]
public record class UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue
    : ModelBase
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

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyList<JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        JsonElement element
    )
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value
    ) => new(value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        List<JsonElement> value
    ) => new((IReadOnlyList<JsonElement>)value);

    public static implicit operator UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(
        UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? other
    ) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter
    : JsonConverter<UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
{
    public override UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(CustomPropertyConverter))]
public record class CustomProperty : ModelBase
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

    public CustomProperty(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomProperty(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomProperty(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomProperty(
        IReadOnlyList<CustomPropertyJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomProperty(
        IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomProperty(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>CustomPropertyJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;CustomPropertyJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyJsonValues(
        [NotNullWhen(true)] out IReadOnlyList<CustomPropertyJsonValue?>? value
    )
    {
        value = this.Value as IReadOnlyList<CustomPropertyJsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>CustomPropertyUnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4Items(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<CustomPropertyJsonValue?>> customPropertyJsonValues,
        Action<
            IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?>
        > customPropertyUnionMember4Items
    )
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
            case IReadOnlyList<CustomPropertyJsonValue?> value:
                customPropertyJsonValues(value);
                break;
            case IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?> value:
                customPropertyUnionMember4Items(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomProperty"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<CustomPropertyJsonValue?>, T> customPropertyJsonValues,
        Func<
            IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?>,
            T
        > customPropertyUnionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<CustomPropertyJsonValue?> value => customPropertyJsonValues(value),
            IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?> value =>
                customPropertyUnionMember4Items(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomProperty"
            ),
        };
    }

    public static implicit operator CustomProperty(string value) => new(value);

    public static implicit operator CustomProperty(double value) => new(value);

    public static implicit operator CustomProperty(bool value) => new(value);

    public static implicit operator CustomProperty(List<CustomPropertyJsonValue?> value) =>
        new((IReadOnlyList<CustomPropertyJsonValue?>)value);

    public static implicit operator CustomProperty(
        Dictionary<string, CustomPropertyUnionMember4Item?> value
    ) => new((IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomProperty"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (customPropertyJsonValues) =>
            {
                foreach (var item in customPropertyJsonValues)
                {
                    item?.Validate();
                }
            },
            (customPropertyUnionMember4Items) =>
            {
                foreach (var item in customPropertyUnionMember4Items.Values)
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(CustomProperty? other) =>
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
            IReadOnlyList<CustomPropertyJsonValue?> _ => 3,
            IReadOnlyDictionary<string, CustomPropertyUnionMember4Item?> _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyConverter : JsonConverter<CustomProperty?>
{
    public override CustomProperty? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<CustomPropertyJsonValue?>>(
                element,
                options
            );
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                Dictionary<string, CustomPropertyUnionMember4Item?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomProperty? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(CustomPropertyJsonValueConverter))]
public record class CustomPropertyJsonValue : ModelBase
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

    public CustomPropertyJsonValue(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyJsonValue(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyJsonValue(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyJsonValue(IReadOnlyList<JsonElement> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyJsonValue(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyJsonValue"
            ),
        };
    }

    public static implicit operator CustomPropertyJsonValue(string value) => new(value);

    public static implicit operator CustomPropertyJsonValue(double value) => new(value);

    public static implicit operator CustomPropertyJsonValue(bool value) => new(value);

    public static implicit operator CustomPropertyJsonValue(List<JsonElement> value) =>
        new((IReadOnlyList<JsonElement>)value);

    public static implicit operator CustomPropertyJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyJsonValue"
            );
        }
    }

    public virtual bool Equals(CustomPropertyJsonValue? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyJsonValueConverter : JsonConverter<CustomPropertyJsonValue?>
{
    public override CustomPropertyJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(CustomPropertyUnionMember4ItemConverter))]
public record class CustomPropertyUnionMember4Item : ModelBase
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

    public CustomPropertyUnionMember4Item(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4Item(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4Item(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4Item(
        IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4Item(
        IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4Item(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>CustomPropertyUnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;CustomPropertyUnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4ItemJsonValues(
        [NotNullWhen(true)] out IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?>? value
    )
    {
        value = this.Value as IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>CustomPropertyUnionMember4ItemUnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4ItemUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4ItemUnionMember4Items(
        [NotNullWhen(true)]
            out IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?>? value
    )
    {
        value =
            this.Value
            as IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<
            IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?>
        > customPropertyUnionMember4ItemJsonValues,
        Action<
            IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?>
        > customPropertyUnionMember4ItemUnionMember4Items
    )
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
            case IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?> value:
                customPropertyUnionMember4ItemJsonValues(value);
                break;
            case IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?> value:
                customPropertyUnionMember4ItemUnionMember4Items(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<
            IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?>,
            T
        > customPropertyUnionMember4ItemJsonValues,
        Func<
            IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?>,
            T
        > customPropertyUnionMember4ItemUnionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?> value =>
                customPropertyUnionMember4ItemJsonValues(value),
            IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?> value =>
                customPropertyUnionMember4ItemUnionMember4Items(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4Item"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4Item(string value) => new(value);

    public static implicit operator CustomPropertyUnionMember4Item(double value) => new(value);

    public static implicit operator CustomPropertyUnionMember4Item(bool value) => new(value);

    public static implicit operator CustomPropertyUnionMember4Item(
        List<CustomPropertyUnionMember4ItemJsonValue?> value
    ) => new((IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?>)value);

    public static implicit operator CustomPropertyUnionMember4Item(
        Dictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?> value
    ) => new((IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (customPropertyUnionMember4ItemJsonValues) =>
            {
                foreach (var item in customPropertyUnionMember4ItemJsonValues)
                {
                    item?.Validate();
                }
            },
            (customPropertyUnionMember4ItemUnionMember4Items) =>
            {
                foreach (var item in customPropertyUnionMember4ItemUnionMember4Items.Values)
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(CustomPropertyUnionMember4Item? other) =>
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
            IReadOnlyList<CustomPropertyUnionMember4ItemJsonValue?> _ => 3,
            IReadOnlyDictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?> _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemConverter
    : JsonConverter<CustomPropertyUnionMember4Item?>
{
    public override CustomPropertyUnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                List<CustomPropertyUnionMember4ItemJsonValue?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                Dictionary<string, CustomPropertyUnionMember4ItemUnionMember4Item?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(CustomPropertyUnionMember4ItemJsonValueConverter))]
public record class CustomPropertyUnionMember4ItemJsonValue : ModelBase
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

    public CustomPropertyUnionMember4ItemJsonValue(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemJsonValue(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemJsonValue(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemJsonValue(
        IReadOnlyList<JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemJsonValue(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4ItemJsonValue(string value) =>
        new(value);

    public static implicit operator CustomPropertyUnionMember4ItemJsonValue(double value) =>
        new(value);

    public static implicit operator CustomPropertyUnionMember4ItemJsonValue(bool value) =>
        new(value);

    public static implicit operator CustomPropertyUnionMember4ItemJsonValue(
        List<JsonElement> value
    ) => new((IReadOnlyList<JsonElement>)value);

    public static implicit operator CustomPropertyUnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(CustomPropertyUnionMember4ItemJsonValue? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemJsonValueConverter
    : JsonConverter<CustomPropertyUnionMember4ItemJsonValue?>
{
    public override CustomPropertyUnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(CustomPropertyUnionMember4ItemUnionMember4ItemConverter))]
public record class CustomPropertyUnionMember4ItemUnionMember4Item : ModelBase
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

    public CustomPropertyUnionMember4ItemUnionMember4Item(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4Item(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4Item(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4Item(
        IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4Item(
        IReadOnlyDictionary<
            string,
            CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
        > value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4Item(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4ItemUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4ItemUnionMember4ItemJsonValues(
        [NotNullWhen(true)]
            out IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?>? value
    )
    {
        value =
            this.Value as IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items(
        [NotNullWhen(true)]
            out IReadOnlyDictionary<
            string,
            CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
        >? value
    )
    {
        value =
            this.Value
            as IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?>
        > customPropertyUnionMember4ItemUnionMember4ItemJsonValues,
        Action<
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items
    )
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
            case IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?> value:
                customPropertyUnionMember4ItemUnionMember4ItemJsonValues(value);
                break;
            case IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > value:
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?>,
            T
        > customPropertyUnionMember4ItemUnionMember4ItemJsonValues,
        Func<
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >,
            T
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?> value =>
                customPropertyUnionMember4ItemUnionMember4ItemJsonValues(value),
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > value => customPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4Item"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4Item(string value) =>
        new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4Item(double value) =>
        new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4Item(bool value) =>
        new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4Item(
        List<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?> value
    ) => new((IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?>)value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4Item(
        Dictionary<string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?> value
    ) =>
        new(
            (IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >)
                value
        );

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (customPropertyUnionMember4ItemUnionMember4ItemJsonValues) =>
            {
                foreach (var item in customPropertyUnionMember4ItemUnionMember4ItemJsonValues)
                {
                    item?.Validate();
                }
            },
            (customPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items) =>
            {
                foreach (
                    var item in customPropertyUnionMember4ItemUnionMember4ItemUnionMember4Items.Values
                )
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(CustomPropertyUnionMember4ItemUnionMember4Item? other) =>
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
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?> _ => 3,
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemUnionMember4ItemConverter
    : JsonConverter<CustomPropertyUnionMember4ItemUnionMember4Item?>
{
    public override CustomPropertyUnionMember4ItemUnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                List<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                Dictionary<string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4ItemUnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(CustomPropertyUnionMember4ItemUnionMember4ItemJsonValueConverter))]
public record class CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue : ModelBase
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

    public CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyList<JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        string value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        double value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        bool value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        List<JsonElement> value
    ) => new((IReadOnlyList<JsonElement>)value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemUnionMember4ItemJsonValueConverter
    : JsonConverter<CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue?>
{
    public override CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4ItemUnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(typeof(CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemConverter))]
public record class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item : ModelBase
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

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        IReadOnlyDictionary<
            string,
            CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
        > value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(
        [NotNullWhen(true)]
            out IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>? value
    )
    {
        value =
            this.Value
            as IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(
        [NotNullWhen(true)]
            out IReadOnlyDictionary<
            string,
            CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
        >? value
    )
    {
        value =
            this.Value
            as IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues,
        Action<
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items
    )
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
            case IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value:
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(value);
                break;
            case IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > value:
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(
                    value
                );
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>,
            T
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues,
        Func<
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >,
            T
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value =>
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(value),
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > value =>
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items(
                    value
                ),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        bool value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        List<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value
    ) =>
        new(
            (IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>)
                value
        );

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item(
        Dictionary<
            string,
            CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
        > value
    ) =>
        new(
            (IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            >)
                value
        );

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues) =>
            {
                foreach (
                    var item in customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
                )
                {
                    item?.Validate();
                }
            },
            (customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items) =>
            {
                foreach (
                    var item in customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Items.Values
                )
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item? other
    ) =>
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
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> _ =>
                3,
            IReadOnlyDictionary<
                string,
                CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
            > _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemConverter
    : JsonConverter<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item?>
{
    public override CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                List<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                Dictionary<
                    string,
                    CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?
                >
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized.Values)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(
    typeof(CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter)
)]
public record class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue
    : ModelBase
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

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyList<JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        JsonElement element
    )
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        List<JsonElement> value
    ) => new((IReadOnlyList<JsonElement>)value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? other
    ) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter
    : JsonConverter<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
{
    public override CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

[JsonConverter(
    typeof(CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemConverter)
)]
public record class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item
    : ModelBase
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

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        JsonElement element
    )
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(
        [NotNullWhen(true)]
            out IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>? value
    )
    {
        value =
            this.Value
            as IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
    )
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
            case IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value:
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(
                    value
                );
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>,
            T
        > customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value =>
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues(
                    value
                ),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        string value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        double value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        bool value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item(
        List<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> value
    ) =>
        new(
            (IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>)
                value
        );

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item"
            );
        }
        this.Switch(
            (_) => { },
            (_) => { },
            (_) => { },
            (
                customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
            ) =>
            {
                foreach (
                    var item in customPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValues
                )
                {
                    item?.Validate();
                }
            }
        );
    }

    public virtual bool Equals(
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item? other
    ) =>
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
            IReadOnlyList<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?> _ =>
                3,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemConverter
    : JsonConverter<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item?>
{
    public override CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<
                List<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
            >(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item?.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4Item? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(
    typeof(CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter)
)]
public record class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue
    : ModelBase
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

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value,
        JsonElement? element = null
    )
    {
        this.Value = value;
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyList<JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        JsonElement element
    )
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            ),
        };
    }

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        string value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        double value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        bool value
    ) => new(value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        List<JsonElement> value
    ) => new((IReadOnlyList<JsonElement>)value);

    public static implicit operator CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue(
        Dictionary<string, JsonElement> value
    ) => new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue"
            );
        }
    }

    public virtual bool Equals(
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? other
    ) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValueConverter
    : JsonConverter<CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue?>
{
    public override CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CustomPropertyUnionMember4ItemUnionMember4ItemUnionMember4ItemUnionMember4ItemJsonValue? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(IsBotConverter))]
public record class IsBot : ModelBase
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

    public IsBot(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public IsBot(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public IsBot(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public IsBot(IReadOnlyList<JsonElement> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public IsBot(IReadOnlyDictionary<string, JsonElement> value, JsonElement? element = null)
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public IsBot(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of IsBot"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of IsBot"
            ),
        };
    }

    public static implicit operator IsBot(string value) => new(value);

    public static implicit operator IsBot(double value) => new(value);

    public static implicit operator IsBot(bool value) => new(value);

    public static implicit operator IsBot(List<JsonElement> value) =>
        new((IReadOnlyList<JsonElement>)value);

    public static implicit operator IsBot(Dictionary<string, JsonElement> value) =>
        new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException("Data did not match any variant of IsBot");
        }
    }

    public virtual bool Equals(IsBot? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class IsBotConverter : JsonConverter<IsBot?>
{
    public override IsBot? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(Utf8JsonWriter writer, IsBot? value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(PhoneNumberConverter))]
public record class PhoneNumber : ModelBase
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

    public PhoneNumber(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public PhoneNumber(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public PhoneNumber(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public PhoneNumber(IReadOnlyList<JsonElement> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public PhoneNumber(IReadOnlyDictionary<string, JsonElement> value, JsonElement? element = null)
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public PhoneNumber(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of PhoneNumber"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of PhoneNumber"
            ),
        };
    }

    public static implicit operator PhoneNumber(string value) => new(value);

    public static implicit operator PhoneNumber(double value) => new(value);

    public static implicit operator PhoneNumber(bool value) => new(value);

    public static implicit operator PhoneNumber(List<JsonElement> value) =>
        new((IReadOnlyList<JsonElement>)value);

    public static implicit operator PhoneNumber(Dictionary<string, JsonElement> value) =>
        new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of PhoneNumber"
            );
        }
    }

    public virtual bool Equals(PhoneNumber? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class PhoneNumberConverter : JsonConverter<PhoneNumber?>
{
    public override PhoneNumber? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        PhoneNumber? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(ZipConverter))]
public record class Zip : ModelBase
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

    public Zip(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Zip(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Zip(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Zip(IReadOnlyList<JsonElement> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public Zip(IReadOnlyDictionary<string, JsonElement> value, JsonElement? element = null)
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public Zip(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException("Data did not match any variant of Zip");
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException("Data did not match any variant of Zip"),
        };
    }

    public static implicit operator Zip(string value) => new(value);

    public static implicit operator Zip(double value) => new(value);

    public static implicit operator Zip(bool value) => new(value);

    public static implicit operator Zip(List<JsonElement> value) =>
        new((IReadOnlyList<JsonElement>)value);

    public static implicit operator Zip(Dictionary<string, JsonElement> value) =>
        new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException("Data did not match any variant of Zip");
        }
    }

    public virtual bool Equals(Zip? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class ZipConverter : JsonConverter<Zip?>
{
    public override Zip? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(Utf8JsonWriter writer, Zip? value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// These properties are used throughout the Ours app to pass known values onto destinations
/// </summary>
[JsonConverter(typeof(JsonModelConverter<DefaultProperties, DefaultPropertiesFromRaw>))]
public sealed record class DefaultProperties : JsonModel
{
    /// <summary>
    /// The Everflow affiliate Click (Transaction) ID, captured from the `_ef_transaction_id`
    /// URL parameter. Ex: ef_click_abc123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? _EfTransactionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("_ef_transaction_id");
        }
        init { this._rawData.Set("_ef_transaction_id", value); }
    }

    /// <summary>
    /// The total time in milliseconds this tab has been visible since the page loaded
    /// </summary>
    public double? ActiveDuration
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("activeDuration");
        }
        init { this._rawData.Set("activeDuration", value); }
    }

    /// <summary>
    /// The ad id detected in the session. This is set by the web sdk automatically.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? AdID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ad_id");
        }
        init { this._rawData.Set("ad_id", value); }
    }

    /// <summary>
    /// The Admitad (Mitgo) affiliate Click ID. Ex: admitad_uid_abc123 Leading and
    /// trailing whitespace is removed before validation.
    /// </summary>
    public string? AdmitadUid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("admitad_uid");
        }
        init { this._rawData.Set("admitad_uid", value); }
    }

    /// <summary>
    /// The adset id detected in the session. This is set by the web sdk automatically.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? AdsetID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("adset_id");
        }
        init { this._rawData.Set("adset_id", value); }
    }

    /// <summary>
    /// The AppLovin alart query parameter. Ex: alart123 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? Alart
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("alart");
        }
        init { this._rawData.Set("alart", value); }
    }

    /// <summary>
    /// The AppLovin aleid query parameter. Ex: aleid123 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? Aleid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("aleid");
        }
        init { this._rawData.Set("aleid", value); }
    }

    /// <summary>
    /// The AppLovin pixel cookie value (_axwrt). Web-only. Leading and trailing
    /// whitespace is removed before validation.
    /// </summary>
    public string? Axwrt
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("axwrt");
        }
        init { this._rawData.Set("axwrt", value); }
    }

    /// <summary>
    /// The Basis DSP Click ID. Ex: basis_cid123 Leading and trailing whitespace is
    /// removed before validation.
    /// </summary>
    public string? BasisCid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("basis_cid");
        }
        init { this._rawData.Set("basis_cid", value); }
    }

    /// <summary>
    /// The Beeswax (FreeWheel Buyer Cloud) auction ID, captured from the `{{AUCTION_ID}}`
    /// macro on creative click URLs. Ex: bx-auc-abc123 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? BeeswaxAuctionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("beeswax_auction_id");
        }
        init { this._rawData.Set("beeswax_auction_id", value); }
    }

    /// <summary>
    /// The language of the browser, lowercased. Ex: en-us Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? BrowserLanguage
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("browser_language");
        }
        init { this._rawData.Set("browser_language", value); }
    }

    /// <summary>
    /// The name of the browser. Ex: Chrome Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? BrowserName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("browser_name");
        }
        init { this._rawData.Set("browser_name", value); }
    }

    /// <summary>
    /// The version of the browser. Ex: 114.0 Leading and trailing whitespace is
    /// removed before validation.
    /// </summary>
    public string? BrowserVersion
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("browser_version");
        }
        init { this._rawData.Set("browser_version", value); }
    }

    /// <summary>
    /// The campaign id detected in the session. This is set by the web sdk automatically.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? CampaignID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("campaign_id");
        }
        init { this._rawData.Set("campaign_id", value); }
    }

    /// <summary>
    /// The Click ID. Ex: clickid123 Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Clickid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("clickid");
        }
        init { this._rawData.Set("clickid", value); }
    }

    /// <summary>
    /// The Generic Click ID. Ex: clid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Clid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("clid");
        }
        init { this._rawData.Set("clid", value); }
    }

    /// <summary>
    /// The architecture of the CPU. Ex: amd64 Leading and trailing whitespace is
    /// removed before validation.
    /// </summary>
    public string? CpuArchitecture
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("cpu_architecture");
        }
        init { this._rawData.Set("cpu_architecture", value); }
    }

    /// <summary>
    /// The full url (including query params) of the current page Leading and trailing
    /// whitespace is removed before validation.
    /// </summary>
    public string? CurrentUrl
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("current_url");
        }
        init { this._rawData.Set("current_url", value); }
    }

    /// <summary>
    /// The DoubleClick Click ID. Ex: dclid123 Leading and trailing whitespace is
    /// removed before validation.
    /// </summary>
    public string? Dclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("dclid");
        }
        init { this._rawData.Set("dclid", value); }
    }

    /// <summary>
    /// The model of the device. Ex: iPhone Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? DeviceModel
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("device_model");
        }
        init { this._rawData.Set("device_model", value); }
    }

    /// <summary>
    /// The type of device the user is using. Ex: mobile Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? DeviceType
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("device_type");
        }
        init { this._rawData.Set("device_type", value); }
    }

    /// <summary>
    /// The vendor of the device. Ex: Apple Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? DeviceVendor
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("device_vendor");
        }
        init { this._rawData.Set("device_vendor", value); }
    }

    /// <summary>
    /// The time in milliseconds since the previous event on this page, or since the
    /// web SDK loaded for the first event. Restarts when a hidden tab becomes visible again.
    /// </summary>
    public double? Duration
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("duration");
        }
        init { this._rawData.Set("duration", value); }
    }

    /// <summary>
    /// The browser's encoding. Ex: UTF-8 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Encoding
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("encoding");
        }
        init { this._rawData.Set("encoding", value); }
    }

    /// <summary>
    /// The name of the browser engine. Ex: Blink Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? EngineName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("engine_name");
        }
        init { this._rawData.Set("engine_name", value); }
    }

    /// <summary>
    /// The version of the browser engine. Ex: 114.0 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? EngineVersion
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("engine_version");
        }
        init { this._rawData.Set("engine_version", value); }
    }

    /// <summary>
    /// The Pinterest Click ID. Ex: epik456 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Epik
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("epik");
        }
        init { this._rawData.Set("epik", value); }
    }

    /// <summary>
    /// Facebook Click ID with prefix format for Conversions API tracking. Ex: fb.1.1554763741205.AbCdEfGhIjKlMnOpQrStUvWxYz1234567890
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Fbc
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("fbc");
        }
        init { this._rawData.Set("fbc", value); }
    }

    /// <summary>
    /// Raw Facebook Click ID query parameter without prefix from ad clicks. Ex:
    /// AbCdEfGhIjKlMnOpQrStUvWxYz1234567890 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Fbclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("fbclid");
        }
        init { this._rawData.Set("fbclid", value); }
    }

    /// <summary>
    /// Facebook Browser ID parameter for identifying browsers and attributing events.
    /// Ex: fb.1.1554763741205.1098115397 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Fbp
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("fbp");
        }
        init { this._rawData.Set("fbp", value); }
    }

    /// <summary>
    /// Deprecated
    /// </summary>
    public bool? Fv
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("fv");
        }
        init { this._rawData.Set("fv", value); }
    }

    /// <summary>
    /// The Google Ad Source. Ex: google Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? GadSource
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gad_source");
        }
        init { this._rawData.Set("gad_source", value); }
    }

    /// <summary>
    /// The Google Braid ID. Ex: gbraid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Gbraid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gbraid");
        }
        init { this._rawData.Set("gbraid", value); }
    }

    /// <summary>
    /// The Google Click ID. Ex: gclid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Gclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gclid");
        }
        init { this._rawData.Set("gclid", value); }
    }

    /// <summary>
    /// The Gravity attribution ID, captured from the `graid` URL parameter on the
    /// landing page. Gravity resolves it to the campaign, publisher, and ad IDs
    /// server-side. Ex: graid-abc123 Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Graid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("graid");
        }
        init { this._rawData.Set("graid", value); }
    }

    /// <summary>
    /// The Gravity click ID, captured from the `grclid` URL parameter that Gravity
    /// (trygravity.com) appends to ad click-through URLs on the landing page. Ex:
    /// grclid-abc123 Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Grclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("grclid");
        }
        init { this._rawData.Set("grclid", value); }
    }

    /// <summary>
    /// The host of the current page. Ex: example.com Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? Host
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("host");
        }
        init { this._rawData.Set("host", value); }
    }

    /// <summary>
    /// Whether the user is in an iframe. Ex: true
    /// </summary>
    public bool? Iframe
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("iframe");
        }
        init { this._rawData.Set("iframe", value); }
    }

    /// <summary>
    /// The Impact Click ID reference. Ex: im_ref123 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? ImRef
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("im_ref");
        }
        init { this._rawData.Set("im_ref", value); }
    }

    /// <summary>
    /// The IP address of the user. Ex: 127.0.0.1 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? IP
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ip");
        }
        init { this._rawData.Set("ip", value); }
    }

    /// <summary>
    /// The Impact Click ID. Ex: irclickid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Irclickid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("irclickid");
        }
        init { this._rawData.Set("irclickid", value); }
    }

    /// <summary>
    /// Any JSON value: string, number, boolean, array, object, or null.
    /// </summary>
    public DefaultPropertiesIsBot? IsBot
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<DefaultPropertiesIsBot>("is_bot");
        }
        init { this._rawData.Set("is_bot", value); }
    }

    /// <summary>
    /// The LinkedIn Click ID. Ex: li_fat_id123 Leading and trailing whitespace is
    /// removed before validation.
    /// </summary>
    public string? LiFatID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("li_fat_id");
        }
        init { this._rawData.Set("li_fat_id", value); }
    }

    /// <summary>
    /// The Microsoft Click ID. Ex: msclkid123 Leading and trailing whitespace is
    /// removed before validation.
    /// </summary>
    public string? Msclkid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("msclkid");
        }
        init { this._rawData.Set("msclkid", value); }
    }

    /// <summary>
    /// The NextDoor Click ID. Ex: ndclid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Ndclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ndclid");
        }
        init { this._rawData.Set("ndclid", value); }
    }

    /// <summary>
    /// Deprecated
    /// </summary>
    public bool? NewS
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("new_s");
        }
        init { this._rawData.Set("new_s", value); }
    }

    /// <summary>
    /// The Outbrain click ID, captured from the `ob_click_id` URL parameter (Outbrain
    /// `{{ob_click_id}}` macro) on the landing page. Ex: ob_click_abc123 Leading
    /// and trailing whitespace is removed before validation.
    /// </summary>
    public string? ObClickID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ob_click_id");
        }
        init { this._rawData.Set("ob_click_id", value); }
    }

    /// <summary>
    /// The OpenAI Ads privacy-preserving reference, captured from the `oppref` URL
    /// parameter on landing pages (the OpenAI Pixel also stores it in a `__oppref`
    /// cookie). Sent to OpenAI Ads on Conversions API events for attribution. Ex:
    /// oppref_abc Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Oppref
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("oppref");
        }
        init { this._rawData.Set("oppref", value); }
    }

    /// <summary>
    /// The name of the operating system. Ex: Windows Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? OsName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("os_name");
        }
        init { this._rawData.Set("os_name", value); }
    }

    /// <summary>
    /// The version of the operating system. Ex: 10.0 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? OsVersion
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("os_version");
        }
        init { this._rawData.Set("os_version", value); }
    }

    /// <summary>
    /// A random set of numbers for the page load
    /// </summary>
    public double? PageHash
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("page_hash");
        }
        init { this._rawData.Set("page_hash", value); }
    }

    /// <summary>
    /// The pathname of the current page. Ex: /home Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? Pathname
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("pathname");
        }
        init { this._rawData.Set("pathname", value); }
    }

    /// <summary>
    /// The Quora Click ID. Ex: qclid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Qclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("qclid");
        }
        init { this._rawData.Set("qclid", value); }
    }

    /// <summary>
    /// The Reddit Click ID. Ex: rdt_cid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? RdtCid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("rdt_cid");
        }
        init { this._rawData.Set("rdt_cid", value); }
    }

    /// <summary>
    /// The time an Ours server received the event, in ISO 8601 UTC
    /// </summary>
    public string? ReceivedAt
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("received_at");
        }
        init { this._rawData.Set("received_at", value); }
    }

    /// <summary>
    /// The referrer URL of the current page Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Referrer
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("referrer");
        }
        init { this._rawData.Set("referrer", value); }
    }

    /// <summary>
    /// The referring domain of the current page Leading and trailing whitespace is
    /// removed before validation.
    /// </summary>
    public string? ReferringDomain
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("referring_domain");
        }
        init { this._rawData.Set("referring_domain", value); }
    }

    /// <summary>
    /// The StackAdapt Tracking ID. Ex: sacid123 Leading and trailing whitespace
    /// is removed before validation.
    /// </summary>
    public string? Sacid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sacid");
        }
        init { this._rawData.Set("sacid", value); }
    }

    /// <summary>
    /// The Snapchat Click ID. Ex: sccid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Sccid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sccid");
        }
        init { this._rawData.Set("sccid", value); }
    }

    /// <summary>
    /// The height of the screen. Ex: 1080
    /// </summary>
    public double? ScreenHeight
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("screen_height");
        }
        init { this._rawData.Set("screen_height", value); }
    }

    /// <summary>
    /// The width of the screen. Ex: 1920
    /// </summary>
    public double? ScreenWidth
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("screen_width");
        }
        init { this._rawData.Set("screen_width", value); }
    }

    /// <summary>
    /// The number of sessions the user has had. Ex: 3
    /// </summary>
    public double? SessionCount
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("sessionCount");
        }
        init { this._rawData.Set("sessionCount", value); }
    }

    /// <summary>
    /// The session ID as assigned automatically by the web SDK. This is required
    /// for session replay Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Sid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sid");
        }
        init { this._rawData.Set("sid", value); }
    }

    /// <summary>
    /// The screen resolution as width x height in pixels. Ex: 1920x1080 Leading
    /// and trailing whitespace is removed before validation.
    /// </summary>
    public string? Sr
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sr");
        }
        init { this._rawData.Set("sr", value); }
    }

    /// <summary>
    /// The title of the current page Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Title
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("title");
        }
        init { this._rawData.Set("title", value); }
    }

    /// <summary>
    /// The TikTok Click ID. Ex: ttclid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Ttclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ttclid");
        }
        init { this._rawData.Set("ttclid", value); }
    }

    /// <summary>
    /// The Trade Desk impression ID, captured from a configured ttd_impression_id
    /// URL parameter using the %%TTD_IMPRESSIONID%% creative macro. Sent as imp
    /// on conversions. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? TtdImpressionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ttd_impression_id");
        }
        init { this._rawData.Set("ttd_impression_id", value); }
    }

    /// <summary>
    /// The Twitter Click ID. Ex: twclid123 Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? Twclid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("twclid");
        }
        init { this._rawData.Set("twclid", value); }
    }

    /// <summary>
    /// The browser brands and full versions from User-Agent Client Hints, as brand;version
    /// pairs separated by |. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Uafvl
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("uafvl");
        }
        init { this._rawData.Set("uafvl", value); }
    }

    /// <summary>
    /// The user agent of the browser Leading and trailing whitespace is removed
    /// before validation.
    /// </summary>
    public string? UserAgent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("user_agent");
        }
        init { this._rawData.Set("user_agent", value); }
    }

    /// <summary>
    /// The UTM Campaign. The web SDK automatically captures this from the query
    /// params. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? UtmCampaign
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_campaign");
        }
        init { this._rawData.Set("utm_campaign", value); }
    }

    /// <summary>
    /// The UTM Content. The web SDK automatically captures this from the query params.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? UtmContent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_content");
        }
        init { this._rawData.Set("utm_content", value); }
    }

    /// <summary>
    /// The UTM Medium. The web SDK automatically captures this from the query params.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? UtmMedium
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_medium");
        }
        init { this._rawData.Set("utm_medium", value); }
    }

    /// <summary>
    /// The UTM Name. The web SDK automatically captures this from the query params.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? UtmName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_name");
        }
        init { this._rawData.Set("utm_name", value); }
    }

    /// <summary>
    /// The UTM Source. The web SDK automatically captures this from the query params.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? UtmSource
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_source");
        }
        init { this._rawData.Set("utm_source", value); }
    }

    /// <summary>
    /// The UTM Term. The web SDK automatically captures this from the query params.
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? UtmTerm
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_term");
        }
        init { this._rawData.Set("utm_term", value); }
    }

    /// <summary>
    /// The SDK version (e.g., web SDK or ingest-sdk-* via generated SDK headers)
    /// Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Version
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("version");
        }
        init { this._rawData.Set("version", value); }
    }

    /// <summary>
    /// The Viant (Adelphic) Click ID, captured from the `viant_click_id` URL parameter
    /// (Viant `${ADELPHIC_CLICKID}` macro). Sent as `xid` on Viant postbacks. Ex:
    /// viant_click_abc123 Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? ViantClickID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("viant_click_id");
        }
        init { this._rawData.Set("viant_click_id", value); }
    }

    /// <summary>
    /// The Viant (Adelphic) Impression ID, captured from the `viant_impression_id`
    /// URL parameter (Viant `${ADELPHIC_IMPRESSIONID}` macro). Sent as `imp_id` on
    /// Viant postbacks for post-view attribution. Ex: viant_imp_abc123 Leading and
    /// trailing whitespace is removed before validation.
    /// </summary>
    public string? ViantImpressionID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("viant_impression_id");
        }
        init { this._rawData.Set("viant_impression_id", value); }
    }

    /// <summary>
    /// The WBRAID Identifier. The web SDK automatically captures this from the query
    /// params. Leading and trailing whitespace is removed before validation.
    /// </summary>
    public string? Wbraid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("wbraid");
        }
        init { this._rawData.Set("wbraid", value); }
    }

    /// <summary>
    /// Whether the user is in a webview. Ex: true
    /// </summary>
    public bool? Webview
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("webview");
        }
        init { this._rawData.Set("webview", value); }
    }

    /// <summary>
    /// The Yelp Click ID from the landing URL, sent unchanged and unhashed.
    /// </summary>
    public string? Ylpcid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ylpcid");
        }
        init { this._rawData.Set("ylpcid", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this._EfTransactionID;
        _ = this.ActiveDuration;
        _ = this.AdID;
        _ = this.AdmitadUid;
        _ = this.AdsetID;
        _ = this.Alart;
        _ = this.Aleid;
        _ = this.Axwrt;
        _ = this.BasisCid;
        _ = this.BeeswaxAuctionID;
        _ = this.BrowserLanguage;
        _ = this.BrowserName;
        _ = this.BrowserVersion;
        _ = this.CampaignID;
        _ = this.Clickid;
        _ = this.Clid;
        _ = this.CpuArchitecture;
        _ = this.CurrentUrl;
        _ = this.Dclid;
        _ = this.DeviceModel;
        _ = this.DeviceType;
        _ = this.DeviceVendor;
        _ = this.Duration;
        _ = this.Encoding;
        _ = this.EngineName;
        _ = this.EngineVersion;
        _ = this.Epik;
        _ = this.Fbc;
        _ = this.Fbclid;
        _ = this.Fbp;
        _ = this.Fv;
        _ = this.GadSource;
        _ = this.Gbraid;
        _ = this.Gclid;
        _ = this.Graid;
        _ = this.Grclid;
        _ = this.Host;
        _ = this.Iframe;
        _ = this.ImRef;
        _ = this.IP;
        _ = this.Irclickid;
        this.IsBot?.Validate();
        _ = this.LiFatID;
        _ = this.Msclkid;
        _ = this.Ndclid;
        _ = this.NewS;
        _ = this.ObClickID;
        _ = this.Oppref;
        _ = this.OsName;
        _ = this.OsVersion;
        _ = this.PageHash;
        _ = this.Pathname;
        _ = this.Qclid;
        _ = this.RdtCid;
        _ = this.ReceivedAt;
        _ = this.Referrer;
        _ = this.ReferringDomain;
        _ = this.Sacid;
        _ = this.Sccid;
        _ = this.ScreenHeight;
        _ = this.ScreenWidth;
        _ = this.SessionCount;
        _ = this.Sid;
        _ = this.Sr;
        _ = this.Title;
        _ = this.Ttclid;
        _ = this.TtdImpressionID;
        _ = this.Twclid;
        _ = this.Uafvl;
        _ = this.UserAgent;
        _ = this.UtmCampaign;
        _ = this.UtmContent;
        _ = this.UtmMedium;
        _ = this.UtmName;
        _ = this.UtmSource;
        _ = this.UtmTerm;
        _ = this.Version;
        _ = this.ViantClickID;
        _ = this.ViantImpressionID;
        _ = this.Wbraid;
        _ = this.Webview;
        _ = this.Ylpcid;
    }

    public DefaultProperties() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public DefaultProperties(DefaultProperties defaultProperties)
        : base(defaultProperties) { }
#pragma warning restore CS8618

    public DefaultProperties(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    DefaultProperties(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="DefaultPropertiesFromRaw.FromRawUnchecked"/>
    public static DefaultProperties FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class DefaultPropertiesFromRaw : IFromRawJson<DefaultProperties>
{
    /// <inheritdoc/>
    public DefaultProperties FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        DefaultProperties.FromRawUnchecked(rawData);
}

/// <summary>
/// Any JSON value: string, number, boolean, array, object, or null.
/// </summary>
[JsonConverter(typeof(DefaultPropertiesIsBotConverter))]
public record class DefaultPropertiesIsBot : ModelBase
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

    public DefaultPropertiesIsBot(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public DefaultPropertiesIsBot(double value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public DefaultPropertiesIsBot(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public DefaultPropertiesIsBot(IReadOnlyList<JsonElement> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public DefaultPropertiesIsBot(
        IReadOnlyDictionary<string, JsonElement> value,
        JsonElement? element = null
    )
    {
        this.Value = FrozenDictionary.ToFrozenDictionary(value);
        this._element = element;
    }

    public DefaultPropertiesIsBot(JsonElement element)
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
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements([NotNullWhen(true)] out IReadOnlyList<JsonElement>? value)
    {
        value = this.Value as IReadOnlyList<JsonElement>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Dictionary{Key, Value}"/> with a <c>Key</c> of <c>string</c> and a <c>Value</c> of <c>JsonElement</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickJsonElements1(out var value)) {
    ///     // `value` is of type `IReadOnlyDictionary&lt;string, JsonElement&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickJsonElements1(
        [NotNullWhen(true)] out IReadOnlyDictionary<string, JsonElement>? value
    )
    {
        value = this.Value as IReadOnlyDictionary<string, JsonElement>;
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<string> @string,
        Action<double> @double,
        Action<bool> @bool,
        Action<IReadOnlyList<JsonElement>> jsonElements,
        Action<IReadOnlyDictionary<string, JsonElement>> jsonElements1
    )
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
            case IReadOnlyList<JsonElement> value:
                jsonElements(value);
                break;
            case IReadOnlyDictionary<string, JsonElement> value:
                jsonElements1(value);
                break;
            default:
                throw new OursPrivacyInvalidDataException(
                    "Data did not match any variant of DefaultPropertiesIsBot"
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
    ///     (bool value) =&gt; {...},
    ///     (IReadOnlyList&lt;JsonElement&gt; value) =&gt; {...},
    ///     (IReadOnlyDictionary&lt;string, JsonElement&gt; value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<string, T> @string,
        Func<double, T> @double,
        Func<bool, T> @bool,
        Func<IReadOnlyList<JsonElement>, T> jsonElements,
        Func<IReadOnlyDictionary<string, JsonElement>, T> jsonElements1
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            double value => @double(value),
            bool value => @bool(value),
            IReadOnlyList<JsonElement> value => jsonElements(value),
            IReadOnlyDictionary<string, JsonElement> value => jsonElements1(value),
            _ => throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of DefaultPropertiesIsBot"
            ),
        };
    }

    public static implicit operator DefaultPropertiesIsBot(string value) => new(value);

    public static implicit operator DefaultPropertiesIsBot(double value) => new(value);

    public static implicit operator DefaultPropertiesIsBot(bool value) => new(value);

    public static implicit operator DefaultPropertiesIsBot(List<JsonElement> value) =>
        new((IReadOnlyList<JsonElement>)value);

    public static implicit operator DefaultPropertiesIsBot(Dictionary<string, JsonElement> value) =>
        new((IReadOnlyDictionary<string, JsonElement>)value);

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
            throw new OursPrivacyInvalidDataException(
                "Data did not match any variant of DefaultPropertiesIsBot"
            );
        }
    }

    public virtual bool Equals(DefaultPropertiesIsBot? other) =>
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
            IReadOnlyList<JsonElement> _ => 3,
            IReadOnlyDictionary<string, JsonElement> _ => 4,
            _ => -1,
        };
    }
}

sealed class DefaultPropertiesIsBotConverter : JsonConverter<DefaultPropertiesIsBot?>
{
    public override DefaultPropertiesIsBot? Read(
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

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<JsonElement>>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                element,
                options
            );
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is OursPrivacyInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        DefaultPropertiesIsBot? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}

/// <summary>
/// End-user network context for server-side calls. Required for probabilistic identity
/// resolution when the caller is a backend server rather than an end-user browser.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<IdentityContext, IdentityContextFromRaw>))]
public sealed record class IdentityContext : JsonModel
{
    /// <summary>
    /// The end-user IP address (not the server IP).
    /// </summary>
    public required string IP
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("ip");
        }
        init { this._rawData.Set("ip", value); }
    }

    /// <summary>
    /// The end-user User-Agent string (not the server UA).
    /// </summary>
    public required string UserAgent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("userAgent");
        }
        init { this._rawData.Set("userAgent", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.IP;
        _ = this.UserAgent;
    }

    public IdentityContext() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public IdentityContext(IdentityContext identityContext)
        : base(identityContext) { }
#pragma warning restore CS8618

    public IdentityContext(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    IdentityContext(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="IdentityContextFromRaw.FromRawUnchecked"/>
    public static IdentityContext FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class IdentityContextFromRaw : IFromRawJson<IdentityContext>
{
    /// <inheritdoc/>
    public IdentityContext FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        IdentityContext.FromRawUnchecked(rawData);
}
