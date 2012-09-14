using ossClient.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Oss;
using ossClient.Events;

namespace ossClient.ViewModels
{
   [Export(typeof(IRightWorkSpace))]
    class ObjectViewModel : PropertyChangedBase, IRightWorkSpace, IHandle<SelectedPathEvent>
    {
       readonly IEventAggregator events;

         [ImportingConstructor]
       public ObjectViewModel(IEventAggregator _events)
       {
            this.events = _events;
            events.Subscribe(this);
        }


         public void Handle(SelectedPathEvent message)
         {
             string temp = message.BuketName;
         }


       public BindableCollection<OssObject> objectList { get; set; }

    }
}
