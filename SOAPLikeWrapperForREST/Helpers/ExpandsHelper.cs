using System;
using System.Collections.Generic;

using Acumatica.RESTClient.ContractBasedApi.Model;

using static SOAPLikeWrapperForREST.Helpers.EntityStructureHelper;

namespace SOAPLikeWrapperForREST.Helpers
{
    internal static class ExpandsHelper
    {
        public static string ComposeExpands(Entity entity) 
        {
            return string.Join(",",
                GetSubEntitiesWithReturnBehavior(entity));
        }
        public static string ComposeFilesExpand(Entity entity)
        {
            return  "files";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetSubEntitiesWithReturnBehavior(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot get expands for null");
            }
            else if (entity.ReturnBehavior == ReturnBehavior.All)
            {
                return GetAllSubEntitiesRecursive(entity.GetType());
            }
            else
            {
                HashSet<string> result = new HashSet<string>();
                foreach (var detailField in GetDetailEntitiesWithValues(entity))
                {
                    foreach (Entity detailValue in detailField.Details)
                    {
                        if (detailValue != null && detailValue.ReturnBehavior != ReturnBehavior.None)
                        {
                            foreach (var subentityName in GetSubEntitiesWithReturnBehavior(detailValue))
                            {
                                result.Add($"{detailField.Name}/{subentityName}");
                            }
                            result.Add(detailField.Name);
                        }
                    }
                }
                foreach (var linkedEntity in GetLinkedEntitiesWithValues(entity))
                {
                    if (linkedEntity != null && linkedEntity.Value.ReturnBehavior != ReturnBehavior.None)
                    {
                        foreach (var subentityName in GetSubEntitiesWithReturnBehavior(linkedEntity.Value))
                        {
                            result.Add($"{linkedEntity.Name}/{subentityName}");
                        }
                        result.Add(linkedEntity.Name);
                    }
                }
                return result;
            }
        }

    }
}
