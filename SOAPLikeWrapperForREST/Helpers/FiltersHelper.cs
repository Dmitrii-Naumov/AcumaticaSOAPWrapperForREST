using System;
using System.Collections.Generic;
using System.Linq;

using Acumatica.RESTClient.ContractBasedApi.Model;

using static SOAPLikeWrapperForREST.Helpers.EntityStructureHelper;

namespace SOAPLikeWrapperForREST.Helpers
{
    internal static class FiltersHelper
    {
        public static string ComposeFilters(Entity entity, bool addPossibleKeyFields = false)
        {
            return string.Join(" and ",
                ComposeFiltersInternal(entity)
                .Concat(addPossibleKeyFields ?
                    ComposeFiltersForPossibleKeyFieldsInternal(entity)
                    : new string[0]));
        }
        public static IEnumerable<EntityField> GetSearchFieldsWithValues<SearchType>(Entity entity)
        {
            foreach (var entityField in GetFieldsWithValues(entity))
            {
                if (typeof(SearchType) == entityField.Type)
                {
                    yield return entityField;
                }
            }
            foreach (var linkedEntity in GetLinkedEntitiesWithValues(entity))
            {
                // search recursively for all Linked entities inside other linked entities
                foreach (var entityField in GetSearchFieldsWithValues<SearchType>(linkedEntity.Value))
                {
                    entityField.Name = $"{linkedEntity.Name}/{entityField.Name}";
                    yield return entityField;
                }
            }
        }

        public static IEnumerable<string> ComposeFiltersInternal(Entity entity)
        {
            List<string> filters = new List<string>();

            filters.AddRange(GetSearchFieldsWithValues<StringSearch>(entity)
                .Select(GetFilterByStringCondition));

            filters.AddRange(GetSearchFieldsWithValues<IntSearch>(entity)
                .Select(GetFilterByIntCondition));

            filters.AddRange(GetSearchFieldsWithValues<LongSearch>(entity)
                .Select(GetFilterByLongCondition));

            filters.AddRange(GetSearchFieldsWithValues<GuidSearch>(entity)
                .Select(GetFilterByGuidCondition));

            filters.AddRange(GetSearchFieldsWithValues<DecimalSearch>(entity)
                .Select(GetFilterByDecimalCondition));

            filters.AddRange(GetSearchFieldsWithValues<BooleanSearch>(entity)
                .Select(GetFilterByBooleanCondition));

            filters.AddRange(GetSearchFieldsWithValues<DateTimeSearch>(entity)
                .Select(GetFilterByDateTimeCondition));
            return filters;
        }

        private static string GetFilterByBooleanCondition(EntityField field)
        {
            BooleanSearch search = (BooleanSearch)field.Value;

            switch (search.Condition)
            {
                case BooleanCondition.IsNotNull: return $"{field.Name} ne null";
                case BooleanCondition.IsNull: return $"{field.Name} eq null";
                case BooleanCondition.Equal: return $"{field.Name} eq {search.Value.ToString().ToLower()}";
                case BooleanCondition.NotEqual: return $"{field.Name} ne {search.Value.ToString().ToLower()}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private static string GetFilterByDecimalCondition(EntityField field)
        {
            DecimalSearch search = (DecimalSearch)field.Value;

            switch (search.Condition)
            {
                case DecimalCondition.IsNotNull: return $"{field.Name} ne null";
                case DecimalCondition.IsNull: return $"{field.Name} eq null";
                case DecimalCondition.Equal: return $"{field.Name} eq {search.Value}";
                case DecimalCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                case DecimalCondition.IsBetween: return $"({field.Name} ge {search.Value} and {field.Name} le {search.Value2})";
                case DecimalCondition.IsGreaterThan: return $"{field.Name} gt {search.Value}";
                case DecimalCondition.IsGreaterThanOrEqualsTo: return $"{field.Name} ge {search.Value}";
                case DecimalCondition.IsLessThan: return $"{field.Name} lt {search.Value}";
                case DecimalCondition.IsLessThanOrEqualsTo: return $"{field.Name} le {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private static string GetFilterByIntCondition(EntityField field)
        {
            IntSearch search = (IntSearch)field.Value;

            switch (search.Condition)
            {
                case IntCondition.IsNotNull: return $"{field.Name} ne null";
                case IntCondition.IsNull: return $"{field.Name} eq null";
                case IntCondition.Equal: return $"{field.Name} eq {search.Value}";
                case IntCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                case IntCondition.IsBetween: return $"({field.Name} ge {search.Value} and {field.Name} le {search.Value2})";
                case IntCondition.IsGreaterThan: return $"{field.Name} gt {search.Value}";
                case IntCondition.IsGreaterThanOrEqualsTo: return $"{field.Name} ge {search.Value}";
                case IntCondition.IsLessThan: return $"{field.Name} lt {search.Value}";
                case IntCondition.IsLessThanOrEqualsTo: return $"{field.Name} le {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private static string GetFilterByLongCondition(EntityField field)
        {
            LongSearch search = (LongSearch)field.Value;

            switch (search.Condition)
            {
                case LongCondition.IsNotNull: return $"{field.Name} ne null";
                case LongCondition.IsNull: return $"{field.Name} eq null";
                case LongCondition.Equal: return $"{field.Name} eq {search.Value}";
                case LongCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                case LongCondition.IsBetween: return $"({field.Name} ge {search.Value} and {field.Name} le {search.Value2})";
                case LongCondition.IsGreaterThan: return $"{field.Name} gt {search.Value}";
                case LongCondition.IsGreaterThanOrEqualsTo: return $"{field.Name} ge {search.Value}";
                case LongCondition.IsLessThan: return $"{field.Name} lt {search.Value}";
                case LongCondition.IsLessThanOrEqualsTo: return $"{field.Name} le {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private static string GetFilterByGuidCondition(EntityField field)
        {
            GuidSearch search = (GuidSearch)field.Value;

            switch (search.Condition)
            {
                case GuidCondition.IsNotNull: return $"{field.Name} ne null";
                case GuidCondition.IsNull: return $"{field.Name} eq null";
                case GuidCondition.Equal: return $"{field.Name} eq {search.Value}";
                case GuidCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }


        private static string GetFilterByStringCondition(EntityField field)
        {
            StringSearch search = (StringSearch)field.Value;

            switch (search.Condition)
            {
                case StringCondition.IsNotNull: return $"{field.Name} ne null";
                case StringCondition.Contains: return $"substringof('{search.Value}',{field.Name})";
                case StringCondition.StartsWith: return $"startswith({field.Name}, '{search.Value}')";
                case StringCondition.NotEqual: return $"{field.Name} ne '{search.Value}'";
                case StringCondition.DoesNotContain: return $"substringof('{search.Value}', {field.Name}) eq false";
                case StringCondition.Equal: return $"{field.Name} eq '{search.Value}'";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private static string GetFilterByDateTimeCondition(EntityField field)
        {
            DateTimeSearch search = (DateTimeSearch)field.Value;

            switch (search.Condition)
            {
                case DateTimeCondition.Equal: return $"{field.Name} eq datetimeoffset'{search.Value?.ToString("o")}'";
                case DateTimeCondition.IsGreaterThan: return $"{field.Name} gt datetimeoffset'{search.Value?.ToString("o")}'";
                case DateTimeCondition.IsLessThan: return $"field.Name lt datetimeoffset'{search.Value?.ToString("o")}'";
                case DateTimeCondition.IsGreaterThanOrEqualsTo: return $"{field.Name} ge datetimeoffset'{search.Value?.ToString("o")}'";
                case DateTimeCondition.IsLessThanOrEqualsTo: return $"{field.Name} le datetimeoffset'{search.Value?.ToString("o")}'";
                case DateTimeCondition.IsBetween: return $"({field.Name} ge datetimeoffset'{search.Value?.ToString("o")}' and {field.Name} le datetimeoffset'{search.Value2?.ToString("o")}')";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        public static IEnumerable<string> ComposeFiltersForPossibleKeyFieldsInternal<T>(T entity) where T : Entity
        {
            return GetPossibleKeyFieldsWithValues(entity)
                            .Where(f => f.Value != null)
                            .Select(field =>
                            {
                                if (field.Type == typeof(StringValue))
                                    return $"{field.Name} eq '{field.Value}'";
                                else
                                    return $"{field.Name} eq {field.Value}";
                            });
        }

        /// <summary>
        /// The assumption is that only string, int and Long fields may be key fields
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static IEnumerable<EntityField> GetPossibleKeyFieldsWithValues(Entity entity)
        {
            foreach (var field in entity.GetType().GetProperties())
            {
                var fieldValue = field.GetValue(entity);
                if (fieldValue != null)
                {
                    switch (fieldValue.GetType().Name)
                    {
                        case nameof(StringValue):
                            yield return new EntityField(field.PropertyType, ((StringValue)field.GetValue(entity))?.Value, field.Name);
                            break;
                        case nameof(IntValue):
                            yield return new EntityField(field.PropertyType, ((IntValue)field.GetValue(entity))?.Value, field.Name);
                            break;
                        case nameof(LongValue):
                            yield return new EntityField(field.PropertyType, ((LongValue)field.GetValue(entity))?.Value, field.Name);
                            break;
                    }
                }
            }
        }


    }
}
