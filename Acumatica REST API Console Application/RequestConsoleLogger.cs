using System;
using System.IO;

using RestSharp;

namespace AcumaticaSoapLikeApiExample
{
    public static class RequestConsoleLogger
    {

        /// <summary>
        /// Logs response to Console
        /// </summary>
        public static void LogResponse(RestRequest request, RestResponse response, RestClient restClient)
        {
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine("Response");
            Console.WriteLine("\tStatus code: " + response.StatusCode);
            Console.WriteLine("\tContent: " + response.Content);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
        }

        /// <summary>
        /// Logs request to Console
        /// </summary>
        public static void LogRequest(RestRequest request, RestClient restClient)
        {
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine("Request");
            Console.WriteLine("\tMethod: " + request.Method);
            string parameters = "";
            string body = "";
            foreach (var parameter in request.Parameters)
            {
                if (parameter.Type == ParameterType.RequestBody)
                    body += parameter.Value;
            }

            Console.WriteLine("\tURL: " + restClient.BuildUri(request) + parameters);
            if (!String.IsNullOrEmpty(body))
                Console.WriteLine("\tBody: " + body);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
        }
    }
}
