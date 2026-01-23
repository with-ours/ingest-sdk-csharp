using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyBadRequestException : OursPrivacy4xxException
{
    public OursPrivacyBadRequestException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
