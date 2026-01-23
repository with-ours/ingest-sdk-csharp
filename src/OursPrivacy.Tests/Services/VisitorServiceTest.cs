using System.Collections.Generic;
using System.Threading.Tasks;

namespace OursPrivacy.Tests.Services;

public class VisitorServiceTest : TestBase
{
    [Fact(Skip = "Prism tests are disabled")]
    public async Task Upsert_Works()
    {
        var response = await this.client.Visitor.Upsert(
            new()
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
            },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }
}
