using System;
using System.Collections.Generic;
using System.Text;

namespace bayonet.Core.Common
{
    public static class Constants
    {
        public const string Bayonet = "BAYONET";

        public const string StoriesEndpoint = @"https://hacker-news.firebaseio.com/v0/BAYONETstories.json?print=pretty";
        public const string ItemEndpoint = @"https://hacker-news.firebaseio.com/v0/item/BAYONET.json?print=pretty";
        public const string MaxItemEndpoint = @"https://hacker-news.firebaseio.com/v0/maxitem.json?print=pretty";
    }
}
