using HomestayWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomestayWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProjectHomeStayContext _context;

        public IndexModel(ILogger<IndexModel> logger, ProjectHomeStayContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Homestay> Homestays { get; set; }

        public void OnGet()
        {
            Homestays = _context.Homestays
                .Include(h => h.Images)
                .ToList();
        }
    }
}