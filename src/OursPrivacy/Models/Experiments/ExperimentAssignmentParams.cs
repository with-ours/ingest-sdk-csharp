using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using OursPrivacy.Core;

namespace OursPrivacy.Models.Experiments;

/// <summary>
/// Return the server-side variant assignment for a visitor in a single A/B or multivariate
/// experiment, identified by its experiment key. Bucketing is deterministic on `visitor_id`
/// and is sticky across calls. Tracking an impression is the default — pass `track_impression:
/// false` to read without recording. The browser SDK and this endpoint converge on
/// the same variant for the same visitor.
///
/// <para>NOTE: Do not inherit from this type outside the SDK unless you're okay with
/// breaking changes in non-major versions. We may add new methods in the future that
/// cause existing derived classes to break.</para>
/// </summary>
public record class ExperimentAssignmentParams : ParamsBase
{
    readonly JsonDictionary _rawBodyData = new();
    public IReadOnlyDictionary<string, JsonElement> RawBodyData
    {
        get { return this._rawBodyData.Freeze(); }
    }

    public string? ExperimentKey { get; init; }

    /// <summary>
    /// The experiment token (`exp_*`) for the experiment settings holding this experiment.
    /// Available from the dashboard.
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
    /// Stable identifier for the visitor — typically the Ours visitor id from your
    /// browser cookie, or your own server-side user id if you keep the same id consistent
    /// across browser and server.
    /// </summary>
    public required string VisitorID
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNotNullClass<string>("visitor_id");
        }
        init { this._rawBodyData.Set("visitor_id", value); }
    }

    /// <summary>
    /// Optional page context for URL + query-param eligibility. Variant bucketing
    /// is deterministic on `visitor_id` regardless of context.
    /// </summary>
    public Context? Context
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<Context>("context");
        }
        init { this._rawBodyData.Set("context", value); }
    }

    /// <summary>
    /// When true (default), an `$experiment_impression` event is enqueued and the
    /// visitor's `experiment_assignments` map is updated. Set to false to read the
    /// assignment without recording an impression — useful for in-test diagnostics.
    /// </summary>
    public bool? TrackImpression
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<bool>("track_impression");
        }
        init { this._rawBodyData.Set("track_impression", value); }
    }

    public ExperimentAssignmentParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public ExperimentAssignmentParams(ExperimentAssignmentParams experimentAssignmentParams)
        : base(experimentAssignmentParams)
    {
        this.ExperimentKey = experimentAssignmentParams.ExperimentKey;

        this._rawBodyData = new(experimentAssignmentParams._rawBodyData);
    }
#pragma warning restore CS8618

    public ExperimentAssignmentParams(
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
    ExperimentAssignmentParams(
        FrozenDictionary<string, JsonElement> rawHeaderData,
        FrozenDictionary<string, JsonElement> rawQueryData,
        FrozenDictionary<string, JsonElement> rawBodyData,
        string experimentKey
    )
    {
        this._rawHeaderData = new(rawHeaderData);
        this._rawQueryData = new(rawQueryData);
        this._rawBodyData = new(rawBodyData);
        this.ExperimentKey = experimentKey;
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="IFromRawJson{T}.FromRawUnchecked"/>
    public static ExperimentAssignmentParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawHeaderData,
        IReadOnlyDictionary<string, JsonElement> rawQueryData,
        IReadOnlyDictionary<string, JsonElement> rawBodyData,
        string experimentKey
    )
    {
        return new(
            FrozenDictionary.ToFrozenDictionary(rawHeaderData),
            FrozenDictionary.ToFrozenDictionary(rawQueryData),
            FrozenDictionary.ToFrozenDictionary(rawBodyData),
            experimentKey
        );
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(
                new Dictionary<string, JsonElement>()
                {
                    ["ExperimentKey"] = JsonSerializer.SerializeToElement(this.ExperimentKey),
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

    public virtual bool Equals(ExperimentAssignmentParams? other)
    {
        if (other == null)
        {
            return false;
        }
        return (this.ExperimentKey?.Equals(other.ExperimentKey) ?? other.ExperimentKey == null)
            && this._rawHeaderData.Equals(other._rawHeaderData)
            && this._rawQueryData.Equals(other._rawQueryData)
            && this._rawBodyData.Equals(other._rawBodyData);
    }

    public override Uri Url(ClientOptions options)
    {
        return new UriBuilder(
            options.BaseUrl.ToString().TrimEnd('/')
                + string.Format("/experiments/assignments/{0}", this.ExperimentKey)
        )
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
/// Optional page context for URL + query-param eligibility. Variant bucketing is
/// deterministic on `visitor_id` regardless of context.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Context, ContextFromRaw>))]
public sealed record class Context : JsonModel
{
    /// <summary>
    /// The current query string (e.g. `?utm_source=meta`). When provided, the experiment's
    /// query-param conditions are evaluated for eligibility. If omitted, the query
    /// string is parsed from `url`.
    /// </summary>
    public string? Search
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("search");
        }
        init { this._rawData.Set("search", value); }
    }

    /// <summary>
    /// The current page URL. When provided, the experiment's URL patterns are evaluated
    /// for eligibility — visitors on non-matching URLs are returned `in_experiment:
    /// false`. Omit when the caller is pre-gating the request.
    /// </summary>
    public string? Url
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("url");
        }
        init { this._rawData.Set("url", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Search;
        _ = this.Url;
    }

    public Context() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Context(Context context)
        : base(context) { }
#pragma warning restore CS8618

    public Context(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Context(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ContextFromRaw.FromRawUnchecked"/>
    public static Context FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ContextFromRaw : IFromRawJson<Context>
{
    /// <inheritdoc/>
    public Context FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Context.FromRawUnchecked(rawData);
}
