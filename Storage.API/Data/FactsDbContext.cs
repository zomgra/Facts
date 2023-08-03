using Microsoft.EntityFrameworkCore;
using Storage.Core;
using Storage.Core.Interfaces;

namespace Storage.API.Data
{
    public class FactsDbContext : DbContext, IFactDbContext
    {
        public FactsDbContext(DbContextOptions<FactsDbContext> options) : base(options)
        {

        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Fact> Facts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
