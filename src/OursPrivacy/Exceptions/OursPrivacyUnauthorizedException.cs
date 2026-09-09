using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyUnauthorizedException : OursPrivacy4xxException
{
    public OursPrivacyUnauthorizedException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
