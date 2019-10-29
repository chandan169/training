using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetMVC.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using AspNetMVC.Services;

namespace AspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        IMemoryCache memoryCache;
        IDistributedCache distributedCache;
        DataService ds;
        public HomeController(IMemoryCache cache, IDistributedCache distCache, DataService ds)
        {
            memoryCache = cache;
            distributedCache = distCache;
            this.ds = ds;
        }
        public IActionResult Index()
        {
            ds.Message = "this is DI Demo";
            var data = memoryCache.Get<DateTime?>("now");
            if (data == null)
            {
                data = DateTime.Now;
                memoryCache.Set<DateTime?>("now", data, DateTimeOffset.Now.AddMinutes(3));

            }
            ViewBag.time = data;
            //Distribution cache test
            var cacheData = distributedCache.GetString("Users");
            if (string.IsNullOrEmpty(cacheData))
            {
                Dictionary<int, string> mydata = new Dictionary<int, string>
                {
                    {101, "Chandan" },
                    {102, "Priyo" },
                    {103, "Munu" }
                 };
                distributedCache.SetString("Users", JsonConvert.SerializeObject(mydata), new DistributedCacheEntryOptions {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                });
                ViewBag.Users = mydata;
                ViewBag.source = "loaded initially";
            }
            else
            {
                ViewBag.Users = JsonConvert.DeserializeObject<Dictionary<int, string>>(cacheData);
                ViewBag.source = "loaded from cache";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            if ((bool)HttpContext.Items["isVerified"])
            {
                ViewBag.Message = "Request data is valid";
            }
            else
            {
                ViewBag.Message = "Request data is invalid";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
