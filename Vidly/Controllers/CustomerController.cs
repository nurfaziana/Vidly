using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;

namespace Vidly2.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var customer = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customer);
        }

        public ActionResult New()  //section 4
        { 
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            }; 

            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id)  //section 4  --edit form
        {
            var customer = _context.Customers.SingleOrDefault(c => c.ID == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);     //new.cshtml
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.ID == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)  //section 4  --req dataa  //updating data tukar ke 
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.ID == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.ID == customer.ID);

                //TryUpdateModel(customerInDb);  //tak disarankan, akan ade issue

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            //_context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        

        
        

        // GET: Customers
        //public ActionResult Index()
        //{
        //    //var customer = new Customer() { Name = "John" };
        //    //return View(customer);

        //    var customers = new List<Customer>
        //    {
        //        new Customer { Name = "John", ID = 1 },
        //        new Customer { Name = "Wick", ID = 2 }
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Customers = customers
        //    };


        //    return View(viewModel);
        //}

        //public ActionResult Details(int id)
        //{
        //    if (id == 1)
        //    {
        //        var customers = new List<Customer>
        //        {
        //            new Customer { Name = "John", ID = id }
        //        };

        //        var viewModel = new RandomMovieViewModel
        //        {
        //            Customers = customers
        //        };
        //        return View(viewModel);
        //    }
        //    else if (id == 2)
        //    {
        //        var customers = new List<Customer>
        //        {
        //            new Customer { Name = "Wick", ID = id }
        //        };

        //        var viewModel = new RandomMovieViewModel
        //        {
        //            Customers = customers
        //        };
        //        return View(viewModel);
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }

        //    //return View(viewModel);
        //}
    }
}