using System; 
using Microsoft.EntityFrameworkCore;
using Slices.Features.Customer;

namespace Slices.Infra
{
	public class ApplicationContext : DbContext
    {
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

		public DbSet<Customer> Customers { set; get; }
    }
}
