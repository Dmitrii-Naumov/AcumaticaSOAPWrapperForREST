using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

using Acumatica.Auth.Api;
using Acumatica.Auth.Model;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.FileApi;
using Acumatica.RESTClient.Model;

using RestSharp;

using SOAPLikeWrapperForREST.Helpers;

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
        public SOAPLikeClient(
            string siteURL, 
            string endpointPath, 
            int timeout = 10000, 
            Action<RestRequest, RestClient> requestInterceptor = null, 
            Action<RestRequest, RestResponse, RestClient> responseInterceptor = null)
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
        public SOAPLikeClient(string endpointURL, 
            int timeout = 10000, 
            Action<RestRequest, RestClient> 
            requestInterceptor = null, 
            Action<RestRequest, RestResponse, RestClient> responseInterceptor = null)
            : this(EndpointPathHelper.TakeSiteURL(EndpointPathHelper.TrimRedundantPartsOfTheURL(endpointURL)),
                   EndpointPathHelper.TakeEndpointPath(EndpointPathHelper.TrimRedundantPartsOfTheURL(endpointURL)),
                   timeout,
                   requestInterceptor,
                   responseInterceptor)
        { }
        #endregion

        #region State
        protected AuthApi AuthorizationApi;
        protected Configuration CurrentConfiguration;
        protected string EndpointPath;
        protected int Timeout;
        protected DateTime? BusinessDate;
        protected Dictionary<string, DateTime> ProcessStartTime;
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
            ProcessStartTime = null;
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <remarks>Can execute several REST API calls internally</remarks>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Obsolete("Get method is for backward compatibility with SOAP only. Use one of the following REST methods instead: GetList, GetByKeys, GetByID")]
        public T Get<T>(T entity)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);

            T result = api.GetById(GetRecordIDViaGetList(entity, api),
                filter: ComposeFilters(entity),
                expand: ComposeExpands(entity),
                custom: ComposeCustomParameters(entity),
                select: ComposeSelects(entity)
                );
            result.ReturnBehavior = entity.ReturnBehavior;
            return result;
        }

       

        public T[] GetList<T>(T entity, int? top = null, int? skip = null, Dictionary<string, string> customHeaders = null)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);

            var result = api.GetList(
                filter: ComposeFilters(entity),
                expand: ComposeExpands(entity),
                custom: ComposeCustomParameters(entity),
                select: ComposeSelects(entity),
                skip: skip,
                top: top,
                customHeaders: customHeaders);
            foreach (var record in result)
            {
                record.ReturnBehavior = entity.ReturnBehavior;
            }
            return result.ToArray();
        }


        public T Put<T>(T entity)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            var result = api.PutEntity(entity,
                filter: ComposeFilters(entity),
                expand: ComposeExpands(entity),
                custom: ComposeCustomParameters(entity),
                select: ComposeSelects(entity),
                businessDate: BusinessDate); ;
            result.ReturnBehavior = entity.ReturnBehavior;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <param name="files"></param>
        /// <remarks>Can execute several REST API calls internally</remarks>
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        /// <remarks>Can execute several REST API calls internally</remarks>
        [Obsolete("GetFiles method is for backward compatibility with SOAP only. Use one of the following REST methods instead: FileApi.GetFile")]
        public File[] GetFiles<T>(T entity)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);

            T record = api.GetById(GetRecordIDViaGetList(entity, api),
                expand: ExpandsHelper.ComposeFilesExpand(entity)
              );
            if (record?.Files == null)
            {
                throw new ApiException(500, "Failed to retrieve files");
            }
            if (record.Files.Count == 0)
            {
                return new File[0];
            }

            var filesArray = new File[record.Files.Count];
            FileApi fileApi = new FileApi(CurrentConfiguration);

            for (int i = 0; i < filesArray.Length; i++)
            {
                filesArray[i] = new File();
                filesArray[i].Name = record.Files[i].Filename;
                using (var sourceStream = fileApi.GetFile(record.Files[i]))
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <remarks>Can execute several REST API calls internally</remarks>
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

        public Entity GetCustomFieldSchema<T>(T entity)
            where T : Entity
        {
            SOAPLikeEntityAPI<T> api = new SOAPLikeEntityAPI<T>(CurrentConfiguration, EndpointPath);
            return api.GetAdHocSchema();
        }
        #endregion

        #region Implementation
        protected virtual string ComposeFilters<T>(T entity, bool addPossibleKeyFields = false) where T : Entity
        {
            return FiltersHelper.ComposeFilters(entity, addPossibleKeyFields);
        }

        protected virtual string ComposeExpands<T>(T entity) where T : Entity
        {
            return ExpandsHelper.ComposeExpands(entity);
        }
        protected virtual string ComposeSelects<T>(T entity) where T : Entity
        {
            return SelectsHelper.ComposeSelects(entity);
        }

        protected virtual string ComposeCustomParameters<T>(T entity) where T : Entity
        {
            return CustomFieldsHelper.ComposeCustomParameter(entity);
        }

        private Guid GetRecordIDViaGetList<T>(T entity, SOAPLikeEntityAPI<T> api) where T : Entity
        {
            if (entity.ID.HasValue)
            {
                return entity.ID.Value;
            }
            else
            {
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
                return list[0].ID.Value;
            }
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
    }
}