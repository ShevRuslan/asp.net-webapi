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
        [HttpGet("api/errors")]
        public IActionResult GetErrors()
        {
            StreamReader stream = new StreamReader(pathToJson);
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(stream.ReadToEnd());
            List<FilesLogs> files = jsonObject.files.FindAll(file => file.result == false);
            List<FileErrorsDTO> filesErrors = new List<FileErrorsDTO>();
            foreach(FilesLogs file in files)
            {
                List<string> errors = new List<string>();

                foreach(ErrorLogs error in file.errors)
                {
                    errors.Add(error.error);
                }


                filesErrors.Add(new FileErrorsDTO
                {
                    filename = file.filename,
                    errors = errors
                });
            }
            return new ObjectResult(filesErrors);
        }
        [HttpGet("api/errors/count")]
        public IActionResult GetErrorCount()
        {
            StreamReader stream = new StreamReader(pathToJson);
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(stream.ReadToEnd());
            return new ObjectResult(jsonObject.scan.errorCount);
        }
        [HttpGet("api/errors/{index}")]
        public IActionResult GetErrorCount(int index)
        {
            StreamReader stream = new StreamReader(pathToJson);
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(stream.ReadToEnd());
            List<FilesLogs> files = jsonObject.files.FindAll(file => file.result == false);
            List<FileErrorsDTO> filesErrors = new List<FileErrorsDTO>();
            foreach (FilesLogs file in files)
            {
                List<string> errors = new List<string>();

                foreach (ErrorLogs error in file.errors)
                {
                    errors.Add(error.error);
                }


                filesErrors.Add(new FileErrorsDTO
                {
                    filename = file.filename,
                    errors = errors
                });
            }
            if (index >= 0 && index <= filesErrors.Count)
                return new ObjectResult(filesErrors[index]);
            else
                return NotFound(new Error { statusCode = 404, error = "Элемент с таким индексом не найден!" });
        }
    }
}
