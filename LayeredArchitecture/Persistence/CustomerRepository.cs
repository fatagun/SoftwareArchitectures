using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LayeredArchitecture.Domain;

namespace LayeredArchitecture.Persistence
{
	public class CustomerRepository : IRepository<Customer>
    {
		readonly ApplicationContext _db;

		public CustomerRepository(ApplicationContext db)
        {
			_db = db;
        }

		public IQueryable<Customer> Table => _db.Customers;

		public IEnumerable<Customer> FindAll(Expression<Func<Customer, bool>> predicte)
		{
			return _db.Customers.Where(predicte).AsEnumerable();
		}

		public Customer FindFirst(Expression<Func<Customer, bool>> predicate)
		{
			return _db.Customers.FirstOrDefault(predicate);
		}

		public Customer FindSingle(Expression<Func<Customer, bool>> predicate)
		{
			return _db.Customers.Single(predicate);
		}

		public Customer Get(int id)
		{
			var customer = _db.Customers.FirstOrDefault(e=> e.Id == id);

			return customer;
		}

		public Customer Save(Customer customer)
		{
			if(customer == null)
			{
				throw new ArgumentNullException();
			}

			var customerRecord = _db.Customers.Add(customer).Entity;
			_db.SaveChanges();

			return customerRecord;
		}
	}
}
