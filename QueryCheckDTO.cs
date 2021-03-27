using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class QueryCheckDTO
    {
        public int total { get; set; }
        public int correct { get; set; }
        public int errors { get; set; }
        public List<string> filenames { get; set; }
    }
}
