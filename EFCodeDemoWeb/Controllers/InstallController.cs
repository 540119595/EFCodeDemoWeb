using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.CodeGenerator;
using Microsoft.AspNetCore.Mvc;

namespace EFCodeDemoWeb.Controllers
{
    public class InstallController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string uname)
        {

            return View();
        }

        [HttpPost]
        public IActionResult Creating(string x)
        {
            CodeGenerator.Generate();
            return View();
        }
    }
}