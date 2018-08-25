using Microsoft.AspNetCore.Mvc;
using Slices.Infra; 
using System.Linq; 

namespace Slices.Features.Customer
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid == false)
            {
                return View(customer);
            }

            var createdCustomer = _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = createdCustomer.Entity.Id });
        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customers.FirstOrDefault(e=>e.Id == id);

            return View(customer);
        }
    }
}
