using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Core.Patterns;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Items
{
    public class GetItemCommand : Command<Result<Item>>
    {
        private readonly IWebService webService;
        private readonly string id;

        public GetItemCommand(IWebService webService, string id)
        {
            this.webService = webService;
            this.id = id;
        }

        public override async Task<Result<Item>> ExecuteAsync()
        {
            try
            {
                var item = await this.webService.GetContentAsync<Item>(Constants.ItemEndpoint.Replace(Constants.Bayonet, this.id));
                return new Result<Item>()
                {
                    Value = item,
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
