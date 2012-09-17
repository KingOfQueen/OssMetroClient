using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Model
{
    public class FolderListModel : List<FolderModel>
    {

        public FolderListModel(OssClient _client) 
        {
            client = _client;
        }

        public async Task<FolderModel> getFolderModel(string buketName, string folderKey = "")
        {
            FolderModel folderModle = find(buketName, folderKey);
            if (folderModle == null)
            {
                return await getFolderModelFromWeb(buketName, folderKey);
            }
            else
            {
                return folderModle;
            }

        }

        FolderModel find(string buketName, string folderKey)
        {
            return this.Find(x => x.buketName == buketName && x.folderKey == folderKey);
        }

        async Task<FolderModel> getFolderModelFromWeb(string buketName, string folderKey = "")
        {
            List<OssObjectSummary> resultList = new List<OssObjectSummary>();
            ListObjectsRequest listRequest = new ListObjectsRequest(buketName);

            if (folderKey != "")
                listRequest.Prefix = folderKey;
            listRequest.Delimiter = "/";
            ObjectListing reslut = await client.ListObjects(listRequest);
            resultList.AddRange(reslut.ObjectSummaries);
            while (reslut.IsTrunked)
            {
                ListObjectsRequest listRequest2 = new ListObjectsRequest(buketName);
                listRequest2.Marker = reslut.NextMarker;
                reslut = await client.ListObjects(listRequest2);
                resultList.AddRange(reslut.ObjectSummaries);
            }

            FolderModel folderModel = new FolderModel();
            folderModel.buketName = buketName;
            folderModel.folderKey = folderKey;
            folderModel.objList = resultList;

            this.Add(folderModel);

            return folderModel;
        }
        
       public  OssClient client{get; set;}
    }
}
