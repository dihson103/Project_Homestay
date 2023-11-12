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
                .Include(h => h.Images)
                .FirstOrDefaultAsync(m => m.HomestayId == id && m.Status);

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
            try
            {
                // Authentication check
                var username = AuthenticateUser();

                if (!ModelState.IsValid)
                {
                    Homestay = _context.Homestays.Include(h => h.Images).SingleOrDefault(m => m.HomestayId == id && m.Status);
                    return Page();
                }

                // Check homestay availability
                CheckHomestayAvailability(id);

                // Get user
                var customer = GetUser(username, password);

                // Create order
                var order = CreateOrder(customer.UserId, id);

                // Create order detail
                CreateOrderDetail(order.OrderId, CartItem.DateStart, CartItem.DateEnd, id);

                // Update homestay
                UpdateHomestayStatus(id);

                return RedirectToAction("/HomeStays/Details", new { id, message = "Order request is pending confirm" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("/HomeStays/Details", new { id, message = ex.Message });
            }
        }

        private string AuthenticateUser()
        {
            var user = HttpContext.User;
            var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == null)
            {
                throw new Exception("Please login to order.");
            }

            return username;
        }

        private void CheckHomestayAvailability(int id)
        {
            Homestay = _context.Homestays.Include(h => h.Images).SingleOrDefault(m => m.HomestayId == id && m.Status);

            if (Homestay == null)
            {
                throw new Exception($"Can not find homestay with id: {id}");
            }

            if (!Homestay.Type.Equals(HomeStayType.AVAILABLE))
            {
                throw new Exception("This homestay is not available to order");
            }
        }

        private User GetUser(string username, string password)
        {
            if (password == null)
            {
                throw new Exception("Please enter your password to order");
            }

            var customer = _context.Users.SingleOrDefault(u => u.Password == password && u.Username == username);

            if (customer == null)
            {
                throw new Exception("Password is invalid");
            }

            return customer;
        }

        private Order CreateOrder(int userId, int homestayId)
        {
            DateTime currentDate = DateTime.Now;
            Order order = new Order()
            {
                UserId = userId,
                HomestayId = homestayId,
                OrderDate = currentDate,
                Status = OrderStatus.PENDING_CONFIRM
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        private void CreateOrderDetail(int orderId, DateTime fromDate, DateTime endDate, int homestayId)
        {
            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = orderId,
                FromDate = fromDate,
                EndDate = endDate,
                PriceWhenSell = getPriceWhenSell(homestayId, DateTime.Now),
                IsPayment = false
            };

            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        private void UpdateHomestayStatus(int homestayId)
        {
            Homestay.Type = HomeStayType.ORDERED;
            _context.Homestays.Update(Homestay);
            _context.SaveChanges();
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

            throw new Exception("Homestay not found");
        }

    }
}
