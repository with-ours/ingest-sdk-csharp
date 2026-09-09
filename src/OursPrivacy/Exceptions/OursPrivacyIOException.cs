using System;
using System.Net.Http;

namespace OursPrivacy.Exceptions;

public class OursPrivacyIOException : OursPrivacyException
{
    public new HttpRequestException InnerException
    {
        get
        {
            if (base.InnerException == null)
            {
                throw new ArgumentNullException();
            }
            return (HttpRequestException)base.InnerException;
        }
    }

    public OursPrivacyIOException(string message, HttpRequestException? innerException = null)
        : base(message, innerException) { }
}
