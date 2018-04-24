using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackerNewsApi;
using HackerNewsApi.Controllers;
using HackerNewsApi.Data;
using HackerNewsApi.Models;
using HackerNewsApi.Tests.Models;
using Telerik.JustMock;

namespace HackerNewsApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private string _searchString = "";
        private string _currentFilter = "";
        private string _sortOrder = "";
        private int? _page = null;

        [TestMethod]
        public void GetAuthorSortParam()
        {
            // Act
            HomeController controller= new HomeController();

            // Act
            ViewResult result = controller.Index(_searchString, _currentFilter, _sortOrder, _page) as ViewResult;

            // Assert
            Assert.AreEqual("Author_desc", result.ViewBag.AuthorSortParam);
        }

        [TestMethod]
        public void GetTitleSortParam()
        {
            // Act
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(_searchString, _currentFilter, _sortOrder, _page) as ViewResult;

            // Assert
            Assert.AreEqual("Title", result.ViewBag.TitleSortParam);
        }

        [TestMethod]
        public void GetCurrentFilterWhenSearchStringIsEmpty()
        {
            // Act
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(_searchString, _currentFilter, _sortOrder, _page) as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewBag.CurrentFilter);
        }

        [TestMethod]
        public void GetCurrentFilterWhenSearchStringIsNotEmpty()
        {
            this._searchString = "Google_blogger";

            // Act
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(_searchString, _currentFilter, _sortOrder, _page) as ViewResult;

            // Assert
            Assert.AreEqual("Google_blogger", result.ViewBag.CurrentFilter);
        }

        [TestMethod]
        public void FindAllStories()
        {
            // Arrange
            var storyRepository = Mock.Create<IStoryRepository>();
            Mock.Arrange(() => storyRepository.Get(_searchString, _currentFilter, _sortOrder, _page))
                .Returns(new List<Story>(StoriesTest.Get()));

            // Act
            HomeController controller = new HomeController(storyRepository);
            ViewResult actionResult = (ViewResult) controller.Index(_searchString, _currentFilter, _sortOrder, _page);
            var model = actionResult.Model as IEnumerable<Story>;

            // Assert
            Assert.AreEqual(10, model.Count());
            Assert.AreEqual("_teacher", model.ToList()[0].Author);
            Assert.AreEqual("News item 9", model.ToList()[3].Title);
        }

        [TestMethod]
        public void FindAllStoriesWhereSearchStringIsBen()
        {
            this._searchString = "Ben";
            this._page = 1;
            // Arrange
            var storyRepository = Mock.Create<IStoryRepository>();
            Mock.Arrange(() => storyRepository.Get(_searchString, _currentFilter, _sortOrder, _page))
                .Returns(new List<Story>(StoriesTest.Get()));

            // Act
            HomeController controller = new HomeController(storyRepository);
            ViewResult actionResult = (ViewResult)controller.Index(_searchString, _currentFilter, _sortOrder, _page);
            var model = actionResult.Model as IEnumerable<Story>;

            // Assert
            Assert.AreEqual(1, model.Count());
            Assert.AreEqual("Ben", model.ToList()[0].Author);
            Assert.AreEqual("News item 1", model.ToList()[0].Title);
        }

        [TestMethod]
        public void SortOrderByDefault()
        {
            // Arrange
            var storyRepository = Mock.Create<IStoryRepository>();
            Mock.Arrange(() => storyRepository.Get(_searchString, _currentFilter, _sortOrder, _page))
                .Returns(StoriesTest.Get());

            // Act
            HomeController controller = new HomeController(storyRepository);
            ViewResult actionResult = (ViewResult)controller.Index(_searchString, _currentFilter, _sortOrder, _page);
            var model = actionResult.Model as IEnumerable<Story>;

            // Assert
            Assert.AreEqual(10, model.Count());
            Assert.AreEqual("_teacher", model.ToList()[0].Author);
            Assert.AreEqual("News item 6", model.ToList()[0].Title);
        }

        [TestMethod]
        public void SortOrderByAuthorDescending()
        {
            this._sortOrder = "Author_desc";
            // Arrange
            var storyRepository = Mock.Create<IStoryRepository>();
            Mock.Arrange(() => storyRepository.Get(_searchString, _currentFilter, _sortOrder, _page))
                .Returns(StoriesTest.Get());

            // Act
            HomeController controller = new HomeController(storyRepository);
            ViewResult actionResult = (ViewResult)controller.Index(_searchString, _currentFilter, _sortOrder, _page);
            var model = actionResult.Model as IEnumerable<Story>;

            // Assert
            Assert.AreEqual(10, model.Count());
            Assert.AreEqual("zee", model.ToList()[0].Author);
            Assert.AreEqual("News item 7", model.ToList()[0].Title);
        }

        [TestMethod]
        public void SortOrderByTitle()
        {
            this._sortOrder = "Title";
            // Arrange
            var storyRepository = Mock.Create<IStoryRepository>();
            Mock.Arrange(() => storyRepository.Get(_searchString, _currentFilter, _sortOrder, _page))
                .Returns(StoriesTest.Get());

            // Act
            HomeController controller = new HomeController(storyRepository);
            ViewResult actionResult = (ViewResult)controller.Index(_searchString, _currentFilter, _sortOrder, _page);
            var model = actionResult.Model as IEnumerable<Story>;

            // Assert
            Assert.AreEqual(10, model.Count());
            Assert.AreEqual("News item 1", model.ToList()[0].Title);
        }

        [TestMethod]
        public void SortOrderByTitleDescending()
        {
            this._sortOrder = "Title_desc";
            // Arrange
            var storyRepository = Mock.Create<IStoryRepository>();
            Mock.Arrange(() => storyRepository.Get(_searchString, _currentFilter, _sortOrder, _page))
                .Returns(StoriesTest.Get());

            // Act
            HomeController controller = new HomeController(storyRepository);
            ViewResult actionResult = (ViewResult)controller.Index(_searchString, _currentFilter, _sortOrder, _page);
            var model = actionResult.Model as IEnumerable<Story>;

            // Assert
            Assert.AreEqual(10, model.Count());
            Assert.AreEqual("News item 9", model.ToList()[0].Title);
        }
    }
}
