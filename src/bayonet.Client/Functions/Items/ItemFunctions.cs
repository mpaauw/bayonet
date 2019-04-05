using bayonet.Core.Common;
using bayonet.Core.Models;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bayonet.Client.Functions.Items
{
    public class ItemFunctions : IItemFunctions
    {
        private readonly IFlurlClient flurlClient;

        public ItemFunctions(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public async Task<Result<Item>> GetItem(string id)
        {
            var result = await this.flurlClient
                .Request(
                Constants.ApiSegment,
                Constants.ItemsSegment,
                id)
                .GetJsonAsync<Result<Item>>();
            return result;
        }

        public async Task<Result<Item>> GetMaxItem()
        {
            return await this.flurlClient
                .Request(
                Constants.ApiSegment,
                Constants.ItemsSegment,
                Constants.GetMaxItemSegment)
                .GetJsonAsync<Result<Item>>();
        }

        public async Task<Result<IEnumerable<Item>>> GetUpdatedItems(int count)
        {
            return await this.flurlClient
                .Request(
                Constants.ApiSegment,
                Constants.ItemsSegment,
                Constants.UpdatesSegment,
                count)
                .GetJsonAsync<Result<IEnumerable<Item>>>();
        }
    }
}
