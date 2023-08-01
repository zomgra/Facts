using Microsoft.EntityFrameworkCore;
using Storage.Core;

namespace Storage.API.Data
{
    public interface IFactDbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Fact> Facts { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
