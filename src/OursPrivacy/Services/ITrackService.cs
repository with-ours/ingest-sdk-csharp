using System;
using System.Threading;
using System.Threading.Tasks;
using OursPrivacy.Core;
using OursPrivacy.Models.Track;

namespace OursPrivacy.Services;

/// <summary>
/// NOTE: Do not inherit from this type outside the SDK unless you're okay with breaking
/// changes in non-major versions. We may add new methods in the future that cause
/// existing derived classes to break.
/// </summary>
public interface ITrackService
{
    /// <summary>
    /// Returns a view of this service that provides access to raw HTTP responses
    /// for each method.
    /// </summary>
    ITrackServiceWithRawResponse WithRawResponse { get; }

    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    ITrackService WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Track events from your server. Please include at least one of: userId,
    /// externalId, or email. These properties help us associate events with existing
    /// users. For top-level visitor properties: null clears the existing value, while
    /// undefined, omitted fields, and empty strings are ignored. For entries inside
    /// custom_properties: null, undefined, and empty strings are all ignored
    /// (custom_properties use merge semantics). See
    /// https://docs.oursprivacy.com/docs/data-types for details and common pitfalls.
    /// </summary>
    Task<TrackEventResponse> Event(
        TrackEventParams parameters,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
/// A view of <see cref="ITrackService"/> that provides access to raw
/// HTTP responses for each method.
/// </summary>
public interface ITrackServiceWithRawResponse
{
    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    ITrackServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Returns a raw HTTP response for <c>post /track</c>, but is otherwise the
    /// same as <see cref="ITrackService.Event(TrackEventParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<TrackEventResponse>> Event(
        TrackEventParams parameters,
        CancellationToken cancellationToken = default
    );
}
