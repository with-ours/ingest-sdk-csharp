using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyRateLimitException : OursPrivacy4xxException
{
    public OursPrivacyRateLimitException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
