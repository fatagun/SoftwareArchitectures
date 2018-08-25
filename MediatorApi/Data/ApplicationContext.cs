using System;
using MediatorApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace MediatorApi.Data
{
	public class ApplicationContext : DbContext
    {
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

		public DbSet<Customer> Customers { set; get; }
         
    }
}
