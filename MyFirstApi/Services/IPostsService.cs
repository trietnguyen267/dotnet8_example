using MyFirstApi.Models;

namespace MyFirstApi.Services;
public interface IPostsService
{
    Task CreatePost(Post item);
    Task<Post> UpdatePost(Guid id, Post item);
    Task<Post> GetPost(Guid id);
    Task<List<Post>> GetPosts();
    Task DeletePost(Guid id);
}