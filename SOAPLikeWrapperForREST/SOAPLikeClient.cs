using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using Acumatica.Auth.Api;
using Acumatica.Auth.Model;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.FileApi;
using Acumatica.RESTClient.Model;

using RestSharp;

[assembly: InternalsVisibleTo("SOAPWrapperTests")]

namespace SOAPLikeWrapperForREST
{
    public class SOAPLikeClient
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SOAPLikeClient"/> class
        /// </summary>
        /// <param name="siteURL">
        /// Path to the Acumatica instance e.g. <c>https://example.acumatica.com/</c>
        /// </param>
        /// <param name="endpointPath">
        /// Relative endpoint path, e.g. <c>entity/Default/22.200.001</c>
        /// </param>
        /// <param name="timeout">
        /// Request timeout of the <see cref="SOAPLikeClient"/> in milliseconds. Default to 10000 milliseconds.
        /// </param>
        /// <param name="requestInterceptor">
        /// An action delegate that will be executed along with sending an API request. Can be used for logging purposes.
        /// </param>
        /// <param name="responseInterceptor">
        /// An action delegate that will be executed along with receiving an API response. Can be used for logging purposes.
        /// </param>
        public SOAPLikeClient(string siteURL, string endpointPath, int timeout = 10000, Action<RestRequest, RestClient> requestInterceptor = null, Action<RestRequest, RestResponse, RestClient> responseInterceptor = null)
        {
            AuthorizationApi = new AuthApi(siteURL, timeout, requestInterceptor, responseInterceptor);
            ProcessStartTime = new Dictionary<string, DateTime>();
            Timeout = timeout;
            if (!endpointPath.StartsWith("entity"))
            {
                throw new ArgumentException("Incorrect endpoint path. The endpoint path must start with 'entity' keyword.");
            }
            EndpointPath = endpointPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SOAPLikeClient"/> class
        /// </summary>
        /// <param name="endpointURL">
        /// Path to the Acumatica instance e.g. <c>https://example.acumatica.com/entity/Default/22.200.001</c>
        /// </param>
        /// <param name="timeout">
        /// Request timeout of the <see cref="SOAPLikeClient"/> in milliseconds. Default to 10000 milliseconds.
        /// </param>
        /// <param name="requestInterceptor">
        /// An action delegate that will be executed along with sending an API request. Can be used for logging purposes.
        /// </param>
        /// <param name="responseInterceptor">
        /// An action delegate that will be executed along with receiving an API response. Can be used for logging purposes.
        /// </param>
        public SOAPLikeClient(string endpointURL, int timeout = 10000, Action<RestRequest, RestClient> requestInterceptor = null, Action<RestRequest, RestResponse, RestClient> responseInterceptor = null)
            : this(TakeSiteURL(TrimRedundantPartsOfTheURL(endpointURL)),
                   TakeEndpointPath(TrimRedundantPartsOfTheURL(endpointURL)),
                   timeout,
                   requestInterceptor,
                   responseInterceptor)
        { }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the business date to the current instance of API client. 
        /// The business date will be sent as a header in all Put requests.
        /// </summary>
        public void SetBusinessDate(DateTime businessDate)
        {
            BusinessDate = businessDate;
        }

        /// <summary>
        /// Logs the current instance of API client to the Acumatica web service.
        /// </summary>
        /// <param name="username">Name of the user that is used to open a new session (required).</param>
        /// <param name="password">User password (required).</param>
        /// <param name="tenant">Defines the tenant to log in.</param>
        /// <param name="branch">Defines the branch to log in.</param>
        /// <param name="locale">Defines the locale to use for localizable data.</param>
        public void Login(string username, string password, string tenant = null, string branch = null, string locale = null)
        {
            CurrentConfiguration = AuthorizationApi.LogIn(
                new Credentials(
                    name: username, 
                    password: password, 
                    tenant: tenant, 
                    branch: branch, 
                    locale: locale));
            CurrentConfiguration.Timeout = Timeout;
        }

        /// <summary>
        /// Closes the open API session.
        /// Rests the <see cref="BusinessDate"/>
        /// </summary>
        public void Logout()
        {
            AuthorizationApi.TryLogout();
            BusinessDate = null;
        }

        public T GetById<T>(Guid? id, string select = null, string filter = null, string expand = null, string custom = null)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            return api.GetById(id, select, filter, expand, custom);
        }
        public T GetByKeys<T>(IEnumerable<string> ids, string select = null, string filter = null, string expand = null, string custom = null)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            return api.GetByKeys(ids, select, filter, expand, custom);
        }

        [Obsolete("Get method is for backward compatibility with SOAP only. Use one of the following REST methods instead: GetList, GetByKeys, GetByID")]
        public T Get<T>(T entity, bool retrieveFiles = false)
            where T : Entity
        {
            string expand = ComposeExpands(entity, retrieveFiles);
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            if (entity.ID.HasValue)
            {
                T resultByID = api.GetById(entity.ID, expand: expand);
                resultByID.ReturnBehavior = entity.ReturnBehavior;
                return resultByID;
            }
            string filter = ComposeFilters(entity, true);
            var list = api.GetList(filter: filter);
            if (list.Count > 1)
            {
                throw new Exception("More than one entity satisfies the condition.");
            }
            if (list.Count == 0)
            {
                throw new Exception("No entities satisfy the condition.");
            }
            T result = api.GetById(list[0].ID, expand: expand);
            result.ReturnBehavior = entity.ReturnBehavior;
            return result;
        }
        public T[] GetList<T>(T entity, int? top = null, int? skip = null)
            where T : Entity
        {
            string expand = ComposeExpands(entity);
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);

            string filter = ComposeFilters(entity);
            var result = api.GetList(filter: filter, expand: expand, skip: skip, top: top);

            return result.ToArray();
        }
        public T Put<T>(T entity)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            var result = api.PutEntity(entity,
                expand: ComposeExpands(entity),
                filter: ComposeFilters(entity), 
                businessDate: BusinessDate); ;
            result.ReturnBehavior = entity.ReturnBehavior;
            return result;
        }
        [Obsolete("PutFiles method is for backward compatibility with SOAP only. Use one of the following REST methods instead: PutFile, PutFileAsync")]
        public void PutFiles<T>(List<string> keys, File[] files)
            where T : Entity
        {
            foreach (var file in files)
            {
                SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
                api.PutFile(keys, file.Name, file.Content);
            }
        }

        [Obsolete("GetFiles method is for backward compatibility with SOAP only. Use one of the following REST methods instead: FileApi.GetFile")]
        public File[] GetFiles<T>(T entity)
            where T : Entity
        {
            var record = Get(entity, retrieveFiles: true);
            if (record?.Files == null)
            {
                throw new ApiException(500, "Failed to retrieve files");
            }
            if (record.Files.Count == 0)
            {
                return new File[0];
            }

            var filesArray = new File[record.Files.Count];
            FileApi api = new FileApi(CurrentConfiguration);

            for (int i = 0; i < filesArray.Length; i++)
            {
                filesArray[i] = new File();
                filesArray[i].Name = record.Files[i].Filename;
                using (var sourceStream = api.GetFile(record.Files[i]))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        sourceStream.CopyTo(memoryStream);
                        filesArray[i].Content = memoryStream.ToArray();
                    }
                }
            }
            return filesArray;
        }

        [Obsolete("Delete method is for backward compatibility with SOAP only. Use one of the following REST methods instead: DeleteByID, DeleteByKeys")]
        public void Delete<T>(T entity)
            where T : Entity
        {
            if (entity.ID != null) DeleteById<T>(entity.ID);
            else DeleteById<T>(Get(entity).ID);
        }

        /// <summary>
        /// Deletes the record by its session identifier. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <returns></returns>
        public void DeleteById<T>(Guid? id) where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            api.DeleteById(id);
        }

        /// <summary>
        /// Deletes the record by its keys. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <returns></returns>
        public void DeleteByKeys<T>(IEnumerable<string> ids)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            api.DeleteByKeys(ids);
        }

        public string Invoke<T>(EntityAction<T> action)
              where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            string invokeResult = api.InvokeAction(action, businessDate: BusinessDate);
            if (ProcessStartTime.ContainsKey(invokeResult))
            { }
            else
            {
                ProcessStartTime.Add(invokeResult, DateTime.Now);
            }
            return invokeResult;
        }
        public ProcessResult GetProcessStatus(string invokeResult)
        {
            SOAPLikeEntityAPI<Entity> api = new SOAPLikeEntityAPI<Entity>(CurrentConfiguration, EndpointPath);
            return new ProcessResult()
            {
                Status = (ProcessStatus)api.GetProcessStatus(invokeResult),
                Seconds = GetProcessingSeconds(invokeResult),
                Message = invokeResult
            };
        }

        public string Invoke<T>(T entity, EntityAction<T> action)
            where T : Entity
        {
            action.Entity = entity;
            return Invoke(action);
        }
        public ProcessResult WaitInvoke<T>(T entity, EntityAction<T> action)
            where T : Entity
        {
            action.Entity = entity;
            return WaitInvoke(action);
        }
        public ProcessResult WaitInvoke<T>(EntityAction<T> action, bool throwOnFail = true)
            where T : Entity
        {
            InvokeResult invokeResult = Invoke(action);

            while (true)
            {
                ProcessResult processResult = GetProcessStatus(invokeResult);

                System.Threading.Thread.Sleep(100);

                switch (processResult.Status)
                {
                    case ProcessStatus.NotExists:
                    case ProcessStatus.Aborted:
                        if (throwOnFail)
                            throw new SystemException("Process status: " +
                                                      processResult.Status + "; Error: " +
                                                      processResult.Message);
                        return processResult;
                    case ProcessStatus.Completed:
                    case ProcessStatus.OK:
                        return processResult;
                    case ProcessStatus.InProcess:
                        if (processResult.Seconds > 30)
                            throw new TimeoutException();
                        continue;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        #endregion

        #region Implementation
        Dictionary<string, DateTime> ProcessStartTime;

        protected AuthApi AuthorizationApi;
        protected Configuration CurrentConfiguration;
        protected string EndpointPath;
        protected int Timeout;
        protected DateTime? BusinessDate;


        protected IEnumerable<EntityField> GetSearchFields<SearchType>(Entity entity)
        {
            Type[] searchTypes = new Type[] { typeof(SearchType) };

            foreach (var field in entity.GetType().GetProperties())
            {
                if (field.GetValue(entity) != null)
                {
                    if (searchTypes.Contains(field.GetValue(entity).GetType()))
                    {
                        yield return new EntityField(field.GetValue(entity).GetType(), (SearchType)field.GetValue(entity), field.Name);
                    }
                }
            }
            foreach (var linkedEntity in GetLinkedEntities(entity))
            {
                if (linkedEntity.Value != null)
                {
                    // search recursively for all Linked entities inside other linked entities
                    foreach (var field in GetSearchFields<SearchType>(linkedEntity.Value))
                    {
                        field.Name = $"{linkedEntity.Name}/{field.Name}";
                        yield return field;
                    }
                }
            }
        }

        protected IEnumerable<EntityField> GetPossibleKeyFields<T>(T entity)
            where T : Entity
        {
            foreach (var field in entity.GetType().GetProperties())
            {
                if (field.PropertyType == typeof(StringValue))
                {
                    yield return new EntityField(field.PropertyType, ((StringValue)field.GetValue(entity))?.Value, field.Name);
                }
                if (field.PropertyType == typeof(IntValue))
                {
                    yield return new EntityField(field.PropertyType, ((IntValue)field.GetValue(entity))?.Value, field.Name);
                }
                if (field.PropertyType == typeof(LongValue))
                {
                    yield return new EntityField(field.PropertyType, ((LongValue)field.GetValue(entity))?.Value, field.Name);
                }
            }
        }

        protected IEnumerable<string> GetSubEntitiesWithReturnBehavior(Type entityType, Entity entity, bool returnAll = false)
        {
            List<string> result = new List<string>();
            foreach (var field in entityType.GetProperties())
            {
                if (typeof(IEnumerable).IsAssignableFrom(field.PropertyType)
                    && field.Name != "Custom"
                    && field.Name != "Files"
                    && field.PropertyType != typeof(CustomField[])
                    && field.PropertyType != typeof(String)
                    )
                {
                    if (returnAll || (entity != null && entity.ReturnBehavior == ReturnBehavior.All))
                    {
                        foreach (var subentity in GetSubEntitiesWithReturnBehavior(GetSubentityType(field), null, true))
                        {
                            result.Add(field.Name + "/" + subentity);
                        }
                        result.Add(field.Name);
                    }
                    else
                    {
                        Entity item = null;
                        if (entity != null && field.GetValue(entity) != null)
                        {
                            foreach (var detail in (IEnumerable)field.GetValue(entity))
                            {
                                if (detail != null)
                                {
                                    item = (Entity)detail;
                                    if (item != null && item.ReturnBehavior == ReturnBehavior.All)
                                    {
                                        foreach (var subentity in GetSubEntitiesWithReturnBehavior(GetSubentityType(field), null, true))
                                        {
                                            result.Add(field.Name + "/" + subentity);
                                        }
                                        result.Add(field.Name);
                                    }
                                    else if (item != null && item.ReturnBehavior == ReturnBehavior.Default)
                                    {
                                        foreach (var subentity in GetSubEntitiesWithReturnBehavior(GetSubentityType(field), item))
                                        {
                                            result.Add(field.Name + "/" + subentity);
                                        }
                                        result.Add(field.Name);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (typeof(Entity).IsAssignableFrom(field.PropertyType))
                {
                    Entity item = null;
                    if (entity != null)
                    {
                        item = (Entity)field.GetValue(entity);
                    }
                    if (returnAll || (entity != null && entity.ReturnBehavior == ReturnBehavior.All) || (item != null && item.ReturnBehavior == ReturnBehavior.All))
                    {
                        foreach (var subentity in GetSubEntitiesWithReturnBehavior(field.PropertyType, null, true))
                        {
                            result.Add(field.Name + "/" + subentity);
                        }
                        result.Add(field.Name);
                    }
                    else if (item != null && item.ReturnBehavior == ReturnBehavior.Default)
                    {
                        foreach (var subentity in GetSubEntitiesWithReturnBehavior(field.PropertyType, item))
                        {
                            result.Add(field.Name + "/" + subentity);
                        }
                        result.Add(field.Name);
                    }
                }
            }

            return result.Distinct();
        }

        private static Type GetSubentityType(PropertyInfo field)
        {
            Type subentityType;
            if (field.PropertyType.Name == "List`1")
            {

                subentityType = field.PropertyType.GetGenericArguments().FirstOrDefault();
            }
            else
            {
                subentityType = field.PropertyType.GetElementType();
            }

            return subentityType;
        }

        protected IEnumerable<LinkedEntity> GetLinkedEntities<T>(T entity)
            where T : Entity
        {
            foreach (var field in entity.GetType().GetProperties())
            {
                if (typeof(Entity).IsAssignableFrom(field.PropertyType))
                {
                    yield return new LinkedEntity(field.PropertyType, (Entity)field.GetValue(entity), field.Name);
                }
            }
        }

        protected string ComposeFilters<T>(T entity, bool addPossibleKeyFields = false) where T : Entity
        {
            return string.Join(" and ",
                ComposeFiltersInternal(entity)
                .Concat(addPossibleKeyFields ?
                    ComposeFiltersForPossibleKeyFieldsInternal(entity)
                    : new string[0]));
        }

        private IEnumerable<string> ComposeFiltersInternal(Entity entity)
        {
            List<string> filters = new List<string>();

            filters.AddRange(GetSearchFields<StringSearch>(entity)
                .Select(GetFilterByStringCondition));

            filters.AddRange(GetSearchFields<IntSearch>(entity)
                .Select(GetFilterByIntCondition));

            filters.AddRange(GetSearchFields<LongSearch>(entity)
                .Select(GetFilterByLongCondition));

            filters.AddRange(GetSearchFields<GuidSearch>(entity)
                .Select(GetFilterByGuidCondition));

            filters.AddRange(GetSearchFields<DecimalSearch>(entity)
                .Select(GetFilterByDecimalCondition));

            filters.AddRange(GetSearchFields<BooleanSearch>(entity)
                .Select(GetFilterByBooleanCondition));

            filters.AddRange(GetSearchFields<DateTimeSearch>(entity)
                .Select(GetFilterByDateTimeCondition));
            return filters;
        }

        private string GetFilterByBooleanCondition(EntityField field)
        {
            BooleanSearch search = (BooleanSearch)field.Value;

            switch (search.Condition)
            {
                case BooleanCondition.IsNotNull: return $"{field.Name} ne null";
                case BooleanCondition.IsNull: return $"{field.Name} eq null";
                case BooleanCondition.Equal: return $"{field.Name} eq {search.Value})";
                case BooleanCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private string GetFilterByDecimalCondition(EntityField field)
        {
            DecimalSearch search = (DecimalSearch)field.Value;

            switch (search.Condition)
            {
                case DecimalCondition.IsNotNull: return $"{field.Name} ne null";
                case DecimalCondition.IsNull: return $"{field.Name} eq null";
                case DecimalCondition.Equal: return $"{field.Name} eq {search.Value})";
                case DecimalCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                case DecimalCondition.IsBetween: return $"({field.Name} ge {search.Value} and {field.Name} le {search.Value2})";
                case DecimalCondition.IsGreaterThan: return $"{field.Name} gt {search.Value}";
                case DecimalCondition.IsGreaterThanOrEqualsTo: return $"{field.Name} ge {search.Value}";
                case DecimalCondition.IsLessThan: return $"{field.Name} lt {search.Value}";
                case DecimalCondition.IsLessThanOrEqualsTo: return $"{field.Name} le {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private string GetFilterByIntCondition(EntityField field)
        {
            IntSearch search = (IntSearch)field.Value;

            switch (search.Condition)
            {
                case IntCondition.IsNotNull: return $"{field.Name} ne null";
                case IntCondition.IsNull: return $"{field.Name} eq null";
                case IntCondition.Equal: return $"{field.Name} eq {search.Value})";
                case IntCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                case IntCondition.IsBetween: return $"({field.Name} ge {search.Value} and {field.Name} le {search.Value2})"; 
                case IntCondition.IsGreaterThan: return $"{field.Name} gt {search.Value}";
                case IntCondition.IsGreaterThanOrEqualsTo: return $"{field.Name} ge {search.Value}";
                case IntCondition.IsLessThan: return $"{field.Name} lt {search.Value}";
                case IntCondition.IsLessThanOrEqualsTo: return $"{field.Name} le {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private string GetFilterByLongCondition(EntityField field)
        {
            LongSearch search = (LongSearch)field.Value;

            switch (search.Condition)
            {
                case LongCondition.IsNotNull: return $"{field.Name} ne null";
                case LongCondition.IsNull: return $"{field.Name} eq null";
                case LongCondition.Equal: return $"{field.Name} eq {search.Value})";
                case LongCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                case LongCondition.IsBetween: return $"({field.Name} ge {search.Value} and {field.Name} le {search.Value2})";
                case LongCondition.IsGreaterThan: return $"{field.Name} gt {search.Value}";
                case LongCondition.IsGreaterThanOrEqualsTo: return $"{field.Name} ge {search.Value}";
                case LongCondition.IsLessThan: return $"{field.Name} lt {search.Value}";
                case LongCondition.IsLessThanOrEqualsTo: return $"{field.Name} le {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }

        private string GetFilterByGuidCondition(EntityField field)
        {
            GuidSearch search = (GuidSearch)field.Value;

            switch (search.Condition)
            {
                case GuidCondition.IsNotNull: return $"{field.Name} ne null";
                case GuidCondition.IsNull: return $"{field.Name} eq null";
                case GuidCondition.Equal: return $"{field.Name} eq {search.Value})";
                case GuidCondition.NotEqual: return $"{field.Name} ne {search.Value}";
                default: throw new NotImplementedException($"Condition {search.Condition} is not implemented");
            }
        }


        private string GetFilterByStringCondition(EntityField field)
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

        private IEnumerable<string> ComposeFiltersForPossibleKeyFieldsInternal<T>(T entity) where T : Entity
        {
            return GetPossibleKeyFields(entity)
                            .Where(f => f.Value != null)
                            .Select(field =>
                            {
                                if (field.Type == typeof(StringValue))
                                    return $"{field.Name} eq '{field.Value}'";
                                else
                                    return $"{field.Name} eq {field.Value}";
                            });
        }

        protected string ComposeExpands<T>(T entity, bool addFiles = false) where T : Entity
        {
            return string.Join(",",
                GetSubEntitiesWithReturnBehavior(entity.GetType(), entity)
                .Concat(addFiles ? new[] { "files" } : new string[0]));
        }
        protected int GetProcessingSeconds(string invokeResult)
        {
            if (ProcessStartTime.ContainsKey(invokeResult))
            {
                return (int)(DateTime.Now - ProcessStartTime[invokeResult]).TotalSeconds;
            }
            return 0;
        }
        #endregion

        #region Auxiliary

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

        #endregion
    }
}