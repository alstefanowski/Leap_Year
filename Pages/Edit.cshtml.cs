using Leap_Year.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Leap_Year.Pages
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
       private readonly Leap_Year.Data.ApplicationDbContext _context;
        [BindProperty]
        public LeapYear LeapYear { get; set; }
        public EditModel(ApplicationDbContext context)
        {   
            _context = context;
        }
       public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            LeapYear = await _context.LeapYear.FirstOrDefaultAsync(m => m.ID == id);
            if(LeapYear == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
