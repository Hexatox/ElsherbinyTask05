using ElsherbinyTask05.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElsherbinyTask05.API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<Book> Books { get; set; }

    public DbSet<User> Users { get; set; }



    
}
