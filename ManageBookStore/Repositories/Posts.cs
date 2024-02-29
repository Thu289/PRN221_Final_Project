using ManageBookStore.IRepositories;
using ManageBookStore.Models;

namespace ManageBookStore.Repositories
{
    public class Posts : IPost
    {
        void IPost.AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        void IPost.DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        List<Post> IPost.GetAllPosts()
        {
            throw new NotImplementedException();
        }

        List<Post> IPost.GetPostsByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        List<Post> IPost.GetPostsByBook(int bookId)
        {
            throw new NotImplementedException();
        }

        void IPost.UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
