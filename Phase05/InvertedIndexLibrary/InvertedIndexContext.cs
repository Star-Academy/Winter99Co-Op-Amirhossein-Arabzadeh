using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InvertedIndexLibrary
{
    
    public class InvertedIndexContext : DbContext
    {
        public DbSet<SearchItem> SearchingItems { get; set; }
        public DbSet<Doc> Docs { get; set; }

        public InvertedIndexContext(DbContextOptions<InvertedIndexContext> options) : base(options)
        {
        }
        
    }
    public class InvertedIndexContextFactory : IDesignTimeDbContextFactory<InvertedIndexContext>
    {
        public InvertedIndexContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InvertedIndexContext>();
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PT8A28F;Database=InvertedIndexDb;Trusted_Connection=True;");
    
            return new InvertedIndexContext(optionsBuilder.Options);
        }
    }
}