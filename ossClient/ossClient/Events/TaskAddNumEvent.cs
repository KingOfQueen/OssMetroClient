using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    class TaskAddNumEvent
    {
        public TaskAddNumEvent(int _num)
        {
            num = _num;
        }
        public int num;
    }
}
