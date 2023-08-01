﻿using Microsoft.EntityFrameworkCore;
using Storage.Core;

namespace Storage.API.Data
{
    public class FactsDbContext : DbContext, IFactDbContext
    {
        public FactsDbContext(DbContextOptions<FactsDbContext> options) : base(options)
        {

        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Fact> Facts { get; set; }
    }
}
