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
    class ObjectViewModel : PropertyChangedBase, IRightWorkSpace, IHandle<SelectedPathEvent>, IHandle<CreateFolderEvent>
    {
            readonly IEventAggregator events;
        readonly IClientService clientService;
        readonly IWindowManager windowManager;

        public ObjectViewModel(IEventAggregator _events, IClientService _clientService, IWindowManager _windowManager)
       {
            this.events = _events;
            clientService = _clientService;
            windowManager = _windowManager;
            objListModel = new ObjectListModel(new OssClient("bm9crcnr0rtnuw8bnrfvq7w8", "RbtJoExTnA8vYLynUfDh7Ior+oM="));
            events.Subscribe(this);
            objectList = new BindableCollection<ObjectModel>();
            objListModel = _clientService.objects;

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

       ObjectModel handleObject(OssObjectSummary obj, string prefix)
       {
           string subString = obj.Key.Remove(0, prefix.Length);
           string[] ss = subString.Split('/');
           if (ss.Count() == 1 && subString != "")
                 return new ObjectModel(){ BucketName = obj.BucketName, key = obj.Key};
           else if (ss.Count() == 2 && subString.EndsWith("/"))
               return new ObjectModel() { BucketName = obj.BucketName, key = obj.Key };
           else
           {
               if (ss.Count() > 1 && ss[1] != "")
               {
                   if (objectList.FirstOrDefault(x => x.key == (prefix + ss[0] + "/")) == null)
                   {
                       return new ObjectModel() { BucketName = obj.BucketName, key = (prefix + ss[0] + "/") };
                   }
               }
               
           }


           return null;
       }


       public void createFolder()
       {
           windowManager.ShowWindow(new CreateFolderViewModel(events));
       }

        public void refreshObjectList(string BucketName, string key)
        {
            IEnumerable<OssObjectSummary> list = objListModel.getObjectList(BucketName, key);

          objectList.Clear();
          foreach (OssObjectSummary obj in list)
          {

              ObjectModel model =   handleObject(obj, key);
              if(model != null)
                  objectList.Add(model);
          }

        }

       public void passInto()
       {
           ObjectModel temp = objectList[selectedIndex];
           currentFolderObj = temp;

           refreshObjectList(temp.BucketName, temp.key);


           //events.Publish(new SelectedPathEvent(buckets[selectedBuketIndex].Name));

       }
       ObjectModel currentFolderObj;
       string currentBuketName;

         public   void Handle(SelectedPathEvent message)
         {
             IEnumerable<OssObjectSummary>list = objListModel.getObjectList(message.BuketName);
             currentBuketName = message.BuketName;
             objectList.Clear();
             foreach (OssObjectSummary obj in list)
             {
                 ObjectModel model = handleObject(obj, "");
                 if (model != null)
                     objectList.Add(model);               
             }

         }



         public async void Handle(CreateFolderEvent message)
         {
             try
             {
                 MemoryStream s = new MemoryStream();
                 ObjectMetadata oMetaData = new ObjectMetadata();
                 OssObjectSummary ossObjSummary  = new OssObjectSummary();
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
                 objListModel.Add(ossObjSummary);
                 if (currentFolderObj == null)
                 {
                     refreshObjectList(currentBuketName, "");
                 }
                 else
                 {
                     refreshObjectList(currentBuketName, currentFolderObj.key);
                 }

             }
             catch (Exception ex)
             {

             }
         }

         public ObjectListModel objListModel;
         public BindableCollection<ObjectModel> objectList { get; set; }

    }
}
