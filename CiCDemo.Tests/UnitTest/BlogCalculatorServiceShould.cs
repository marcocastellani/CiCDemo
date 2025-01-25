using cicdemo.Model;
using cicdemo.Services;
using FluentAssertions;

namespace CiCDemo.Tests;
[Trait("type","unit")]
public class BlogCalculatorServiceShould
{
    string RandomStringOf(int maxLen)
    {
        string ret = "";
        for (int i = 0; i < maxLen; i++)
        {
            ret += "1";
        }

        return ret;
    }
    
    [Fact]
    public void classify_small_post()
    {
        var post = new Post();
        post.Content = RandomStringOf(PostCalculatorService.SmallPost);
        post.Size = new PostCalculatorService(null).CalculateSize(post);
        post.Size.Should().Be(Post.PostSize.Small);
    }
    
    [Fact]
    public void classify_medium_post()
    {
        var post = new Post();
        post.Content = RandomStringOf(PostCalculatorService.MediumPost);
        post.Size = new PostCalculatorService(null).CalculateSize(post);
        post.Size.Should().Be(Post.PostSize.Medium);
    }
    
    [Fact]
    public void throw_for_huge_post()
    {
        var post = new Post();
        post.Content = RandomStringOf(PostCalculatorService.MediumPost +1);
        var pcs = new PostCalculatorService(null);
        pcs.Invoking(y => y.CalculateSize(post))
            .Should().Throw<TooLongException>()
            .WithMessage("Message is too long, limit is " + PostCalculatorService.MediumPost);

    }
}