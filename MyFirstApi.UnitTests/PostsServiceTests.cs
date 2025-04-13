using Bogus;
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

    private static readonly Faker _faker = new();

    public PostsServiceTests()
    {
        _postsService = new PostsService();

    }
    private static readonly Arbitrary<Post> validPost = Arb.From(
        from userId in Gen.Choose(1, 5)
            from title in Gen.ListOf(Gen.Choose('A','z').Select(i =>(char)i))
                .Select(chars => new string([.. chars]))
                .Where(s => s.Length <= 50)
            from body in Gen.ListOf(Gen.Elements(_faker.Name.Random.Words(5)))
                        .Select(words => string.Join(" ", words))
            select new Post
            {
                UserId = userId,
                Title = title,
                Body = body
            });

    [Fact]
    public void CreatedPostShouldBeRetrievable()
    {
        Prop.ForAll(validPost, post =>
        {
            // Act
            _postsService.CreatePost(post).Wait();
            var retrievedPost = _postsService.GetPost(post.Id).Result;
            
            // Assert
            var result = retrievedPost.UserId == post.UserId;

            return result
                    .Collect($"Title: {retrievedPost.Title}")
                    .Collect($"UserId: {retrievedPost.UserId}");
        }).QuickCheckThrowOnFailure();

    }
}
