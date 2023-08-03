using EmaiSender.Core.Inderfaces;
using EmaiSender.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.API.Data
{
    public class EmailDbContext : DbContext, IEmailDbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
