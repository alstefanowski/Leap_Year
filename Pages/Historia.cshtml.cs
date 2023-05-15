using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Leap_Year.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Leap_Year.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Distributed;

namespace Leap_Year.Pages
{
    public class HistoriaModel : PageModel
    {
        private readonly Leap_Year.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<LeapYear> leapYears { get; set; }
        public HistoriaModel(Leap_Year.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public LeapYear LeapYear { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            
            IQueryable<LeapYear> _LeapYear = from s in _context.LeapYear select s;

            /*
            switch (sortOrder)
            {
                case "name_desc":
                    _LeapYear = _LeapYear.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    _LeapYear = _LeapYear.OrderBy(s => s.SearchTime);
                    break;
                case "date_desc":
                    _LeapYear = _LeapYear.OrderByDescending(s => s.SearchTime);
                    break;
                default:
                    _LeapYear = _LeapYear.OrderBy(s => s.Year);
                    break;
            }
            */
            _LeapYear = _LeapYear.OrderByDescending(s => s.SearchTime);
            var pageSize = _configuration.GetValue("PageSize", 20);
            leapYears = await PaginatedList<LeapYear>.CreateAsync(
                _LeapYear.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        /*
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.LeapYear == null)
            {
                return Page();
            }
            _context.LeapYear.Add(LeapYear);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        */
    }
}
