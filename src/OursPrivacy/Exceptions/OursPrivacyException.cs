using System;
using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyException : Exception
{
    public OursPrivacyException(string message, Exception? innerException = null)
        : base(message, innerException) { }

    protected OursPrivacyException(HttpRequestException? innerException)
        : base(null, innerException) { }
}
