using Infra;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Customer.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationContext _context;

        public CustomerController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers;
            return View(customers);
        }
    }
}
