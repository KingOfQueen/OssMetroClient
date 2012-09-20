using Caliburn.Micro;
using OssClientMetro.Events;
using OssClientMetro.Framework;
using OssClientMetro.Model;
using OssClientMetro.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.ViewModels
{
    class DownloadViewModel : PropertyChangedBase, IHandle<DownloadViewEvent>
    {


        readonly IEventAggregator events;
        readonly IClientService clientService;
        readonly IWindowManager windowManager;

        public DownloadViewModel(IEventAggregator _events, IClientService _clientService,
            IWindowManager _windowManager)
        {
            this.events = _events;
            clientService = _clientService;
            windowManager = _windowManager;
            events.Subscribe(this);
        }

        public  void Handle(DownloadViewEvent message)
        {
            try
            {
                objectList.Clear();
                if (message.type == BuketSelectedEventType.DOWNLOADINGVIEW)
                {       
                    objectList.AddRange(downloadingListModel);
                }

            }
            catch (Exception ex)
            {

            }
        }




        public FolderListModel downloadingListModel;
        public FolderListModel uploadingListModel;
        public FolderListModel compeletedListModel;

        public BindableCollection<ObjectModel> objectList { get; set; }

    }
}
