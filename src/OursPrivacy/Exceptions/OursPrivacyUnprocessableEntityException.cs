using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyUnprocessableEntityException : OursPrivacy4xxException
{
    public OursPrivacyUnprocessableEntityException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
