using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HailStorm.Infrastructure.Data
{
    public interface ICosmosDbClientFactory
    {
        ICosmosDbClient GetClient(string collectionName);
    }
}
