using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNewsApi.Models;

namespace HackerNewsApi.Tests.Models
{
    public static class StoriesTest
    {
        public static IEnumerable<Story> Get()
        {
            return new List<Story>()
            {
                new Story{ Author = "Ben", Number = "1", Score = null, Title = "News item 1", Url = "www.google.com"},
                new Story{ Author = "Writer 1", Number = "2", Score = null, Title = "News item 2", Url = "www.google.com"},
                new Story{ Author = "Another writer", Number = "3", Score = null, Title = "News item 3", Url = "www.google.com"},
                new Story{ Author = "Writer100", Number = "4", Score = null, Title = "News item 4", Url = "www.google.com"},
                new Story{ Author = "Test", Number = "5", Score = null, Title = "News item 5", Url = "www.google.com"},
                new Story{ Author = "_teacher", Number = "6", Score = null, Title = "News item 6", Url = "www.google.com"},
                new Story{ Author = "zee", Number = "7", Score = null, Title = "News item 7", Url = "www.google.com"},
                new Story{ Author = "John", Number = "8", Score = null, Title = "News item 8", Url = "www.google.com"},
                new Story{ Author = "Google_blogger", Number = "9", Score = null, Title = "News item 9", Url = "www.google.com"},
                new Story{ Author = "querer", Number = "10", Score = null, Title = "News item 10", Url = "www.google.com"}
            };
        }
    }
}
