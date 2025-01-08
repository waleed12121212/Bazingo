using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(string userId);  // Add this method
        Task AddReviewAsync(Review review);
        Task DeleteReviewAsync(int reviewId);
        Task<IEnumerable<Review>> GetAllReviewsAsync( );
    }

}
