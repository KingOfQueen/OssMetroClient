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

       public void createFolder()
       {
           createFolderVM = new CreateFolderViewModel(events);
           windowManager.ShowWindow(createFolderVM);
       }

        public void refreshObjectList(FolderContainterModel folderModel)
        {
            if (folderModel == null)
            {
                objectList.Clear();
            }
            else
            {
                IEnumerable<OssObjectSummary> list = folderModel.objList;

                objectList.Clear();

                foreach (OssObjectSummary obj in list)
                {
                    if (obj.Key != folderModel.folderKey)
                        objectList.Add(new FileModel() { bucketName = obj.BucketName, key = obj.Key });
                }

                foreach (string prefix in folderModel.CommonPrefixes)
                {

                    objectList.Add(new FolderModel() { bucketName = folderModel.buketName, key = prefix });
                }
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
            currentFolder = await folderListModel.refreshFolderModel(currentFolder.buketName, currentFolder.folderKey);
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
                 MemoryStream s = new MemoryStream();
                 ObjectMetadata oMetaData = new ObjectMetadata();
                 OssObjectSummary ossObjSummary = new OssObjectSummary();
                 ossObjSummary.BucketName = currentFolder.buketName;

                 ossObjSummary.Key = currentFolder.folderKey + message.folderName + "/";

                 await clientService.ossClient.PutObject(ossObjSummary.BucketName, ossObjSummary.Key, s, oMetaData);
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
           if (currentFolder.buketName != ss[0])
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


       async void downloadfile(string bucketName, string key, string fileName)
       {
           OssObject obj = await folderListModel.downloadFile(bucketName, key);        
           Stream stream = obj.Content;
           FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
           await stream.CopyToAsync(fs);
           fs.Position = 0;
           fs.Flush();
           fs.Close();
           stream.Close();
       }

       async void downloadFolder(string bucketName, string key, string  savePath)
       {
           if(!Directory.Exists(savePath))
               Directory.CreateDirectory(savePath);

           FolderContainterModel folderModel = await folderListModel.getFolderModel(bucketName, key);

           IEnumerable<OssObjectSummary> list = folderModel.objList;


           foreach (OssObjectSummary obj in list)
           {
               if (obj.Key != folderModel.folderKey)
                   downloadfile(obj.BucketName, obj.Key, savePath + "/" + obj.Key.Substring(folderModel.folderKey.Length));
           }

           foreach (string prefix in folderModel.CommonPrefixes)
           {
               downloadFolder(bucketName, prefix, savePath + "/" + prefix.Substring(folderModel.folderKey.Length));
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
                    ObjectModel objModel = objectList[selectedIndex];
                    if (objModel != null)
                    {

                        if (objModel is FileModel)
                        {
                            string fileName = foulderPath + "/" + objModel.key.Substring(currentFolder.folderKey.Length);
                            downloadfile(objModel.bucketName, objModel.key, fileName);
                        }
                        else
                        {
                            downloadFolder(objModel.bucketName, objModel.key, foulderPath + "/" + objModel.key.Substring(currentFolder.folderKey.Length));
                        }
                    }
                }

             }
       }

       private async void uploadSingleFile()
       {

       }


       public async void uploadFile()
       {
           string[] fileNames = fileFolderDialogService.openFileDialog();
           if (fileNames != null)
           {
               foreach (string fileName in fileNames)
               {

               }

           }
          

       }

       public void uploadFolder()
       {

       }
         
         public FolderContainterListModel folderListModel;
         public FolderContainterModel currentFolder;
         public BindableCollection<ObjectModel> objectList { get; set; }
         public History history{get;set;}
    }
}
