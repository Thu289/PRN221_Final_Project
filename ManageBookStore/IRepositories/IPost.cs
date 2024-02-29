using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IPost
    {
        void AddPost (Post post);

        void UpdatePost(Post post); 

        void DeletePost (int postId);

        List<Post> GetAllPosts ();

        List<Post> GetPostsByBook(int bookId);

        List <Post> GetPostsByAuthor(int authorId);
    }
}
