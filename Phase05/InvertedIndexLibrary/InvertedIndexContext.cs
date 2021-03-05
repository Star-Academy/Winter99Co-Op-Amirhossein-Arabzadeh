using Microsoft.EntityFrameworkCore;

namespace InvertedIndexLibrary
{
    
    public class InvertedIndexContext : DbContext
    {
        public DbSet<SearchItem> SearchingItems { get; set; }
        public DbSet<Doc> Docs { get; set; }

        public InvertedIndexContext(DbContextOptions<InvertedIndexContext> options) : base(options)
        {
        }

        public InvertedIndexContext()
        {
        }


        // protected override void OnConfiguring(DbContextOptionsBuilder _dbContextOptionsBuilder)
        // {
        //     _dbContextOptionsBuilder.UseSqlServer(@"Server=DESKTOP-PT8A28F;Database=InvertedIndexDb;Trusted_Connection=True;");
        // }


    }
}