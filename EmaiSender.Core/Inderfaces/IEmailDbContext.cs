using Microsoft.EntityFrameworkCore;
using EmaiSender.Core.Models;

namespace EmaiSender.Core.Inderfaces
{
    public interface IEmailDbContext
    {
        DbSet<Tag> Tags { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
