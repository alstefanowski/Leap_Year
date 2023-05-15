using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Leap_Year.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Leap_Year.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Leap_Year.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public DetailsModel(Leap_Year.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public IList<LeapYear> Users { get; set; }
        public LeapYear LeapYear { get; set; } = default!;
        //public IdentityUser User { get; set; }
   
       public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LeapYear = await _context.LeapYear.FirstOrDefaultAsync(m => m.ID == id);
            if (LeapYear == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var user = await _context.LeapYear.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.LeapYear.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
