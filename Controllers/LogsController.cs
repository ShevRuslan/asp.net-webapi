using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Controllers
{
    public class LogsController : Controller
    {
        private string pathToJson = "data.json";

        public IActionResult Index()
        {
            return View();
        }
        private ReadFile
    }
}
