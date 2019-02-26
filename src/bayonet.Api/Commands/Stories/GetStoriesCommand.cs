using bayonet.Api.Commands.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Core.Patterns;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Stories
{
    public class GetStoriesCommand : Command<Result<IEnumerable<Item>>>
    {
        private readonly IWebService webService;
        private readonly string storyType;
        private readonly int count;
        
        public GetStoriesCommand(
            IWebService webService,
            string storyType,
            int count)
        {
            this.webService = webService;
            this.storyType = BayonetHelper.FormatStoryType(storyType);
            this.count = count;
        }

        public override async Task<Result<IEnumerable<Item>>> ExecuteAsync()
        {
            try
            {
                if(!BayonetHelper.ValidateStoryType(storyType))
                {
                    return new Result<IEnumerable<Item>>()
                    {
                        IsError = true,
                        ErrorMessage = "Invalid Story Type."
                    };
                }
                var stories = new List<Item>();
                var storyIds = await this.webService.GetContentAsync<IEnumerable<string>>(Constants.StoriesEndpoint.Replace(Constants.Bayonet, this.storyType));
                foreach (var id in storyIds.Take(this.count))
                {
                    var getItemCommand = new GetItemCommand(webService, id);
                    var getItemCommandResult = await getItemCommand.ExecuteAsync();
                    stories.Add(getItemCommandResult.Value);
                }

                return new Result<IEnumerable<Item>>()
                {
                    Value = stories
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
