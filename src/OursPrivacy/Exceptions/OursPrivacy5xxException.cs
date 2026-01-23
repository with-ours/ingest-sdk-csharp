using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacy5xxException : OursPrivacyApiException
{
    public OursPrivacy5xxException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
