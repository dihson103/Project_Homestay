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

namespace HomestayWeb.Pages.Users
{
    [Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly HomestayWeb.Models.ProjectHomeStayContext _context;

        public IndexModel(HomestayWeb.Models.ProjectHomeStayContext context)
        {
            _context = context;
        }

        public PaginatedList<User> User { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 1;
            if (pageIndex == null)
            {
                pageSize = 1;
            }

            if (_context.Users != null)
            {
                var users = _context.Users;
                User = await PaginatedList<User>.CreateAsync(
                            users.AsNoTracking(),
                            pageIndex ?? 1, pageSize
                         );
            }
        }
    }
}
