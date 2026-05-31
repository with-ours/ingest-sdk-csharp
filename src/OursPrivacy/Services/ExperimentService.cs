using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;
using OursPrivacy.Models.Experiments;

namespace OursPrivacy.Services;

/// <inheritdoc/>
public sealed class ExperimentService : IExperimentService
{
    readonly Lazy<IExperimentServiceWithRawResponse> _withRawResponse;

    /// <inheritdoc/>
    public IExperimentServiceWithRawResponse WithRawResponse
    {
        get { return _withRawResponse.Value; }
    }

    readonly IOursPrivacyClient _client;

    /// <inheritdoc/>
    public IExperimentService WithOptions(Func<ClientOptions, ClientOptions> modifier)
    {
        return new ExperimentService(this._client.WithOptions(modifier));
    }

    public ExperimentService(IOursPrivacyClient client)
    {
        _client = client;

        _withRawResponse = new(() => new ExperimentServiceWithRawResponse(client.WithRawResponse));
    }

    /// <inheritdoc/>
    public async Task<ExperimentAssignmentResponse> Assignment(
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Assignment(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public Task<ExperimentAssignmentResponse> Assignment(
        string experimentKey,
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        return this.Assignment(
            parameters with
            {
                ExperimentKey = experimentKey,
            },
            cancellationToken
        );
    }

    /// <inheritdoc/>
    public async Task<ExperimentPersonalizationResponse> Personalization(
        ExperimentPersonalizationParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Personalization(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }
}

/// <inheritdoc/>
public sealed class ExperimentServiceWithRawResponse : IExperimentServiceWithRawResponse
{
    readonly IOursPrivacyClientWithRawResponse _client;

    /// <inheritdoc/>
    public IExperimentServiceWithRawResponse WithOptions(
        Func<ClientOptions, ClientOptions> modifier
    )
    {
        return new ExperimentServiceWithRawResponse(this._client.WithOptions(modifier));
    }

    public ExperimentServiceWithRawResponse(IOursPrivacyClientWithRawResponse client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<ExperimentAssignmentResponse>> Assignment(
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        if (parameters.ExperimentKey == null)
        {
            throw new OursPrivacyInvalidDataException("'parameters.ExperimentKey' cannot be null");
        }

        HttpRequest<ExperimentAssignmentParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<ExperimentAssignmentResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public Task<HttpResponse<ExperimentAssignmentResponse>> Assignment(
        string experimentKey,
        ExperimentAssignmentParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        return this.Assignment(
            parameters with
            {
                ExperimentKey = experimentKey,
            },
            cancellationToken
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<ExperimentPersonalizationResponse>> Personalization(
        ExperimentPersonalizationParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        HttpRequest<ExperimentPersonalizationParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<ExperimentPersonalizationResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }
}
