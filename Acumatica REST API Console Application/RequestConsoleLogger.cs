﻿using System;
using System.IO;
using System.Net.Http;

namespace AcumaticaSoapLikeApiExample
{
    public static class RequestConsoleLogger
    {

        /// <summary>
        /// Logs response to Console
        /// </summary>
        public static void LogResponse(HttpResponseMessage response)
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
        public static void LogRequest(HttpRequestMessage request)
        {
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine("Request");
            Console.WriteLine("\tMethod: " + request.Method);
            string body = request.Content?.ReadAsStringAsync().Result;

            Console.WriteLine("\tURL: " + request.RequestUri);
            if (!String.IsNullOrEmpty(body))
                Console.WriteLine("\tBody: " + body);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
        }
    }
}
