using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Core.Patterns;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands
{
    public class GetTopStoriesCommand : bayonet.Core.Patterns.Command<Result<IEnumerable<Item>>>
    {
        private readonly IWebService webService;
        private readonly int count;
        
        public GetTopStoriesCommand(IWebService webService, int count)
        {
            this.webService = webService;
            this.count = count;
        }

        public override async Task<Result<IEnumerable<Item>>> ExecuteAsync()
        {
            var topStories = new List<Item>();
            try
            {
                var rawStoryIds = await this.webService.GetContentAsync<IEnumerable<int>>(Constants.TopStoriesEndpoint);
                foreach (var id in rawStoryIds.Take(this.count))
                {
                    var story = await this.webService.GetContentAsync<Item>(Constants.StoryEndpoint.Replace(Constants.Bayonet, id.ToString()));
                    topStories.Add(story);
                }

                return new Result<IEnumerable<Item>>()
                {
                    Value = topStories
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
