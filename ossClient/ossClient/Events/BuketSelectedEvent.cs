using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    class BuketSelectedEvent
    {
        public BuketSelectedEvent(string buketName)
        {
            BuketName = buketName;
        }

        public string BuketName { get; set; }



    }
}
