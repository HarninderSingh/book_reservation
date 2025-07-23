using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Book_Reservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Book_Reservation.Controllers
{

    [Route("Book")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            var userId = GetUserId();
            var books = _context.Books.Where(b => b.UserId == userId).ToList();
            return View(books);
        }



        [HttpPost("Reserve/{id}")]
        public IActionResult Reserve(int id)
        {
            var userId = GetUserId();
            if (!_context.Users.Any(u => u.Id == userId))
            {
                TempData["Error"] = "User not found. Please log in again.";
                return RedirectToAction("Login", "Account");
            }
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                // Check if already reserved
                bool alreadyReserved = _context.Reservations.Any(r => r.BookId == id && r.UserId == userId);
                if (!alreadyReserved)
                {
                    var reservation = new Reservation
                    {
                        BookId = id,
                        UserId = userId,
                        ReservedAt = DateTime.UtcNow
                    };
                    _context.Reservations.Add(reservation);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost("Cancel/{id}")]
        public IActionResult CancelReservation(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                TempData["CancelMessage"] = $"Reservation for '{book.Title}' has been canceled.";
            }
            return RedirectToAction("Dashboard");
        }

        private int GetUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            return int.TryParse(userIdStr, out var userId) ? userId : 0;
        }

        [HttpGet("Browse")]
        public IActionResult Browse(string title, string author, string genre)
        {
            var filtered = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                filtered = filtered.Where(b => b.Title.ToLower().Contains(title.ToLower()));
            if (!string.IsNullOrEmpty(author))
                filtered = filtered.Where(b => b.Author.ToLower().Contains(author.ToLower()));
            if (!string.IsNullOrEmpty(genre))
                filtered = filtered.Where(b => b.Genre.ToLower().Contains(genre.ToLower()));

            return View(filtered.ToList());
        }

    }
}
