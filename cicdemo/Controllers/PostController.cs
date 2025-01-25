using cicdemo.Model;
using cicdemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly BlogDC _context;
    public PostController(BlogDC context)
    {
        _context = context;
    }

    // GET: api/Post
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPost()
    {
        return await _context.Posts.ToListAsync();
    }

    // GET: api/Post/5
    [HttpGet("{postid}")]
    public async Task<ActionResult<Post>> GetPost(int postid)
    {
        var post = await _context.Posts.FindAsync(postid);

        if (post == null)
        {
            return NotFound();
        }

        return post;
    }

    // PUT: api/Post/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{postid}")]
    public async Task<IActionResult> PutPost(int? postid, Post post)
    {
        if (postid != post.PostId)
        {
            return BadRequest();
        }
        
        _context.Entry(post).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostExists(postid))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Post
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Post>> PostPost(Post post)
    {
        _context.Posts.Add(post);
        var bcs = new PostCalculatorService(null);
        
        post.Size = bcs.CalculateSize(post);
        
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPost", new { postid = post.PostId }, post);
    }

    // DELETE: api/Post/5
    [HttpDelete("{postid}")]
    public async Task<IActionResult> DeletePost(int? postid)
    {
        var post = await _context.Posts.FindAsync(postid);
        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PostExists(int? postid)
    {
        return _context.Posts.Any(e => e.PostId == postid);
    }
}
