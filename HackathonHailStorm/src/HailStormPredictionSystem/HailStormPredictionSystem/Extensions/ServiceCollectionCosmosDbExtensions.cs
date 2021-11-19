using HailStorm.Core.Interfaces;
using HailStorm.Infrastructure.Data;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HailStormPredictionSystem.Extensions
{
    public static class ServiceCollectionCosmosDbExtensions
    {
        private static DocumentClient documentClient;
        public static IServiceCollection AddCosmosDb(this IServiceCollection services, Uri serviceEndpoint,
            string authKey, string databaseName, List<string> collectionNames)
        {
            documentClient = new DocumentClient(
                serviceEndpoint,
                authKey,
                new ConnectionPolicy
                {
                    ConnectionMode = ConnectionMode.Direct,
                    ConnectionProtocol = Protocol.Tcp
                });

            var cosmosDbClientFactory = new CosmosDbClientFactory(databaseName, collectionNames, documentClient);
            cosmosDbClientFactory.EnsureDbSetupAsync().Wait();

            services.AddSingleton<ICosmosDbClientFactory>(cosmosDbClientFactory);
            services.AddSingleton<IHailItemRepository, HailItemRepository>();

            return services;
        }
    }
}
