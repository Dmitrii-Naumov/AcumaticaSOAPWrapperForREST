
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi.Model;
using Acumatica.RESTClient.ContractBasedApi;

namespace SOAPLikeWrapperForREST
{
    public class SOAPLikeEntityAPI<T> : EntityAPI<T>
        where T : Entity, new()
    {
        protected readonly string EndpointPath;
        public SOAPLikeEntityAPI(ApiClient client, string endpointPath) : base(client)
        {
            EndpointPath = endpointPath;
        }

        public override string GetEndpointPath()
		{
			return EndpointPath;
		}
	}
}
