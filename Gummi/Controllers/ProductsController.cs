using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gummi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gummi.Controllers
{
  public class ProductsController : Controller
  {

    private IProductRepository productRepo;  // New!

		public ProductsController(IProductRepository repo = null)
		{
			if (repo == null)
			{
				this.productRepo = new EFProductRepository();
			}
			else
			{
				this.productRepo = repo;
			}
		}


		public ViewResult Index()
		{
			return View(productRepo.Products.ToList());
		}

    public IActionResult Details(int id)
    {
			Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
			return View(thisProduct);
    }

    public IActionResult Create()
    {
            ViewBag.CategoryId = new SelectList(productRepo.Categories, "CategoryId", "Name");
            return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
      productRepo.Save(product);
      return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
            ViewBag.CategoryId = new SelectList(productRepo.Categories, "CategoryId", "Name");
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
            productRepo.Edit(product);
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
      return View(thisProduct);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            productRepo.Remove(thisProduct);
      return RedirectToAction("Index");
    }
  }
}
