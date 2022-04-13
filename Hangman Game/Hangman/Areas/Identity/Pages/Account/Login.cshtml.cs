// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using Hangman.Data;
using Hangman.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Hangman.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
        public IActionResult OnGet()
        {
            var session_username = HttpContext.Session.GetString("session_username");
            var is_logged_in = HttpContext.Session.GetString("is_logged_in");

            if (session_username != null || is_logged_in == "true")
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            User user;
            var username = Input.Username;
            var password = Input.Password;
            var userID = 0;

            try
            {
                user = _context.Users.Single(u => u.UserName == username);
                userID = _context.Users.Single(u => u.UserName == username).Id;
            } catch (Exception ex)
            {
                HttpContext.Session.SetString("session_username", ""); 
                HttpContext.Session.SetString("is_logged_in", "false");
                HttpContext.Session.SetInt32("userID", userID);
                _ = ex.Message;
                return NotFound();
            }
            var hashed = HashPassword(password, user.Salt);

            if (username.Equals(user.UserName) && user.PasswordHash.Equals(hashed))
            {
                HttpContext.Session.SetString("session_username", user.UserName);
                HttpContext.Session.SetString("is_logged_in", "true");
                HttpContext.Session.SetInt32("userID", userID);
                return LocalRedirect("/Index");
            }
            else
            {
                return Page();
            }
        }

        private static string HashPassword(string password, string salt)
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 1,
            numBytesRequested: 256 / 8));

            return hash;
        }
    }
}