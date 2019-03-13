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

        public GetItemCommand(string id)
        {
            this.id = id;
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
                    var item = await this.webService.GetContentAsync<Item>(Constants.ItemEndpoint.Replace(Constants.Bayonet, function.id));
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
        }
    }
}
