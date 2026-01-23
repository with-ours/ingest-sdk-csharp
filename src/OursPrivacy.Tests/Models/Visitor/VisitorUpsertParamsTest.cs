using System;
using System.Collections.Generic;
using System.Text.Json;
using OursPrivacy.Core;
using OursPrivacy.Models.Visitor;

namespace OursPrivacy.Tests.Models.Visitor;

public class VisitorUpsertParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new VisitorUpsertParams
        {
            Token = "x",
            UserProperties = new()
            {
                AdID = "ad_id",
                AdsetID = "adset_id",
                Alart = "alart",
                Aleid = "aleid",
                BasisCid = "basis_cid",
                CampaignID = "campaign_id",
                City = "city",
                Clickid = "clickid",
                Clid = "clid",
                CompanyName = "company_name",
                Consent = new Dictionary<string, string?>() { { "foo", "string" } },
                Country = "country",
                CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
                DateOfBirth = "date_of_birth",
                Dclid = "dclid",
                Email = "email",
                Epik = "epik",
                ExternalID = "external_id",
                Fbc = "fbc",
                Fbclid = "fbclid",
                Fbp = "fbp",
                FirstName = "first_name",
                GadSource = "gad_source",
                Gbraid = "gbraid",
                Gclid = "gclid",
                Gender = "gender",
                ImRef = "im_ref",
                IP = "ip",
                Irclickid = "irclickid",
                IsBot = "is_bot",
                JobTitle = "job_title",
                LastName = "last_name",
                LiFatID = "li_fat_id",
                Msclkid = "msclkid",
                Ndclid = "ndclid",
                PhoneNumber = "phone_number",
                Qclid = "qclid",
                RdtCid = "rdt_cid",
                Referrer = "referrer",
                ReferringDomain = "referring_domain",
                Sacid = "sacid",
                Sccid = "sccid",
                Sid = "sid",
                State = "state",
                Ttclid = "ttclid",
                Twclid = "twclid",
                UserAgent = "user_agent",
                UserAgentFullList = "user_agent_full_list",
                UtmCampaign = "utm_campaign",
                UtmContent = "utm_content",
                UtmMedium = "utm_medium",
                UtmName = "utm_name",
                UtmSource = "utm_source",
                UtmTerm = "utm_term",
                Wbraid = "wbraid",
                Zip = "zip",
            },
            DefaultProperties = new()
            {
                ActiveDuration = 0,
                AdID = "ad_id",
                AdsetID = "adset_id",
                Alart = "alart",
                Aleid = "aleid",
                BasisCid = "basis_cid",
                BrowserLanguage = "browser_language",
                BrowserName = "browser_name",
                BrowserVersion = "browser_version",
                CampaignID = "campaign_id",
                Clickid = "clickid",
                Clid = "clid",
                CpuArchitecture = "cpu_architecture",
                CurrentUrl = "current_url",
                Dclid = "dclid",
                DeviceModel = "device_model",
                DeviceType = "device_type",
                DeviceVendor = "device_vendor",
                Duration = 0,
                Encoding = "encoding",
                EngineName = "engine_name",
                EngineVersion = "engine_version",
                Epik = "epik",
                Fbc = "fbc",
                Fbclid = "fbclid",
                Fbp = "fbp",
                Fv = true,
                GadSource = "gad_source",
                Gbraid = "gbraid",
                Gclid = "gclid",
                Host = "host",
                Iframe = true,
                ImRef = "im_ref",
                IP = "ip",
                Irclickid = "irclickid",
                IsBot = "is_bot",
                LiFatID = "li_fat_id",
                Msclkid = "msclkid",
                Ndclid = "ndclid",
                NewS = true,
                OsName = "os_name",
                OsVersion = "os_version",
                PageHash = 0,
                Pathname = "pathname",
                Qclid = "qclid",
                RdtCid = "rdt_cid",
                ReceivedAt = "received_at",
                Referrer = "referrer",
                ReferringDomain = "referring_domain",
                Sacid = "sacid",
                Sccid = "sccid",
                ScreenHeight = 0,
                ScreenWidth = 0,
                SessionCount = 0,
                Sid = "sid",
                Sr = "sr",
                Title = "title",
                Ttclid = "ttclid",
                Twclid = "twclid",
                Uafvl = "uafvl",
                UserAgent = "user_agent",
                UtmCampaign = "utm_campaign",
                UtmContent = "utm_content",
                UtmMedium = "utm_medium",
                UtmName = "utm_name",
                UtmSource = "utm_source",
                UtmTerm = "utm_term",
                Version = "version",
                Wbraid = "wbraid",
                Webview = true,
            },
            Email = "x",
            ExternalID = "x",
            UserID = "x",
        };

        string expectedToken = "x";
        UserProperties expectedUserProperties = new()
        {
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            CampaignID = "campaign_id",
            City = "city",
            Clickid = "clickid",
            Clid = "clid",
            CompanyName = "company_name",
            Consent = new Dictionary<string, string?>() { { "foo", "string" } },
            Country = "country",
            CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
            DateOfBirth = "date_of_birth",
            Dclid = "dclid",
            Email = "email",
            Epik = "epik",
            ExternalID = "external_id",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            FirstName = "first_name",
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Gender = "gender",
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            JobTitle = "job_title",
            LastName = "last_name",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            PhoneNumber = "phone_number",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            Sid = "sid",
            State = "state",
            Ttclid = "ttclid",
            Twclid = "twclid",
            UserAgent = "user_agent",
            UserAgentFullList = "user_agent_full_list",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Wbraid = "wbraid",
            Zip = "zip",
        };
        DefaultProperties expectedDefaultProperties = new()
        {
            ActiveDuration = 0,
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            BrowserLanguage = "browser_language",
            BrowserName = "browser_name",
            BrowserVersion = "browser_version",
            CampaignID = "campaign_id",
            Clickid = "clickid",
            Clid = "clid",
            CpuArchitecture = "cpu_architecture",
            CurrentUrl = "current_url",
            Dclid = "dclid",
            DeviceModel = "device_model",
            DeviceType = "device_type",
            DeviceVendor = "device_vendor",
            Duration = 0,
            Encoding = "encoding",
            EngineName = "engine_name",
            EngineVersion = "engine_version",
            Epik = "epik",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            Fv = true,
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Host = "host",
            Iframe = true,
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            NewS = true,
            OsName = "os_name",
            OsVersion = "os_version",
            PageHash = 0,
            Pathname = "pathname",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            ReceivedAt = "received_at",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            ScreenHeight = 0,
            ScreenWidth = 0,
            SessionCount = 0,
            Sid = "sid",
            Sr = "sr",
            Title = "title",
            Ttclid = "ttclid",
            Twclid = "twclid",
            Uafvl = "uafvl",
            UserAgent = "user_agent",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Version = "version",
            Wbraid = "wbraid",
            Webview = true,
        };
        string expectedEmail = "x";
        string expectedExternalID = "x";
        string expectedUserID = "x";

        Assert.Equal(expectedToken, parameters.Token);
        Assert.Equal(expectedUserProperties, parameters.UserProperties);
        Assert.Equal(expectedDefaultProperties, parameters.DefaultProperties);
        Assert.Equal(expectedEmail, parameters.Email);
        Assert.Equal(expectedExternalID, parameters.ExternalID);
        Assert.Equal(expectedUserID, parameters.UserID);
    }

    [Fact]
    public void OptionalNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new VisitorUpsertParams
        {
            Token = "x",
            UserProperties = new()
            {
                AdID = "ad_id",
                AdsetID = "adset_id",
                Alart = "alart",
                Aleid = "aleid",
                BasisCid = "basis_cid",
                CampaignID = "campaign_id",
                City = "city",
                Clickid = "clickid",
                Clid = "clid",
                CompanyName = "company_name",
                Consent = new Dictionary<string, string?>() { { "foo", "string" } },
                Country = "country",
                CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
                DateOfBirth = "date_of_birth",
                Dclid = "dclid",
                Email = "email",
                Epik = "epik",
                ExternalID = "external_id",
                Fbc = "fbc",
                Fbclid = "fbclid",
                Fbp = "fbp",
                FirstName = "first_name",
                GadSource = "gad_source",
                Gbraid = "gbraid",
                Gclid = "gclid",
                Gender = "gender",
                ImRef = "im_ref",
                IP = "ip",
                Irclickid = "irclickid",
                IsBot = "is_bot",
                JobTitle = "job_title",
                LastName = "last_name",
                LiFatID = "li_fat_id",
                Msclkid = "msclkid",
                Ndclid = "ndclid",
                PhoneNumber = "phone_number",
                Qclid = "qclid",
                RdtCid = "rdt_cid",
                Referrer = "referrer",
                ReferringDomain = "referring_domain",
                Sacid = "sacid",
                Sccid = "sccid",
                Sid = "sid",
                State = "state",
                Ttclid = "ttclid",
                Twclid = "twclid",
                UserAgent = "user_agent",
                UserAgentFullList = "user_agent_full_list",
                UtmCampaign = "utm_campaign",
                UtmContent = "utm_content",
                UtmMedium = "utm_medium",
                UtmName = "utm_name",
                UtmSource = "utm_source",
                UtmTerm = "utm_term",
                Wbraid = "wbraid",
                Zip = "zip",
            },
        };

        Assert.Null(parameters.DefaultProperties);
        Assert.False(parameters.RawBodyData.ContainsKey("defaultProperties"));
        Assert.Null(parameters.Email);
        Assert.False(parameters.RawBodyData.ContainsKey("email"));
        Assert.Null(parameters.ExternalID);
        Assert.False(parameters.RawBodyData.ContainsKey("externalId"));
        Assert.Null(parameters.UserID);
        Assert.False(parameters.RawBodyData.ContainsKey("userId"));
    }

    [Fact]
    public void OptionalNullableParamsSetToNullAreSetToNull_Works()
    {
        var parameters = new VisitorUpsertParams
        {
            Token = "x",
            UserProperties = new()
            {
                AdID = "ad_id",
                AdsetID = "adset_id",
                Alart = "alart",
                Aleid = "aleid",
                BasisCid = "basis_cid",
                CampaignID = "campaign_id",
                City = "city",
                Clickid = "clickid",
                Clid = "clid",
                CompanyName = "company_name",
                Consent = new Dictionary<string, string?>() { { "foo", "string" } },
                Country = "country",
                CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
                DateOfBirth = "date_of_birth",
                Dclid = "dclid",
                Email = "email",
                Epik = "epik",
                ExternalID = "external_id",
                Fbc = "fbc",
                Fbclid = "fbclid",
                Fbp = "fbp",
                FirstName = "first_name",
                GadSource = "gad_source",
                Gbraid = "gbraid",
                Gclid = "gclid",
                Gender = "gender",
                ImRef = "im_ref",
                IP = "ip",
                Irclickid = "irclickid",
                IsBot = "is_bot",
                JobTitle = "job_title",
                LastName = "last_name",
                LiFatID = "li_fat_id",
                Msclkid = "msclkid",
                Ndclid = "ndclid",
                PhoneNumber = "phone_number",
                Qclid = "qclid",
                RdtCid = "rdt_cid",
                Referrer = "referrer",
                ReferringDomain = "referring_domain",
                Sacid = "sacid",
                Sccid = "sccid",
                Sid = "sid",
                State = "state",
                Ttclid = "ttclid",
                Twclid = "twclid",
                UserAgent = "user_agent",
                UserAgentFullList = "user_agent_full_list",
                UtmCampaign = "utm_campaign",
                UtmContent = "utm_content",
                UtmMedium = "utm_medium",
                UtmName = "utm_name",
                UtmSource = "utm_source",
                UtmTerm = "utm_term",
                Wbraid = "wbraid",
                Zip = "zip",
            },

            DefaultProperties = null,
            Email = null,
            ExternalID = null,
            UserID = null,
        };

        Assert.Null(parameters.DefaultProperties);
        Assert.True(parameters.RawBodyData.ContainsKey("defaultProperties"));
        Assert.Null(parameters.Email);
        Assert.True(parameters.RawBodyData.ContainsKey("email"));
        Assert.Null(parameters.ExternalID);
        Assert.True(parameters.RawBodyData.ContainsKey("externalId"));
        Assert.Null(parameters.UserID);
        Assert.True(parameters.RawBodyData.ContainsKey("userId"));
    }

    [Fact]
    public void Url_Works()
    {
        VisitorUpsertParams parameters = new()
        {
            Token = "x",
            UserProperties = new()
            {
                AdID = "ad_id",
                AdsetID = "adset_id",
                Alart = "alart",
                Aleid = "aleid",
                BasisCid = "basis_cid",
                CampaignID = "campaign_id",
                City = "city",
                Clickid = "clickid",
                Clid = "clid",
                CompanyName = "company_name",
                Consent = new Dictionary<string, string?>() { { "foo", "string" } },
                Country = "country",
                CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
                DateOfBirth = "date_of_birth",
                Dclid = "dclid",
                Email = "email",
                Epik = "epik",
                ExternalID = "external_id",
                Fbc = "fbc",
                Fbclid = "fbclid",
                Fbp = "fbp",
                FirstName = "first_name",
                GadSource = "gad_source",
                Gbraid = "gbraid",
                Gclid = "gclid",
                Gender = "gender",
                ImRef = "im_ref",
                IP = "ip",
                Irclickid = "irclickid",
                IsBot = "is_bot",
                JobTitle = "job_title",
                LastName = "last_name",
                LiFatID = "li_fat_id",
                Msclkid = "msclkid",
                Ndclid = "ndclid",
                PhoneNumber = "phone_number",
                Qclid = "qclid",
                RdtCid = "rdt_cid",
                Referrer = "referrer",
                ReferringDomain = "referring_domain",
                Sacid = "sacid",
                Sccid = "sccid",
                Sid = "sid",
                State = "state",
                Ttclid = "ttclid",
                Twclid = "twclid",
                UserAgent = "user_agent",
                UserAgentFullList = "user_agent_full_list",
                UtmCampaign = "utm_campaign",
                UtmContent = "utm_content",
                UtmMedium = "utm_medium",
                UtmName = "utm_name",
                UtmSource = "utm_source",
                UtmTerm = "utm_term",
                Wbraid = "wbraid",
                Zip = "zip",
            },
        };

        var url = parameters.Url(new() { });

        Assert.Equal(new Uri("https://api.oursprivacy.com/api/v1/identify"), url);
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new VisitorUpsertParams
        {
            Token = "x",
            UserProperties = new()
            {
                AdID = "ad_id",
                AdsetID = "adset_id",
                Alart = "alart",
                Aleid = "aleid",
                BasisCid = "basis_cid",
                CampaignID = "campaign_id",
                City = "city",
                Clickid = "clickid",
                Clid = "clid",
                CompanyName = "company_name",
                Consent = new Dictionary<string, string?>() { { "foo", "string" } },
                Country = "country",
                CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
                DateOfBirth = "date_of_birth",
                Dclid = "dclid",
                Email = "email",
                Epik = "epik",
                ExternalID = "external_id",
                Fbc = "fbc",
                Fbclid = "fbclid",
                Fbp = "fbp",
                FirstName = "first_name",
                GadSource = "gad_source",
                Gbraid = "gbraid",
                Gclid = "gclid",
                Gender = "gender",
                ImRef = "im_ref",
                IP = "ip",
                Irclickid = "irclickid",
                IsBot = "is_bot",
                JobTitle = "job_title",
                LastName = "last_name",
                LiFatID = "li_fat_id",
                Msclkid = "msclkid",
                Ndclid = "ndclid",
                PhoneNumber = "phone_number",
                Qclid = "qclid",
                RdtCid = "rdt_cid",
                Referrer = "referrer",
                ReferringDomain = "referring_domain",
                Sacid = "sacid",
                Sccid = "sccid",
                Sid = "sid",
                State = "state",
                Ttclid = "ttclid",
                Twclid = "twclid",
                UserAgent = "user_agent",
                UserAgentFullList = "user_agent_full_list",
                UtmCampaign = "utm_campaign",
                UtmContent = "utm_content",
                UtmMedium = "utm_medium",
                UtmName = "utm_name",
                UtmSource = "utm_source",
                UtmTerm = "utm_term",
                Wbraid = "wbraid",
                Zip = "zip",
            },
            DefaultProperties = new()
            {
                ActiveDuration = 0,
                AdID = "ad_id",
                AdsetID = "adset_id",
                Alart = "alart",
                Aleid = "aleid",
                BasisCid = "basis_cid",
                BrowserLanguage = "browser_language",
                BrowserName = "browser_name",
                BrowserVersion = "browser_version",
                CampaignID = "campaign_id",
                Clickid = "clickid",
                Clid = "clid",
                CpuArchitecture = "cpu_architecture",
                CurrentUrl = "current_url",
                Dclid = "dclid",
                DeviceModel = "device_model",
                DeviceType = "device_type",
                DeviceVendor = "device_vendor",
                Duration = 0,
                Encoding = "encoding",
                EngineName = "engine_name",
                EngineVersion = "engine_version",
                Epik = "epik",
                Fbc = "fbc",
                Fbclid = "fbclid",
                Fbp = "fbp",
                Fv = true,
                GadSource = "gad_source",
                Gbraid = "gbraid",
                Gclid = "gclid",
                Host = "host",
                Iframe = true,
                ImRef = "im_ref",
                IP = "ip",
                Irclickid = "irclickid",
                IsBot = "is_bot",
                LiFatID = "li_fat_id",
                Msclkid = "msclkid",
                Ndclid = "ndclid",
                NewS = true,
                OsName = "os_name",
                OsVersion = "os_version",
                PageHash = 0,
                Pathname = "pathname",
                Qclid = "qclid",
                RdtCid = "rdt_cid",
                ReceivedAt = "received_at",
                Referrer = "referrer",
                ReferringDomain = "referring_domain",
                Sacid = "sacid",
                Sccid = "sccid",
                ScreenHeight = 0,
                ScreenWidth = 0,
                SessionCount = 0,
                Sid = "sid",
                Sr = "sr",
                Title = "title",
                Ttclid = "ttclid",
                Twclid = "twclid",
                Uafvl = "uafvl",
                UserAgent = "user_agent",
                UtmCampaign = "utm_campaign",
                UtmContent = "utm_content",
                UtmMedium = "utm_medium",
                UtmName = "utm_name",
                UtmSource = "utm_source",
                UtmTerm = "utm_term",
                Version = "version",
                Wbraid = "wbraid",
                Webview = true,
            },
            Email = "x",
            ExternalID = "x",
            UserID = "x",
        };

        VisitorUpsertParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}

public class UserPropertiesTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new UserProperties
        {
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            CampaignID = "campaign_id",
            City = "city",
            Clickid = "clickid",
            Clid = "clid",
            CompanyName = "company_name",
            Consent = new Dictionary<string, string?>() { { "foo", "string" } },
            Country = "country",
            CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
            DateOfBirth = "date_of_birth",
            Dclid = "dclid",
            Email = "email",
            Epik = "epik",
            ExternalID = "external_id",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            FirstName = "first_name",
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Gender = "gender",
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            JobTitle = "job_title",
            LastName = "last_name",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            PhoneNumber = "phone_number",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            Sid = "sid",
            State = "state",
            Ttclid = "ttclid",
            Twclid = "twclid",
            UserAgent = "user_agent",
            UserAgentFullList = "user_agent_full_list",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Wbraid = "wbraid",
            Zip = "zip",
        };

        string expectedAdID = "ad_id";
        string expectedAdsetID = "adset_id";
        string expectedAlart = "alart";
        string expectedAleid = "aleid";
        string expectedBasisCid = "basis_cid";
        string expectedCampaignID = "campaign_id";
        string expectedCity = "city";
        string expectedClickid = "clickid";
        string expectedClid = "clid";
        string expectedCompanyName = "company_name";
        Dictionary<string, string?> expectedConsent = new() { { "foo", "string" } };
        string expectedCountry = "country";
        Dictionary<string, string?> expectedCustomProperties = new() { { "foo", "string" } };
        string expectedDateOfBirth = "date_of_birth";
        string expectedDclid = "dclid";
        string expectedEmail = "email";
        string expectedEpik = "epik";
        string expectedExternalID = "external_id";
        string expectedFbc = "fbc";
        string expectedFbclid = "fbclid";
        string expectedFbp = "fbp";
        string expectedFirstName = "first_name";
        string expectedGadSource = "gad_source";
        string expectedGbraid = "gbraid";
        string expectedGclid = "gclid";
        string expectedGender = "gender";
        string expectedImRef = "im_ref";
        string expectedIP = "ip";
        string expectedIrclickid = "irclickid";
        string expectedIsBot = "is_bot";
        string expectedJobTitle = "job_title";
        string expectedLastName = "last_name";
        string expectedLiFatID = "li_fat_id";
        string expectedMsclkid = "msclkid";
        string expectedNdclid = "ndclid";
        string expectedPhoneNumber = "phone_number";
        string expectedQclid = "qclid";
        string expectedRdtCid = "rdt_cid";
        string expectedReferrer = "referrer";
        string expectedReferringDomain = "referring_domain";
        string expectedSacid = "sacid";
        string expectedSccid = "sccid";
        string expectedSid = "sid";
        string expectedState = "state";
        string expectedTtclid = "ttclid";
        string expectedTwclid = "twclid";
        string expectedUserAgent = "user_agent";
        string expectedUserAgentFullList = "user_agent_full_list";
        string expectedUtmCampaign = "utm_campaign";
        string expectedUtmContent = "utm_content";
        string expectedUtmMedium = "utm_medium";
        string expectedUtmName = "utm_name";
        string expectedUtmSource = "utm_source";
        string expectedUtmTerm = "utm_term";
        string expectedWbraid = "wbraid";
        string expectedZip = "zip";

        Assert.Equal(expectedAdID, model.AdID);
        Assert.Equal(expectedAdsetID, model.AdsetID);
        Assert.Equal(expectedAlart, model.Alart);
        Assert.Equal(expectedAleid, model.Aleid);
        Assert.Equal(expectedBasisCid, model.BasisCid);
        Assert.Equal(expectedCampaignID, model.CampaignID);
        Assert.Equal(expectedCity, model.City);
        Assert.Equal(expectedClickid, model.Clickid);
        Assert.Equal(expectedClid, model.Clid);
        Assert.Equal(expectedCompanyName, model.CompanyName);
        Assert.NotNull(model.Consent);
        Assert.Equal(expectedConsent.Count, model.Consent.Count);
        foreach (var item in expectedConsent)
        {
            Assert.True(model.Consent.TryGetValue(item.Key, out var value));

            Assert.Equal(value, model.Consent[item.Key]);
        }
        Assert.Equal(expectedCountry, model.Country);
        Assert.NotNull(model.CustomProperties);
        Assert.Equal(expectedCustomProperties.Count, model.CustomProperties.Count);
        foreach (var item in expectedCustomProperties)
        {
            Assert.True(model.CustomProperties.TryGetValue(item.Key, out var value));

            Assert.Equal(value, model.CustomProperties[item.Key]);
        }
        Assert.Equal(expectedDateOfBirth, model.DateOfBirth);
        Assert.Equal(expectedDclid, model.Dclid);
        Assert.Equal(expectedEmail, model.Email);
        Assert.Equal(expectedEpik, model.Epik);
        Assert.Equal(expectedExternalID, model.ExternalID);
        Assert.Equal(expectedFbc, model.Fbc);
        Assert.Equal(expectedFbclid, model.Fbclid);
        Assert.Equal(expectedFbp, model.Fbp);
        Assert.Equal(expectedFirstName, model.FirstName);
        Assert.Equal(expectedGadSource, model.GadSource);
        Assert.Equal(expectedGbraid, model.Gbraid);
        Assert.Equal(expectedGclid, model.Gclid);
        Assert.Equal(expectedGender, model.Gender);
        Assert.Equal(expectedImRef, model.ImRef);
        Assert.Equal(expectedIP, model.IP);
        Assert.Equal(expectedIrclickid, model.Irclickid);
        Assert.Equal(expectedIsBot, model.IsBot);
        Assert.Equal(expectedJobTitle, model.JobTitle);
        Assert.Equal(expectedLastName, model.LastName);
        Assert.Equal(expectedLiFatID, model.LiFatID);
        Assert.Equal(expectedMsclkid, model.Msclkid);
        Assert.Equal(expectedNdclid, model.Ndclid);
        Assert.Equal(expectedPhoneNumber, model.PhoneNumber);
        Assert.Equal(expectedQclid, model.Qclid);
        Assert.Equal(expectedRdtCid, model.RdtCid);
        Assert.Equal(expectedReferrer, model.Referrer);
        Assert.Equal(expectedReferringDomain, model.ReferringDomain);
        Assert.Equal(expectedSacid, model.Sacid);
        Assert.Equal(expectedSccid, model.Sccid);
        Assert.Equal(expectedSid, model.Sid);
        Assert.Equal(expectedState, model.State);
        Assert.Equal(expectedTtclid, model.Ttclid);
        Assert.Equal(expectedTwclid, model.Twclid);
        Assert.Equal(expectedUserAgent, model.UserAgent);
        Assert.Equal(expectedUserAgentFullList, model.UserAgentFullList);
        Assert.Equal(expectedUtmCampaign, model.UtmCampaign);
        Assert.Equal(expectedUtmContent, model.UtmContent);
        Assert.Equal(expectedUtmMedium, model.UtmMedium);
        Assert.Equal(expectedUtmName, model.UtmName);
        Assert.Equal(expectedUtmSource, model.UtmSource);
        Assert.Equal(expectedUtmTerm, model.UtmTerm);
        Assert.Equal(expectedWbraid, model.Wbraid);
        Assert.Equal(expectedZip, model.Zip);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new UserProperties
        {
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            CampaignID = "campaign_id",
            City = "city",
            Clickid = "clickid",
            Clid = "clid",
            CompanyName = "company_name",
            Consent = new Dictionary<string, string?>() { { "foo", "string" } },
            Country = "country",
            CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
            DateOfBirth = "date_of_birth",
            Dclid = "dclid",
            Email = "email",
            Epik = "epik",
            ExternalID = "external_id",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            FirstName = "first_name",
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Gender = "gender",
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            JobTitle = "job_title",
            LastName = "last_name",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            PhoneNumber = "phone_number",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            Sid = "sid",
            State = "state",
            Ttclid = "ttclid",
            Twclid = "twclid",
            UserAgent = "user_agent",
            UserAgentFullList = "user_agent_full_list",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Wbraid = "wbraid",
            Zip = "zip",
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UserProperties>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new UserProperties
        {
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            CampaignID = "campaign_id",
            City = "city",
            Clickid = "clickid",
            Clid = "clid",
            CompanyName = "company_name",
            Consent = new Dictionary<string, string?>() { { "foo", "string" } },
            Country = "country",
            CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
            DateOfBirth = "date_of_birth",
            Dclid = "dclid",
            Email = "email",
            Epik = "epik",
            ExternalID = "external_id",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            FirstName = "first_name",
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Gender = "gender",
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            JobTitle = "job_title",
            LastName = "last_name",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            PhoneNumber = "phone_number",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            Sid = "sid",
            State = "state",
            Ttclid = "ttclid",
            Twclid = "twclid",
            UserAgent = "user_agent",
            UserAgentFullList = "user_agent_full_list",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Wbraid = "wbraid",
            Zip = "zip",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<UserProperties>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedAdID = "ad_id";
        string expectedAdsetID = "adset_id";
        string expectedAlart = "alart";
        string expectedAleid = "aleid";
        string expectedBasisCid = "basis_cid";
        string expectedCampaignID = "campaign_id";
        string expectedCity = "city";
        string expectedClickid = "clickid";
        string expectedClid = "clid";
        string expectedCompanyName = "company_name";
        Dictionary<string, string?> expectedConsent = new() { { "foo", "string" } };
        string expectedCountry = "country";
        Dictionary<string, string?> expectedCustomProperties = new() { { "foo", "string" } };
        string expectedDateOfBirth = "date_of_birth";
        string expectedDclid = "dclid";
        string expectedEmail = "email";
        string expectedEpik = "epik";
        string expectedExternalID = "external_id";
        string expectedFbc = "fbc";
        string expectedFbclid = "fbclid";
        string expectedFbp = "fbp";
        string expectedFirstName = "first_name";
        string expectedGadSource = "gad_source";
        string expectedGbraid = "gbraid";
        string expectedGclid = "gclid";
        string expectedGender = "gender";
        string expectedImRef = "im_ref";
        string expectedIP = "ip";
        string expectedIrclickid = "irclickid";
        string expectedIsBot = "is_bot";
        string expectedJobTitle = "job_title";
        string expectedLastName = "last_name";
        string expectedLiFatID = "li_fat_id";
        string expectedMsclkid = "msclkid";
        string expectedNdclid = "ndclid";
        string expectedPhoneNumber = "phone_number";
        string expectedQclid = "qclid";
        string expectedRdtCid = "rdt_cid";
        string expectedReferrer = "referrer";
        string expectedReferringDomain = "referring_domain";
        string expectedSacid = "sacid";
        string expectedSccid = "sccid";
        string expectedSid = "sid";
        string expectedState = "state";
        string expectedTtclid = "ttclid";
        string expectedTwclid = "twclid";
        string expectedUserAgent = "user_agent";
        string expectedUserAgentFullList = "user_agent_full_list";
        string expectedUtmCampaign = "utm_campaign";
        string expectedUtmContent = "utm_content";
        string expectedUtmMedium = "utm_medium";
        string expectedUtmName = "utm_name";
        string expectedUtmSource = "utm_source";
        string expectedUtmTerm = "utm_term";
        string expectedWbraid = "wbraid";
        string expectedZip = "zip";

        Assert.Equal(expectedAdID, deserialized.AdID);
        Assert.Equal(expectedAdsetID, deserialized.AdsetID);
        Assert.Equal(expectedAlart, deserialized.Alart);
        Assert.Equal(expectedAleid, deserialized.Aleid);
        Assert.Equal(expectedBasisCid, deserialized.BasisCid);
        Assert.Equal(expectedCampaignID, deserialized.CampaignID);
        Assert.Equal(expectedCity, deserialized.City);
        Assert.Equal(expectedClickid, deserialized.Clickid);
        Assert.Equal(expectedClid, deserialized.Clid);
        Assert.Equal(expectedCompanyName, deserialized.CompanyName);
        Assert.NotNull(deserialized.Consent);
        Assert.Equal(expectedConsent.Count, deserialized.Consent.Count);
        foreach (var item in expectedConsent)
        {
            Assert.True(deserialized.Consent.TryGetValue(item.Key, out var value));

            Assert.Equal(value, deserialized.Consent[item.Key]);
        }
        Assert.Equal(expectedCountry, deserialized.Country);
        Assert.NotNull(deserialized.CustomProperties);
        Assert.Equal(expectedCustomProperties.Count, deserialized.CustomProperties.Count);
        foreach (var item in expectedCustomProperties)
        {
            Assert.True(deserialized.CustomProperties.TryGetValue(item.Key, out var value));

            Assert.Equal(value, deserialized.CustomProperties[item.Key]);
        }
        Assert.Equal(expectedDateOfBirth, deserialized.DateOfBirth);
        Assert.Equal(expectedDclid, deserialized.Dclid);
        Assert.Equal(expectedEmail, deserialized.Email);
        Assert.Equal(expectedEpik, deserialized.Epik);
        Assert.Equal(expectedExternalID, deserialized.ExternalID);
        Assert.Equal(expectedFbc, deserialized.Fbc);
        Assert.Equal(expectedFbclid, deserialized.Fbclid);
        Assert.Equal(expectedFbp, deserialized.Fbp);
        Assert.Equal(expectedFirstName, deserialized.FirstName);
        Assert.Equal(expectedGadSource, deserialized.GadSource);
        Assert.Equal(expectedGbraid, deserialized.Gbraid);
        Assert.Equal(expectedGclid, deserialized.Gclid);
        Assert.Equal(expectedGender, deserialized.Gender);
        Assert.Equal(expectedImRef, deserialized.ImRef);
        Assert.Equal(expectedIP, deserialized.IP);
        Assert.Equal(expectedIrclickid, deserialized.Irclickid);
        Assert.Equal(expectedIsBot, deserialized.IsBot);
        Assert.Equal(expectedJobTitle, deserialized.JobTitle);
        Assert.Equal(expectedLastName, deserialized.LastName);
        Assert.Equal(expectedLiFatID, deserialized.LiFatID);
        Assert.Equal(expectedMsclkid, deserialized.Msclkid);
        Assert.Equal(expectedNdclid, deserialized.Ndclid);
        Assert.Equal(expectedPhoneNumber, deserialized.PhoneNumber);
        Assert.Equal(expectedQclid, deserialized.Qclid);
        Assert.Equal(expectedRdtCid, deserialized.RdtCid);
        Assert.Equal(expectedReferrer, deserialized.Referrer);
        Assert.Equal(expectedReferringDomain, deserialized.ReferringDomain);
        Assert.Equal(expectedSacid, deserialized.Sacid);
        Assert.Equal(expectedSccid, deserialized.Sccid);
        Assert.Equal(expectedSid, deserialized.Sid);
        Assert.Equal(expectedState, deserialized.State);
        Assert.Equal(expectedTtclid, deserialized.Ttclid);
        Assert.Equal(expectedTwclid, deserialized.Twclid);
        Assert.Equal(expectedUserAgent, deserialized.UserAgent);
        Assert.Equal(expectedUserAgentFullList, deserialized.UserAgentFullList);
        Assert.Equal(expectedUtmCampaign, deserialized.UtmCampaign);
        Assert.Equal(expectedUtmContent, deserialized.UtmContent);
        Assert.Equal(expectedUtmMedium, deserialized.UtmMedium);
        Assert.Equal(expectedUtmName, deserialized.UtmName);
        Assert.Equal(expectedUtmSource, deserialized.UtmSource);
        Assert.Equal(expectedUtmTerm, deserialized.UtmTerm);
        Assert.Equal(expectedWbraid, deserialized.Wbraid);
        Assert.Equal(expectedZip, deserialized.Zip);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new UserProperties
        {
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            CampaignID = "campaign_id",
            City = "city",
            Clickid = "clickid",
            Clid = "clid",
            CompanyName = "company_name",
            Consent = new Dictionary<string, string?>() { { "foo", "string" } },
            Country = "country",
            CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
            DateOfBirth = "date_of_birth",
            Dclid = "dclid",
            Email = "email",
            Epik = "epik",
            ExternalID = "external_id",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            FirstName = "first_name",
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Gender = "gender",
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            JobTitle = "job_title",
            LastName = "last_name",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            PhoneNumber = "phone_number",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            Sid = "sid",
            State = "state",
            Ttclid = "ttclid",
            Twclid = "twclid",
            UserAgent = "user_agent",
            UserAgentFullList = "user_agent_full_list",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Wbraid = "wbraid",
            Zip = "zip",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new UserProperties { };

        Assert.Null(model.AdID);
        Assert.False(model.RawData.ContainsKey("ad_id"));
        Assert.Null(model.AdsetID);
        Assert.False(model.RawData.ContainsKey("adset_id"));
        Assert.Null(model.Alart);
        Assert.False(model.RawData.ContainsKey("alart"));
        Assert.Null(model.Aleid);
        Assert.False(model.RawData.ContainsKey("aleid"));
        Assert.Null(model.BasisCid);
        Assert.False(model.RawData.ContainsKey("basis_cid"));
        Assert.Null(model.CampaignID);
        Assert.False(model.RawData.ContainsKey("campaign_id"));
        Assert.Null(model.City);
        Assert.False(model.RawData.ContainsKey("city"));
        Assert.Null(model.Clickid);
        Assert.False(model.RawData.ContainsKey("clickid"));
        Assert.Null(model.Clid);
        Assert.False(model.RawData.ContainsKey("clid"));
        Assert.Null(model.CompanyName);
        Assert.False(model.RawData.ContainsKey("company_name"));
        Assert.Null(model.Consent);
        Assert.False(model.RawData.ContainsKey("consent"));
        Assert.Null(model.Country);
        Assert.False(model.RawData.ContainsKey("country"));
        Assert.Null(model.CustomProperties);
        Assert.False(model.RawData.ContainsKey("custom_properties"));
        Assert.Null(model.DateOfBirth);
        Assert.False(model.RawData.ContainsKey("date_of_birth"));
        Assert.Null(model.Dclid);
        Assert.False(model.RawData.ContainsKey("dclid"));
        Assert.Null(model.Email);
        Assert.False(model.RawData.ContainsKey("email"));
        Assert.Null(model.Epik);
        Assert.False(model.RawData.ContainsKey("epik"));
        Assert.Null(model.ExternalID);
        Assert.False(model.RawData.ContainsKey("external_id"));
        Assert.Null(model.Fbc);
        Assert.False(model.RawData.ContainsKey("fbc"));
        Assert.Null(model.Fbclid);
        Assert.False(model.RawData.ContainsKey("fbclid"));
        Assert.Null(model.Fbp);
        Assert.False(model.RawData.ContainsKey("fbp"));
        Assert.Null(model.FirstName);
        Assert.False(model.RawData.ContainsKey("first_name"));
        Assert.Null(model.GadSource);
        Assert.False(model.RawData.ContainsKey("gad_source"));
        Assert.Null(model.Gbraid);
        Assert.False(model.RawData.ContainsKey("gbraid"));
        Assert.Null(model.Gclid);
        Assert.False(model.RawData.ContainsKey("gclid"));
        Assert.Null(model.Gender);
        Assert.False(model.RawData.ContainsKey("gender"));
        Assert.Null(model.ImRef);
        Assert.False(model.RawData.ContainsKey("im_ref"));
        Assert.Null(model.IP);
        Assert.False(model.RawData.ContainsKey("ip"));
        Assert.Null(model.Irclickid);
        Assert.False(model.RawData.ContainsKey("irclickid"));
        Assert.Null(model.IsBot);
        Assert.False(model.RawData.ContainsKey("is_bot"));
        Assert.Null(model.JobTitle);
        Assert.False(model.RawData.ContainsKey("job_title"));
        Assert.Null(model.LastName);
        Assert.False(model.RawData.ContainsKey("last_name"));
        Assert.Null(model.LiFatID);
        Assert.False(model.RawData.ContainsKey("li_fat_id"));
        Assert.Null(model.Msclkid);
        Assert.False(model.RawData.ContainsKey("msclkid"));
        Assert.Null(model.Ndclid);
        Assert.False(model.RawData.ContainsKey("ndclid"));
        Assert.Null(model.PhoneNumber);
        Assert.False(model.RawData.ContainsKey("phone_number"));
        Assert.Null(model.Qclid);
        Assert.False(model.RawData.ContainsKey("qclid"));
        Assert.Null(model.RdtCid);
        Assert.False(model.RawData.ContainsKey("rdt_cid"));
        Assert.Null(model.Referrer);
        Assert.False(model.RawData.ContainsKey("referrer"));
        Assert.Null(model.ReferringDomain);
        Assert.False(model.RawData.ContainsKey("referring_domain"));
        Assert.Null(model.Sacid);
        Assert.False(model.RawData.ContainsKey("sacid"));
        Assert.Null(model.Sccid);
        Assert.False(model.RawData.ContainsKey("sccid"));
        Assert.Null(model.Sid);
        Assert.False(model.RawData.ContainsKey("sid"));
        Assert.Null(model.State);
        Assert.False(model.RawData.ContainsKey("state"));
        Assert.Null(model.Ttclid);
        Assert.False(model.RawData.ContainsKey("ttclid"));
        Assert.Null(model.Twclid);
        Assert.False(model.RawData.ContainsKey("twclid"));
        Assert.Null(model.UserAgent);
        Assert.False(model.RawData.ContainsKey("user_agent"));
        Assert.Null(model.UserAgentFullList);
        Assert.False(model.RawData.ContainsKey("user_agent_full_list"));
        Assert.Null(model.UtmCampaign);
        Assert.False(model.RawData.ContainsKey("utm_campaign"));
        Assert.Null(model.UtmContent);
        Assert.False(model.RawData.ContainsKey("utm_content"));
        Assert.Null(model.UtmMedium);
        Assert.False(model.RawData.ContainsKey("utm_medium"));
        Assert.Null(model.UtmName);
        Assert.False(model.RawData.ContainsKey("utm_name"));
        Assert.Null(model.UtmSource);
        Assert.False(model.RawData.ContainsKey("utm_source"));
        Assert.Null(model.UtmTerm);
        Assert.False(model.RawData.ContainsKey("utm_term"));
        Assert.Null(model.Wbraid);
        Assert.False(model.RawData.ContainsKey("wbraid"));
        Assert.Null(model.Zip);
        Assert.False(model.RawData.ContainsKey("zip"));
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetValidation_Works()
    {
        var model = new UserProperties { };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullAreSetToNull_Works()
    {
        var model = new UserProperties
        {
            AdID = null,
            AdsetID = null,
            Alart = null,
            Aleid = null,
            BasisCid = null,
            CampaignID = null,
            City = null,
            Clickid = null,
            Clid = null,
            CompanyName = null,
            Consent = null,
            Country = null,
            CustomProperties = null,
            DateOfBirth = null,
            Dclid = null,
            Email = null,
            Epik = null,
            ExternalID = null,
            Fbc = null,
            Fbclid = null,
            Fbp = null,
            FirstName = null,
            GadSource = null,
            Gbraid = null,
            Gclid = null,
            Gender = null,
            ImRef = null,
            IP = null,
            Irclickid = null,
            IsBot = null,
            JobTitle = null,
            LastName = null,
            LiFatID = null,
            Msclkid = null,
            Ndclid = null,
            PhoneNumber = null,
            Qclid = null,
            RdtCid = null,
            Referrer = null,
            ReferringDomain = null,
            Sacid = null,
            Sccid = null,
            Sid = null,
            State = null,
            Ttclid = null,
            Twclid = null,
            UserAgent = null,
            UserAgentFullList = null,
            UtmCampaign = null,
            UtmContent = null,
            UtmMedium = null,
            UtmName = null,
            UtmSource = null,
            UtmTerm = null,
            Wbraid = null,
            Zip = null,
        };

        Assert.Null(model.AdID);
        Assert.True(model.RawData.ContainsKey("ad_id"));
        Assert.Null(model.AdsetID);
        Assert.True(model.RawData.ContainsKey("adset_id"));
        Assert.Null(model.Alart);
        Assert.True(model.RawData.ContainsKey("alart"));
        Assert.Null(model.Aleid);
        Assert.True(model.RawData.ContainsKey("aleid"));
        Assert.Null(model.BasisCid);
        Assert.True(model.RawData.ContainsKey("basis_cid"));
        Assert.Null(model.CampaignID);
        Assert.True(model.RawData.ContainsKey("campaign_id"));
        Assert.Null(model.City);
        Assert.True(model.RawData.ContainsKey("city"));
        Assert.Null(model.Clickid);
        Assert.True(model.RawData.ContainsKey("clickid"));
        Assert.Null(model.Clid);
        Assert.True(model.RawData.ContainsKey("clid"));
        Assert.Null(model.CompanyName);
        Assert.True(model.RawData.ContainsKey("company_name"));
        Assert.Null(model.Consent);
        Assert.True(model.RawData.ContainsKey("consent"));
        Assert.Null(model.Country);
        Assert.True(model.RawData.ContainsKey("country"));
        Assert.Null(model.CustomProperties);
        Assert.True(model.RawData.ContainsKey("custom_properties"));
        Assert.Null(model.DateOfBirth);
        Assert.True(model.RawData.ContainsKey("date_of_birth"));
        Assert.Null(model.Dclid);
        Assert.True(model.RawData.ContainsKey("dclid"));
        Assert.Null(model.Email);
        Assert.True(model.RawData.ContainsKey("email"));
        Assert.Null(model.Epik);
        Assert.True(model.RawData.ContainsKey("epik"));
        Assert.Null(model.ExternalID);
        Assert.True(model.RawData.ContainsKey("external_id"));
        Assert.Null(model.Fbc);
        Assert.True(model.RawData.ContainsKey("fbc"));
        Assert.Null(model.Fbclid);
        Assert.True(model.RawData.ContainsKey("fbclid"));
        Assert.Null(model.Fbp);
        Assert.True(model.RawData.ContainsKey("fbp"));
        Assert.Null(model.FirstName);
        Assert.True(model.RawData.ContainsKey("first_name"));
        Assert.Null(model.GadSource);
        Assert.True(model.RawData.ContainsKey("gad_source"));
        Assert.Null(model.Gbraid);
        Assert.True(model.RawData.ContainsKey("gbraid"));
        Assert.Null(model.Gclid);
        Assert.True(model.RawData.ContainsKey("gclid"));
        Assert.Null(model.Gender);
        Assert.True(model.RawData.ContainsKey("gender"));
        Assert.Null(model.ImRef);
        Assert.True(model.RawData.ContainsKey("im_ref"));
        Assert.Null(model.IP);
        Assert.True(model.RawData.ContainsKey("ip"));
        Assert.Null(model.Irclickid);
        Assert.True(model.RawData.ContainsKey("irclickid"));
        Assert.Null(model.IsBot);
        Assert.True(model.RawData.ContainsKey("is_bot"));
        Assert.Null(model.JobTitle);
        Assert.True(model.RawData.ContainsKey("job_title"));
        Assert.Null(model.LastName);
        Assert.True(model.RawData.ContainsKey("last_name"));
        Assert.Null(model.LiFatID);
        Assert.True(model.RawData.ContainsKey("li_fat_id"));
        Assert.Null(model.Msclkid);
        Assert.True(model.RawData.ContainsKey("msclkid"));
        Assert.Null(model.Ndclid);
        Assert.True(model.RawData.ContainsKey("ndclid"));
        Assert.Null(model.PhoneNumber);
        Assert.True(model.RawData.ContainsKey("phone_number"));
        Assert.Null(model.Qclid);
        Assert.True(model.RawData.ContainsKey("qclid"));
        Assert.Null(model.RdtCid);
        Assert.True(model.RawData.ContainsKey("rdt_cid"));
        Assert.Null(model.Referrer);
        Assert.True(model.RawData.ContainsKey("referrer"));
        Assert.Null(model.ReferringDomain);
        Assert.True(model.RawData.ContainsKey("referring_domain"));
        Assert.Null(model.Sacid);
        Assert.True(model.RawData.ContainsKey("sacid"));
        Assert.Null(model.Sccid);
        Assert.True(model.RawData.ContainsKey("sccid"));
        Assert.Null(model.Sid);
        Assert.True(model.RawData.ContainsKey("sid"));
        Assert.Null(model.State);
        Assert.True(model.RawData.ContainsKey("state"));
        Assert.Null(model.Ttclid);
        Assert.True(model.RawData.ContainsKey("ttclid"));
        Assert.Null(model.Twclid);
        Assert.True(model.RawData.ContainsKey("twclid"));
        Assert.Null(model.UserAgent);
        Assert.True(model.RawData.ContainsKey("user_agent"));
        Assert.Null(model.UserAgentFullList);
        Assert.True(model.RawData.ContainsKey("user_agent_full_list"));
        Assert.Null(model.UtmCampaign);
        Assert.True(model.RawData.ContainsKey("utm_campaign"));
        Assert.Null(model.UtmContent);
        Assert.True(model.RawData.ContainsKey("utm_content"));
        Assert.Null(model.UtmMedium);
        Assert.True(model.RawData.ContainsKey("utm_medium"));
        Assert.Null(model.UtmName);
        Assert.True(model.RawData.ContainsKey("utm_name"));
        Assert.Null(model.UtmSource);
        Assert.True(model.RawData.ContainsKey("utm_source"));
        Assert.Null(model.UtmTerm);
        Assert.True(model.RawData.ContainsKey("utm_term"));
        Assert.Null(model.Wbraid);
        Assert.True(model.RawData.ContainsKey("wbraid"));
        Assert.Null(model.Zip);
        Assert.True(model.RawData.ContainsKey("zip"));
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullValidation_Works()
    {
        var model = new UserProperties
        {
            AdID = null,
            AdsetID = null,
            Alart = null,
            Aleid = null,
            BasisCid = null,
            CampaignID = null,
            City = null,
            Clickid = null,
            Clid = null,
            CompanyName = null,
            Consent = null,
            Country = null,
            CustomProperties = null,
            DateOfBirth = null,
            Dclid = null,
            Email = null,
            Epik = null,
            ExternalID = null,
            Fbc = null,
            Fbclid = null,
            Fbp = null,
            FirstName = null,
            GadSource = null,
            Gbraid = null,
            Gclid = null,
            Gender = null,
            ImRef = null,
            IP = null,
            Irclickid = null,
            IsBot = null,
            JobTitle = null,
            LastName = null,
            LiFatID = null,
            Msclkid = null,
            Ndclid = null,
            PhoneNumber = null,
            Qclid = null,
            RdtCid = null,
            Referrer = null,
            ReferringDomain = null,
            Sacid = null,
            Sccid = null,
            Sid = null,
            State = null,
            Ttclid = null,
            Twclid = null,
            UserAgent = null,
            UserAgentFullList = null,
            UtmCampaign = null,
            UtmContent = null,
            UtmMedium = null,
            UtmName = null,
            UtmSource = null,
            UtmTerm = null,
            Wbraid = null,
            Zip = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new UserProperties
        {
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            CampaignID = "campaign_id",
            City = "city",
            Clickid = "clickid",
            Clid = "clid",
            CompanyName = "company_name",
            Consent = new Dictionary<string, string?>() { { "foo", "string" } },
            Country = "country",
            CustomProperties = new Dictionary<string, string?>() { { "foo", "string" } },
            DateOfBirth = "date_of_birth",
            Dclid = "dclid",
            Email = "email",
            Epik = "epik",
            ExternalID = "external_id",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            FirstName = "first_name",
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Gender = "gender",
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            JobTitle = "job_title",
            LastName = "last_name",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            PhoneNumber = "phone_number",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            Sid = "sid",
            State = "state",
            Ttclid = "ttclid",
            Twclid = "twclid",
            UserAgent = "user_agent",
            UserAgentFullList = "user_agent_full_list",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Wbraid = "wbraid",
            Zip = "zip",
        };

        UserProperties copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class DefaultPropertiesTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new DefaultProperties
        {
            ActiveDuration = 0,
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            BrowserLanguage = "browser_language",
            BrowserName = "browser_name",
            BrowserVersion = "browser_version",
            CampaignID = "campaign_id",
            Clickid = "clickid",
            Clid = "clid",
            CpuArchitecture = "cpu_architecture",
            CurrentUrl = "current_url",
            Dclid = "dclid",
            DeviceModel = "device_model",
            DeviceType = "device_type",
            DeviceVendor = "device_vendor",
            Duration = 0,
            Encoding = "encoding",
            EngineName = "engine_name",
            EngineVersion = "engine_version",
            Epik = "epik",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            Fv = true,
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Host = "host",
            Iframe = true,
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            NewS = true,
            OsName = "os_name",
            OsVersion = "os_version",
            PageHash = 0,
            Pathname = "pathname",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            ReceivedAt = "received_at",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            ScreenHeight = 0,
            ScreenWidth = 0,
            SessionCount = 0,
            Sid = "sid",
            Sr = "sr",
            Title = "title",
            Ttclid = "ttclid",
            Twclid = "twclid",
            Uafvl = "uafvl",
            UserAgent = "user_agent",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Version = "version",
            Wbraid = "wbraid",
            Webview = true,
        };

        double expectedActiveDuration = 0;
        string expectedAdID = "ad_id";
        string expectedAdsetID = "adset_id";
        string expectedAlart = "alart";
        string expectedAleid = "aleid";
        string expectedBasisCid = "basis_cid";
        string expectedBrowserLanguage = "browser_language";
        string expectedBrowserName = "browser_name";
        string expectedBrowserVersion = "browser_version";
        string expectedCampaignID = "campaign_id";
        string expectedClickid = "clickid";
        string expectedClid = "clid";
        string expectedCpuArchitecture = "cpu_architecture";
        string expectedCurrentUrl = "current_url";
        string expectedDclid = "dclid";
        string expectedDeviceModel = "device_model";
        string expectedDeviceType = "device_type";
        string expectedDeviceVendor = "device_vendor";
        double expectedDuration = 0;
        string expectedEncoding = "encoding";
        string expectedEngineName = "engine_name";
        string expectedEngineVersion = "engine_version";
        string expectedEpik = "epik";
        string expectedFbc = "fbc";
        string expectedFbclid = "fbclid";
        string expectedFbp = "fbp";
        bool expectedFv = true;
        string expectedGadSource = "gad_source";
        string expectedGbraid = "gbraid";
        string expectedGclid = "gclid";
        string expectedHost = "host";
        bool expectedIframe = true;
        string expectedImRef = "im_ref";
        string expectedIP = "ip";
        string expectedIrclickid = "irclickid";
        string expectedIsBot = "is_bot";
        string expectedLiFatID = "li_fat_id";
        string expectedMsclkid = "msclkid";
        string expectedNdclid = "ndclid";
        bool expectedNewS = true;
        string expectedOsName = "os_name";
        string expectedOsVersion = "os_version";
        double expectedPageHash = 0;
        string expectedPathname = "pathname";
        string expectedQclid = "qclid";
        string expectedRdtCid = "rdt_cid";
        string expectedReceivedAt = "received_at";
        string expectedReferrer = "referrer";
        string expectedReferringDomain = "referring_domain";
        string expectedSacid = "sacid";
        string expectedSccid = "sccid";
        double expectedScreenHeight = 0;
        double expectedScreenWidth = 0;
        double expectedSessionCount = 0;
        string expectedSid = "sid";
        string expectedSr = "sr";
        string expectedTitle = "title";
        string expectedTtclid = "ttclid";
        string expectedTwclid = "twclid";
        string expectedUafvl = "uafvl";
        string expectedUserAgent = "user_agent";
        string expectedUtmCampaign = "utm_campaign";
        string expectedUtmContent = "utm_content";
        string expectedUtmMedium = "utm_medium";
        string expectedUtmName = "utm_name";
        string expectedUtmSource = "utm_source";
        string expectedUtmTerm = "utm_term";
        string expectedVersion = "version";
        string expectedWbraid = "wbraid";
        bool expectedWebview = true;

        Assert.Equal(expectedActiveDuration, model.ActiveDuration);
        Assert.Equal(expectedAdID, model.AdID);
        Assert.Equal(expectedAdsetID, model.AdsetID);
        Assert.Equal(expectedAlart, model.Alart);
        Assert.Equal(expectedAleid, model.Aleid);
        Assert.Equal(expectedBasisCid, model.BasisCid);
        Assert.Equal(expectedBrowserLanguage, model.BrowserLanguage);
        Assert.Equal(expectedBrowserName, model.BrowserName);
        Assert.Equal(expectedBrowserVersion, model.BrowserVersion);
        Assert.Equal(expectedCampaignID, model.CampaignID);
        Assert.Equal(expectedClickid, model.Clickid);
        Assert.Equal(expectedClid, model.Clid);
        Assert.Equal(expectedCpuArchitecture, model.CpuArchitecture);
        Assert.Equal(expectedCurrentUrl, model.CurrentUrl);
        Assert.Equal(expectedDclid, model.Dclid);
        Assert.Equal(expectedDeviceModel, model.DeviceModel);
        Assert.Equal(expectedDeviceType, model.DeviceType);
        Assert.Equal(expectedDeviceVendor, model.DeviceVendor);
        Assert.Equal(expectedDuration, model.Duration);
        Assert.Equal(expectedEncoding, model.Encoding);
        Assert.Equal(expectedEngineName, model.EngineName);
        Assert.Equal(expectedEngineVersion, model.EngineVersion);
        Assert.Equal(expectedEpik, model.Epik);
        Assert.Equal(expectedFbc, model.Fbc);
        Assert.Equal(expectedFbclid, model.Fbclid);
        Assert.Equal(expectedFbp, model.Fbp);
        Assert.Equal(expectedFv, model.Fv);
        Assert.Equal(expectedGadSource, model.GadSource);
        Assert.Equal(expectedGbraid, model.Gbraid);
        Assert.Equal(expectedGclid, model.Gclid);
        Assert.Equal(expectedHost, model.Host);
        Assert.Equal(expectedIframe, model.Iframe);
        Assert.Equal(expectedImRef, model.ImRef);
        Assert.Equal(expectedIP, model.IP);
        Assert.Equal(expectedIrclickid, model.Irclickid);
        Assert.Equal(expectedIsBot, model.IsBot);
        Assert.Equal(expectedLiFatID, model.LiFatID);
        Assert.Equal(expectedMsclkid, model.Msclkid);
        Assert.Equal(expectedNdclid, model.Ndclid);
        Assert.Equal(expectedNewS, model.NewS);
        Assert.Equal(expectedOsName, model.OsName);
        Assert.Equal(expectedOsVersion, model.OsVersion);
        Assert.Equal(expectedPageHash, model.PageHash);
        Assert.Equal(expectedPathname, model.Pathname);
        Assert.Equal(expectedQclid, model.Qclid);
        Assert.Equal(expectedRdtCid, model.RdtCid);
        Assert.Equal(expectedReceivedAt, model.ReceivedAt);
        Assert.Equal(expectedReferrer, model.Referrer);
        Assert.Equal(expectedReferringDomain, model.ReferringDomain);
        Assert.Equal(expectedSacid, model.Sacid);
        Assert.Equal(expectedSccid, model.Sccid);
        Assert.Equal(expectedScreenHeight, model.ScreenHeight);
        Assert.Equal(expectedScreenWidth, model.ScreenWidth);
        Assert.Equal(expectedSessionCount, model.SessionCount);
        Assert.Equal(expectedSid, model.Sid);
        Assert.Equal(expectedSr, model.Sr);
        Assert.Equal(expectedTitle, model.Title);
        Assert.Equal(expectedTtclid, model.Ttclid);
        Assert.Equal(expectedTwclid, model.Twclid);
        Assert.Equal(expectedUafvl, model.Uafvl);
        Assert.Equal(expectedUserAgent, model.UserAgent);
        Assert.Equal(expectedUtmCampaign, model.UtmCampaign);
        Assert.Equal(expectedUtmContent, model.UtmContent);
        Assert.Equal(expectedUtmMedium, model.UtmMedium);
        Assert.Equal(expectedUtmName, model.UtmName);
        Assert.Equal(expectedUtmSource, model.UtmSource);
        Assert.Equal(expectedUtmTerm, model.UtmTerm);
        Assert.Equal(expectedVersion, model.Version);
        Assert.Equal(expectedWbraid, model.Wbraid);
        Assert.Equal(expectedWebview, model.Webview);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new DefaultProperties
        {
            ActiveDuration = 0,
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            BrowserLanguage = "browser_language",
            BrowserName = "browser_name",
            BrowserVersion = "browser_version",
            CampaignID = "campaign_id",
            Clickid = "clickid",
            Clid = "clid",
            CpuArchitecture = "cpu_architecture",
            CurrentUrl = "current_url",
            Dclid = "dclid",
            DeviceModel = "device_model",
            DeviceType = "device_type",
            DeviceVendor = "device_vendor",
            Duration = 0,
            Encoding = "encoding",
            EngineName = "engine_name",
            EngineVersion = "engine_version",
            Epik = "epik",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            Fv = true,
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Host = "host",
            Iframe = true,
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            NewS = true,
            OsName = "os_name",
            OsVersion = "os_version",
            PageHash = 0,
            Pathname = "pathname",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            ReceivedAt = "received_at",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            ScreenHeight = 0,
            ScreenWidth = 0,
            SessionCount = 0,
            Sid = "sid",
            Sr = "sr",
            Title = "title",
            Ttclid = "ttclid",
            Twclid = "twclid",
            Uafvl = "uafvl",
            UserAgent = "user_agent",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Version = "version",
            Wbraid = "wbraid",
            Webview = true,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<DefaultProperties>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new DefaultProperties
        {
            ActiveDuration = 0,
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            BrowserLanguage = "browser_language",
            BrowserName = "browser_name",
            BrowserVersion = "browser_version",
            CampaignID = "campaign_id",
            Clickid = "clickid",
            Clid = "clid",
            CpuArchitecture = "cpu_architecture",
            CurrentUrl = "current_url",
            Dclid = "dclid",
            DeviceModel = "device_model",
            DeviceType = "device_type",
            DeviceVendor = "device_vendor",
            Duration = 0,
            Encoding = "encoding",
            EngineName = "engine_name",
            EngineVersion = "engine_version",
            Epik = "epik",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            Fv = true,
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Host = "host",
            Iframe = true,
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            NewS = true,
            OsName = "os_name",
            OsVersion = "os_version",
            PageHash = 0,
            Pathname = "pathname",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            ReceivedAt = "received_at",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            ScreenHeight = 0,
            ScreenWidth = 0,
            SessionCount = 0,
            Sid = "sid",
            Sr = "sr",
            Title = "title",
            Ttclid = "ttclid",
            Twclid = "twclid",
            Uafvl = "uafvl",
            UserAgent = "user_agent",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Version = "version",
            Wbraid = "wbraid",
            Webview = true,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<DefaultProperties>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        double expectedActiveDuration = 0;
        string expectedAdID = "ad_id";
        string expectedAdsetID = "adset_id";
        string expectedAlart = "alart";
        string expectedAleid = "aleid";
        string expectedBasisCid = "basis_cid";
        string expectedBrowserLanguage = "browser_language";
        string expectedBrowserName = "browser_name";
        string expectedBrowserVersion = "browser_version";
        string expectedCampaignID = "campaign_id";
        string expectedClickid = "clickid";
        string expectedClid = "clid";
        string expectedCpuArchitecture = "cpu_architecture";
        string expectedCurrentUrl = "current_url";
        string expectedDclid = "dclid";
        string expectedDeviceModel = "device_model";
        string expectedDeviceType = "device_type";
        string expectedDeviceVendor = "device_vendor";
        double expectedDuration = 0;
        string expectedEncoding = "encoding";
        string expectedEngineName = "engine_name";
        string expectedEngineVersion = "engine_version";
        string expectedEpik = "epik";
        string expectedFbc = "fbc";
        string expectedFbclid = "fbclid";
        string expectedFbp = "fbp";
        bool expectedFv = true;
        string expectedGadSource = "gad_source";
        string expectedGbraid = "gbraid";
        string expectedGclid = "gclid";
        string expectedHost = "host";
        bool expectedIframe = true;
        string expectedImRef = "im_ref";
        string expectedIP = "ip";
        string expectedIrclickid = "irclickid";
        string expectedIsBot = "is_bot";
        string expectedLiFatID = "li_fat_id";
        string expectedMsclkid = "msclkid";
        string expectedNdclid = "ndclid";
        bool expectedNewS = true;
        string expectedOsName = "os_name";
        string expectedOsVersion = "os_version";
        double expectedPageHash = 0;
        string expectedPathname = "pathname";
        string expectedQclid = "qclid";
        string expectedRdtCid = "rdt_cid";
        string expectedReceivedAt = "received_at";
        string expectedReferrer = "referrer";
        string expectedReferringDomain = "referring_domain";
        string expectedSacid = "sacid";
        string expectedSccid = "sccid";
        double expectedScreenHeight = 0;
        double expectedScreenWidth = 0;
        double expectedSessionCount = 0;
        string expectedSid = "sid";
        string expectedSr = "sr";
        string expectedTitle = "title";
        string expectedTtclid = "ttclid";
        string expectedTwclid = "twclid";
        string expectedUafvl = "uafvl";
        string expectedUserAgent = "user_agent";
        string expectedUtmCampaign = "utm_campaign";
        string expectedUtmContent = "utm_content";
        string expectedUtmMedium = "utm_medium";
        string expectedUtmName = "utm_name";
        string expectedUtmSource = "utm_source";
        string expectedUtmTerm = "utm_term";
        string expectedVersion = "version";
        string expectedWbraid = "wbraid";
        bool expectedWebview = true;

        Assert.Equal(expectedActiveDuration, deserialized.ActiveDuration);
        Assert.Equal(expectedAdID, deserialized.AdID);
        Assert.Equal(expectedAdsetID, deserialized.AdsetID);
        Assert.Equal(expectedAlart, deserialized.Alart);
        Assert.Equal(expectedAleid, deserialized.Aleid);
        Assert.Equal(expectedBasisCid, deserialized.BasisCid);
        Assert.Equal(expectedBrowserLanguage, deserialized.BrowserLanguage);
        Assert.Equal(expectedBrowserName, deserialized.BrowserName);
        Assert.Equal(expectedBrowserVersion, deserialized.BrowserVersion);
        Assert.Equal(expectedCampaignID, deserialized.CampaignID);
        Assert.Equal(expectedClickid, deserialized.Clickid);
        Assert.Equal(expectedClid, deserialized.Clid);
        Assert.Equal(expectedCpuArchitecture, deserialized.CpuArchitecture);
        Assert.Equal(expectedCurrentUrl, deserialized.CurrentUrl);
        Assert.Equal(expectedDclid, deserialized.Dclid);
        Assert.Equal(expectedDeviceModel, deserialized.DeviceModel);
        Assert.Equal(expectedDeviceType, deserialized.DeviceType);
        Assert.Equal(expectedDeviceVendor, deserialized.DeviceVendor);
        Assert.Equal(expectedDuration, deserialized.Duration);
        Assert.Equal(expectedEncoding, deserialized.Encoding);
        Assert.Equal(expectedEngineName, deserialized.EngineName);
        Assert.Equal(expectedEngineVersion, deserialized.EngineVersion);
        Assert.Equal(expectedEpik, deserialized.Epik);
        Assert.Equal(expectedFbc, deserialized.Fbc);
        Assert.Equal(expectedFbclid, deserialized.Fbclid);
        Assert.Equal(expectedFbp, deserialized.Fbp);
        Assert.Equal(expectedFv, deserialized.Fv);
        Assert.Equal(expectedGadSource, deserialized.GadSource);
        Assert.Equal(expectedGbraid, deserialized.Gbraid);
        Assert.Equal(expectedGclid, deserialized.Gclid);
        Assert.Equal(expectedHost, deserialized.Host);
        Assert.Equal(expectedIframe, deserialized.Iframe);
        Assert.Equal(expectedImRef, deserialized.ImRef);
        Assert.Equal(expectedIP, deserialized.IP);
        Assert.Equal(expectedIrclickid, deserialized.Irclickid);
        Assert.Equal(expectedIsBot, deserialized.IsBot);
        Assert.Equal(expectedLiFatID, deserialized.LiFatID);
        Assert.Equal(expectedMsclkid, deserialized.Msclkid);
        Assert.Equal(expectedNdclid, deserialized.Ndclid);
        Assert.Equal(expectedNewS, deserialized.NewS);
        Assert.Equal(expectedOsName, deserialized.OsName);
        Assert.Equal(expectedOsVersion, deserialized.OsVersion);
        Assert.Equal(expectedPageHash, deserialized.PageHash);
        Assert.Equal(expectedPathname, deserialized.Pathname);
        Assert.Equal(expectedQclid, deserialized.Qclid);
        Assert.Equal(expectedRdtCid, deserialized.RdtCid);
        Assert.Equal(expectedReceivedAt, deserialized.ReceivedAt);
        Assert.Equal(expectedReferrer, deserialized.Referrer);
        Assert.Equal(expectedReferringDomain, deserialized.ReferringDomain);
        Assert.Equal(expectedSacid, deserialized.Sacid);
        Assert.Equal(expectedSccid, deserialized.Sccid);
        Assert.Equal(expectedScreenHeight, deserialized.ScreenHeight);
        Assert.Equal(expectedScreenWidth, deserialized.ScreenWidth);
        Assert.Equal(expectedSessionCount, deserialized.SessionCount);
        Assert.Equal(expectedSid, deserialized.Sid);
        Assert.Equal(expectedSr, deserialized.Sr);
        Assert.Equal(expectedTitle, deserialized.Title);
        Assert.Equal(expectedTtclid, deserialized.Ttclid);
        Assert.Equal(expectedTwclid, deserialized.Twclid);
        Assert.Equal(expectedUafvl, deserialized.Uafvl);
        Assert.Equal(expectedUserAgent, deserialized.UserAgent);
        Assert.Equal(expectedUtmCampaign, deserialized.UtmCampaign);
        Assert.Equal(expectedUtmContent, deserialized.UtmContent);
        Assert.Equal(expectedUtmMedium, deserialized.UtmMedium);
        Assert.Equal(expectedUtmName, deserialized.UtmName);
        Assert.Equal(expectedUtmSource, deserialized.UtmSource);
        Assert.Equal(expectedUtmTerm, deserialized.UtmTerm);
        Assert.Equal(expectedVersion, deserialized.Version);
        Assert.Equal(expectedWbraid, deserialized.Wbraid);
        Assert.Equal(expectedWebview, deserialized.Webview);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new DefaultProperties
        {
            ActiveDuration = 0,
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            BrowserLanguage = "browser_language",
            BrowserName = "browser_name",
            BrowserVersion = "browser_version",
            CampaignID = "campaign_id",
            Clickid = "clickid",
            Clid = "clid",
            CpuArchitecture = "cpu_architecture",
            CurrentUrl = "current_url",
            Dclid = "dclid",
            DeviceModel = "device_model",
            DeviceType = "device_type",
            DeviceVendor = "device_vendor",
            Duration = 0,
            Encoding = "encoding",
            EngineName = "engine_name",
            EngineVersion = "engine_version",
            Epik = "epik",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            Fv = true,
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Host = "host",
            Iframe = true,
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            NewS = true,
            OsName = "os_name",
            OsVersion = "os_version",
            PageHash = 0,
            Pathname = "pathname",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            ReceivedAt = "received_at",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            ScreenHeight = 0,
            ScreenWidth = 0,
            SessionCount = 0,
            Sid = "sid",
            Sr = "sr",
            Title = "title",
            Ttclid = "ttclid",
            Twclid = "twclid",
            Uafvl = "uafvl",
            UserAgent = "user_agent",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Version = "version",
            Wbraid = "wbraid",
            Webview = true,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new DefaultProperties { };

        Assert.Null(model.ActiveDuration);
        Assert.False(model.RawData.ContainsKey("activeDuration"));
        Assert.Null(model.AdID);
        Assert.False(model.RawData.ContainsKey("ad_id"));
        Assert.Null(model.AdsetID);
        Assert.False(model.RawData.ContainsKey("adset_id"));
        Assert.Null(model.Alart);
        Assert.False(model.RawData.ContainsKey("alart"));
        Assert.Null(model.Aleid);
        Assert.False(model.RawData.ContainsKey("aleid"));
        Assert.Null(model.BasisCid);
        Assert.False(model.RawData.ContainsKey("basis_cid"));
        Assert.Null(model.BrowserLanguage);
        Assert.False(model.RawData.ContainsKey("browser_language"));
        Assert.Null(model.BrowserName);
        Assert.False(model.RawData.ContainsKey("browser_name"));
        Assert.Null(model.BrowserVersion);
        Assert.False(model.RawData.ContainsKey("browser_version"));
        Assert.Null(model.CampaignID);
        Assert.False(model.RawData.ContainsKey("campaign_id"));
        Assert.Null(model.Clickid);
        Assert.False(model.RawData.ContainsKey("clickid"));
        Assert.Null(model.Clid);
        Assert.False(model.RawData.ContainsKey("clid"));
        Assert.Null(model.CpuArchitecture);
        Assert.False(model.RawData.ContainsKey("cpu_architecture"));
        Assert.Null(model.CurrentUrl);
        Assert.False(model.RawData.ContainsKey("current_url"));
        Assert.Null(model.Dclid);
        Assert.False(model.RawData.ContainsKey("dclid"));
        Assert.Null(model.DeviceModel);
        Assert.False(model.RawData.ContainsKey("device_model"));
        Assert.Null(model.DeviceType);
        Assert.False(model.RawData.ContainsKey("device_type"));
        Assert.Null(model.DeviceVendor);
        Assert.False(model.RawData.ContainsKey("device_vendor"));
        Assert.Null(model.Duration);
        Assert.False(model.RawData.ContainsKey("duration"));
        Assert.Null(model.Encoding);
        Assert.False(model.RawData.ContainsKey("encoding"));
        Assert.Null(model.EngineName);
        Assert.False(model.RawData.ContainsKey("engine_name"));
        Assert.Null(model.EngineVersion);
        Assert.False(model.RawData.ContainsKey("engine_version"));
        Assert.Null(model.Epik);
        Assert.False(model.RawData.ContainsKey("epik"));
        Assert.Null(model.Fbc);
        Assert.False(model.RawData.ContainsKey("fbc"));
        Assert.Null(model.Fbclid);
        Assert.False(model.RawData.ContainsKey("fbclid"));
        Assert.Null(model.Fbp);
        Assert.False(model.RawData.ContainsKey("fbp"));
        Assert.Null(model.Fv);
        Assert.False(model.RawData.ContainsKey("fv"));
        Assert.Null(model.GadSource);
        Assert.False(model.RawData.ContainsKey("gad_source"));
        Assert.Null(model.Gbraid);
        Assert.False(model.RawData.ContainsKey("gbraid"));
        Assert.Null(model.Gclid);
        Assert.False(model.RawData.ContainsKey("gclid"));
        Assert.Null(model.Host);
        Assert.False(model.RawData.ContainsKey("host"));
        Assert.Null(model.Iframe);
        Assert.False(model.RawData.ContainsKey("iframe"));
        Assert.Null(model.ImRef);
        Assert.False(model.RawData.ContainsKey("im_ref"));
        Assert.Null(model.IP);
        Assert.False(model.RawData.ContainsKey("ip"));
        Assert.Null(model.Irclickid);
        Assert.False(model.RawData.ContainsKey("irclickid"));
        Assert.Null(model.IsBot);
        Assert.False(model.RawData.ContainsKey("is_bot"));
        Assert.Null(model.LiFatID);
        Assert.False(model.RawData.ContainsKey("li_fat_id"));
        Assert.Null(model.Msclkid);
        Assert.False(model.RawData.ContainsKey("msclkid"));
        Assert.Null(model.Ndclid);
        Assert.False(model.RawData.ContainsKey("ndclid"));
        Assert.Null(model.NewS);
        Assert.False(model.RawData.ContainsKey("new_s"));
        Assert.Null(model.OsName);
        Assert.False(model.RawData.ContainsKey("os_name"));
        Assert.Null(model.OsVersion);
        Assert.False(model.RawData.ContainsKey("os_version"));
        Assert.Null(model.PageHash);
        Assert.False(model.RawData.ContainsKey("page_hash"));
        Assert.Null(model.Pathname);
        Assert.False(model.RawData.ContainsKey("pathname"));
        Assert.Null(model.Qclid);
        Assert.False(model.RawData.ContainsKey("qclid"));
        Assert.Null(model.RdtCid);
        Assert.False(model.RawData.ContainsKey("rdt_cid"));
        Assert.Null(model.ReceivedAt);
        Assert.False(model.RawData.ContainsKey("received_at"));
        Assert.Null(model.Referrer);
        Assert.False(model.RawData.ContainsKey("referrer"));
        Assert.Null(model.ReferringDomain);
        Assert.False(model.RawData.ContainsKey("referring_domain"));
        Assert.Null(model.Sacid);
        Assert.False(model.RawData.ContainsKey("sacid"));
        Assert.Null(model.Sccid);
        Assert.False(model.RawData.ContainsKey("sccid"));
        Assert.Null(model.ScreenHeight);
        Assert.False(model.RawData.ContainsKey("screen_height"));
        Assert.Null(model.ScreenWidth);
        Assert.False(model.RawData.ContainsKey("screen_width"));
        Assert.Null(model.SessionCount);
        Assert.False(model.RawData.ContainsKey("sessionCount"));
        Assert.Null(model.Sid);
        Assert.False(model.RawData.ContainsKey("sid"));
        Assert.Null(model.Sr);
        Assert.False(model.RawData.ContainsKey("sr"));
        Assert.Null(model.Title);
        Assert.False(model.RawData.ContainsKey("title"));
        Assert.Null(model.Ttclid);
        Assert.False(model.RawData.ContainsKey("ttclid"));
        Assert.Null(model.Twclid);
        Assert.False(model.RawData.ContainsKey("twclid"));
        Assert.Null(model.Uafvl);
        Assert.False(model.RawData.ContainsKey("uafvl"));
        Assert.Null(model.UserAgent);
        Assert.False(model.RawData.ContainsKey("user_agent"));
        Assert.Null(model.UtmCampaign);
        Assert.False(model.RawData.ContainsKey("utm_campaign"));
        Assert.Null(model.UtmContent);
        Assert.False(model.RawData.ContainsKey("utm_content"));
        Assert.Null(model.UtmMedium);
        Assert.False(model.RawData.ContainsKey("utm_medium"));
        Assert.Null(model.UtmName);
        Assert.False(model.RawData.ContainsKey("utm_name"));
        Assert.Null(model.UtmSource);
        Assert.False(model.RawData.ContainsKey("utm_source"));
        Assert.Null(model.UtmTerm);
        Assert.False(model.RawData.ContainsKey("utm_term"));
        Assert.Null(model.Version);
        Assert.False(model.RawData.ContainsKey("version"));
        Assert.Null(model.Wbraid);
        Assert.False(model.RawData.ContainsKey("wbraid"));
        Assert.Null(model.Webview);
        Assert.False(model.RawData.ContainsKey("webview"));
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetValidation_Works()
    {
        var model = new DefaultProperties { };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullAreSetToNull_Works()
    {
        var model = new DefaultProperties
        {
            ActiveDuration = null,
            AdID = null,
            AdsetID = null,
            Alart = null,
            Aleid = null,
            BasisCid = null,
            BrowserLanguage = null,
            BrowserName = null,
            BrowserVersion = null,
            CampaignID = null,
            Clickid = null,
            Clid = null,
            CpuArchitecture = null,
            CurrentUrl = null,
            Dclid = null,
            DeviceModel = null,
            DeviceType = null,
            DeviceVendor = null,
            Duration = null,
            Encoding = null,
            EngineName = null,
            EngineVersion = null,
            Epik = null,
            Fbc = null,
            Fbclid = null,
            Fbp = null,
            Fv = null,
            GadSource = null,
            Gbraid = null,
            Gclid = null,
            Host = null,
            Iframe = null,
            ImRef = null,
            IP = null,
            Irclickid = null,
            IsBot = null,
            LiFatID = null,
            Msclkid = null,
            Ndclid = null,
            NewS = null,
            OsName = null,
            OsVersion = null,
            PageHash = null,
            Pathname = null,
            Qclid = null,
            RdtCid = null,
            ReceivedAt = null,
            Referrer = null,
            ReferringDomain = null,
            Sacid = null,
            Sccid = null,
            ScreenHeight = null,
            ScreenWidth = null,
            SessionCount = null,
            Sid = null,
            Sr = null,
            Title = null,
            Ttclid = null,
            Twclid = null,
            Uafvl = null,
            UserAgent = null,
            UtmCampaign = null,
            UtmContent = null,
            UtmMedium = null,
            UtmName = null,
            UtmSource = null,
            UtmTerm = null,
            Version = null,
            Wbraid = null,
            Webview = null,
        };

        Assert.Null(model.ActiveDuration);
        Assert.True(model.RawData.ContainsKey("activeDuration"));
        Assert.Null(model.AdID);
        Assert.True(model.RawData.ContainsKey("ad_id"));
        Assert.Null(model.AdsetID);
        Assert.True(model.RawData.ContainsKey("adset_id"));
        Assert.Null(model.Alart);
        Assert.True(model.RawData.ContainsKey("alart"));
        Assert.Null(model.Aleid);
        Assert.True(model.RawData.ContainsKey("aleid"));
        Assert.Null(model.BasisCid);
        Assert.True(model.RawData.ContainsKey("basis_cid"));
        Assert.Null(model.BrowserLanguage);
        Assert.True(model.RawData.ContainsKey("browser_language"));
        Assert.Null(model.BrowserName);
        Assert.True(model.RawData.ContainsKey("browser_name"));
        Assert.Null(model.BrowserVersion);
        Assert.True(model.RawData.ContainsKey("browser_version"));
        Assert.Null(model.CampaignID);
        Assert.True(model.RawData.ContainsKey("campaign_id"));
        Assert.Null(model.Clickid);
        Assert.True(model.RawData.ContainsKey("clickid"));
        Assert.Null(model.Clid);
        Assert.True(model.RawData.ContainsKey("clid"));
        Assert.Null(model.CpuArchitecture);
        Assert.True(model.RawData.ContainsKey("cpu_architecture"));
        Assert.Null(model.CurrentUrl);
        Assert.True(model.RawData.ContainsKey("current_url"));
        Assert.Null(model.Dclid);
        Assert.True(model.RawData.ContainsKey("dclid"));
        Assert.Null(model.DeviceModel);
        Assert.True(model.RawData.ContainsKey("device_model"));
        Assert.Null(model.DeviceType);
        Assert.True(model.RawData.ContainsKey("device_type"));
        Assert.Null(model.DeviceVendor);
        Assert.True(model.RawData.ContainsKey("device_vendor"));
        Assert.Null(model.Duration);
        Assert.True(model.RawData.ContainsKey("duration"));
        Assert.Null(model.Encoding);
        Assert.True(model.RawData.ContainsKey("encoding"));
        Assert.Null(model.EngineName);
        Assert.True(model.RawData.ContainsKey("engine_name"));
        Assert.Null(model.EngineVersion);
        Assert.True(model.RawData.ContainsKey("engine_version"));
        Assert.Null(model.Epik);
        Assert.True(model.RawData.ContainsKey("epik"));
        Assert.Null(model.Fbc);
        Assert.True(model.RawData.ContainsKey("fbc"));
        Assert.Null(model.Fbclid);
        Assert.True(model.RawData.ContainsKey("fbclid"));
        Assert.Null(model.Fbp);
        Assert.True(model.RawData.ContainsKey("fbp"));
        Assert.Null(model.Fv);
        Assert.True(model.RawData.ContainsKey("fv"));
        Assert.Null(model.GadSource);
        Assert.True(model.RawData.ContainsKey("gad_source"));
        Assert.Null(model.Gbraid);
        Assert.True(model.RawData.ContainsKey("gbraid"));
        Assert.Null(model.Gclid);
        Assert.True(model.RawData.ContainsKey("gclid"));
        Assert.Null(model.Host);
        Assert.True(model.RawData.ContainsKey("host"));
        Assert.Null(model.Iframe);
        Assert.True(model.RawData.ContainsKey("iframe"));
        Assert.Null(model.ImRef);
        Assert.True(model.RawData.ContainsKey("im_ref"));
        Assert.Null(model.IP);
        Assert.True(model.RawData.ContainsKey("ip"));
        Assert.Null(model.Irclickid);
        Assert.True(model.RawData.ContainsKey("irclickid"));
        Assert.Null(model.IsBot);
        Assert.True(model.RawData.ContainsKey("is_bot"));
        Assert.Null(model.LiFatID);
        Assert.True(model.RawData.ContainsKey("li_fat_id"));
        Assert.Null(model.Msclkid);
        Assert.True(model.RawData.ContainsKey("msclkid"));
        Assert.Null(model.Ndclid);
        Assert.True(model.RawData.ContainsKey("ndclid"));
        Assert.Null(model.NewS);
        Assert.True(model.RawData.ContainsKey("new_s"));
        Assert.Null(model.OsName);
        Assert.True(model.RawData.ContainsKey("os_name"));
        Assert.Null(model.OsVersion);
        Assert.True(model.RawData.ContainsKey("os_version"));
        Assert.Null(model.PageHash);
        Assert.True(model.RawData.ContainsKey("page_hash"));
        Assert.Null(model.Pathname);
        Assert.True(model.RawData.ContainsKey("pathname"));
        Assert.Null(model.Qclid);
        Assert.True(model.RawData.ContainsKey("qclid"));
        Assert.Null(model.RdtCid);
        Assert.True(model.RawData.ContainsKey("rdt_cid"));
        Assert.Null(model.ReceivedAt);
        Assert.True(model.RawData.ContainsKey("received_at"));
        Assert.Null(model.Referrer);
        Assert.True(model.RawData.ContainsKey("referrer"));
        Assert.Null(model.ReferringDomain);
        Assert.True(model.RawData.ContainsKey("referring_domain"));
        Assert.Null(model.Sacid);
        Assert.True(model.RawData.ContainsKey("sacid"));
        Assert.Null(model.Sccid);
        Assert.True(model.RawData.ContainsKey("sccid"));
        Assert.Null(model.ScreenHeight);
        Assert.True(model.RawData.ContainsKey("screen_height"));
        Assert.Null(model.ScreenWidth);
        Assert.True(model.RawData.ContainsKey("screen_width"));
        Assert.Null(model.SessionCount);
        Assert.True(model.RawData.ContainsKey("sessionCount"));
        Assert.Null(model.Sid);
        Assert.True(model.RawData.ContainsKey("sid"));
        Assert.Null(model.Sr);
        Assert.True(model.RawData.ContainsKey("sr"));
        Assert.Null(model.Title);
        Assert.True(model.RawData.ContainsKey("title"));
        Assert.Null(model.Ttclid);
        Assert.True(model.RawData.ContainsKey("ttclid"));
        Assert.Null(model.Twclid);
        Assert.True(model.RawData.ContainsKey("twclid"));
        Assert.Null(model.Uafvl);
        Assert.True(model.RawData.ContainsKey("uafvl"));
        Assert.Null(model.UserAgent);
        Assert.True(model.RawData.ContainsKey("user_agent"));
        Assert.Null(model.UtmCampaign);
        Assert.True(model.RawData.ContainsKey("utm_campaign"));
        Assert.Null(model.UtmContent);
        Assert.True(model.RawData.ContainsKey("utm_content"));
        Assert.Null(model.UtmMedium);
        Assert.True(model.RawData.ContainsKey("utm_medium"));
        Assert.Null(model.UtmName);
        Assert.True(model.RawData.ContainsKey("utm_name"));
        Assert.Null(model.UtmSource);
        Assert.True(model.RawData.ContainsKey("utm_source"));
        Assert.Null(model.UtmTerm);
        Assert.True(model.RawData.ContainsKey("utm_term"));
        Assert.Null(model.Version);
        Assert.True(model.RawData.ContainsKey("version"));
        Assert.Null(model.Wbraid);
        Assert.True(model.RawData.ContainsKey("wbraid"));
        Assert.Null(model.Webview);
        Assert.True(model.RawData.ContainsKey("webview"));
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullValidation_Works()
    {
        var model = new DefaultProperties
        {
            ActiveDuration = null,
            AdID = null,
            AdsetID = null,
            Alart = null,
            Aleid = null,
            BasisCid = null,
            BrowserLanguage = null,
            BrowserName = null,
            BrowserVersion = null,
            CampaignID = null,
            Clickid = null,
            Clid = null,
            CpuArchitecture = null,
            CurrentUrl = null,
            Dclid = null,
            DeviceModel = null,
            DeviceType = null,
            DeviceVendor = null,
            Duration = null,
            Encoding = null,
            EngineName = null,
            EngineVersion = null,
            Epik = null,
            Fbc = null,
            Fbclid = null,
            Fbp = null,
            Fv = null,
            GadSource = null,
            Gbraid = null,
            Gclid = null,
            Host = null,
            Iframe = null,
            ImRef = null,
            IP = null,
            Irclickid = null,
            IsBot = null,
            LiFatID = null,
            Msclkid = null,
            Ndclid = null,
            NewS = null,
            OsName = null,
            OsVersion = null,
            PageHash = null,
            Pathname = null,
            Qclid = null,
            RdtCid = null,
            ReceivedAt = null,
            Referrer = null,
            ReferringDomain = null,
            Sacid = null,
            Sccid = null,
            ScreenHeight = null,
            ScreenWidth = null,
            SessionCount = null,
            Sid = null,
            Sr = null,
            Title = null,
            Ttclid = null,
            Twclid = null,
            Uafvl = null,
            UserAgent = null,
            UtmCampaign = null,
            UtmContent = null,
            UtmMedium = null,
            UtmName = null,
            UtmSource = null,
            UtmTerm = null,
            Version = null,
            Wbraid = null,
            Webview = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new DefaultProperties
        {
            ActiveDuration = 0,
            AdID = "ad_id",
            AdsetID = "adset_id",
            Alart = "alart",
            Aleid = "aleid",
            BasisCid = "basis_cid",
            BrowserLanguage = "browser_language",
            BrowserName = "browser_name",
            BrowserVersion = "browser_version",
            CampaignID = "campaign_id",
            Clickid = "clickid",
            Clid = "clid",
            CpuArchitecture = "cpu_architecture",
            CurrentUrl = "current_url",
            Dclid = "dclid",
            DeviceModel = "device_model",
            DeviceType = "device_type",
            DeviceVendor = "device_vendor",
            Duration = 0,
            Encoding = "encoding",
            EngineName = "engine_name",
            EngineVersion = "engine_version",
            Epik = "epik",
            Fbc = "fbc",
            Fbclid = "fbclid",
            Fbp = "fbp",
            Fv = true,
            GadSource = "gad_source",
            Gbraid = "gbraid",
            Gclid = "gclid",
            Host = "host",
            Iframe = true,
            ImRef = "im_ref",
            IP = "ip",
            Irclickid = "irclickid",
            IsBot = "is_bot",
            LiFatID = "li_fat_id",
            Msclkid = "msclkid",
            Ndclid = "ndclid",
            NewS = true,
            OsName = "os_name",
            OsVersion = "os_version",
            PageHash = 0,
            Pathname = "pathname",
            Qclid = "qclid",
            RdtCid = "rdt_cid",
            ReceivedAt = "received_at",
            Referrer = "referrer",
            ReferringDomain = "referring_domain",
            Sacid = "sacid",
            Sccid = "sccid",
            ScreenHeight = 0,
            ScreenWidth = 0,
            SessionCount = 0,
            Sid = "sid",
            Sr = "sr",
            Title = "title",
            Ttclid = "ttclid",
            Twclid = "twclid",
            Uafvl = "uafvl",
            UserAgent = "user_agent",
            UtmCampaign = "utm_campaign",
            UtmContent = "utm_content",
            UtmMedium = "utm_medium",
            UtmName = "utm_name",
            UtmSource = "utm_source",
            UtmTerm = "utm_term",
            Version = "version",
            Wbraid = "wbraid",
            Webview = true,
        };

        DefaultProperties copied = new(model);

        Assert.Equal(model, copied);
    }
}
