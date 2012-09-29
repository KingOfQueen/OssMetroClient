using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Model
{
    class TempData
    {
        public TempData(string path)
        {
            Path = path;
        }
        public string Path { get; set; }
    }
}
