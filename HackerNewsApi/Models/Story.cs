using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackerNewsApi.Models
{
    public class Story
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string Score { get; set; }
    }
}