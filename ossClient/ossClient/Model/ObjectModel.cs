using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OssClientMetro.Model
{
    class ObjectModel
    {
        public  ObjectModel(string _bucketName, string _key)
        {
            bucketName = _bucketName;
            key = _key;
        }

        public string bucketName { get; set; }
        public string displayName { get; set; }
        public string key { get; set; }
        public DateTime modifyTime { get; set; }
    }
}
