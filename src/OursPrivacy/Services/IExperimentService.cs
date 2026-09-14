using System;
using System.Threading;
using System.Threading.Tasks;
using OursPrivacy.Core;
using OursPrivacy.Models.Experiments;

namespace OursPrivacy.Services;

/// <summary>
/// NOTE: Do not inherit from this type outside the SDK unless you're okay with breaking
/// changes in non-major versions. We may add new methods in the future that cause
/// existing derived classes to break.
/// </summary>
public interface IExperimentService
{
    /// <summary>
    /// Returns a view of this service that provides access to raw HTTP responses
    /// for each method.
    /// </summary>
    IExperimentServiceWithRawResponse WithRawResponse { get; }

    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    IExperimentService WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Return the server-side variant assignment for a visitor in a single A/B or
    /// multivariate experiment, identified by its experiment key. Bucketing is
    /// deterministic on `visitor_id` and is sticky across calls. Tracking an impression
    /// is the default — pass `track_impression: false` to read without recording. The
    /// browser SDK and this endpoint converge on the same variant for the same visitor.
    /// </summary>
    Task<ExperimentAssignmentResponse> Assignment(
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <inheritdoc cref="Assignment(ExperimentAssignmentParams, CancellationToken)"/>
    Task<ExperimentAssignmentResponse> Assignment(
        string experimentKey,
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Return the visitor traits accumulated by personalization property rules.
    /// Read-only and never records an impression. Use the properties to select
    /// personalized server-rendered copy or targeting; the browser experiment runtime
    /// receives the same bag at initialization.
    /// </summary>
    Task<ExperimentPersonalizationResponse> Personalization(
        ExperimentPersonalizationParams parameters,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
/// A view of <see cref="IExperimentService"/> that provides access to raw
/// HTTP responses for each method.
/// </summary>
public interface IExperimentServiceWithRawResponse
{
    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    IExperimentServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Returns a raw HTTP response for <c>post /experiments/assignments/{experiment_key}</c>, but is otherwise the
    /// same as <see cref="IExperimentService.Assignment(ExperimentAssignmentParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<ExperimentAssignmentResponse>> Assignment(
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <inheritdoc cref="Assignment(ExperimentAssignmentParams, CancellationToken)"/>
    Task<HttpResponse<ExperimentAssignmentResponse>> Assignment(
        string experimentKey,
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /experiments/personalization</c>, but is otherwise the
    /// same as <see cref="IExperimentService.Personalization(ExperimentPersonalizationParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<ExperimentPersonalizationResponse>> Personalization(
        ExperimentPersonalizationParams parameters,
        CancellationToken cancellationToken = default
    );
}
