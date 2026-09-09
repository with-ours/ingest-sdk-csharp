using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacy4xxException : OursPrivacyApiException
{
    public OursPrivacy4xxException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
