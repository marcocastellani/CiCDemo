using Microsoft.EntityFrameworkCore;

public class BlogDC(DbContextOptions<BlogDC> options) : DbContext(options)
{
    public DbSet<Blog> Blog { get; set; } = default!;
}
