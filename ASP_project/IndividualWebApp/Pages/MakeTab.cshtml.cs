using BLL.Models;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace IndividualWebApp.Pages
{
    public class MakeTabModel : PageModel
    {
        private readonly ITabService _tabService;

        public MakeTabModel(ITabService tabservice)
        {
            _tabService = tabservice;
        }

        public List<Tab> userTabs = new List<Tab>();

        public IActionResult OnGet()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            if (userId == 0)
            {
                TempData["ValidationErrors"] = "You have to be logged in to start creating tabs";
                return RedirectToPage("Login");
            }

            userTabs = _tabService.GetTabsFromUser(userId);

            return Page();
        }
    }
}
