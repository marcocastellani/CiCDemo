using cicdemo.Model;
using cicdemo.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CiCDemo.Tests.Integration;
[Trait("type","integration")]
public class BlogCalculatorServiceShould
{
    private readonly BlogDC dbContext;
    
    private readonly IDbContextTransaction trans;
    public BlogCalculatorServiceShould()
    {
        dbContext = new BlogDC( new DbContextOptionsBuilder<BlogDC>()
            .UseNpgsql("Host=localhost;Database=blog;Username=postgres;Password=postgres")
            .Options);
        trans = dbContext.Database.BeginTransaction();
        dbContext.Posts.ExecuteDelete();
        dbContext.Posts.Add(new Post()
        {
            Content = "",
            Size = Post.PostSize.Small,
            Title = "Small post"
        });
        dbContext.Posts.Add(new Post()
        {
            Content = "",
            Size = Post.PostSize.Small,
            Title = "Medium post"
        });
        dbContext.Posts.Add(new Post()
        {
            Content = "",
            Size = Post.PostSize.Small,
            Title = "post post"
        });   
        
        dbContext.SaveChanges();
    }
    
    ~BlogCalculatorServiceShould()
    {
        trans.Rollback();
    }
    
    [Fact]
    public void do_get_frequent_words()
    {
       var pcs = new PostCalculatorService(dbContext);
       pcs.GetFrequentWords( "this primo a test post of", 1)
           .Should()
           .BeEquivalentTo(new List<string>(){"this", "primo", "a", "test",   "of"});
        
    }
     
}