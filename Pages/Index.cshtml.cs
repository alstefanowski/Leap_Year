using Leap_Year.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;

namespace Leap_Year.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public LeapYear LeapYear { get; set; }

        public IdentityUser user { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }
        [BindProperty]
        public IList<LeapYear> Search_List { get; set; }
     

        [AllowAnonymous]
        public IActionResult OnPost()
        {
            if ((LeapYear.Year % 400 == 0))
            {
                LeapYear.Result = "Rok przystepny";
            }
            else if (LeapYear.Year % 100 == 0)
            {
                LeapYear.Result = "Rok nie jest przystepny";
            }

            else if (LeapYear.Year % 4 == 0)
            {
                LeapYear.Result = "Rok przystepny";
            }
            else
            {
                LeapYear.Result = "Rok nie jest przystepny";
            }
           // if (LeapYear.Name == null)
              //  LeapYear.Name = "";
            LeapYear.SearchTime = DateTime.Now;
            _dbContext.LeapYear.Add(LeapYear);
            _dbContext.SaveChanges();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}