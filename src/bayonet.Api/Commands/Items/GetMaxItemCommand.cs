using AndyC.Patterns.Commands;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Net;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Items
{
    public class GetMaxItemCommand : IFunction<Result<Item>>
    {
        public class Handler : IFunctionHandlerAsync<GetMaxItemCommand, Result<Item>>
        {
            private readonly IWebService webService;

            public Handler(IWebService webService)
            {
                this.webService = webService;
            }

            public async Task<Result<Item>> ExecuteAsync(GetMaxItemCommand function)
            {
                try
                {
                    string id = await this.webService.GetContentAsync<string>(Constants.MaxItemEndpoint);
                    var item = await this.webService.GetContentAsync<Item>(Constants.ItemEndpoint.Replace(Constants.Bayonet, id));
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
