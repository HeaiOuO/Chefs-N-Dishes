using Microsoft.EntityFrameworkCore;
using ChefDish.Models;

namespace ChefDish.Models
{
    public class ChefContext : DbContext
    {
        public ChefContext(DbContextOptions<ChefContext> options) : base(options) {}

        public DbSet<Chef> chefs {get; set;}
        public DbSet<Dish> dishs {get; set;}

    }   
}