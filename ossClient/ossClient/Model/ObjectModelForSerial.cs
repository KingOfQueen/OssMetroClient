using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Model
{
    [Serializable]
    class ObjectModelForSerial
    {
        public string bucketName { get; set; }
        public string displayName { get; set; }
        public string key { get; set; }
        public string localPath { get; set; }
        public long? size{ get; set; }
        public DateTime completeTime { get; set; }
        public string compeleteStatus { get; set; }
    }
}
