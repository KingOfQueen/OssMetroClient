using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Events
{
    class ChangeBucketAcl
    {
        public ChangeBucketAcl(string _bucketName, CannedAccessControlList _type)
        {
            bucketName = _bucketName;
            type = _type;
        }
        public string bucketName;
        public CannedAccessControlList type;
    }
}
