using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackerNewsApi.Models;

namespace HackerNewsApi.Utilities
{
    public static class Numbering
    {
        public static List<Story> Get(List<Story> list)
        {
            var numbered = new List<Story>();
            var num = 0;

            foreach (var item in list)
            {
                num += 1;
                numbered.Add(new Story
                {
                    Url = item.Url,
                    Author = item.Author,
                    Title = item.Title,
                    Number = num + "."
                });
            }

            return numbered;
        }
    }
}