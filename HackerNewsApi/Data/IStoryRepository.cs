using System.Collections.Generic;
using HackerNewsApi.Models;

namespace HackerNewsApi.Data
{
    public interface IStoryRepository
    {
        IEnumerable<Story> Get(string searchString, string cerrurntFilter, string sortOrder, int? page);
        StoryItem GetStory(string id);

    }
}
