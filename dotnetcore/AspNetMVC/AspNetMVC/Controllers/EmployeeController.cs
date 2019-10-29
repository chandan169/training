using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMVC.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        DataService ds;
        public EmployeeController(DataService ds)
        {
            this.ds = ds;
        }
        [HttpGet("List",  Name="EmpList")]
        public IActionResult Index()
        {
            ViewBag.DIdata = ds.Message;
            string message = "Hello i am from Session data";
            var data = Encoding.UTF8.GetBytes(message);
            HttpContext.Session.Set("message", data);
            
            return View();
        }
        [HttpGet("details")]
        public IActionResult Details()
        {
            //byte[] ar = new byte[1024];
            //HttpContext.Session.TryGetValue("message", out ar);
            ViewBag.Message = HttpContext.Session.GetString("message");// Encoding.UTF8.GetString(ar);
            return View();
        }

        [HttpGet("mypage")]
        public IActionResult MyPage()
        {
            TempData["someinfo"] = "Here is my tempdata value";
            return RedirectToAction("MyPage2");
        }
        [HttpGet("mypage2")]
        public IActionResult MyPage2()
        {
            return View();
        }
    }
}

























































































































































































