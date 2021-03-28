using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Reflection;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class LogsController : Controller
    {
        private string pathToJson = "data.json";
        private string text = "";
        public LogsController()
        {
            StreamReader stream = new StreamReader(pathToJson);
            text = stream.ReadToEnd();
        }

        [HttpGet("api/allData")]
        public IActionResult GetAllData ()
        {
            return new ObjectResult(text);
        }

        [HttpGet("api/scan")]
        public IActionResult GetScan()
        {
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(text);
            return new ObjectResult(jsonObject.scan);
        }

        [HttpGet("api/filenames")]
        public IActionResult GetFilenameByResult(bool correct)
        {
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(text);
            List<FilesLogs> foundBooks = jsonObject.files.FindAll(file => file.result == correct);
            return new ObjectResult(foundBooks);
        }
        [HttpGet("api/errors")]
        public IActionResult GetErrors()
        {
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(text);
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
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(text);
            return new ObjectResult(jsonObject.scan.errorCount);
        }
        [HttpGet("api/errors/{index}")]
        public IActionResult GetErrorCount(int index)
        {
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(text);
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
            if (index >= 0 && index < filesErrors.Count)
                return new ObjectResult(filesErrors[index]);
            else
                return NotFound(new ErrorDTO { statusCode = 404, error = "Элемент с таким индексом не найден!" });
        }
        [HttpGet("api/query/check")]
        public IActionResult GetFileByNameQuery()
        {
            FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(text);
            List<FilesLogs> files = jsonObject.files;
            int total = 0;
            int correct = 0;
            int errors = 0;
            List<string> filenames = new List<string>();
            foreach(FilesLogs file in files)
            {
                string[] arrayWord = file.filename.ToLower().Split("_");
                if(arrayWord[0] == "query")
                {
                    total++;
                    if (file.result) {
                        correct++;
                    }
                    else
                    {
                        errors++;
                        filenames.Add(file.filename);
                    }
                }
            }
            return new ObjectResult(new QueryCheckDTO { 
                correct = correct,
                total = total,
                errors = errors,
                filenames = filenames,
            });
        }
        [HttpPost("api/newErrors")]
        public IActionResult AddNewErrors([FromForm] string data)
        {
            try
            {
                FileLogs jsonObject = JsonSerializer.Deserialize<FileLogs>(data);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                System.IO.File.WriteAllText(String.Format("{0}.json", fileName), data);
                return new ObjectResult(jsonObject);
            }
            catch(Exception error)
            {
                return BadRequest(new { error = "Данные не правильные! Ошибка парсинга!" });
            }
        }
        [HttpGet("api/service/serviceInfo")]
        public IActionResult GetServiceInfo()
        {
            return new ObjectResult(new ServiceInfoDTO { 
                AppName = Assembly.GetExecutingAssembly().FullName,
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                DateUtc = DateTime.Now.ToUniversalTime(),
            });
        }
    }
}
