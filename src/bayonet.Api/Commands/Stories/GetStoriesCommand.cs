using AndyC.Patterns.Commands;
using bayonet.Api.Commands.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Stories
{
    public class GetStoriesCommand : IFunction<Result<IEnumerable<Item>>>
    {
        private readonly string storyType;
        private readonly int count;
        
        public GetStoriesCommand(string storyType, int count)
        {
            this.storyType = BayonetHelper.FormatStoryType(storyType);
            this.count = count;
        }

        public class Handler : IFunctionHandlerAsync<GetStoriesCommand, Result<IEnumerable<Item>>>
        {
            private readonly IWebService webService;

            public Handler(IWebService webService)
            {
                this.webService = webService;
            }

            public async Task<Result<IEnumerable<Item>>> ExecuteAsync(GetStoriesCommand function)
            {
                try
                {
                    if (!BayonetHelper.ValidateStoryType(function.storyType))
                    {
                        return new Result<IEnumerable<Item>>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            IsError = true,
                            ErrorMessage = "Invalid Story Type."
                        };
                    }
                    if(!BayonetHelper.ValidateCount(function.count))
                    {
                        return new Result<IEnumerable<Item>>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            IsError = true,
                            ErrorMessage = "Invalid count."
                        };
                    }
                    var stories = new List<Item>();
                    var storyIds = await this.webService.GetContentAsync<IEnumerable<string>>(Constants.StoriesEndpoint.Replace(Constants.Bayonet, function.storyType));
                    foreach (var id in storyIds.Take(function.count))
                    {
                        var item = await this.webService.GetContentAsync<Item>(Constants.ItemEndpoint.Replace(Constants.Bayonet, id));
                        stories.Add(item);
                    }

                    return new Result<IEnumerable<Item>>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Value = stories
                    };
                }
                catch (Exception ex)
                {
                    return new Result<IEnumerable<Item>>()
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
