using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Acumatica.RESTClient.ContractBasedApi.Model;

namespace SOAPLikeWrapperForREST.Helpers
{
    internal static class EntityStructureHelper
    {
        public static IEnumerable<PropertyInfo> GetLinkedEntities(Type entityType)
        {
            foreach (var field in entityType.GetProperties())
            {
                if (typeof(Entity).IsAssignableFrom(field.PropertyType))
                {
                    yield return field;
                }
            }
        }
        public static IEnumerable<LinkedEntity> GetLinkedEntitiesWithValues(Entity entity)
        {
            foreach (var linkedEntityField in GetLinkedEntities(entity.GetType()))
            {
                var linkedEntityValue = (Entity)linkedEntityField.GetValue(entity);
                if (linkedEntityValue != null)
                {
                    yield return new LinkedEntity(linkedEntityField.PropertyType, linkedEntityValue, linkedEntityField.Name);
                }
            }
        }
        public static IEnumerable<PropertyInfo> GetDetails(Type entityType)
        {
            foreach (var property in entityType.GetProperties())
            {
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType)
                    && property.Name != nameof(Entity.Custom)
                    && property.Name != nameof(Entity.CustomFields)
                    && property.Name != nameof(Entity.Files)
                    && property.PropertyType != typeof(CustomField[])
                    && property.PropertyType != typeof(string)
                    )
                {
                    yield return property;
                }
            }
        }
        public static IEnumerable<DetailEntity> GetDetailEntitiesWithValues(Entity entity)
        {
            foreach (var detailField in GetDetails(entity.GetType()))
            {
                var detailEntityValues = (IEnumerable)detailField.GetValue(entity);
                if (detailEntityValues != null && detailEntityValues.Cast<object>().Any())
                {
                    yield return new DetailEntity(GetElementType(detailField), detailEntityValues, detailField.Name);
                }
            }
        }
        /// <summary>
        /// Gets all <see cref="RestValueBase{T}">value fields</see> 
        /// that are not <see cref="LinkedEntity"/> or Details. 
        /// Does not return system fields, e.g. <see cref="Entity.ID"/>, 
        /// <see cref="Entity.Note"/> or <see cref="Entity.RowNumber"/>
        /// </summary>
        /// <param name="entityType"></param>
        public static IEnumerable<PropertyInfo> GetFields(Type entityType)
        {
            return entityType.GetProperties()
                .Where(property =>
                        typeof(IRestValueMarker).IsAssignableFrom(property.PropertyType)
                        // We consider Note a system field that we cannot process the same way as normal fields
                        && property.Name != nameof(Entity.Note)
                      );
        }

        public static IEnumerable<EntityField> GetFieldsWithValues(Entity entity)
        {
            foreach (var field in GetFields(entity.GetType()))
            {
                var fieldValue = field.GetValue(entity);
                if (fieldValue != null)
                {
                    yield return new EntityField(fieldValue.GetType(), fieldValue, field.Name);
                }
            }
        }

        public static IEnumerable<string> GetAllSubEntitiesRecursive(Type entityType)
        {
            foreach (var entityField in GetSubentities(entityType))
            {
                foreach (var subEntityField in GetAllSubEntitiesRecursive(GetElementType(entityField)))
                {
                    yield return $"{entityField.Name}/{subEntityField}";
                }
                yield return entityField.Name;
            }
        }

        public static IEnumerable<PropertyInfo> GetSubentities(Type entityType)
        {
            return GetDetails(entityType).Concat(GetLinkedEntities(entityType));
        }

        private static Type GetElementType(PropertyInfo field)
        {
            if (field.PropertyType.Name == "List`1")
            {
                return field.PropertyType.GetGenericArguments().FirstOrDefault();
            }
            else if (field.PropertyType.BaseType == typeof(Array))
            {
                return field.PropertyType.GetElementType();
            }
            else
            {
                return field.PropertyType;
            }
        }
    }
}
