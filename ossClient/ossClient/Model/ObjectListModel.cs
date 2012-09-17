using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Model
{
    public class ObjectListModel : List<OssObjectSummary>
    {
        public ObjectListModel(OssClient _client) 
        {
            client = _client;
        }

        public async Task createData(IEnumerable<Bucket> buketlist)
        {

            foreach (Bucket buket in buketlist)
            {
                this.AddRange(await getObjListFromWeb(buket.Name));                          
            }


        }


        private async Task<IEnumerable<OssObjectSummary>> getObjListFromWeb(string buketName, string prefix = "")
        {
            List<OssObjectSummary> resultList = new List<OssObjectSummary>();
            ListObjectsRequest listRequest = new ListObjectsRequest(buketName);
            ObjectListing reslut = await client.ListObjects(listRequest);
            resultList.AddRange(reslut.ObjectSummaries);
            while (reslut.IsTrunked)
            {
                ListObjectsRequest listRequest2 = new ListObjectsRequest(buketName);
                listRequest2.Marker = reslut.NextMarker;
                reslut = await client.ListObjects(listRequest2);
                resultList.AddRange(reslut.ObjectSummaries);
            }

            return resultList;
        }



        public OssClient client;
        public  ObjectListModel(OssClient _client, IEnumerable<Bucket> buketlist)
        {
            client = _client;
        }


        public IEnumerable<OssObjectSummary> getObjectList(string buketName, string prefix = "")
        {
           var result =  from ossObject in this 
                         where (ossObject.BucketName == buketName
                         && ossObject.Key.StartsWith(prefix))
                         select ossObject; 


           return result;
        }


        public async Task refreshFolder(string buketName, string prefix = "")
        {
            ListObjectsRequest arg = new ListObjectsRequest(buketName);
            arg.Delimiter = @"/";
            arg.Prefix = prefix;
            ObjectListing result = await client.ListObjects(arg);


        }

        
    }
}
