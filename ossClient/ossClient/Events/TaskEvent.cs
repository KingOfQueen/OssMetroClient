using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    public enum TaskEventType
    {
        DOWNLOADING,
        UPLOADING,
        COMPELETED
    }


    public class TaskEvent
    {
        public TaskEvent(ObjectModel _obj, TaskEventType _type)
        {
            obj = _obj;
            type = _type;
        }

        public ObjectModel obj;
        public TaskEventType type;


    }
}
