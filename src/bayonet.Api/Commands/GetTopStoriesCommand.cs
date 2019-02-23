using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands
{
    public class GetTopStoriesCommand
    {
        private readonly int count;
        private readonly IWebService webService;

        private const string TopStoriesEndpoint = @"https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
        private const string StoryEndpoint = @"https://hacker-news.firebaseio.com/v0/item/BAYONET.json?print=pretty";

        public GetTopStoriesCommand(int count)
        {
            this.count = count;
            this.webService = new WebService();
        }

        public async Task<Result<IEnumerable<Item>>> ExecuteAsync()
        {
            var topStories = new List<Item>();
            try
            {
                var rawStoryIds = await this.webService.GetContentAsync<IEnumerable<int>>(TopStoriesEndpoint);
                foreach(var id in rawStoryIds.Take(this.count))
                {
                    var story = await this.webService.GetContentAsync<Item>(StoryEndpoint.Replace("BAYONET", id.ToString()));
                    topStories.Add(story);
                }

                return new Result<IEnumerable<Item>>()
                {
                    Value = topStories
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
