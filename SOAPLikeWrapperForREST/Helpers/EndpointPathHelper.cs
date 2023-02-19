using System;
using System.Collections.Generic;
using System.Text;

namespace SOAPLikeWrapperForREST.Helpers
{
    internal static class EndpointPathHelper
    {
        private const string EntityKeyword = "/entity/";
        internal static string TrimRedundantPartsOfTheURL(string dirtyURL)
        {
            string cleanURL = dirtyURL
                .Replace("?wsdl", "")
                .Replace("/swagger.json", "");

            int indexOfSession = cleanURL.IndexOf("/(W(");
            if (indexOfSession > 0)
            {
                int indexOfSessionEnd = cleanURL.IndexOf("))/", indexOfSession);
                string urlPart1 = cleanURL.Substring(0, indexOfSession);
                string urlPart2 = cleanURL.Substring(indexOfSessionEnd + 2);
                cleanURL = urlPart1 + urlPart2;
            }

            return cleanURL;
        }
        internal static string TakeSiteURL(string fullURL)
        {
            int indexOfEntity = fullURL.IndexOf(EntityKeyword);
            if (indexOfEntity > 0)
            {
                return EnsureSlash(fullURL.Substring(0, indexOfEntity));
            }
            else
            {
                throw new ArgumentException("The provided URL is not valid.");
            }
        }
        internal static string EnsureSlash(string url)
        {
            if (url.EndsWith("/"))
            {
                return url;
            }
            else return url + "/";
        }
        internal static string TakeEndpointPath(string fullURL)
        {
            int indexOfEntity = fullURL.IndexOf(EntityKeyword);
            if (indexOfEntity > 0)
            {
                return fullURL.Substring(indexOfEntity + 1);
            }
            else
            {
                throw new ArgumentException("The provided URL is not valid.");
            }
        }
    }
}
