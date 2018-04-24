using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using HackerNewsApi.Data;
using HackerNewsApi.Models;
using HackerNewsApi.Utilities;
using PagedList;

namespace HackerNewsApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoryRepository _repository;

        public HomeController(IStoryRepository repository)
        {
            _repository = repository;
        }

        public HomeController()
        {
            this._repository = new StoryRepository();
        }

        public ActionResult Index(string searchString, string currentFilter, string sortOrder, int? page)
        {
            int _pageSize = 10;
            var storyList = new List<Story>();
            var storiesCache = (List<Story>)MemoryCache.Default["StoriesCache"];

            ViewBag.AuthorSortParam = String.IsNullOrEmpty(sortOrder) ? "Author_desc" : "";
            ViewBag.TitleSortParam = sortOrder == "Title" ? "Title_desc" : "Title";

            if (!String.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (storiesCache == null)
            {
                storyList = _repository
                    .Get(searchString, currentFilter, sortOrder, page)
                    .ToList();

                MemoryCache.Default["StoriesCache"] = storyList;
            }
            else
            {
                storyList = storiesCache;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                storyList = storyList.Where(s =>
                        s.Author.ToUpper().Contains(searchString) ||
                        s.Title.ToUpper().Contains(searchString))
                    .ToList();
            }

            switch (sortOrder)
            {
                case "Author_desc":
                    storyList = storyList.OrderByDescending(s => s.Author).ToList();
                    break;

                case "Title":
                    storyList = storyList.OrderBy(s => s.Title).ToList();
                    break;

                case "Title_desc":
                    storyList = storyList.OrderByDescending(s => s.Title).ToList();
                    break;

                default:
                    storyList = storyList.OrderBy(s => s.Author).ToList();
                    break;
            }

            storyList = Numbering.Get(storyList);
            var pageNumber = (page ?? 1);
            return View(storyList.ToPagedList(pageNumber, _pageSize));
        }
    }
}