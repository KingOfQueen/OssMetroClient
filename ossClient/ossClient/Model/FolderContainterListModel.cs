using Oss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Model
{
    public class FolderContainterListModel : List<FolderContainterModel>
    {

        public FolderContainterListModel(OssClient _client) 
        {
            client = _client;
        }

        public async Task<FolderContainterModel> getFolderModel(string buketName, string folderKey = "")
        {
            FolderContainterModel folderModle = find(buketName, folderKey);
            if (folderModle == null)
            {
                return await getFolderModelFromWeb(buketName, folderKey);
            }
            else
            {
                return folderModle;
            }

        }

        FolderContainterModel find(string buketName, string folderKey)
        {
            return this.Find(x => x.buketName == buketName && x.folderKey == folderKey);
        }

        async Task<FolderContainterModel> getFolderModelFromWeb(string buketName, string folderKey = "")
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

            FolderContainterModel folderModel = new FolderContainterModel();
            folderModel.buketName = buketName;
            folderModel.folderKey = folderKey;
            folderModel.objList = resultObjList;
            folderModel.CommonPrefixes = resultCommonPrefixes;
            this.Add(folderModel);

            return folderModel;
        }

        public async Task<FolderContainterModel> refreshFolderModel(string buketName, string folderKey = "")
        {
            FolderContainterModel folderModle = find(buketName, folderKey);
            this.Remove(folderModle);

            return await getFolderModelFromWeb(buketName, folderKey);
        }




        public async Task deleteObjectModel(ObjectModel objModel)
        {
            if (objModel is FileModel)
            {
                await client.DeleteObject(objModel.bucketName, objModel.key);
            }
            else
            {
                FolderContainterModel folderModle = await getFolderModel(objModel.bucketName, objModel.key);
                foreach (OssObjectSummary ossObjSummary in folderModle.objList)
                {
                    await client.DeleteObject(ossObjSummary.BucketName, ossObjSummary.Key);
                }
                this.Remove(folderModle);
            }

        }

        
       public  OssClient client{get; set;}
    }
}
