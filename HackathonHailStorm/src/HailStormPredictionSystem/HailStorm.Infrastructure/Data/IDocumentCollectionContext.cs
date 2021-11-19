using HailStorm.Core.Models;
using Microsoft.Azure.Documents;

namespace HailStorm.Infrastructure.Data
{
    public interface IDocumentCollectionContext<in T> where T : Entity
    {
        string CollectionName { get; }
        string GenerateId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
