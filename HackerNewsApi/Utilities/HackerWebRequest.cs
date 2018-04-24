using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace HackerNewsApi.Utilities
{
    public class HackerWebRequest
    {
        public HttpWebRequest Get()
        {
            var request = (System.Net.HttpWebRequest)WebRequest.Create(Utils.BestStories);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            return request;
        }

        public HttpWebRequest Get(string id)
        {
            var request = (HttpWebRequest)WebRequest.Create(String.Format(Utils.Item, id));
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            return request;
        }
    }
}