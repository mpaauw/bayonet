using System;
using System.Collections.Generic;
using System.Text;

namespace bayonet.Core.Common
{
    public static class Constants
    {
        public const string Bayonet = "BAYONET";

        public const string TopStoriesEndpoint = @"https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
        public const string StoryEndpoint = @"https://hacker-news.firebaseio.com/v0/item/BAYONET.json?print=pretty";
    }
}
