using Acumatica.RESTClient.Api;
using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.Helpers;

namespace SOAPWrapperTests
{
    public class EndpointPathHelperTests
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
            string cleanUrl = EndpointPathHelper.TrimRedundantPartsOfTheURL(fullURL);
            string siteURL= EndpointPathHelper.TakeSiteURL(cleanUrl);
            string endpointPath = EndpointPathHelper.TakeEndpointPath(cleanUrl);
            siteURL.Should().Be(expectedSiteURL);
            endpointPath.Should().Be(expectedEndpointPath);
        }

    }
}