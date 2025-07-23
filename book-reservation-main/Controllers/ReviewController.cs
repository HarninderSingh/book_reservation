// Controllers/ReviewController.cs
using Microsoft.AspNetCore.Mvc;
using Book_Reservation.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Reservation.Controllers
{
    [Route("Review")]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
[HttpGet("Add/{bookId}")]
public IActionResult Add(int bookId)
{
    var userId = GetUserId();
    var alreadyReserved = _context.Books.Any(b => b.Id == bookId && b.UserId == userId);
    if (!alreadyReserved)
        return Unauthorized("You can only review books you've reserved.");

    var existingReview = _context.Reviews.FirstOrDefault(r => r.BookId == bookId && r.UserId == userId);
    if (existingReview != null)
        return RedirectToAction("MyReviews", new { id = existingReview.Id });

    ViewBag.BookId = bookId;
    return View(); 
}


        [HttpPost("Add/{bookId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int bookId, Review review)
        {
            var userId = GetUserId();
            review.UserId = userId;
            review.SubmittedOn = DateTime.UtcNow;

            var alreadyReviewed = _context.Reviews.Any(r => r.BookId == review.BookId && r.UserId == userId);
            if (alreadyReviewed)
                return BadRequest("You have already reviewed this book.");

            _context.Reviews.Add(review);
            _context.SaveChanges();
            TempData["Success"] = "Your review has been submitted. Thank you for your feedback!";
            return RedirectToAction("MyReviews");
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var userId = GetUserId();
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (review == null || (DateTime.UtcNow - review.SubmittedOn).TotalDays > 7)
                return Unauthorized("You cannot edit this review.");

            return View(review);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(int id, Review updated)
        {
            var userId = GetUserId();
            // Ensure the review exists and was submitted within the last 7 days
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (review == null || (DateTime.UtcNow - review.SubmittedOn).TotalDays > 7)
                return Unauthorized();

            review.Rating = updated.Rating;
            review.Comment = updated.Comment;
            _context.SaveChanges();
            TempData["Success"] = "Your review has been updated.";

            return RedirectToAction("MyReviews");
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var userId = GetUserId();
            // Ensure the review exists and was submitted within the last 7 days
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (review == null || (DateTime.UtcNow - review.SubmittedOn).TotalDays > 7)
                return Unauthorized();

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            TempData["Success"] = "Your review has been deleted.";

            return RedirectToAction("MyReviews");
        }

        [HttpGet("MyReviews")]
        public IActionResult MyReviews()
        {
            var userId = GetUserId();
            var reviews = _context.Reviews.Include(r => r.Book).Where(r => r.UserId == userId).ToList();
            return View(reviews);
        }

        private int GetUserId()
        {
            var str = HttpContext.Session.GetString("UserId");
            return int.TryParse(str, out var id) ? id : 0;
        }
    }
}
