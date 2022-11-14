﻿using System;
using System.Collections.Generic;

using Acumatica.Default_22_200_001.Model;
using Acumatica.RESTClient.Model;

using SOAPLikeWrapperForREST;

namespace AcumaticaSoapLikeApiExample
{
    public class SOAPLikeExample
    {
        public static void ExampleMethod(string siteURL, string username, string password, string tenant = null, string branch = null, string locale = null)
        {
            var restClient = new SOAPLikeClient(siteURL,
                requestInterceptor: RequestLogger.LogRequest,
                responseInterceptor: RequestLogger.LogResponse);

            try
            {
                restClient.Login(username, password, tenant, branch, locale);
                Console.WriteLine("Logged In");

                Shipment shipment = new Shipment()
                {
                    ShipmentNbr = new StringSearch() { Value = "002644" },
                    Packages = new List<ShipmentPackage>()
                    {
                        new ShipmentPackage {
                            ReturnBehavior=  ReturnBehavior.All,
                            PackageContents = new List<ShipmentPackageDetail>()
                            {
                                new ShipmentPackageDetail() { ReturnBehavior= ReturnBehavior.All}
                            }
                        }
                    },
                    CustomFields = new CustomField[] { new CustomField("CustomStringValue") { viewName="Document", fieldName="ShipmentNbr" } }
                
                };
                shipment = (Shipment)restClient.Get(shipment);

                Console.WriteLine("Shipped Qty= " + shipment.ShippedQty.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                restClient.Logout();
                Console.WriteLine("Logged Out");
            }

        }
    }
}
