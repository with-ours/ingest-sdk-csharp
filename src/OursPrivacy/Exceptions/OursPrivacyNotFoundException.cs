using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyNotFoundException : OursPrivacy4xxException
{
    public OursPrivacyNotFoundException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
