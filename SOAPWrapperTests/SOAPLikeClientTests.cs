using Acumatica.RESTClient.Api;
using Acumatica.RESTClient.Model;

using SOAPLikeWrapperForREST;

namespace SOAPWrapperTests
{
    public class SOAPLikeClientTests
    {
        [Theory]
        [InlineData("http://localhost/22r203/(W(11038))/entity/Default/22.200.001/swagger.json", 
                    "http://localhost/22r203/",
                    "entity/Default/22.200.001")]
        [InlineData("https://example.acumatica.com/entity/Default/20.200.001",
                    "https://example.acumatica.com/",
                    "entity/Default/20.200.001")]
        [InlineData("https://example.acumatica.com/entity/Default/18.200.001?wsdl",
                    "https://example.acumatica.com/",
                    "entity/Default/18.200.001")]
        public void URLHelper_TakesPartsOfURLProperly(string fullURL, string expectedSiteURL, string expectedEndpointPath)
        {
            string cleanUrl = SOAPLikeClient.TrimRedundantPartsOfTheURL(fullURL);
            string siteURL= SOAPLikeClient.TakeSiteURL(cleanUrl);
            string endpointPath = SOAPLikeClient.TakeEndpointPath(cleanUrl);
            siteURL.Should().Be(expectedSiteURL);
            endpointPath.Should().Be(expectedEndpointPath);
        }

    }
}