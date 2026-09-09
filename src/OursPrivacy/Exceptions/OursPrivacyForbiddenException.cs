using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyForbiddenException : OursPrivacy4xxException
{
    public OursPrivacyForbiddenException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
