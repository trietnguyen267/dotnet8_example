using FsCheck;
using FsCheck.Fluent;
using MyFirstApi.Models;

namespace MyFirstApi.UnitTests.Generators;

public class PostGenerators
{
    public static Arbitrary<Post> ValidPosts()
    {
        var generator =
            from userId in Gen.Choose(1, 10)
            from title in Gen.NonEmptyListOf(Gen.Choose('a', 'z').Select(i => (char)i)).Select(chars => new string([.. chars]))
            from body in Gen.NonEmptyListOf(Gen.Elements("Lorem", "ipsum", "dolor", "sit", "amet"))
                        .Select(words => string.Join(" ", words))
            select new Post
            {
                UserId = userId,
                Title = title,
                Body = body
            };

        return Arb.From(generator);
    }

    public static Arbitrary<string> ValidTitles()
    {
        return Arb.From(
            Gen.NonEmptyListOf(Gen.Choose('a', 'z').Select(i => (char)i))
               .Select(chars => new string([.. chars]))
               .Where(s => s.Length <= 100)
        );
    }

    public static Arbitrary<string> ValidBodies()
    {
        return Arb.From(
            Gen.NonEmptyListOf(Gen.Elements("Lorem", "ipsum", "dolor", "sit", "amet"))
               .Select(words => string.Join(" ", words))
               .Where(s => s.Length <= 500)
        );
    }
}