// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Hangman.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Hangman.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        //private readonly SignInManager<User> _signInManager;
        //private readonly ILogger<LogoutModel> _logger;

        public IActionResult OnGet(string returnUrl = null)
        {
            //await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            returnUrl ??= Url.Content("~/");

            HttpContext.Session.SetString("session_username", null);
            HttpContext.Session.SetString("is_logged_in", "false");

            return LocalRedirect(returnUrl);
        }
    }
}
