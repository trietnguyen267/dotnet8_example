using MyFirstApi.Models;

namespace MyFirstApi.Services
{
    public class PostsService: IPostsService
    {
        private static readonly List<Post> AllPosts = [];
        public Task CreatePost(Post item){
            
            AllPosts.Add(item);
            return Task.CompletedTask;
        }

        public Task<Post> UpdatePost (Guid id, Post item){
            var post = AllPosts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                post.UserId = item.UserId;
                post.Title = item.Title;
                post.Body = item.Body;
            }
            return Task.FromResult(post);
        }

        public Task<Post> GetPost(Guid id)
        {
            var post = AllPosts.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(post);
        }

        public Task<List<Post>> GetPosts()
        {
            return Task.FromResult(AllPosts);
        }

        public Task DeletePost(Guid id)
        {
            var post = AllPosts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                AllPosts.Remove(post);
            }
            return Task.CompletedTask;
        }
    }
}