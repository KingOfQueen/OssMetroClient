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

namespace OssClientMetro.ViewModels
{
    class ObjectViewModel : PropertyChangedBase, IRightWorkSpace, IHandle<SelectedPathEvent>
    {
            readonly IEventAggregator events;
        readonly IClientService clientService;

        public ObjectViewModel(IEventAggregator _events, IClientService _clientService)
       {
            this.events = _events;
            clientService = _clientService;
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




       public void Publish()
       {
           ObjectModel temp = objectList[selectedIndex];

           IEnumerable<OssObjectSummary> list = objListModel.getObjectList(temp.BucketName, temp.key);

          objectList.Clear();
          foreach (OssObjectSummary obj in list)
          {

              ObjectModel model =   handleObject(obj, temp.key);
              if(model != null)
                  objectList.Add(model);
          }


           //events.Publish(new SelectedPathEvent(buckets[selectedBuketIndex].Name));

       }

         public   void Handle(SelectedPathEvent message)
         {
             IEnumerable<OssObjectSummary>list = objListModel.getObjectList(message.BuketName);

             objectList.Clear();
             foreach (OssObjectSummary obj in list)
             {
                 ObjectModel model = handleObject(obj, "");
                 if (model != null)
                     objectList.Add(model);               
             }

         }

         public ObjectListModel objListModel;
         public BindableCollection<ObjectModel> objectList { get; set; }

    }
}
