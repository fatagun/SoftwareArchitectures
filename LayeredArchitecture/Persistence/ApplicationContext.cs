using System;
using LayeredArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace LayeredArchitecture.Persistence
{
	public class ApplicationContext : DbContext
    {
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

		public DbSet<Customer> Customers { set; get; }
    }
}
