using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class FilesLogs
    {
        public string filename { get; set; }
        public bool result { get; set; }
        public List<ErrorLogs> errors { get; set; }
        public string scantime { get; set; }
    }
}
