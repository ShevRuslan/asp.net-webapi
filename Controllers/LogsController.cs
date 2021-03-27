using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class LogsController : Controller
    {
        private string pathToJson = "data.json";

        [HttpGet, Route("api/allData")]
        public IActionResult Get ()
        {
            StreamReader stream = new StreamReader(pathToJson);
            return new ObjectResult(stream.ReadToEnd());
        }
    }
}
