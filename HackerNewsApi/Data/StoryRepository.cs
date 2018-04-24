using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;
using HackerNewsApi.Models;
using HackerNewsApi.Utilities;
using Newtonsoft.Json;

namespace HackerNewsApi.Data
{
    public partial class StoryRepository : IStoryRepository
    {
        private HackerWebRequest _request = new HackerWebRequest();

        public IEnumerable<Story> Get(string searchString, string cerrurntFilter, string sortOrder, int? page)
        {
            int newsItems = 50;
            var storyList = new List<Story>();
            var request = this._request.Get();
            var response = request.GetResponse();

            var serializer = new DataContractJsonSerializer(typeof(List<string>));
            var list = (List<string>)serializer.ReadObject(response.GetResponseStream());
            var idList = list.GetRange(0, Math.Min(newsItems, 50));

            foreach (var item in idList)
            {
                var story = this.GetStory(item);

                if (story != null)
                {
                    storyList.Add(new Story
                    {
                        Url = story.url,
                        Author = story.by,
                        Title = story.title
                    });
                }
            }

            return storyList;
        }

        public StoryItem GetStory(string id)
        {
            string jsonString;
            var js = new JavaScriptSerializer();
            var request = this._request.Get(id);

            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                jsonString = sr.ReadToEnd();
            }

            StoryItem story = JsonConvert.DeserializeObject<StoryItem>(jsonString);

            return story;
        }
    }
}