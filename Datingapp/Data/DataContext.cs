using Datingapp.Entities;

namespace Datingapp.Data;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    //Constructor for DataContext
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    //Connecting Entities into DBSet
    public DbSet<AppUser> Users { get; set; }
}