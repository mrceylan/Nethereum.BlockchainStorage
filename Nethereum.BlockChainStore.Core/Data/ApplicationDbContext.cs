using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsICO.Core.Entities;

namespace OpsICO.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<NodeBlock> NodeBlock { get; set; }
        public DbSet<NodeTransaction> NodeTransaction { get; set; }
        public DbSet<NodeTransactionDetail> NodeTransactionDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
