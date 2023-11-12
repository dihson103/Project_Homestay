using HomestayWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomestayWeb.Pages
{
    public class SearchDto
    {
        public string SearchString { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
    } 

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
        [BindProperty]
        public SearchDto Search { get; set; } = default!;

        public void OnGet()
        {
            Homestays = _context.Homestays
                .Include(h => h.Images)
                .Where(h => h.Status)
                .ToList();
            Search = new SearchDto();
        }

        public void OnPost()
        {
            var query = _context.Homestays.Include(h => h.Images).Where(h => h.Status);

            if (!string.IsNullOrEmpty(Search?.SearchString))
            {
                query = query.Where(h => h.HomestayName.Contains(Search.SearchString));
            }

            if (!string.IsNullOrEmpty(Search?.City))
            {
                query = query.Where(h => h.City.Equals(Search.City));
            }

            if (!string.IsNullOrEmpty(Search?.District))
            {
                query = query.Where(h => h.District.Equals(Search.District));
            }

            if (!string.IsNullOrEmpty(Search?.Ward))
            {
                query = query.Where(h => h.Commune.Equals(Search.Ward));
            }

            Homestays = query.ToList();
        }


    }
}