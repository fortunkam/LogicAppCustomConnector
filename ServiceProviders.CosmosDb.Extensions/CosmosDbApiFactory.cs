using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Workflows.ServiceProvider.Extensions.CosmosDB
{
    public static class CosmosDbApiFactory
    {
        /// <summary>
        /// The Azure cosmos db API.
        /// </summary>
        public static ServiceOperationApi CosmosDbOperationApi = new ServiceOperationApi
        {
            Name = "cosmosdb",
            Id = "/serviceProviders/cosmosdb",
            Type = DesignerApiType.ServiceProvider,
            Properties = new ServiceOperationApiProperties
            {
                BrandColor = 0xC4D5FF,
                Description = "Connect to Azure Cosmos db to receive document.",
                DisplayName = "Cosmos Db",
                IconUri = new Uri("https://raw.githubusercontent.com/praveensri/LogicAppCustomConnector/main/ServiceProviders.CosmosDb.Extensions/icon.png"),
                Capabilities = new ApiCapability[] { ApiCapability.Triggers, ApiCapability.Actions },
                ConnectionParameters = new ConnectionParameters
                {
                    ConnectionString = new ConnectionStringParameters
                    {
                        Type = ConnectionStringType.SecureString,
                        ParameterSource = ConnectionParameterSource.AppConfiguration,
                        UIDefinition = new UIDefinition
                        {
                            DisplayName = "Connection String",
                            Description = "Azure Cosmos db Connection String",
                            Tooltip = "Provide Azure Cosmos db Connection String",
                            Constraints = new Constraints
                            {
                                Required = "true",
                            },
                        },
                    },
                },
            },
        };
    }
}
