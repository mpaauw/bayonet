using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Core.Patterns;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands
{
    public class GetMaxItemCommand : Command<Result<Item>>
    {
        private readonly IWebService webService;

        public GetMaxItemCommand(IWebService webService)
        {
            this.webService = webService;
        }

        public override async Task<Result<Item>> ExecuteAsync()
        {
            try
            {
                string id = await this.webService.GetContentAsync<string>(Constants.MaxItemEndpoint);
                var getItemCommand = new GetItemCommand(this.webService, id);
                var getItemCommandResult = await getItemCommand.ExecuteAsync();
                var item = getItemCommandResult.Value;
                return new Result<Item>()
                {
                    Value = item
                };
            }
            catch(Exception ex)
            {
                return new Result<Item>()
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
