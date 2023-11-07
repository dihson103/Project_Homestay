using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomestayWeb.Models;
using Microsoft.Identity.Client;
using HomestayWeb.Dtos;
using Newtonsoft.Json;
using System.Security.Claims;

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
        [BindProperty]
        public CartItem CartItem { get; set; } = default!;

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

        public IActionResult OnPostAddToCart(string? password, int id)
        {
            var user = HttpContext.User;
            var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == null)
            {
                string message = "Please login to order.";
                return RedirectToPage("/Login/Index", new { message });
            }

            if(!ModelState.IsValid)
            {
                Homestay = _context.Homestays.SingleOrDefault(m => m.HomestayId == id);
                return Page();
            }

            if (password == null)
            {
                string message = "Please enter your password to order";
                return RedirectToAction("/HomeStays/Details", new {id, message});
            }

            if (!isValidPassword(username, password))
            {
                string message = "Password is invalid";
                return RedirectToAction("/HomeStays/Details", new { id, message });
            }

            // If everything is valid, you can proceed with the action
            // ...

            return Page(); // Or Redirect to another page if needed
        }


        private bool isValidPassword(string username, string password)
        {
            return _context.Users.Any(u => u.Password == password && u.Username == username);
        }


    }
}
