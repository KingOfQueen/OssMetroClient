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

        public async Task<List<ObjectListing>> getObjectListing(string buketName, string folderKey = "", bool hasDelimiter = false)
        {
            List<ObjectListing> resultList = new List<ObjectListing>();
            ListObjectsRequest listRequest = new ListObjectsRequest(buketName);

            if (folderKey != "")
                listRequest.Prefix = folderKey;

            if (hasDelimiter)
                listRequest.Delimiter = "/";
            ObjectListing reslut = await client.ListObjects(listRequest);
            resultList.Add(reslut);

            while (reslut.IsTrunked)
            {
                listRequest.Marker = reslut.NextMarker;
                reslut = await client.ListObjects(listRequest);
                resultList.Add(reslut);
            }

            return resultList;
        }



        FolderModel find(string buketName, string folderKey)
        {
            return this.Find(x => x.bucketName == buketName && x.key == folderKey);
        }

        async Task<FolderModel> getFolderModelFromWeb(string buketName, string folderKey = "")
        {


            List<ObjectListing> listObjectListing = await getObjectListing(buketName, folderKey, true);
          

            FolderModel folderModel = new FolderModel();
            folderModel.bucketName = buketName;
            folderModel.key = folderKey;
            folderModel.objList = new List<FileModel>();
            folderModel.folderList = new List<FolderModel>();

            foreach (ObjectListing objectlisting in listObjectListing)
            {
                foreach (OssObjectSummary ossObj in objectlisting.ObjectSummaries)
                {
                    if (ossObj.Key != folderKey)
                    {
                        FileModel fileModel = new FileModel() { bucketName = ossObj.BucketName, key = ossObj.Key, Size = ossObj.Size };
                        fileModel.modifyTime = ossObj.LastModified;
                        fileModel.initial();
                        folderModel.objList.Add(fileModel);
                    }
                }

                foreach (string prefix in objectlisting.CommonPrefixes)
                {
                    FolderModel folder= new FolderModel() { bucketName = folderModel.bucketName, key = prefix };
                    folder.initial();
                    folderModel.folderList.Add(folder);
                }
            }

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
            try
            {
                List<ObjectListing> listObjectListing = await getObjectListing(buketName, key);

                foreach (ObjectListing objectlisting in listObjectListing)
                {
                    foreach (OssObjectSummary ossObj in objectlisting.ObjectSummaries)
                    {
                        await client.DeleteObject(ossObj.BucketName, ossObj.Key);
                    }
                }
                FolderModel folderModle = find(buketName, key);
                this.Remove(folderModle);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public async Task deleteFile(string buketName, string key)
        {
            await client.DeleteObject(buketName, key);
        }

        public async Task deleteBuketResource(string buketName)
        {
            List<ObjectListing> listObjectListing = await getObjectListing(buketName);

            foreach (ObjectListing objectlisting in listObjectListing)
            {
                foreach (OssObjectSummary ossObj in objectlisting.ObjectSummaries)
                {
                    await client.DeleteObject(ossObj.BucketName, ossObj.Key);
                }
            }

            this.RemoveAll(x => x.bucketName == buketName);
        }

        public async Task<OssObject> downloadFile(string buketName, string key, Action<HttpProcessData> callback = null, System.Threading.CancellationToken? cancellationToken = null)
        {
            return await client.GetObject(buketName, key, callback, cancellationToken);
        }

        public async Task initFolderForDownload(FolderModel folderModel)
        {
            List<ObjectListing> listObjectListing = await getObjectListing(folderModel.bucketName, folderModel.key);

         


            long size = 0;
            folderModel.objListAll = new List<FileModel>();


            foreach (ObjectListing objectlisting in listObjectListing)
            {
                foreach (OssObjectSummary ossObj in objectlisting.ObjectSummaries)
                {
                    if (!ossObj.Key.EndsWith("/"))
                        folderModel.objListAll.Add(new FileModel() { bucketName = ossObj.BucketName, key = ossObj.Key, Size = ossObj.Size });
                    size += ossObj.Size;
                }
            }
            folderModel.Size = size;
        }





       public  OssClient client{get; set;}
    }
}
