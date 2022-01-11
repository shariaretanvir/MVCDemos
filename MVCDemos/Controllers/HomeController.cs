﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCDemos.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            byte[] data =  PrivacyModel.GetData();
            ViewBag.file = Convert.ToBase64String(data);
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(PrivacyModel model, IFormFile file)
        {
            string fileName = file.FileName;
            byte[] datafile = null;
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                datafile = target.ToArray();
            }

            PrivacyModel.Save(datafile);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
