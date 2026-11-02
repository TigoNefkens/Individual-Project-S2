using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Interfaces;
using TabberUnitTests.Mock;
namespace TabberUnitTests
{
    [TestClass]
    public class PreferredCategoryUnitTests
    {

        [TestMethod]
        public void GetRecommendedTabs_ReturnsTabsMatchingUserPreferences()
        {
            var category1 = new Category(1, "Rock");
            var category2 = new Category(2, "Jazz");

            var user = new User(1, "Tigo", 1, new List<Category> { category1 });

            var tabs = new List<Tab>();

            tabs.Add(new Tab(1, "content1", true, category1, user, "Tab1", 10));
            tabs.Add(new Tab(2, "content2", true, category2, user, "Tab2", 5));
       
            var strategy = new PreferredCategoryStrategy();

            var result = strategy.GetRecommendedTabs(user, tabs);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Tab1", result.First().TabName);
        }

        [TestMethod]
        public void GetRecommendedTabs_ReturnsEmptyList_WhenNoTabsFound()
        {

            var category1 = new Category(1, "Acoustic");
            var category2 = new Category(2, "Jazz");

            var user = new User(1, "Tigo", 1, new List<Category> { category2 });

            var tabs = new List<Tab>();

            Tab tab = new Tab(1, "content1", true, category1, user, "Tab1", 10);

            tabs.Add(tab);

            var strategy = new PreferredCategoryStrategy();

            var result = strategy.GetRecommendedTabs(user, tabs);

            Assert.AreEqual(0, result.Count);
        }

    }
}
