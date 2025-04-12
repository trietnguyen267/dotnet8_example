using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;
using MyFirstApi.Models;
using MyFirstApi.Services;
using Xunit.Abstractions;

namespace MyFirstApi.UnitTests;

public class PostsServiceTests
{
    private readonly PostsService _postsService;
    private readonly ITestOutputHelper _TestOutputHelper;
    public PostsServiceTests(ITestOutputHelper testOutputHelper)
    {
        _TestOutputHelper = testOutputHelper;
        _postsService = new PostsService();
    }
    private static readonly Arbitrary<Post> validPost = Arb.From(
        from userId in Gen.Choose(1, 10)
            from title in Gen.NonEmptyListOf(Gen.Choose('a', 'z').Select(i => (char)i)).Select(chars => new string([.. chars]))
            from body in Gen.NonEmptyListOf(Gen.Elements("Lorem", "ipsum", "dolor", "sit", "amet"))
                        .Select(words => string.Join(" ", words))
            select new Post
            {
                UserId = userId,
                Title = title,
                Body = body
            });

    [Property(MaxTest = 500, EndSize = 500)]
    public Property CreatedPostShouldBeRetrievable()
    {
        return Prop.ForAll(validPost, post =>
        {
            // Act
            _postsService.CreatePost(post).Wait();
            var retrievedPost = _postsService.GetPost(post.Id).Result;
            
            // Assert
            var result = retrievedPost == post;
            return result;
        }).Label("Created post should be retrievable");
    }
}