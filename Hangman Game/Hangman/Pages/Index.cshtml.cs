using Hangman.Data;
using Hangman.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Hangman.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<IndexModel> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public void OnGet()
        {
            var session_username = _httpContextAccessor.HttpContext.Session.GetString("session_username");
            var is_logged_in = _httpContextAccessor.HttpContext.Session.GetString("is_logged_in");
        }
    }
}