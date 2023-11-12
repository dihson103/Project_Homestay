using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomestayWeb.Models;
using Microsoft.AspNetCore.Authorization;
using HomestayWeb.Utils;

namespace HomestayWeb.Pages.HomeStays
{
    [Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly HomestayWeb.Models.ProjectHomeStayContext _context;

        public IndexModel(HomestayWeb.Models.ProjectHomeStayContext context)
        {
            _context = context;
        }

        public PaginatedList<Homestay> Homestay { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 1;
            if(pageIndex == null)
            {
                pageIndex = 1;
            }

            IQueryable<Homestay> homestays = _context.Homestays;

            if (_context.Homestays != null)
            {
                Homestay = await PaginatedList<Homestay>.CreateAsync(
                            homestays.AsNoTracking(),
                            pageIndex ?? 1, pageSize
                         );
            }
        }
    }
}
