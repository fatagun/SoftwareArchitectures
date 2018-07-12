using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LayeredArchitecture.Models;
using LayeredArchitecture.Services;
using LayeredArchitecture.Domain;

namespace LayeredArchitecture.Controllers
{
    public class HomeController : Controller
    {
		readonly ICustomerService _customerService;

		public HomeController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public IActionResult Index()
        {
			var customers = _customerService.GetCustomers();

            return View(customers);
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Customer customer)
		{
			if(ModelState.IsValid == false)
			{
				return View(customer);
			}

			var createdCustomer = _customerService.CreateCustomer(customer);

			return RedirectToAction("Details", new { id = createdCustomer.Id});
		}

		public IActionResult Details(int id)
		{
			var customer = _customerService.GetCustomer(id);

			return View(customer);
		}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
