using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackerNewsApi.Utilities
{
    public static class Utils
    {
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";
        public const string TopStories = BaseUrl + "topstories.json";
        public const string BestStories = BaseUrl + "beststories.json";
        public const string Item = BaseUrl + "item/{0}.json";
    }
}