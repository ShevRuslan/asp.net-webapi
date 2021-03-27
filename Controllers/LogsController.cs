﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("api/allData")]
        public IActionResult GetAllData ()
        {
            StreamReader stream = new StreamReader(pathToJson);
            return new ObjectResult(stream.ReadToEnd());
        }

        [HttpGet("api/scan")]
        public IActionResult GetScan()
        {
            StreamReader stream = new StreamReader(pathToJson);
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(stream.ReadToEnd());
            return new ObjectResult(jsonObject.scan);
        }

        [HttpGet("api/filenames")]
        public IActionResult GetFilenameByResult(bool value)
        {
            StreamReader stream = new StreamReader(pathToJson);
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(stream.ReadToEnd());
            List<FilesLogs> foundBooks = jsonObject.files.FindAll(file => file.result == value);
            return new ObjectResult(foundBooks);
        }
    }
}
