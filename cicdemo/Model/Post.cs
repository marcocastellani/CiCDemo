namespace cicdemo.Model;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public PostSize Size { get; set; }

    public enum PostSize
    {
        Small = 1, // valorizzare sempre per evitare che un refactor cambi il significato
        Medium =2  
    }
}