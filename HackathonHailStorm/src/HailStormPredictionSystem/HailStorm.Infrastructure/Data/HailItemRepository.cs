using System;
using Microsoft.Azure.Documents;
using HailStorm.Core.Interfaces;
using HailStorm.Core.Models;

namespace HailStorm.Infrastructure.Data
{
    public class HailItemRepository : CosmosDbRepository<HailItem>, IHailItemRepository
    {
        public HailItemRepository(ICosmosDbClientFactory factory) : base(factory) { }

        public override string CollectionName { get; } = "HailItems";
        public override string GenerateId(HailItem entity) => $"{entity.Id}:{Guid.NewGuid()}";
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);//.Split(':')[0]
    }
}
