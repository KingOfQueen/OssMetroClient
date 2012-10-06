using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Model
{
    class AccessControlModel
    {
        public AccessControlModel(CannedAccessControlList _type)
        {
            type = _type;
        }
        public CannedAccessControlList type { get; set; }
        public string DisplayName
        {
            get
            {
                if (type == CannedAccessControlList.Private)
                    return "private";
                if (type == CannedAccessControlList.PublicRead)
                    return "public-read";
                if (type == CannedAccessControlList.PublicReadWrite)
                    return "public-read-write";

                return "";
            }
        }
    }
}
