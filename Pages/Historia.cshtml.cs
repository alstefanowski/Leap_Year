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

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
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

            _LeapYear = _LeapYear.OrderByDescending(s => s.SearchTime);
            var pageSize = _configuration.GetValue("PageSize", 20);
            leapYears = await PaginatedList<LeapYear>.CreateAsync(
                _LeapYear.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

    }
}
