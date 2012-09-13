using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ossClient.Model
{
    class ObjectListModel : List<OssObject>
    {

        public IEnumerable<OssObject> filter(string buketName)
        {
            var result = from ossObject in this
                         where (ossObject.BucketName == buketName)
                         select ossObject;
            return result;
        }


        public IEnumerable<OssObject> filter(string buketName, string prefix)
        {
           var result =  from ossObject in this 
                         where (ossObject.BucketName == buketName
                         && ossObject.Key.StartsWith(prefix))
                         select ossObject; 
           return result;
        }



        
    }
}
