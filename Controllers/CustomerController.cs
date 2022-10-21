using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo_Product.Controllers
{
    public class CustomerController : Controller
    {
        CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
        JobManager jobManager = new JobManager(new EfJobDal());
        public IActionResult Index()
        {
            var values = customerManager.GetCustomersListWithJob();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            JobManager jobManager = new JobManager(new EfJobDal());

            List<SelectListItem> value = (from x in jobManager.TGetList()
                                         select new  SelectListItem
                                             {
                                             Text = x.JobName,
                                             Value = x.JobName

            }).ToList();
            ViewBag.v = value;

            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            customerManager.TAdd(customer);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCustomer(int id)
        {
            var result = customerManager.TGetById(id);
            customerManager.TRemove(result);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            
            List<SelectListItem> values = (from x in jobManager.TGetList()
                                          select new SelectListItem
                                          {
                                              Text = x.JobName,
                                              Value = x.JobName

                                          }).ToList();
            ViewBag.v = values;
            var value = customerManager.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
       

            customerManager.TUpdate(customer);
            return RedirectToAction("Index");
        }
    }
}
