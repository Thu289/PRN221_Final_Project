using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IRating
    {
        void AddRating (Rating rating);

        void UpdateRating (Rating rating);  

        void DeleteRating (int rateId);

        List<Rating> GetAllRatings ();

        List<Rating> GetRatingsByBook (int bookId); 

        List<Rating> GetRatingOfPost (int postId);
    }
}
