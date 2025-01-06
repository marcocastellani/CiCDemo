namespace cicdemo.Services;

public class BlogCalculatorService
{
    public Post.PostSize CalculateFingerprint(Post post)
    {
        return post.Content.Length > 200 ? Post.PostSize.Medium : Post.PostSize.Small;
    }
}