using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace Microsoft.Azure.Workflows.ServiceProvider.Extensions.CosmosDB
{
    public static class DebugTest
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
                    Scopes = new OperationScope[] { OperationScope.Action },
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
                    { "body", new SwaggerSchema
                        {
                            Type = SwaggerSchemaType.Object,
                             Properties = new OrdinalDictionary<SwaggerSchema>
                             {
                                 {
                                    "outputParam", new SwaggerSchema
                                    {
                                        Type = SwaggerSchemaType.String,
                                        Title = "Output Param",
                                        Description = "Output Parameter"

                                    }
                                },
                                {
                                    "operationId", new SwaggerSchema
                                    {
                                        Type = SwaggerSchemaType.String,
                                        Title = "Operation Id",
                                        Description = "Operation Id"

                                    }
                                }
                             }
                        }
                    }
                }                
            },            
            Inputs = new SwaggerSchema
            {
                Type = SwaggerSchemaType.Object,
                Properties = new OrdinalDictionary<SwaggerSchema>
                {
                    {
                        "inputParam", new SwaggerSchema
                        {
                            Type = SwaggerSchemaType.String,
                            Title = "matt Debug Test Param 1 Title",
                            Description = "Matt Debug Test Output Description",
                        }
                    }
                },
                Required = new string[]
               {
                    "inputParam",
               },
            },
            Connector = CosmosDbApiFactory.CosmosDbOperationApi
        };

        /// <summary>
        /// The debug action.
        /// </summary>
        public static readonly ServiceOperation Operation = new ServiceOperation
        {
            Name = "mattDebugTest",
            Id = "mattDebugTest",
            Type = "mattDebugTest",
            Properties = new ServiceOperationProperties
            {
                Api = CosmosDbApiFactory.CosmosDbOperationApi.GetFlattenedApi(),
                Summary = "Matts Debug Test",
                Description = "Matts Debug Test",
                Visibility = Visibility.Important,
                //This is the magic setting that makes it an Action
                OperationType = OperationType.ServiceProvider,
                BrandColor = 0x1C3A56,
                IconUri = new Uri("https://raw.githubusercontent.com/praveensri/LogicAppCustomConnector/main/ServiceProviders.CosmosDb.Extensions/icon.png")
            },
        };
    }
}
