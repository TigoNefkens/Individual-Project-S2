    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Identity.Client;
using BLL.Models;

namespace IndividualWebApp.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IUserService _userService;

        private readonly ICategoryServices _categoryService;

        public string ValidationErrors { get; set; }

        public LoginModel(IUserService userService, ICategoryServices categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }

        public List<Category> categoryList = new List<Category>(); // list met alle categorieen

        [BindProperty]
        public List<int> SelectedCategories { get; set; } // List met ID's van geselecteerde categorieën

        [BindProperty]
        [Required(ErrorMessage = "Please enter your email-address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email-address")]
        public string LoginUsername { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your password")]
        public string LoginPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter an email-address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email-address")]
        public string RegisterUsername { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a password")]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 characters long")]
        public string RegisterPassword { get; set; }

        public IActionResult OnGet()
        {
            categoryList = _categoryService.GetCategories();

            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {

            ModelState.Remove(nameof(LoginUsername));
            ModelState.Remove(nameof(LoginPassword));

            if (SelectedCategories == null || SelectedCategories.Count < 2)
            {
                ModelState.AddModelError(nameof(SelectedCategories), "Select at least two categories");
            }

            if (!ModelState.IsValid) // check de data annotations, return naar de page met errors in tempdata
            {
                categoryList = _categoryService.GetCategories();
                var orderedKeys = new List<string>
                {
                    nameof(RegisterUsername),
                    nameof(RegisterPassword),
                    nameof(SelectedCategories)
                };

                var errors = orderedKeys
                .Where(key => ModelState.ContainsKey(key))
                .SelectMany(key => ModelState[key].Errors.Select(e => e.ErrorMessage))
                .ToList();

                ValidationErrors = string.Join("\n\n", errors);

                TempData["ValidationErrors"] = ValidationErrors;
                return Page();
            }

            var result = _userService.TryRegisterUser(RegisterUsername, RegisterPassword);
            if (result.isException)
            {
                categoryList = _categoryService.GetCategories();
                ValidationErrors = result.exception;

                TempData["ValidationErrors"] = ValidationErrors;
                return Page();
            }

            User user = _userService.GetUser(RegisterUsername);

            foreach (var categoryId in SelectedCategories)
            {
                _categoryService.SetUserCategories(user.Id, categoryId);
            }
            
            categoryList = _categoryService.GetCategories();
            ValidationErrors = "Account has been created, You can now log in with your credentials";

            TempData["ValidationErrors"] = ValidationErrors;
            return Page();
         
        }

        public IActionResult OnPostLogin()
        {
            ModelState.Remove(nameof(RegisterUsername));
            ModelState.Remove(nameof(RegisterPassword));

            if (!ModelState.IsValid)
            {
                categoryList = _categoryService.GetCategories();
                var orderedKeys = new List<string>
                {
                    nameof(LoginUsername),
                    nameof(LoginPassword)
                };

                var errors = orderedKeys
                .Where(key => ModelState.ContainsKey(key))
                .SelectMany(key => ModelState[key].Errors.Select(e => e.ErrorMessage))
                .ToList();

                ValidationErrors = string.Join("\n\n", errors);

                TempData["ValidationErrors"] = ValidationErrors;
                return Page();
            }

            var result = _userService.TryLoginUser(LoginUsername, LoginPassword);
            if (result.isException)
            {
                categoryList = _categoryService.GetCategories();
                ValidationErrors = result.exception;

                TempData["ValidationErrors"] = ValidationErrors;
                return Page();
            }
            else
            {
                var user = _userService.GetUser(LoginUsername);
                HttpContext.Session.SetString("Username", user.userName);
                HttpContext.Session.SetInt32("UserId", user.Id);

                return RedirectToPage("index");
            }
        }
    }
}
