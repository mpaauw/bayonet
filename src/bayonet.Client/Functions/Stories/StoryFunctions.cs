using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bayonet.Core.Common;
using bayonet.Core.Models;
using Flurl.Http;

namespace bayonet.Client.Functions.Stories
{
    public class StoryFunctions : IStoryFunctions
    {
        private readonly IFlurlClient flurlClient;

        public StoryFunctions(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public async Task<Result<IEnumerable<Item>>> GetStories(string storyType, int count)
        {
            return await this.flurlClient
                .Request(
                Constants.ApiSegment,
                Constants.StoriesSegment,
                storyType,
                count)
                .GetJsonAsync<Result<IEnumerable<Item>>>();
        }
    }
}
