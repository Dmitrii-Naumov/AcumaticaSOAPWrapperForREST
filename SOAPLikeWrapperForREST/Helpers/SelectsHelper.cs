using System;
using System.Collections.Generic;
using System.Linq;

using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.SOAPTypeWrappers;

using static SOAPLikeWrapperForREST.Helpers.EntityStructureHelper;

namespace SOAPLikeWrapperForREST.Helpers
{
    internal static class SelectsHelper
    {
        public static string ComposeSelects(Entity entity)
        {
            return string.Join(",", FindFieldsToSelect(entity));
        }

        private static IEnumerable<string> FindFieldsToSelect(Entity entity)
        {
            switch (entity.ReturnBehavior)
            {
                // no need to send select parameter, we need all the fields
                case ReturnBehavior.All: break;

                // no need to compose select, we don't need these fields
                case ReturnBehavior.None: break;

                // not supported
                case ReturnBehavior.OnlySystem: break;

                case ReturnBehavior.Default:
                    IEnumerable<string> skipFields = GetFieldsWithValues(entity).Where(_ => typeof(ISkipValueMarker).IsAssignableFrom(_.Type)).Select(_ => _.Name);
                    if (skipFields.Any())
                    {
                        foreach (var fieldToReturn in GetFields(entity.GetType()).Select(_ => _.Name).Where(_ => !skipFields.Contains(_)))
                        {
                            yield return fieldToReturn;
                        }
                    }
                    break;
                case ReturnBehavior.OnlySpecified:
                    foreach (var specifiedField in GetFieldsWithValues(entity).Where(_ => !typeof(ISkipValueMarker).IsAssignableFrom(_.Type)).Select(_ => _.Name))
                    {
                        yield return specifiedField;
                    }
                    break;
            }
        }
    }
}
