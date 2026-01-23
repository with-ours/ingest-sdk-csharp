using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyUnexpectedStatusCodeException : OursPrivacyApiException
{
    public OursPrivacyUnexpectedStatusCodeException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
