using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OursPrivacy.Core;
using OursPrivacy.Models.Track;

namespace OursPrivacy.Services;

/// <inheritdoc/>
public sealed class TrackService : ITrackService
{
    readonly Lazy<ITrackServiceWithRawResponse> _withRawResponse;

    /// <inheritdoc/>
    public ITrackServiceWithRawResponse WithRawResponse
    {
        get { return _withRawResponse.Value; }
    }

    readonly IOursPrivacyClient _client;

    /// <inheritdoc/>
    public ITrackService WithOptions(Func<ClientOptions, ClientOptions> modifier)
    {
        return new TrackService(this._client.WithOptions(modifier));
    }

    public TrackService(IOursPrivacyClient client)
    {
        _client = client;

        _withRawResponse = new(() => new TrackServiceWithRawResponse(client.WithRawResponse));
    }

    /// <inheritdoc/>
    public async Task<TrackEventResponse> Event(
        TrackEventParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Event(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }
}

/// <inheritdoc/>
public sealed class TrackServiceWithRawResponse : ITrackServiceWithRawResponse
{
    readonly IOursPrivacyClientWithRawResponse _client;

    /// <inheritdoc/>
    public ITrackServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier)
    {
        return new TrackServiceWithRawResponse(this._client.WithOptions(modifier));
    }

    public TrackServiceWithRawResponse(IOursPrivacyClientWithRawResponse client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<TrackEventResponse>> Event(
        TrackEventParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        HttpRequest<TrackEventParams> request = new()
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
                    .Deserialize<TrackEventResponse>(token)
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
