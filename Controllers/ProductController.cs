using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
//using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Demo_Product.Controllers
{
    public class ProductController : Controller
    {
        ProductManager productManager = new ProductManager(new EfProductDal());
        public IActionResult Index()
        {
            var values = productManager.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            ProductValidator validator = new ProductValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(product);
            if (result.IsValid) { 
            productManager.TAdd(product);
            return RedirectToAction("Index");
        }
            else
            {
                foreach (var item in result.Errors) {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(result);
    }

        public IActionResult DeleteProduct(int id)
        {
            var result =productManager.TGetById(id);
            productManager.TRemove(result);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var value =productManager.TGetById(id); 
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
           
            productManager.TUpdate(product);
            return RedirectToAction("Index");
        }
    }
}
