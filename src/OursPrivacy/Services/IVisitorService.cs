using System;
using System.Threading;
using System.Threading.Tasks;
using OursPrivacy.Core;
using OursPrivacy.Models.Visitor;

namespace OursPrivacy.Services;

/// <summary>
/// NOTE: Do not inherit from this type outside the SDK unless you're okay with breaking
/// changes in non-major versions. We may add new methods in the future that cause
/// existing derived classes to break.
/// </summary>
public interface IVisitorService
{
    /// <summary>
    /// Returns a view of this service that provides access to raw HTTP responses
    /// for each method.
    /// </summary>
    IVisitorServiceWithRawResponse WithRawResponse { get; }

    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    IVisitorService WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Set or update properties on an existing visitor, or create a new visitor if no
    /// match is found. This fires a $identify event, making the call visible in the
    /// event stream. Identity resolution runs in priority order: userId (direct, no
    /// lookup) → externalId (lookup by your ID) → email (fallback lookup). When a
    /// visitor is found, their Ours Visitor ID is used going forward so all future
    /// events are attached to the same profile. For top-level visitor properties: null
    /// clears the existing value, while undefined, omitted fields, and empty strings
    /// are ignored. For entries inside custom_properties: null, undefined, and empty
    /// strings are all ignored (custom_properties use merge semantics). See
    /// https://docs.oursprivacy.com/docs/data-types for details and common pitfalls.
    /// </summary>
    Task<VisitorUpsertResponse> Upsert(
        VisitorUpsertParams parameters,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
/// A view of <see cref="IVisitorService"/> that provides access to raw
/// HTTP responses for each method.
/// </summary>
public interface IVisitorServiceWithRawResponse
{
    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    IVisitorServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Returns a raw HTTP response for <c>post /identify</c>, but is otherwise the
    /// same as <see cref="IVisitorService.Upsert(VisitorUpsertParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<VisitorUpsertResponse>> Upsert(
        VisitorUpsertParams parameters,
        CancellationToken cancellationToken = default
    );
}
