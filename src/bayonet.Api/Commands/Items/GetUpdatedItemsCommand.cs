using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Core.Patterns;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Items
{
    public class GetUpdatedItemsCommand : Command<Result<IEnumerable<Item>>>
    {
        private readonly IWebService webService;
        private readonly int count;

        public GetUpdatedItemsCommand(IWebService webService, int count)
        {
            this.webService = webService;
            this.count = count;
        }

        public override async Task<Result<IEnumerable<Item>>> ExecuteAsync()
        {
            try
            {
                var updates = await this.webService.GetContentAsync<Updates>(Constants.UpdatesEndpoint);
                var updatedItems = new List<Item>();
                foreach (var itemId in updates.Items.Take(this.count))
                {
                    var getItemCommand = new GetItemCommand(this.webService, itemId);
                    var getItemCommandResult = await getItemCommand.ExecuteAsync();
                    updatedItems.Add(getItemCommandResult.Value);
                }
                return new Result<IEnumerable<Item>>()
                {
                    Value = updatedItems
                };
            }
            catch(Exception ex)
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
