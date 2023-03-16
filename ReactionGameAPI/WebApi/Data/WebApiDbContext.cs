using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }

        //The tables of the database
        public DbSet<Highscore> Highscores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WebApi.db");
        }
    }
}


