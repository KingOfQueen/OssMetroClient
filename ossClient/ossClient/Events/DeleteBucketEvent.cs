using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    class DeleteBucketEvent
    {
        public DeleteBucketEvent(string _bucketName)
        {
            bucketName = _bucketName;
        }
        public string bucketName;
    }
}
