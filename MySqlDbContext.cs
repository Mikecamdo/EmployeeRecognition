using Microsoft.EntityFrameworkCore;

public class MySqlDbContext : DbContext
{
    
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
    {
    }
}
