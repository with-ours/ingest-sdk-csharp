using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OursPrivacy.Core;
using OursPrivacy.Models.Visitor;

namespace OursPrivacy.Services;

/// <inheritdoc/>
public sealed class VisitorService : IVisitorService
{
    readonly Lazy<IVisitorServiceWithRawResponse> _withRawResponse;

    /// <inheritdoc/>
    public IVisitorServiceWithRawResponse WithRawResponse
    {
        get { return _withRawResponse.Value; }
    }

    readonly IOursPrivacyClient _client;

    /// <inheritdoc/>
    public IVisitorService WithOptions(Func<ClientOptions, ClientOptions> modifier)
    {
        return new VisitorService(this._client.WithOptions(modifier));
    }

    public VisitorService(IOursPrivacyClient client)
    {
        _client = client;

        _withRawResponse = new(() => new VisitorServiceWithRawResponse(client.WithRawResponse));
    }

    /// <inheritdoc/>
    public async Task<VisitorUpsertResponse> Upsert(
        VisitorUpsertParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Upsert(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }
}

/// <inheritdoc/>
public sealed class VisitorServiceWithRawResponse : IVisitorServiceWithRawResponse
{
    readonly IOursPrivacyClientWithRawResponse _client;

    /// <inheritdoc/>
    public IVisitorServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier)
    {
        return new VisitorServiceWithRawResponse(this._client.WithOptions(modifier));
    }

    public VisitorServiceWithRawResponse(IOursPrivacyClientWithRawResponse client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<VisitorUpsertResponse>> Upsert(
        VisitorUpsertParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        HttpRequest<VisitorUpsertParams> request = new()
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
                    .Deserialize<VisitorUpsertResponse>(token)
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
