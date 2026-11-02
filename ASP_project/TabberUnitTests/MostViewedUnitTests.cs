using BLL.Models;

namespace TabberUnitTests;

[TestClass]
public class MostViewedUnitTests
{
    [TestMethod]
    public void GetRecommendedTabs_ReturnsTop3ByViewCount()
    {
        var category = new Category(1, "Rock");
        List<Category> categoryList = new List<Category>();
        categoryList.Add(category);
        var user = new User(1, "Tigo", 1, categoryList);

        var tabs = new List<Tab>
        {
            new Tab(1, "content1", true, category, user, "Tab1", 10),
            new Tab(2, "content2", true, category, user, "Tab2", 50),
            new Tab(3, "content3", true, category, user, "Tab3", 20),
            new Tab(4, "content4", true, category, user, "Tab4", 5),
        };

        var strategy = new MostViewedStrategy();

        var result = strategy.GetRecommendedTabs(user, tabs);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("Tab2", result[0].TabName);
        Assert.AreEqual("Tab3", result[1].TabName);
        Assert.AreEqual("Tab1", result[2].TabName);
    }

    [TestMethod]
    public void GetRecommendedTabs_ReturnsInOrderWhenTopValuesMatch()
    {
        var category = new Category(1, "Rock");
        List<Category> categoryList = new List<Category>();
        categoryList.Add(category);
        var user = new User(1, "Tigo", 1, categoryList);

        var tabs = new List<Tab>
        {
            new Tab(1, "content1", true, category, user, "Tab1", 10),
            new Tab(2, "content2", true, category, user, "Tab2", 50),
            new Tab(3, "content3", true, category, user, "Tab3", 20),
            new Tab(4, "content4", true, category, user, "Tab4", 5),
            new Tab(1, "content1", true, category, user, "Tab5", 10),
            new Tab(2, "content2", true, category, user, "Tab6", 50),
            new Tab(3, "content3", true, category, user, "Tab7", 20),
            new Tab(4, "content4", true, category, user, "Tab8", 50),
        };

        var strategy = new MostViewedStrategy();

        var result = strategy.GetRecommendedTabs(user, tabs);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("Tab2", result[0].TabName);
        Assert.AreEqual("Tab6", result[1].TabName);
        Assert.AreEqual("Tab8", result[2].TabName);
    }

}
