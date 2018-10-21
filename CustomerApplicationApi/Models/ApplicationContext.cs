using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerApplicationApi.Models
{
    // Class defines the context and entity that make up our model.
    // DbContext must have instance of DbContextOptions to execute.
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
