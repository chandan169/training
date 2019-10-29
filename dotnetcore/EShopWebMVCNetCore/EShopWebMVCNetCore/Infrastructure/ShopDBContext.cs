using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopWebMVCNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopWebMVCNetCore.Infrastructure
{
    public class ShopDBContext:DbContext
    {
        public ShopDBContext(DbContextOptions<ShopDBContext>options):base(options)
        {
                
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
