using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hangman.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            HttpContext.Session.Clear();

            return LocalRedirect(returnUrl);
        }
    }
}
