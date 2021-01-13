using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.Workflows.ServiceProvider.Extensions.CosmosDB;
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.Azure.Workflows.ServiceProviders.WebJobs.Abstractions.Providers;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Extensions;
using Microsoft.WindowsAzure.ResourceStack.Common.Storage.Cosmos;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceProviders.CosmosDb.Extensions
{
    [ServiceOperationsProvider(Id = CosmosDbTriggerServiceOperationProvider.ServiceId, Name = CosmosDbTriggerServiceOperationProvider.ServiceName)]
    public class CosmosDbTriggerServiceOperationProvider : IServiceOperationsTriggerProvider
    {
        /// <summary>
        /// The service name.
        /// </summary>
        public const string ServiceName = "cosmosdb";

        /// <summary>
        /// The service id.
        /// </summary>
        public const string ServiceId = "/serviceProviders/cosmosdb";

        /// <summary>
        /// Gets or sets service Operations.
        /// </summary>
        private readonly List<ServiceOperation> serviceOperationsList;

        /// <summary>
        /// The set of all API Operations.
        /// </summary>
        private readonly InsensitiveDictionary<ServiceOperation> apiOperationsList;

        public CosmosDbTriggerServiceOperationProvider()
        {
            this.serviceOperationsList = new List<ServiceOperation>();
            this.apiOperationsList = new InsensitiveDictionary<ServiceOperation>();

            this.apiOperationsList.AddRange(new InsensitiveDictionary<ServiceOperation>
            {
                { "receiveDocument", ReceiveDocument.Operation },
                { "debugTest", DebugTest.Operation }
            });

            this.serviceOperationsList.AddRange(new List<ServiceOperation>
            {
                { ReceiveDocument.Operation.CloneWithManifest(ReceiveDocument.Manifiest
                    ) },
                { DebugTest.Operation.CloneWithManifest(DebugTest.Manifiest
                    ) },
            });
        }

        public string GetBindingConnectionInformation(string operationId, InsensitiveDictionary<JToken> connectionParameters)
        {
            return ServiceOperationsProviderUtilities
                    .GetRequiredParameterValue(
                        serviceId: ServiceId,
                        operationId: operationId,
                        parameterName: "connectionString",
                        parameters: connectionParameters)?
                    .ToValue<string>();
        }

        public string GetFunctionTriggerType()
        {
            return "cosmosDBTrigger";
        }

        public IEnumerable<ServiceOperation> GetOperations(bool expandManifest)
        {
            return expandManifest ? serviceOperationsList : GetApiOperations();
        }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        private IEnumerable<ServiceOperation> GetApiOperations()
        {
            return this.apiOperationsList.Values;
        }

        public ServiceOperationApi GetService()
        {
            return CosmosDbApiFactory.CosmosDbOperationApi;
        }

        public Task<ServiceOperationResponse> InvokeActionOperation(string operationId, InsensitiveDictionary<JToken> connectionParameters, ServiceOperationRequest serviceOperationRequest)
        {
            return Task.Run(() =>
            {                
                return new ServiceOperationResponse(new JObject
                {
                    { "outputParam",  serviceOperationRequest.Parameters["inputParam"].ToString() },
                    { "operationId",  operationId }
                    
                });
            });
        }

        

        
    }
}
