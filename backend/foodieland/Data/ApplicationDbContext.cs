using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
    {
    }
    
    public DbSet<Recipe> Recipes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recipe>().HasData(
            new Recipe() {Name = "Pancakes"},
            new Recipe() {Name = "Egg fried rice"},
            new Recipe() {Name = "Shepherd's pie"}
        );
    }
}