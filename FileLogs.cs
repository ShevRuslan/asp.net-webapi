using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class FileLogs
    {
        public string Filename { get; set; }
        public bool Result { get; set; }
        public List<ErrorLogs> Errors { get; set; }
        public string ScanTime { get; set; }
    }
}
