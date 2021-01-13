using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Workflows.ServiceProvider.Extensions.CosmosDB
{
    public static class ReceiveDocument
    {
        public static readonly ServiceOperationManifest Manifiest = new ServiceOperationManifest
        {
            ConnectionReference = new ConnectionReferenceFormat
            {
                ReferenceKeyFormat = ConnectionReferenceKeyFormat.ServiceProvider,
            },
            Settings = new OperationManifestSettings
            {
                SecureData = new OperationManifestSettingWithOptions<SecureDataOptions>(),
                TrackedProperties = new OperationManifestSetting
                {
                    Scopes = new OperationScope[] { OperationScope.Trigger },
                },
            },
            InputsLocation = new InputsLocation[]
            {
                InputsLocation.Inputs,
                InputsLocation.Parameters,
            },
            Outputs = new SwaggerSchema
            {
                Type = SwaggerSchemaType.Object,
                Properties = new OrdinalDictionary<SwaggerSchema>
                {
                    {
                        "body", new SwaggerSchema
                        {
                            Type = SwaggerSchemaType.Array,
                            Title = "Receive document",
                            Description = "Receive document description",
                            Items = new SwaggerSchema
                            {
                                Type = SwaggerSchemaType.Object,
                                Properties = new OrdinalDictionary<SwaggerSchema>
                                {
                                    {
                                        "contentData", new SwaggerSchema
                                        {
                                            Type = SwaggerSchemaType.String,
                                            Title = "Content",
                                            Format = "byte",
                                            Description = "content",
                                        }
                                    },
                                    {
                                        "Properties", new SwaggerSchema
                                        {
                                            Type = SwaggerSchemaType.Object,
                                            Title = "documentProperties",
                                            AdditionalProperties = new JObject
                                            {
                                                { "type", "object" },
                                                { "properties", new JObject { } },
                                                { "required", new JObject { } },
                                            },
                                            Description = "document data properties",
                                        }
                                    },
                                },
                            },
                        }
                    },
                },
            },
            Inputs = new SwaggerSchema
            {
                Type = SwaggerSchemaType.Object,
                Properties = new OrdinalDictionary<SwaggerSchema>
                {
                    {
                        "databaseName", new SwaggerSchema
                        {
                            Type = SwaggerSchemaType.String,
                            Title = "database name",
                            Description = "database name",
                        }
                    },
                    {
                         "collectionName", new SwaggerSchema
                        {
                            Type = SwaggerSchemaType.String,
                            Title = "collection name",
                            Description = "collection name",
                        }
                    },
                },
                Required = new string[]
                {
                    "databaseName",
                },
            },
            Connector = CosmosDbApiFactory.CosmosDbOperationApi,
            Trigger = TriggerType.Batch,
            Recurrence = new RecurrenceSetting
            {
                Type = RecurrenceType.None,
            },
        };

        /// <summary>
        /// The receive documents operation.
        /// </summary>
        public static readonly ServiceOperation Operation = new ServiceOperation
        {
            Name = "receiveDocument",
            Id = "receiveDocument",
            Type = "receiveDocument",
            Properties = new ServiceOperationProperties
            {
                Api = CosmosDbApiFactory.CosmosDbOperationApi.GetFlattenedApi(),
                Summary = "receive document",
                Description = "receive document",
                Visibility = Visibility.Important,
                OperationType = OperationType.ServiceProvider,
                BrandColor = 0x1C3A56,
                IconUri = new Uri("https://raw.githubusercontent.com/praveensri/LogicAppCustomConnector/main/ServiceProviders.CosmosDb.Extensions/icon.png"),
                Trigger = TriggerType.Batch,
            },
        };

    }
}
