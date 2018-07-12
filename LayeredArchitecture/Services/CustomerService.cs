using System;
using System.Collections.Generic;
using System.Linq;
using LayeredArchitecture.Domain;
using LayeredArchitecture.Persistence;

namespace LayeredArchitecture.Services
{
	public class CustomerService : ICustomerService
    {
	    readonly IRepository<Customer> _customerRepository;

		public CustomerService(IRepository<Customer> customerRepository)
        {
			_customerRepository = customerRepository;
        }

		public Customer CreateCustomer(Customer customer)
		{
			if(customer == null)
			{
				throw new ArgumentNullException();
			}

			return _customerRepository.Save(customer);
		}

		public IEnumerable<Customer> FindByAge(int age)
		{
			return _customerRepository.FindAll(u => u.Age == age);
		}

		public IEnumerable<Customer> FindByCompany(string company)
		{
			return _customerRepository.FindAll(u => u.Company == company);
		}

		public IEnumerable<Customer> FindByFavoriteDrink(string drink)
		{
			return _customerRepository.FindAll(u => u.FavoriteDrink == drink);
		}

		public Customer GetCustomer(int identifier)
		{
			return _customerRepository.FindFirst(u => u.Id == identifier);
		}

		public IQueryable<Customer> GetCustomers()
		{
			return _customerRepository.Table;
		}
	}
}
