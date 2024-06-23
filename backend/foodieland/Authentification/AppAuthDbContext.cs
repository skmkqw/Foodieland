using foodieland.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Authentification;
public class AppAuthDbContext(DbContextOptions<AppAuthDbContext> options, IConfiguration configuration) : IdentityDbContext<AppUser>(options)
{ 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    { 
        base.OnConfiguring(optionsBuilder); 
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); 
    }
}