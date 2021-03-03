using Microsoft.EntityFrameworkCore;

namespace InvertedIndexLibrary
{
    public class InvertedIndexContext : DbContext
    {
        public DbSet<SearchItem> SearchItems { get; set; }
        public DbSet<Doc> Docs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PT8A28F;Database=InvertedIndexDb;Trusted_Connection=True;");
        }

    }
}