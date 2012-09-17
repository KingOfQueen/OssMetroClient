using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OssClientMetro.Model
{
    public abstract class ObjectModel
    {
        public string bucketName { get; set; }
        public string displayName { get; set; }
        public string key { get; set; }
        public DateTime modifyTime { get; set; }
    }
}
