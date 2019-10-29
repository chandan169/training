using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopWebMVCNetCore.Infrastructure;
using EShopWebMVCNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopWebMVCNetCore.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private ShopDBContext db;
        private int PageSize = 5;
        private static List<Category> categories = new List<Category>()
        {
            new Category{ Id=101,Name="Mobiles",Description="get all the latest mobiles here"},
            new Category{Id=102,Name="Television",Description="get all the latest television here"},
            new Category{Id=103,Name="Camera",Description="get all the latest camera here"}
        };
        public ProductController(ShopDBContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        [HttpGet("add", Name="AddProduct")]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }
        [HttpPost("add", Name = "AddProduct")]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = categories;
                return View("AddProduct",product);
            }
        }
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if(product != null)
            {
                ViewBag.Categories = db.Categories.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost("edit")]
        public async Task<IActionResult> EditAsync(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry<Product>(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = db.Categories.ToList();
                return View("Edit", product);
            }
        }
        [HttpGet]
        public IActionResult Index(int currentPage=1)
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)db.Products.Count() / (decimal)PageSize);
            ViewBag.CurrentPage = currentPage;
            var products = GetPagedProduct(currentPage);
            return View(products);
        }
        [NonAction]
        private IEnumerable<Product> GetPagedProduct(int pageNo)
        {
            return db.Products.Include(p => p.Category).Skip(PageSize * (pageNo - 1)).Take(PageSize);
        }
    }
}