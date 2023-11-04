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
    public class DetailsModel : PageModel
    {
        private readonly HomestayWeb.Models.ProjectHomeStayContext _context;

        public DetailsModel(HomestayWeb.Models.ProjectHomeStayContext context)
        {
            _context = context;
        }

      public Homestay Homestay { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Homestays == null)
            {
                return NotFound();
            }

            var homestay = await _context.Homestays.FirstOrDefaultAsync(m => m.HomestayId == id);
            if (homestay == null)
            {
                return NotFound();
            }
            else 
            {
                Homestay = homestay;
            }
            return Page();
        }
    }
}
