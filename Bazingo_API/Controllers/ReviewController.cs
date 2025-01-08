using Bazingo_Application.DTOs.Reviews;
using Bazingo_Core.DomainLogic;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewManager _reviewManager;

        public ReviewController(ReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        // Add Review
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDTO reviewCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var review = new Review
            {
                ProductID = reviewCreateDTO.ProductID ,
                UserID = reviewCreateDTO.UserID ,
                Rating = reviewCreateDTO.Rating ,
                Comment = reviewCreateDTO.Comment ,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewManager.AddReviewAsync(review);
            return Ok(new { message = "Review added successfully." });
        }

        // Get Reviews by Product ID
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductReviews(int productId)
        {
            var reviews = await _reviewManager.GetReviewsByProductIdAsync(productId);
            if (!reviews.Any()) return NotFound(new { message = "No reviews found for this product." });

            var reviewDTOs = reviews.Select(r => new ReviewDTO
            {
                ReviewID = r.ReviewID ,
                ProductID = r.ProductID ,
                UserID = r.UserID ,
                Rating = r.Rating ,
                Comment = r.Comment ,
                CreatedAt = r.CreatedAt
            });

            return Ok(reviewDTOs);
        }

        // Get Reviews by User ID
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserReviews(string userId)
        {
            var reviews = await _reviewManager.GetReviewsByUserIdAsync(userId);
            if (!reviews.Any()) return NotFound(new { message = "No reviews found for this user." });

            var reviewDTOs = reviews.Select(r => new ReviewDTO
            {
                ReviewID = r.ReviewID ,
                ProductID = r.ProductID ,
                UserID = r.UserID ,
                Rating = r.Rating ,
                Comment = r.Comment ,
                CreatedAt = r.CreatedAt
            });

            return Ok(reviewDTOs);
        }

        // Delete Review
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await _reviewManager.DeleteReviewAsync(id);
                return Ok(new { message = "Review deleted successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Review not found." });
            }
        }
    }
}
