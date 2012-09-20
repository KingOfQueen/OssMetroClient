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
                if (message.type == BuketSelectedEventType.DOWNLOADINGVIEW)
                {
                    ObjectList = downloadingListModel;
                }
                else if (message.type == BuketSelectedEventType.UPLOADINGVIEW)
                {
                    ObjectList = uploadingListModel;
                }
                else if (message.type == BuketSelectedEventType.COMPELETEDVIEW)
                {
                    ObjectList = compeletedListModel;
                }

            }
            catch (Exception ex)
            {

            }
        }




        public BindableCollection<ObjectModel> downloadingListModel = new BindableCollection<ObjectModel>();
        public BindableCollection<ObjectModel> uploadingListModel = new BindableCollection<ObjectModel>();
        public BindableCollection<ObjectModel> compeletedListModel = new BindableCollection<ObjectModel>();

        private BindableCollection<ObjectModel> objectList;




        public BindableCollection<ObjectModel> ObjectList 
        {
            get
            {
                return this.objectList;
            }
            set
            {
                this.objectList = value;
                NotifyOfPropertyChange(() => this.ObjectList);
            }
        }

    }
}
