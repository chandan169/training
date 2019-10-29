using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConfigurationDemo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;
        private AppConfiguration appConfig;
        public HomeController(IConfiguration config, IOptions<AppConfiguration> options,
            IOptions<ProjectDetails> projectDetails)
        {
            this.configuration = config;
            this.appConfig = options.Value;
        }
        public IActionResult Index()
        {
            //var company = configuration.GetValue<string>("CompanyName");
            //var location = configuration.GetValue<string>("Location");
            //var PCount = configuration.GetValue<int>("ParticipantCount");
            //var arch = configuration.GetValue<string>("PROCESSOR_ARCHITECTURE");
            //var NOoFpROCESSOR = configuration.GetValue<string>("NUMBER_OF_PROCESSORs");
            //var title = configuration.GetValue<string>("ProjectDetail:Title");
            //var project = configuration.GetSection("ProjectDetail");
            //var projtitle = project["Title"];
            //var duration = project["Duration"];
            //var status = project["Status"];

            var companyName = appConfig.CompanyName;
            var location = appConfig.Location;
            var partCount = appConfig.ParticipantCount;

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
