using System.Text.RegularExpressions;
using cicdemo.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace cicdemo.Services;

public class PostCalculatorService
{
    private readonly BlogDC _context;
    public const int SmallPost = 200;
    public const int MediumPost = 600;

    public PostCalculatorService(BlogDC context)
    {
        _context = context;
    }
    
    public Post.PostSize CalculateSize(Post post)
    {
        if (post.Content.Length > MediumPost)
            throw new TooLongException(MediumPost);

        return post.Content.Length > SmallPost ? Post.PostSize.Medium : Post.PostSize.Small;
    }

    public List<string> GetFrequentWords(string content, int minFrequency)
    {
        var words = Regex.Replace(content.ToLower(), @"[^\w\s]", "").Split(' ');

        var wordFrequency = words.GroupBy(w => w)
            .Where(g => g.Key.Length > 0)
            .ToDictionary(g => g.Key, g => g.Count());

        var frequentWords = wordFrequency.Where(wf => wf.Value >= minFrequency)
            .Select(wf => wf.Key)
            .ToList();

        return frequentWords.Except(GetStopWords()).ToList();
    }

    public List<string> GetStopWords()
    {
        var stopWords = _context.Database.SqlQuery<string>($"select magicstoredproc()").ToList();
        return stopWords;
    }
}