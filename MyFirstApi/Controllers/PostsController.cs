using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Services;
namespace MyFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostsService postsService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post item)
        {
            await postsService.CreatePost(item);
            return CreatedAtAction(nameof(GetPost), new { id = item.Id }, item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            var post = await postsService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        
    }
}
