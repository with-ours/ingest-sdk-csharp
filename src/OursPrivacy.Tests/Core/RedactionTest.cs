using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using OursPrivacy.Core;
using OursPrivacy.Exceptions;
using OursPrivacy.Models.Batch;

namespace OursPrivacy.Tests.Core;

public class RedactionTest : TestBase
{
    [Fact]
    public void Redact_ReplacesSensitiveValuesAtAnyDepth()
    {
        var json = JsonSerializer.Deserialize<JsonElement>(
            "{\""
                + "token"
                + "\":\""
                + "SENTINEL_SECRET_DO_NOT_LOG"
                + "\",\"nested\":[{\""
                + "token"
                + "\":[\""
                + "SENTINEL_SECRET_DO_NOT_LOG"
                + "\"]}],\"event\":\"still_visible\"}"
        );

        var printed = JsonSerializer.Serialize(FriendlyJsonPrinter.Redact(json));

        Assert.DoesNotContain("SENTINEL_SECRET_DO_NOT_LOG", printed);
        Assert.Contains("still_visible", printed);
        Assert.Contains("***", printed);
    }

    [Fact]
    public void ApiExceptionMessage_RedactsEchoedSecrets()
    {
        var exception = OursPrivacyExceptionFactory.CreateApiException(
            HttpStatusCode.BadRequest,
            "{\""
                + "token"
                + "\":\""
                + "SENTINEL_SECRET_DO_NOT_LOG"
                + "\",\"code\":\"still_visible\"}"
        );

        Assert.DoesNotContain("SENTINEL_SECRET_DO_NOT_LOG", exception.Message);
        Assert.DoesNotContain("SENTINEL_SECRET_DO_NOT_LOG", exception.ToString());
        Assert.Contains("still_visible", exception.Message);
    }

    [Fact]
    public void ParamsToString_RedactsSecretsWithoutChangingTheBody()
    {
        var rawBody = new Dictionary<string, JsonElement>
        {
            ["token"] = JsonSerializer.SerializeToElement("SENTINEL_SECRET_DO_NOT_LOG"),
            ["event"] = JsonSerializer.SerializeToElement("still_visible"),
        };
        var parameters = BatchCreateParams.FromRawUnchecked(
            new Dictionary<string, JsonElement>(),
            new Dictionary<string, JsonElement>(),
            rawBody
        );

        var printed = parameters.ToString();

        Assert.DoesNotContain("SENTINEL_SECRET_DO_NOT_LOG", printed);
        Assert.Contains("still_visible", printed);
        Assert.Contains("***", printed);
        Assert.Contains(
            "SENTINEL_SECRET_DO_NOT_LOG",
            JsonSerializer.Serialize(parameters.RawBodyData)
        );
    }
}
