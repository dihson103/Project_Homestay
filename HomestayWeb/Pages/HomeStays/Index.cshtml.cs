using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomestayWeb.Models;

namespace HomestayWeb.Pages.HomeStays
{
    public class IndexModel : PageModel
    {
        private readonly HomestayWeb.Models.ProjectHomeStayContext _context;

        public IndexModel(HomestayWeb.Models.ProjectHomeStayContext context)
        {
            _context = context;
        }

        public IList<Homestay> Homestay { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Homestays != null)
            {
                Homestay = await _context.Homestays.ToListAsync();
            }
        }
    }
}
