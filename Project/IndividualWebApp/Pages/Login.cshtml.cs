using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;

namespace IndividualWebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string LoginUsername { get; set; }

        [BindProperty]
        public string LoginPassword { get; set; }

        [BindProperty]
        public string CreateUsername { get; set; }

        [BindProperty]
        public string CreatePassword { get; set; }

        public void RegisterUser()
        {
            _userService.RegisterUser(LoginUsername, LoginPassword);
        }

        public IActionResult OnPostRegister()
        {
            var result = _userService.RegisterUser(CreateUsername, CreatePassword);

            if (result)
            {
                return RedirectToPage("/Success");
            }

            ModelState.AddModelError("", "Registratie mislukt.");
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
