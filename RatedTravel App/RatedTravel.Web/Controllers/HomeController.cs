﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RatedTravel.Web.ViewModels.Home;

namespace RatedTravel.App.Web.Controllers
{
    public class HomeController : Controller
    {
       

        public HomeController()
        {
            
        }

        public IActionResult Index()
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