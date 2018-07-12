using System;
using System.Collections.Generic;
using System.Linq;
using LayeredArchitecture.Domain;

namespace LayeredArchitecture.Services
{
	public interface ICustomerService
    {
		Customer CreateCustomer(Customer customer);
		Customer GetCustomer(int identifier);
		IEnumerable<Customer> FindByAge(int age);
		IEnumerable<Customer> FindByCompany(string company);
		IEnumerable<Customer> FindByFavoriteDrink(string drink);
		IQueryable<Customer> GetCustomers();
    }
}
