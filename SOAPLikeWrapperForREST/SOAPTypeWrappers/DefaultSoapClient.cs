 using System;

namespace SOAPLikeWrapperForREST
{
	public class DefaultSoapClient : SOAPLikeClient, IDisposable
	{
		public DefaultSoapClient(string siteURL) : base(siteURL)
		{ }
        public DefaultSoapClient(string siteURL, int timeout) : base(siteURL, timeout)
        { }

        public void Dispose()
		{
			Logout();
		}
	}
}