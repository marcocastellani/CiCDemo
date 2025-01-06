using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly BlogDC _context;
    public BlogController(BlogDC context)
    {
        _context = context;
    }

    // GET: api/Blog
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> GetBlog()
    {
        return await _context.Blog.ToListAsync();
    }

    // GET: api/Blog/5
    [HttpGet("{blogid}")]
    public async Task<ActionResult<Blog>> GetBlog(int blogid)
    {
        var blog = await _context.Blog.FindAsync(blogid);

        if (blog == null)
        {
            return NotFound();
        }

        return blog;
    }

    // PUT: api/Blog/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{blogid}")]
    public async Task<IActionResult> PutBlog(int? blogid, Blog blog)
    {
        if (blogid != blog.BlogId)
        {
            return BadRequest();
        }
        
        _context.Entry(blog).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BlogExists(blogid))
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

    // POST: api/Blog
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Blog>> PostBlog(Blog blog)
    {
        _context.Blog.Add(blog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBlog", new { blogid = blog.BlogId }, blog);
    }

    // DELETE: api/Blog/5
    [HttpDelete("{blogid}")]
    public async Task<IActionResult> DeleteBlog(int? blogid)
    {
        var blog = await _context.Blog.FindAsync(blogid);
        if (blog == null)
        {
            return NotFound();
        }

        _context.Blog.Remove(blog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BlogExists(int? blogid)
    {
        return _context.Blog.Any(e => e.BlogId == blogid);
    }
}
