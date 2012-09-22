using Caliburn.Micro;
using Oss;
using OssClientMetro.Events;
using OssClientMetro.Framework;
using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.ViewModels
{
    class NavigateViewModel : PropertyChangedBase, ILeftWorkSpace, IHandle<BuketSelectedUiUpdateEvent>
    {
        readonly IEventAggregator events;
        readonly IClientService clientService;

        public NavigateViewModel(IEventAggregator _events, IClientService _clientService)
        {
            events = _events;
            clientService = _clientService;
            buckets = clientService.buckets;
            events.Subscribe(this);

        }

        bool uiSelected = true;
        public void Publish()
        {
            if (uiSelected)
            {
                if (selectedBuketIndex >= 0)
                {
                    events.Publish(new BuketSelectedEvent(buckets[selectedBuketIndex].Name));
                }
                else
                {
                    events.Publish(new BuketSelectedEvent(null));
                }
            }
            else
            {
                uiSelected = true;
            }

        }


        private int m_selectedBuketIndex = 0;

        public int selectedBuketIndex
        {
            get
            {
                return this.m_selectedBuketIndex;
            }
            set
            {
                this.m_selectedBuketIndex = value;
                NotifyOfPropertyChange(() => this.selectedBuketIndex);               
            }
        }


        private string m_inputBucketName = "";

        public string inputBucketName
        {
            get
            {
                return this.m_inputBucketName;
            }
            set
            {
                this.m_inputBucketName = value;
                NotifyOfPropertyChange(() => this.inputBucketName);
            }
        }

         public async void refreshBuckets()
        {
            await buckets.refreshBuckets();
        }



        public async void createBucket()
        {
            await buckets.createBucket(inputBucketName, CannedAccessControlList.Private);
        }

        public async void deleteBucket()
        {
            if (selectedBuketIndex < 0)
            {
            }
            else
            {
                string bucketName = buckets[selectedBuketIndex].Name;
                await clientService.folders.deleteBuketResource(bucketName);
                await buckets.deleteBucket(bucketName);
            }
        }

        public void Handle(BuketSelectedUiUpdateEvent message)
        {
            if (message.BuketName != null)
            {
                uiSelected = false;
                selectedBuketIndex = buckets.IndexOf(buckets.First(x => x.Name == message.BuketName));
            }
        }

        private bool isExpened;

        public void expanded()
        {
             SelectedViewIndex = 0;
             downloadingView();
        }

        private int selectedViewIndex;

        public int SelectedViewIndex
        {
            get
            {
                return this.selectedViewIndex;
            }
            set
            {
                this.selectedViewIndex = value;
                NotifyOfPropertyChange(() => this.SelectedViewIndex);               
            }
        }

        DownloadViewEvent downloadViewEvent = new DownloadViewEvent(DownloadViewEventType.DOWNLOADINGVIEW);

        DownloadViewEvent uploadingViewEvent = new DownloadViewEvent(DownloadViewEventType.UPLOADINGVIEW);
        DownloadViewEvent compeletedViewEvent = new DownloadViewEvent(DownloadViewEventType.COMPELETEDVIEW);

        public void downloadingView()
        {
            events.Publish(downloadViewEvent);
        }

        public void uploadingView()
        {
            events.Publish(uploadingViewEvent);
        }


        public void compeletedView() 
       {
           events.Publish(compeletedViewEvent);

       }





        public BucketListModel buckets { get; set; }

    }
}
