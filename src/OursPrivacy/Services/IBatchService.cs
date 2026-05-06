using System;
using System.Threading;
using System.Threading.Tasks;
using OursPrivacy.Core;
using OursPrivacy.Models.Batch;

namespace OursPrivacy.Services;

/// <summary>
/// NOTE: Do not inherit from this type outside the SDK unless you're okay with breaking
/// changes in non-major versions. We may add new methods in the future that cause
/// existing derived classes to break.
/// </summary>
public interface IBatchService
{
    /// <summary>
    /// Returns a view of this service that provides access to raw HTTP responses
    /// for each method.
    /// </summary>
    IBatchServiceWithRawResponse WithRawResponse { get; }

    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    IBatchService WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Send multiple `/track`-shaped events in a single request. The top-level token is
    /// authorized once for the full batch. Each batch row must include `distinctId`,
    /// and mixed validation or queue outcomes are reported per row.
    /// </summary>
    Task<BatchCreateResponse> Create(
        BatchCreateParams parameters,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
/// A view of <see cref="IBatchService"/> that provides access to raw
/// HTTP responses for each method.
/// </summary>
public interface IBatchServiceWithRawResponse
{
    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    IBatchServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Returns a raw HTTP response for <c>post /batch</c>, but is otherwise the
    /// same as <see cref="IBatchService.Create(BatchCreateParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<BatchCreateResponse>> Create(
        BatchCreateParams parameters,
        CancellationToken cancellationToken = default
    );
}
