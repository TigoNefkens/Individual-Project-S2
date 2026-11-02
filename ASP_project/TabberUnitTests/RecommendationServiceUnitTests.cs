using BLL.Models;
using BLL.Services;
using TabberUnitTests.Mock;

namespace TabberUnitTests;

[TestClass]
public class RecommendationServiceUnitTests
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
    public void GetRecommendedTabs_ReturnsCorrectObjectTypes()
    {
        var strategy = new TestStrategy();

        var result = _recommendationService.GetRecommendedTabs(strategy, "TestUser");

        Assert.AreEqual(2, result.Count);
        Assert.IsInstanceOfType(result, typeof(List<Tab>));
    }

    [TestMethod]
    public void GetRecommendedTabs_ReturnsOnlyPublicTabs()
    {
        var strategy = new TestStrategy();

        var result = _recommendationService.GetRecommendedTabs(strategy, "TestUser");

        Assert.IsFalse(result.Any(tab => tab.Publicity == false));
    }
}
