using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    enum TaskCountEventType
    {
        DOWNLOADING,
        UPLOADING,
        COMPELETED,
    }

    class TaskCountEvent
    {
        public TaskCountEvent(int _count, TaskCountEventType _type)
        {
            count = _count;
            type = _type;
        }

        public int count;
        public TaskCountEventType type;
    }
}
