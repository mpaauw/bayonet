using AndyC.Patterns.Commands;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Items
{
    public class GetUpdatedItemsCommand : IFunction<Result<IEnumerable<Item>>>
    {
        private readonly int count;

        public GetUpdatedItemsCommand(int count)
        {
            this.count = count;
        }

        public class Handler : IFunctionHandlerAsync<GetUpdatedItemsCommand, Result<IEnumerable<Item>>>
        {
            private readonly IWebService webService;

            public Handler(IWebService webService)
            {
                this.webService = webService;
            }

            public async Task<Result<IEnumerable<Item>>> ExecuteAsync(GetUpdatedItemsCommand function)
            {
                try
                {
                    var updates = await this.webService.GetContentAsync<Updates>(Constants.UpdatesEndpoint);
                    var updatedItems = new List<Item>();
                    foreach (var itemId in updates.Items.Take(function.count))
                    {
                        var item = await this.webService.GetContentAsync<Item>(Constants.ItemEndpoint.Replace(Constants.Bayonet, itemId));
                        updatedItems.Add(item);
                    }
                    return new Result<IEnumerable<Item>>()
                    {
                        Value = updatedItems
                    };
                }
                catch (Exception ex)
                {
                    return new Result<IEnumerable<Item>>()
                    {
                        IsError = true,
                        ErrorMessage = ex.Message
                    };
                }
            }
        }
    }
}
