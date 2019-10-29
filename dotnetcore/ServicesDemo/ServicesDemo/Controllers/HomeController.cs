using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesDemo.Models;
using ServicesDemo.Services;
using Microsoft.Extensions.Configuration;

namespace ServicesDemo.Controllers
{
    public class HomeController : Controller
    {
        private IDataManager dm;
        private IConfiguration configuration;

        public HomeController(IDataManager dataManager /*, IConfiguration config*/)
        {
            this.dm = dataManager;
            //this.configuration = config;
        }
        //public HomeController(SqlDataManager dataManager)
        //{
        //    this.dm = dataManager;
        //}
        public IActionResult Index([FromServices]IConfiguration configuration)
        {

            ViewBag.Message = dm.GetData();
            ViewBag.Username = configuration.GetValue<string>("UserDetails:Name");
            ViewBag.Age = configuration.GetValue<string>("UserDetails:Age");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
