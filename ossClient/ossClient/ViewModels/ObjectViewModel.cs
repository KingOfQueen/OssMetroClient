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

       //ObjectModel filterObject(OssObjectSummary obj, string prefix)
       //{
       //    string subString = obj.Key.Remove(0, prefix.Length);
       //    string[] ss = subString.Split('/');
       //    if (ss.Count() == 1 && subString != "")
       //        return new ObjectModel() { BucketName = obj.BucketName, key = obj.Key };
       //    else if (ss.Count() == 2 && subString.EndsWith("/"))
       //        return new ObjectModel() { BucketName = obj.BucketName, key = obj.Key };
       //    else
       //    {
       //        if (ss.Count() > 1 && ss[1] != "")
       //        {
       //            if (objectList.FirstOrDefault(x => x.key == (prefix + ss[0] + "/")) == null)
       //            {
       //                return new ObjectModel() { BucketName = obj.BucketName, key = (prefix + ss[0] + "/") };
       //            }
       //        }

       //    }


       //    return null;
       //}

       CreateFolderViewModel createFolderVM;
       public void createFolder()
       {
           createFolderVM = new CreateFolderViewModel(events);
           windowManager.ShowWindow(createFolderVM);
       }

        public void refreshObjectList(FolderModel folderModel)
        {

            IEnumerable<OssObjectSummary> list = folderModel.objList;

            objectList.Clear();
          
            foreach (OssObjectSummary obj in list)
            {

                objectList.Add(new ObjectModel(obj.BucketName, obj.Key));
;
            }

        }

        public async void OpenFolder()
       {
           ObjectModel temp = objectList[selectedIndex];

           currentFolder = await folderListModel.getFolderModel(temp.bucketName, temp.key);
           refreshObjectList(currentFolder);
       }

       ObjectModel currentFolderObj;
       string currentBuketName;

         public async   void Handle(BuketSelectedEvent message)
         {
             currentFolder =  await folderListModel.getFolderModel(message.BuketName);
             refreshObjectList(currentFolder);
         }

         public async void Handle(CreateFolderEvent message)
         {
             try
             {
                 MemoryStream s = new MemoryStream();
                 ObjectMetadata oMetaData = new ObjectMetadata();
                 OssObjectSummary ossObjSummary = new OssObjectSummary();
                 ossObjSummary.BucketName = currentBuketName;
                 if (currentFolderObj == null)
                 {

                     ossObjSummary.Key = message.folderName + "/";
                 }
                 else
                 {
                     ossObjSummary.Key = currentFolderObj.key + message.folderName + "/";
                 }
                 await clientService.ossClient.PutObject(ossObjSummary.BucketName, ossObjSummary.Key, s, oMetaData);
                 //objListModel.Add(ossObjSummary);
                 //if (currentFolderObj == null)
                 //{
                 //    refreshObjectList(currentBuketName, "");
                 //}
                 //else
                 //{
                 //    refreshObjectList(currentBuketName, currentFolderObj.key);
                 //}

             }
             catch (Exception ex)
             {

             }
         }

         
         public FolderListModel folderListModel;
         public FolderModel currentFolder;
         public BindableCollection<ObjectModel> objectList { get; set; }

    }
}
