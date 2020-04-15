using Acumatica.RESTClient.Api;
using Acumatica.RESTClient.Client;
using Acumatica.POS_17_200_001.Model;

namespace Acumatica.POS_17_200_001.Api
{
	public class EmailProcessingApi : EntityAPI<EmailProcessing>
	{
		public EmailProcessingApi(Configuration configuration) : base(configuration)
		{ }
	}
}