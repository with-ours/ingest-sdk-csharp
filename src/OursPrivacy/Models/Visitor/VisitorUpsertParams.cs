using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using OursPrivacy.Core;

namespace OursPrivacy.Models.Visitor;

/// <summary>
/// Define visitor properties on an existing visitor or create a new visitor. This
/// fires a $identify event, making the call visible in the event stream.
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
    /// The token for your Source. You can find this in the dashboard.
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
    /// The email address of a user. We will associate this event with the user or
    /// create a user. Used for lookup if externalId and userId are not included
    /// in the request.
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
    /// The externalId (the ID in your system) of a user. We will associate this
    /// event with the user or create a user. If included in the request, email lookup
    /// is ignored.
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
    /// The Ours user id stored in local storage and cookies on your web properties.
    /// If userId is included in the request, we do not lookup the user by email or externalId.
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

    /// <inheritdoc cref="IFromRawJson.FromRawUnchecked"/>
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
    public string? AdID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("ad_id");
        }
        init { this._rawData.Set("ad_id", value); }
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

    public string? BasisCid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("basis_cid");
        }
        init { this._rawData.Set("basis_cid", value); }
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

    public string? CompanyName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("company_name");
        }
        init { this._rawData.Set("company_name", value); }
    }

    public IReadOnlyDictionary<string, string?>? Consent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<FrozenDictionary<string, string?>>("consent");
        }
        init
        {
            this._rawData.Set<FrozenDictionary<string, string?>?>(
                "consent",
                value == null ? null : FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    public string? Country
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("country");
        }
        init { this._rawData.Set("country", value); }
    }

    public IReadOnlyDictionary<string, string?>? CustomProperties
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<FrozenDictionary<string, string?>>(
                "custom_properties"
            );
        }
        init
        {
            this._rawData.Set<FrozenDictionary<string, string?>?>(
                "custom_properties",
                value == null ? null : FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

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

    public string? Email
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("email");
        }
        init { this._rawData.Set("email", value); }
    }

    public string? Epik
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("epik");
        }
        init { this._rawData.Set("epik", value); }
    }

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

    public string? Gender
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("gender");
        }
        init { this._rawData.Set("gender", value); }
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
    /// The IP address of the user
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

    public string? IsBot
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("is_bot");
        }
        init { this._rawData.Set("is_bot", value); }
    }

    public string? JobTitle
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("job_title");
        }
        init { this._rawData.Set("job_title", value); }
    }

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

    public string? PhoneNumber
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("phone_number");
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

    public string? Sid
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("sid");
        }
        init { this._rawData.Set("sid", value); }
    }

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

    public string? UtmCampaign
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_campaign");
        }
        init { this._rawData.Set("utm_campaign", value); }
    }

    public string? UtmContent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_content");
        }
        init { this._rawData.Set("utm_content", value); }
    }

    public string? UtmMedium
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_medium");
        }
        init { this._rawData.Set("utm_medium", value); }
    }

    public string? UtmName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_name");
        }
        init { this._rawData.Set("utm_name", value); }
    }

    public string? UtmSource
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_source");
        }
        init { this._rawData.Set("utm_source", value); }
    }

    public string? UtmTerm
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("utm_term");
        }
        init { this._rawData.Set("utm_term", value); }
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

    public string? Zip
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("zip");
        }
        init { this._rawData.Set("zip", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.AdID;
        _ = this.AdsetID;
        _ = this.Alart;
        _ = this.Aleid;
        _ = this.BasisCid;
        _ = this.CampaignID;
        _ = this.City;
        _ = this.Clickid;
        _ = this.Clid;
        _ = this.CompanyName;
        _ = this.Consent;
        _ = this.Country;
        _ = this.CustomProperties;
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
        _ = this.ImRef;
        _ = this.IP;
        _ = this.Irclickid;
        _ = this.IsBot;
        _ = this.JobTitle;
        _ = this.LastName;
        _ = this.LiFatID;
        _ = this.Msclkid;
        _ = this.Ndclid;
        _ = this.PhoneNumber;
        _ = this.Qclid;
        _ = this.RdtCid;
        _ = this.Referrer;
        _ = this.ReferringDomain;
        _ = this.Sacid;
        _ = this.Sccid;
        _ = this.Sid;
        _ = this.State;
        _ = this.Ttclid;
        _ = this.Twclid;
        _ = this.UserAgent;
        _ = this.UserAgentFullList;
        _ = this.UtmCampaign;
        _ = this.UtmContent;
        _ = this.UtmMedium;
        _ = this.UtmName;
        _ = this.UtmSource;
        _ = this.UtmTerm;
        _ = this.Wbraid;
        _ = this.Zip;
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

/// <summary>
/// These properties are used throughout the Ours app to pass known values onto destinations
/// </summary>
[JsonConverter(typeof(JsonModelConverter<DefaultProperties, DefaultPropertiesFromRaw>))]
public sealed record class DefaultProperties : JsonModel
{
    /// <summary>
    /// The active time in milliseconds that the user had this tab active
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
    /// The ad id for detected in the session. This is set by the web sdk automatically.
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
    /// The adset id for detected in the session. This is set by the web sdk automatically.
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
    /// The AppLovin alart query parameter. Ex: alart123
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
    /// The AppLovin aleid query parameter. Ex: aleid123
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
    /// The Basis DSP Click ID. Ex: basis_cid123
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
    /// The language of the browser. Ex: en-US
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
    /// The name of the browser. Ex: Chrome
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
    /// The version of the browser. Ex: 114.0
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
    /// The campaign id for detected in the session. This is set by the web sdk automatically.
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
    /// The Click ID. Ex: clickid123
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
    /// The Generic Click ID. Ex: clid123
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
    /// The architecture of the CPU. Ex: x64
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
    /// The full url (including query params) of the current page
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
    /// The DoubleClick Click ID. Ex: dclid123
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
    /// The model of the device. Ex: iPhone 13
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
    /// The type of device the user is using. Ex: mobile
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
    /// The vendor of the device. Ex: Apple
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
    /// The time in milliseconds since the page was loaded // script was loaded
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
    /// The browsers encoding. Ex: UTF-8
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
    /// The name of the browser engine. Ex: Blink
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
    /// The version of the browser engine. Ex: 114.0
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
    /// The Pinterest Click ID. Ex: epik456
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
    /// Raw Facebook Click ID query parameter without prefix from ad clicks. Ex: AbCdEfGhIjKlMnOpQrStUvWxYz1234567890
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
    /// Ex: fb.1.1554763741205.1098115397
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
    /// The Google Ad Source. Ex: google
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
    /// The Google Braid ID. Ex: gbraid123
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
    /// The Google Click ID. Ex: gclid123
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
    /// The host of the current page. Ex: example.com
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
    /// The Impact Click ID reference. Ex: im_ref123
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
    /// The IP address of the user. Ex: 127.0.0.1
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
    /// The Impact Click ID. Ex: irclickid123
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
    /// Whether we have detected that the user is a bot. This is set automatically
    /// by the Ours server primarily for events tracked through the web SDK.
    /// </summary>
    public string? IsBot
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("is_bot");
        }
        init { this._rawData.Set("is_bot", value); }
    }

    /// <summary>
    /// The LinkedIn Click ID. Ex: li_fat_id123
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
    /// The Microsoft Click ID. Ex: msclkid123
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
    /// The NextDoor Click ID. Ex: ndclid123
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
    /// The name of the operating system. Ex: Windows
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
    /// The version of the operating system. Ex: 10.0
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
    /// The pathname of the current page. Ex: /home
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
    /// The Quora Click ID. Ex: qclid123
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
    /// The Reddit Click ID. Ex: rdt_cid123
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
    /// The time the event was received by an Ours server in ISO format
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
    /// The referrer URL of the current page
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
    /// The referring domain of the current page
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
    /// The StackAdapt Tracking ID. Ex: sacid123
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
    /// The SnapChat Click ID. Ex: sccid123
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
    /// for session replay
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
    /// The title of the current page
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
    /// The TikTok Click ID. Ex: ttclid123
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
    /// The Twitter Click ID. Ex: twclid123
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
    /// User agent as a full list of strings.
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
    /// The user agent of the browser
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
    /// The UTM Campaign. The web SDK automatically captures this from the query params.
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
    /// The version of the web SDK
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
    /// The WBRAID Identifier. The web SDK automatically captures this from the query params.
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

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.ActiveDuration;
        _ = this.AdID;
        _ = this.AdsetID;
        _ = this.Alart;
        _ = this.Aleid;
        _ = this.BasisCid;
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
        _ = this.Host;
        _ = this.Iframe;
        _ = this.ImRef;
        _ = this.IP;
        _ = this.Irclickid;
        _ = this.IsBot;
        _ = this.LiFatID;
        _ = this.Msclkid;
        _ = this.Ndclid;
        _ = this.NewS;
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
        _ = this.Wbraid;
        _ = this.Webview;
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
