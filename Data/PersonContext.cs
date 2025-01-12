using Microsoft.EntityFrameworkCore;
using Person.API.Models;

namespace Person.API.Data;

public class PersonContext : DbContext
{
    public DbSet<PersonModel> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=person;Username=postgres;Password=123456");
        base.OnConfiguring(optionsBuilder);
    }
}