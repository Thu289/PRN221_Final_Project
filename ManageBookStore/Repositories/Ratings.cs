using ManageBookStore.IRepositories;
using ManageBookStore.Models;

namespace ManageBookStore.Repositories
{
    public class Ratings : IRating
    {
        void IRating.AddRating(Rating rating)
        {
            throw new NotImplementedException();
        }

        void IRating.DeleteRating(int rateId)
        {
            throw new NotImplementedException();
        }

        List<Rating> IRating.GetAllRatings()
        {
            throw new NotImplementedException();
        }

        List<Rating> IRating.GetRatingOfPost(int postId)
        {
            throw new NotImplementedException();
        }

        List<Rating> IRating.GetRatingsByBook(int bookId)
        {
            throw new NotImplementedException();
        }

        void IRating.UpdateRating(Rating rating)
        {
            throw new NotImplementedException();
        }
    }
}
