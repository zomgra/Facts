using Microsoft.EntityFrameworkCore;

namespace Storage.API.Data
{
    public class FactsDbContext : DbContext
    {
        public FactsDbContext(DbContextOptions<FactsDbContext> options) : base(options)
        {

        }
    }
}
