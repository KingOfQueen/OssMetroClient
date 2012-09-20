using Oss;
using System;
using System.Collections.Generic;
using System.IO;
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
            return this.Find(x => x.bucketName == buketName && x.key == folderKey);
        }

        async Task<FolderModel> getFolderModelFromWeb(string buketName, string folderKey = "")
        {
            List<OssObjectSummary> resultObjList = new List<OssObjectSummary>();
            List<string> resultCommonPrefixes = new List<string>();
            ListObjectsRequest listRequest = new ListObjectsRequest(buketName);

            if (folderKey != "")
                listRequest.Prefix = folderKey;
            listRequest.Delimiter = "/";
            ObjectListing reslut = await client.ListObjects(listRequest);
            resultObjList.AddRange(reslut.ObjectSummaries);
            resultCommonPrefixes.AddRange(reslut.CommonPrefixes);
            while (reslut.IsTrunked)
            {
                ListObjectsRequest listRequest2 = new ListObjectsRequest(buketName);
                listRequest2.Marker = reslut.NextMarker;
                reslut = await client.ListObjects(listRequest2);
                resultObjList.AddRange(reslut.ObjectSummaries);
                resultCommonPrefixes.AddRange(reslut.CommonPrefixes);
            }

            FolderModel folderModel = new FolderModel();
            folderModel.bucketName = buketName;
            folderModel.key = folderKey;
            folderModel.objList = new List<FileModel>();

            foreach (OssObjectSummary ossObj in resultObjList)
            {
                if (ossObj.Key != folderKey)
                {
                    folderModel.objList.Add(new FileModel() { bucketName = ossObj.BucketName, key = ossObj.Key, Size = ossObj .Size});
                }
            }

            folderModel.folderList = new List<FolderModel>();
            foreach (string prefix in resultCommonPrefixes)
            {
                folderModel.folderList.Add(new FolderModel() { bucketName = folderModel.bucketName, key = prefix });
            }
;
            this.Add(folderModel);

            return folderModel;
        }

        public async Task<FolderModel> refreshFolderModel(string buketName, string folderKey = "")
        {
            FolderModel folderModle = find(buketName, folderKey);
            this.Remove(folderModle);

            return await getFolderModelFromWeb(buketName, folderKey);
        }

        public async Task deleteFolder(string buketName, string key)
        {
            FolderModel folderModle = await getFolderModel(buketName, key);
            foreach (FileModel file in folderModle.objList)
            {
                await client.DeleteObject(file.bucketName, file.key);
            }
            this.Remove(folderModle);

        }

        public async Task deleteFile(string buketName, string key)
        {
            await client.DeleteObject(buketName, key);
        }

        public async Task deleteBuket(string buketName)
        {
            ListObjectsRequest listRequest = new ListObjectsRequest(buketName);
            ObjectListing reslut = await client.ListObjects(listRequest);

            foreach (OssObjectSummary ossObjSummary in reslut.ObjectSummaries)
            {
                await client.DeleteObject(ossObjSummary.BucketName, ossObjSummary.Key);
            }

            this.RemoveAll(x => x.bucketName == buketName);
        }

        public async Task<OssObject> downloadFile(string buketName, string key)
        {
            return await client.GetObject(buketName, key);
        }


        
       public  OssClient client{get; set;}
    }
}
