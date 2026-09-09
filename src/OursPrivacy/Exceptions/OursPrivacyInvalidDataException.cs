using System;

namespace OursPrivacy.Exceptions;

public class OursPrivacyInvalidDataException : OursPrivacyException
{
    public OursPrivacyInvalidDataException(string message, Exception? innerException = null)
        : base(message, innerException) { }
}
