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
using HomestayWeb.Contants;

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

            var homestay = await _context.Homestays
                .Include(h=>h.Images)
                .FirstOrDefaultAsync(m => m.HomestayId == id);
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
            string message;

            if (username == null)
            {
                message = "Please login to order.";
                return RedirectToPage("/Login/Index", new { message });
            }

            if(!ModelState.IsValid)
            {
                Homestay = _context.Homestays.SingleOrDefault(m => m.HomestayId == id);
                return Page();
            }

            if (password == null)
            {
                message = "Please enter your password to order";
                return RedirectToAction("/HomeStays/Details", new {id, message});
            }

            User? customer = _context.Users.SingleOrDefault(u => u.Password == password && u.Username == username);
            if(customer == null)
            {
                message = "Password is invalid";
                return RedirectToAction("/HomeStays/Details", new { id, message });
            }

            DateTime currentDate = DateTime.Now;
            Order order = new Order()
            {
                UserId = customer.UserId,
                HomestayId = id,
                OrderDate = currentDate,
                Status = OrderStatus.PENDING_CONFIRM
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

             
            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = order.OrderId,
                FromDate = CartItem.DateStart,
                EndDate = CartItem.DateEnd,
                PriceWhenSell = getPriceWhenSell(id, currentDate),
                IsPayment = false
            };

            message = "Order request is pending confirm";
            return RedirectToAction("/HomeStays/Details", new { id, message });
        }

        private decimal getPriceWhenSell(int homstayId, DateTime currentDate)
        {
            decimal price = 0;

            List<Discount> discounts = _context.Discounts
                .Where(d => d.HomstayId == homstayId && d.DateStart <= currentDate && d.DateEnd >= currentDate)
                .ToList();

            Homestay? homestay = _context.Homestays.SingleOrDefault(d => d.HomestayId == homstayId);

            decimal totalDiscount = 0;
            if (homestay != null)
            {
                decimal homestayPrice = homestay.Price;

                if (homestayPrice != 0 && discounts.Any())
                {
                    totalDiscount = discounts.Sum(x => ((decimal)x.Discount1 / 100) * homestayPrice);
                }

                price = homestay.Price - totalDiscount;

                return price < 0 ? 0 : price;
            }

            throw new NotImplementedException();
        }

    }
}
