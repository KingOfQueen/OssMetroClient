using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{

    public enum TaskStartEventType
    {
        DOWNLOAD,
        UPLOAD
    }


    class TaskStartEvent
    {
        public TaskStartEvent(ObjectModel _obj, TaskStartEventType _type)
        {
            type = _type;
            obj = _obj;
        }
        public ObjectModel obj;
        public TaskStartEventType type;
    }
}
