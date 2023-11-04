using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomestayWeb.Models;

namespace HomestayWeb.Pages.HomeStays
{
    public class CreateModel : PageModel
    {
        private readonly HomestayWeb.Models.ProjectHomeStayContext _context;

        public CreateModel(HomestayWeb.Models.ProjectHomeStayContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Homestay Homestay { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Homestays == null || Homestay == null)
            {
                return Page();
            }

            _context.Homestays.Add(Homestay);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
