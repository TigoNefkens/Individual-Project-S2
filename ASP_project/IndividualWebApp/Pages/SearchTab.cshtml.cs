using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace IndividualWebApp.Pages
{
    public class SearchTabModel : PageModel
    {
        private readonly ITabService _tabService;

        private readonly IRecommendationServices _recommendationServices;

        public SearchTabModel(ITabService tabservice, IRecommendationServices recommendationServices)
        {
            _tabService = tabservice;
            _recommendationServices = recommendationServices;
        }

        public List<Tab> userTabs = new List<Tab>();

        [BindProperty]
        public int tabId { get; set; }

        [BindProperty]
        public string searchTerms { get; set; }

        public IActionResult OnGet()
        {
            var username = HttpContext.Session.GetString("Username");
            IRecommendationStrategy preferredRecommendation = new PreferredCategoryStrategy();

            if (!username.IsNullOrEmpty())
            {
                var result = _recommendationServices.GetRecommendedTabs(preferredRecommendation, username);
                if (result.Count == 0)
                {
                    userTabs = _tabService.GetAllTabs();
                }
                else
                {
                    userTabs = result;
                }
            }
            else
            {
                userTabs = _tabService.GetAllTabs();
            }
            
                

            return Page();
        }

        public IActionResult OnPostPreferences()
        {
            userTabs.Clear();
            var username = HttpContext.Session.GetString("Username");
            IRecommendationStrategy preferredRecommendation = new PreferredCategoryStrategy();

            if (username.IsNullOrEmpty())
            {
                TempData["ValidationErrors"] = "You have to be logged in to view preferred tabs";
                return RedirectToPage("Login");
            }

            userTabs = _recommendationServices.GetRecommendedTabs(preferredRecommendation, username);

            return Page();
        }

        public IActionResult OnPostMostClicked()
        {
            userTabs.Clear();
            var username = HttpContext.Session.GetString("Username");
            IRecommendationStrategy preferredRecommendation = new MostViewedStrategy();
            userTabs = _recommendationServices.GetRecommendedTabs(preferredRecommendation, username);

            return Page();
        }
        public IActionResult OnPostAll()
        {
            userTabs = _tabService.GetAllTabs();
            return Page();
        }

        public IActionResult OnPostView()
        {
            _tabService.IncreaseViewCount(tabId);
            userTabs = _tabService.GetAllTabs();
            return Page();
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrWhiteSpace(searchTerms))
            {
                TempData["ErrorMessage"] = "Please enter a search term.";
                userTabs = new List<Tab>();

                return Page();
            }

            var result = _tabService.GetTabsBySearch(searchTerms);

            if (result == null || result.Count == 0)
            {
                TempData["ErrorMessage"] = "No tabs found for your search.";
                userTabs = new List<Tab>();
            }
            else
            {
                userTabs = result;
            }

            return Page();
        }
    }
}
