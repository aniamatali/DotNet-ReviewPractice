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

		//private GummiDbContext db = new GummiDbContext();

		public ViewResult Index()
		{
			// Updated:
			return View(productRepo.Products.ToList());
		}

		//public IActionResult Index()
    //{
    //  return View(db.Products.Include(items => items.Category).ToList());
    //}

    public IActionResult Details(int id)
    {
			//var thisProduct = db.Products.FirstOrDefault(items => items.ProductId == id);Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
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
            //db.Products.Add(item);
            productRepo.Save(product);
      //db.SaveChanges();
      return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
            //var thisProduct = db.Products.FirstOrDefault(items => items.ProductId == id);
            ViewBag.CategoryId = new SelectList(productRepo.Categories, "CategoryId", "Name");
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
            //db.Entry(item).State = EntityState.Modified;
            //db.SaveChanges();

            productRepo.Edit(product);
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
            //var thisProduct = db.Products.FirstOrDefault(items => items.ProductId == id);
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
      return View(thisProduct);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            //var thisProduct = db.Products.FirstOrDefault(items => items.ProductId == id);
            productRepo.Remove(thisProduct);
      //db.Products.Remove(thisProduct);
      //db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
