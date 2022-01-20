using Acumatica.RESTClient.Api;
using Acumatica.RESTClient.Client;
using Acumatica.Manufacturing_21_200_001.Model;

namespace Acumatica.Manufacturing_21_200_001.Api
{
	public class MRPDetailInquiryApi : BaseEndpointApi<MRPDetailInquiry>
	{
		public MRPDetailInquiryApi(Configuration configuration) : base(configuration)
		{ }
	}
}