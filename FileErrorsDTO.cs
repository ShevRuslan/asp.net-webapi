using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class FileErrorsDTO
    {
        public string filename { get; set; }
        public List<string> errors { get; set; }
    }
}
