using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IServices.Sys;
using Microsoft.AspNetCore.Mvc;

namespace EFCodeDemoWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello test");
        }
    }
}