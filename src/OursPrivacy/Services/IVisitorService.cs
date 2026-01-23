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
    /// Define visitor properties on an existing visitor or create a new visitor.
    /// Note: This does not fire an event. If you want to fire an event, use the
    /// track method and include properties for the visitor.
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
    /// Returns a raw HTTP response for `post /identify`, but is otherwise the
    /// same as <see cref="IVisitorService.Upsert(VisitorUpsertParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<VisitorUpsertResponse>> Upsert(
        VisitorUpsertParams parameters,
        CancellationToken cancellationToken = default
    );
}
