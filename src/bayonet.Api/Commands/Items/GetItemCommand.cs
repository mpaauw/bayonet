using AndyC.Patterns.Commands;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Net;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Items
{
    public class GetItemCommand : IFunction<Result<Item>>
    {
        private readonly string id;
        private readonly bool retrieveChildren;

        public GetItemCommand(string id, bool retrieveChildren = false)
        {
            this.id = id;
            this.retrieveChildren = retrieveChildren;
        }

        public class Handler : IFunctionHandlerAsync<GetItemCommand, Result<Item>>
        {
            private readonly IWebService webService;

            public Handler(IWebService webService)
            {
                this.webService = webService;
            }

            public async Task<Result<Item>> ExecuteAsync(GetItemCommand function)
            {
                try
                {
                    if(!BayonetHelper.ValidateId(function.id))
                    {
                        return new Result<Item>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            IsError = true,
                            ErrorMessage = "Invalid id."
                        };
                    }
                    var item = (!function.retrieveChildren)
                        ? await this.webService.GetContentAsync<Item>(Constants.ItemEndpoint.Replace(Constants.Bayonet, function.id))
                        : await RetrieveItemChildrenRecursively(function.id);
                    return new Result<Item>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Value = item
                    };
                }
                catch (Exception ex)
                {
                    return new Result<Item>()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        IsError = true,
                        ErrorMessage = ex.Message
                    };
                }
            }

            private async Task<Item> RetrieveItemChildrenRecursively(string id)
            {
                var item = await this.webService.GetContentAsync<Item>(Constants.ItemEndpoint.Replace(Constants.Bayonet, id));
                if ((item.Kids is null) || (item.Kids.Length < 1))
                {
                    return item;
                }
                item.Children = new Item[item.Kids.Length];
                for (int i = 0; i < item.Kids.Length; i++)
                {
                    item.Children[i] = await RetrieveItemChildrenRecursively(item.Kids[i]);
                }
                return item;
            }
        }


    }
}
