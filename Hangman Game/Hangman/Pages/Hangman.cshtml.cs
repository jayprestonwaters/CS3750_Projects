using Hangman.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebSocketsSample.Controllers;

namespace Hangman.Pages
{
    public class HangmanModel : PageModel
    {
        public ApplicationDbContext _dbContext;       
        public HangmanModel(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void OnGet()
        {
        }
    }
}
