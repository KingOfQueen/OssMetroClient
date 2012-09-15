using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Model
{
    class ObjectListModel : List<OssObjectSummary>
    {
        public ObjectListModel(OssClient _client) 
        {
            client = _client;
        }

        public async Task createData(IEnumerable<Bucket> buketlist)
        {

            foreach (Bucket buket in buketlist)
            {
                ListObjectsRequest listRequest = new ListObjectsRequest(buket.Name);
                ObjectListing reslut = await client.ListObjects(listRequest);
                this.AddRange(reslut.ObjectSummaries);
                while (reslut.IsTrunked)
                {
                    ListObjectsRequest listRequest2 = new ListObjectsRequest(buket.Name);
                    listRequest2.Marker = reslut.NextMarker;
                    reslut = await client.ListObjects(listRequest2);
                    this.AddRange(reslut.ObjectSummaries);
                }
                
            }


        }



        public OssClient client;
        public  ObjectListModel(OssClient _client, IEnumerable<Bucket> buketlist)
        {
            client = _client;
        }

        //bool indir(string key, string prefix = "")
        //{
        //    string subString = key.Remove(0, prefix.Length);
        //    string [] ss = subString.Split('/');

        //    if ((ss.Count() == 1 && subString != "") || (ss.Count() == 2 && subString.EndsWith("/")))
        //        return true;

        //    return false;
        //}

        //void addFolder( IEnumerable<OssObjectSummary>  list, string prefix)
        //{
        //    foreach (OssObjectSummary obj in list)
        //    {
        //        string subString = obj.Key.Remove(0, prefix.Length);
        //        string[] ss = subString.Split('/');
        //        if (ss.Count() > 1 && ss[1] != "")
        //        {
        //            if (this.Find(x => x.Key == (prefix + ss[0] + "/")) == null)
        //            {
        //                this.Add(new OssObjectSummary() {});
        //            }
        //        }
        //    }

        //}



        public IEnumerable<OssObjectSummary> getObjectList(string buketName)
        {
            var result = from ossObject in this
                         where (ossObject.BucketName == buketName)
                         select ossObject;
            return result;
        }


        public IEnumerable<OssObjectSummary> getObjectList(string buketName, string prefix)
        {
           var result =  from ossObject in this 
                         where (ossObject.BucketName == buketName
                         && ossObject.Key.StartsWith(prefix))
                         select ossObject; 


           return result;
        }



        
    }
}
