using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Services
{

    public class Path
    {
        public Path(string _bucketName, string _key = "", string _searchKey = null)
        {
            bucketName = _bucketName;
            key = _key;
            searchKey = _searchKey;

        }

        string bucketName;
        string key;
        string searchKey;
    }
}
