using bayonet.Core.Common;
using bayonet.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bayonet.Client.Functions.Items
{
    public interface IItemFunctions
    {
        Task<Result<Item>> GetItem(string id);

        Task<Result<Item>> GetMaxItem();

        Task<Result<IEnumerable<Item>>> GetUpdatedItems(int count);
    }
}
