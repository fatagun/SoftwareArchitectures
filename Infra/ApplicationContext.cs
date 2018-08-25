using System; 
using Microsoft.EntityFrameworkCore;
using Modular.Domain;

namespace Infra
{
	public class ApplicationContext : DbContext
    {
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

		public DbSet<Customer> Customers { set; get; }
    }
}
