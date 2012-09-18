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

        public ObjectViewModel(IEventAggregator _events, IClientService _clientService, IWindowManager _windowManager)
       {
            this.events = _events;
            clientService = _clientService;
            windowManager = _windowManager;
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
               NowPath = history.NowPath;
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
                 NowPath = history.NowPath;


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
             if (history.CanGoBack)
             {
                 history.goBack();
                 NowPath = history.NowPath;
             }

         }

       public void gofoward()
         {
             if (history.CanGoForward)
             {
                 history.goForward();
                 NowPath = history.NowPath;
             }

         }


         public string nowPath;

         public string NowPath
         {
             get
             {
                 return nowPath;
             }
             set
             {
                 this.nowPath = value;
                 NotifyOfPropertyChange(() => this.NowPath);
             }
         }
         
         
         public FolderContainterListModel folderListModel;
         public FolderContainterModel currentFolder;
         public BindableCollection<ObjectModel> objectList { get; set; }
         public History history;
    }
}
