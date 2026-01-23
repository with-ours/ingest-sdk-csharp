using System;
using OursPrivacy;

namespace OursPrivacy.Tests;

public class TestBase
{
    protected IOursPrivacyClient client;

    public TestBase()
    {
        client = new OursPrivacyClient()
        {
            BaseUrl =
                Environment.GetEnvironmentVariable("TEST_API_BASE_URL") ?? "http://localhost:4010",
        };
    }
}
