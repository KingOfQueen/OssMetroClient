using OssClientMetro.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Oss;
using OssClientMetro.Events;
using OssClientMetro.Model;
using System.IO;
using OssClientMetro.Services;
using System.IO.Compression;

namespace OssClientMetro.ViewModels
{
    class ObjectViewModel : PropertyChangedBase, IRightWorkSpace, IHandle<BuketSelectedEvent>, IHandle<CreateFolderEvent>
    {
            readonly IEventAggregator events;
        readonly IClientService clientService;
        readonly IWindowManager windowManager;
        readonly IFileFolderDialogService fileFolderDialogService;

        public ObjectViewModel(IEventAggregator _events, IClientService _clientService,
            IWindowManager _windowManager, IFileFolderDialogService _fileFolderDialogServic)
       {
            this.events = _events;
            clientService = _clientService;
            windowManager = _windowManager;
            fileFolderDialogService = _fileFolderDialogServic;
            events.Subscribe(this);
            objectList = new BindableCollection<ObjectModel>();
            folderListModel = _clientService.folders;
            history = new History();

        }


       private int m_selectedIndex = 0;

       public int selectedIndex
       {
           get
           {
               return this.m_selectedIndex;
           }
           set
           {
               this.m_selectedIndex = value;
               NotifyOfPropertyChange(() => this.selectedIndex);
           }
       }

       CreateFolderViewModel createFolderVM;

       public void createFolderOperate()
       {
           createFolderVM = new CreateFolderViewModel(events);
           windowManager.ShowWindow(createFolderVM);
       }

       private async Task createFolder(string bucketName, string parentKey, string folderName)  //key is the parent 
       {
           MemoryStream s = new MemoryStream();
           ObjectMetadata oMetaData = new ObjectMetadata();
           OssObjectSummary ossObjSummary = new OssObjectSummary();
           ossObjSummary.BucketName = bucketName;

           ossObjSummary.Key = parentKey + folderName + "/";

           await clientService.ossClient.PutObject(ossObjSummary.BucketName, ossObjSummary.Key, s, oMetaData);
           s.Dispose();
       }

        public void refreshObjectList(FolderModel folderModel)
        {
            if (folderModel == null)
            {
                objectList.Clear();
            }
            else
            {
                objectList.Clear();
                objectList.AddRange(folderModel.objList);
                objectList.AddRange(folderModel.folderList);
            }

        }

        public async void OpenFolder()
       {
           ObjectModel temp = objectList[selectedIndex];

           if (temp is FolderModel)
           {
               currentFolder = await folderListModel.getFolderModel(temp.bucketName, temp.key);
               refreshObjectList(currentFolder);
               history.add(temp.bucketName + "/" + temp.key);
           }
       }

        public async void refresh()
        {
            currentFolder = await folderListModel.refreshFolderModel(currentFolder.bucketName, currentFolder.key);
            refreshObjectList(currentFolder);
        }

    

         public async   void Handle(BuketSelectedEvent message)
         {
             if (message.BuketName != null)
             {
                 currentFolder = await folderListModel.getFolderModel(message.BuketName);
                 refreshObjectList(currentFolder);

                 history.add(message.BuketName + "/");
                


             }
             else
             {
                 currentFolder = null;
                 refreshObjectList(null);
             }
         }

         public async void Handle(CreateFolderEvent message)
         {
             try
             {
                  await createFolder(currentFolder.bucketName, currentFolder.key, message.folderName);
                  refresh();

             }
             catch (Exception ex)
             {

             }
         }

         public async void delete()
         {
             if (selectedIndex < 0)
             {


             }
             else
             {
                ObjectModel objModel = objectList[selectedIndex];
                if (objModel is FileModel)
                {
                    await folderListModel.deleteFile(objModel.bucketName, objModel.key);
                }
                else
                {
                    await folderListModel.deleteFolder(objModel.bucketName, objModel.key);
                }
              
                 refresh();
             }
         }


       public  void goback()
         {
            history.goBack();
            refreshPath();           
         }

       async Task refreshPath()
       {
           string[] ss = history.NowPath.Split('/');
           if (currentFolder.bucketName != ss[0])
           {
               events.Publish(new BuketSelectedUiUpdateEvent(ss[0]));
           }
           currentFolder = await folderListModel.getFolderModel(ss[0], history.NowPath.Substring(ss[0].Length + 1));
           refreshObjectList(currentFolder);
       }

       public void gofoward()
         {

             history.goForward();
             refreshPath();

         }


       async Task downloadfile(string bucketName, string key, string fileName, System.Action<HttpProcessData> callback = null)
       {
           OssObject obj = await folderListModel.downloadFile(bucketName, key, callback);        
           Stream stream = obj.Content;
           FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
           await stream.CopyToAsync(fs);
           fs.Position = 0;
           fs.Flush();
           fs.Close();
           stream.Close();
       }

       async Task downloadFolder(string bucketName, string key, string  savePath)
       {
           if(!Directory.Exists(savePath))
               Directory.CreateDirectory(savePath);

           FolderModel folderModel = await folderListModel.getFolderModel(bucketName, key);

          // IEnumerable<OssObjectSummary> list = folderModel.objList;


           foreach (FileModel file in folderModel.objList)
           {
               downloadfile(file.bucketName, file.key, savePath + "/" + file.key.Substring(folderModel.key.Length));
           }

           foreach (FolderModel folder in folderModel.folderList)
           {
               downloadFolder(folder.bucketName, folder.key, savePath + "/" + folder.key.Substring(folderModel.key.Length));
           }
       }

       public async void download()
       {
            if (selectedIndex < 0)
             {


             }
             else
             {
                string foulderPath = fileFolderDialogService.openFolderDialog();
                if (foulderPath != null)
                {
                    if (foulderPath[foulderPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                    {
                        foulderPath += System.IO.Path.DirectorySeparatorChar;
                    }

                    ObjectModel objModel = objectList[selectedIndex];
                    if (objModel != null)
                    {

                        if (objModel is FileModel)
                        {
                            string fileName = foulderPath  + objModel.key.Substring(currentFolder.key.Length);
                            events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADING));
                            await downloadfile(objModel.bucketName, objModel.key, fileName, objModel.callback);
                            events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADCOMPELETED));
                        }
                        else
                        {
                            await downloadFolder(objModel.bucketName, objModel.key, foulderPath + objModel.key.Substring(currentFolder.key.Length));
                        }
                    }
                }

             }
       }

       private async Task uploadSingleFile(string bucket, string parentKey, string fileName, System.Action<HttpProcessData> callback = null)
       {
           FileInfo fileInfo = new FileInfo(fileName);
           FileStream fs = new FileStream(fileName, FileMode.Open);
           ObjectMetadata oMetaData = new ObjectMetadata();
           await folderListModel.client.PutObject(bucket, parentKey + fileInfo.Name, fs, oMetaData, callback);
           fs.Dispose();
       }

       private async void uploadfolder(string bucket, string parentKey, string dir)
       {
           DirectoryInfo dirInfo = new DirectoryInfo(dir);

           await createFolder(bucket, parentKey, dirInfo.Name);

           string currentKey = parentKey + dirInfo.Name + "/";

           FileInfo[] fileInfos = dirInfo.GetFiles();
           foreach (FileInfo fileinfo in fileInfos)
                uploadSingleFile(bucket, currentKey, fileinfo.FullName);

           DirectoryInfo[] sonDirInfos = dirInfo.GetDirectories();
           foreach (DirectoryInfo sonDirInfo in sonDirInfos)
           {
               uploadfolder(bucket, currentKey, sonDirInfo.FullName);
           }

       }

        private async Task uploadfoldeZip(string bucket, string parentKey, string dir)
       {
           DirectoryInfo dirInfo = new DirectoryInfo(dir);
           string zipFileName = dirInfo.FullName + ".zip";
            int i = 0;

            while (File.Exists(zipFileName))
            {
                zipFileName = dirInfo.FullName + "_" + i.ToString() + ".zip";
            }
           

           ZipFile.CreateFromDirectory(dir, zipFileName);
           await uploadSingleFile(bucket, parentKey, zipFileName);
       }




       public async void uploadFileOperate()
       {
           if (currentFolder != null)
           {
               string[] fileNames = fileFolderDialogService.openFileDialog();
               if (fileNames != null)
               {
                   foreach (string fileName in fileNames)
                   {
                        await uploadSingleFile(currentFolder.bucketName, currentFolder.key, fileName);
                   }
                   refresh();

               }
           }
          

       }

       public void uploadFolderOperate()
       {
           if (currentFolder != null)
           {
               string foulderPath = fileFolderDialogService.openFolderDialog();
               if (foulderPath != null)
               {
                   uploadfolder(currentFolder.bucketName, currentFolder.key, foulderPath);
               }
               refresh();
           }

       }

       public async void uploadFolderZipOperate()
       {
           if (currentFolder != null)
           {
               string foulderPath = fileFolderDialogService.openFolderDialog();
               if (foulderPath != null)
               {
                  await uploadfoldeZip(currentFolder.bucketName, currentFolder.key, foulderPath);
               }
               refresh();
           }
       }
         public FolderListModel folderListModel;
         public FolderModel currentFolder;
         public BindableCollection<ObjectModel> objectList { get; set; }
         public History history{get;set;}
    }
}
