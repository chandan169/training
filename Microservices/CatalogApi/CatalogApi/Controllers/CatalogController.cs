using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Infrastructure;
using CatalogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private CatalogContext db;

        public CatalogController(CatalogContext db)
        {
            this.db = db;
        }

        [HttpGet("", Name = "GetProducts")]
        public async Task<List<CatalogItem>> GetProducts()
        {
            var result = await this.db.Catalog.FindAsync<CatalogItem>(FilterDefinition<CatalogItem>.Empty);
            return result.ToList();
        }

        [HttpPost("", Name="AddProducts")]
        public ActionResult<CatalogItem> AddProduct(CatalogItem item)
        {
            this.db.Catalog.InsertOne(item);
            return item;
        }
        [HttpGet("{id}", Name="FindById")]
        public async Task<ActionResult<CatalogItem>> FindProductbyId(string id)
        {
            var builder = Builders<CatalogItem>.Filter;
            var filter = builder.Eq("Id", id);
            var item = await db.Catalog.FindAsync(filter);
            return item.FirstOrDefault();                
        }
        [HttpPost("product")]
        public ActionResult<CatalogItem> AddProduct()
        {
            var imageName = SaveImageToLocal(Request.Form.Files[0]);
            //var image = Request.Form.Files[0];
            //var dirName = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            //if(!Directory.Exists(dirName))
            //{
            //    Directory.CreateDirectory(dirName);
            //}
            //var filePath = Path.Combine(dirName, imageName);
            //using (FileStream fs = new FileStream(filePath, FileMode.Create))
            //{
            //    image.CopyTo(fs);
            //}

                var catalogItem = new CatalogItem()
                {
                    Name = Request.Form["name"],
                    Price = Double.Parse(Request.Form["price"]),
                    Quantity = Int32.Parse(Request.Form["quantity"]),
                    ManufacturingDate = DateTime.Parse(Request.Form["mDate"]),
                    ReorderLevel = Int32.Parse(Request.Form["rLevel"]),
                    Vendors = new List<Vendor>(),
                    ImageUrl = imageName
                };
            db.Catalog.InsertOne(catalogItem);
            return catalogItem;
        }
        [NonAction]
        private string SaveImageToLocal(IFormFile image)
        {
            var imageName =$"{Guid.NewGuid()}_{image.FileName}";            
            var dirName = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            var filePath = Path.Combine(dirName, imageName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fs);
            }
            return $"/Images/{imageName}";
        }
    }
}