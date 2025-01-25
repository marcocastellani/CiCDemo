using cicdemo.Model;
using Microsoft.EntityFrameworkCore;

public class BlogDC(DbContextOptions<BlogDC> options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; } = default!;
    
}
