using Acumatica.RESTClient.Api;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.Model;

namespace SOAPLikeWrapperForREST
{
    public class SOAPLikeEntityAPI<T> : EntityAPI<T>
        where T : Entity
    {
        protected readonly string EndpointPath;
        public SOAPLikeEntityAPI(Configuration configuration, string endpointPath) : base(configuration)
        {
            EndpointPath = endpointPath;
        }

        public override string GetEndpointPath()
		{
			return EndpointPath;
		}
	}
}
