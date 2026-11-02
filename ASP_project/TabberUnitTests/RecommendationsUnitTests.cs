using TabberUnitTests.Mock;
using BLL.Models;
using BLL.Services;

namespace TabberUnitTests;

[TestClass]
public class RecommendationsUnitTests
{

    private RecommendationServices _recommendationService;

    [TestInitialize]
    public void Setup()
    {
        var mockUserRepo = new MockUserRepo();
        var mockTabRepo = new MockTabRepo();
        _recommendationService = new RecommendationServices(mockUserRepo, mockTabRepo);
    }

    [TestMethod]
    public void RecommendationServicesReturnsPreferredTabsInTabObjectsWhenPublic()
    {
        var strategy = new PreferredCategoryStrategy();

        var result = _recommendationService.GetRecommendedTabs(strategy, "TestUser");

        Assert.IsFalse(result.Any(tab => tab.Publicity == false));
        Assert.IsInstanceOfType(result, typeof(List<Tab>));
        Assert.IsTrue(result.Any(tab => tab.Category.ID == 5 || tab.Category.ID == 4));
        Assert.IsTrue(result.Count() == 2);
    }
}
