using Caliburn.Micro;
using Oss;
using OssClientMetro.Events;
using OssClientMetro.Framework;
using OssClientMetro.Model;
using OssClientMetro.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OssClientMetro.ViewModels
{
    class NavigateViewModel : PropertyChangedBase, ILeftWorkSpace, IHandle<BuketSelectedUiUpdateEvent>, IHandle<TaskCountEvent>
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
            try
            {
                if (errorInfoVis == Visibility.Visible)
                {
                    errorInfoVis = Visibility.Collapsed;
                }
                await buckets.createBucket(inputBucketName, CannedAccessControlList.Private);
                TextBoxActive = Visibility.Collapsed;
                inputBucketName = "";
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                errorInfoVis = Visibility.Visible;
            }
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

        public void Handle(TaskCountEvent message)
        {
            if (message.type == TaskCountEventType.DOWNLOADING)
                DownloadingCount = message.count;

            if (message.type == TaskCountEventType.UPLOADING)
                UploadingCount = message.count;

            if (message.type == TaskCountEventType.COMPELETED)
                CompeletedCount = message.count;

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

        public void createBucket2()
        {
            if (TextBoxActive == Visibility.Visible && inputBucketName != "")
            {
                createBucket();
            }
        }




        public void createBucket3(object sender)
        {
            NavigateView view = sender as NavigateView;
            if (TextBoxActive == Visibility.Visible && inputBucketName != "")
            {
                createBucket();
            }
        }


        string m_errorInfo;

        public string errorInfo
        {
            get
            {
                return this.m_errorInfo;
            }
            set
            {
                this.m_errorInfo = value;
                NotifyOfPropertyChange(() => this.errorInfo);
            }
        }

        Visibility textBoxActive = Visibility.Collapsed;

        public Visibility TextBoxActive
        {
            get
            {
                return this.textBoxActive;
            }
            set
            {
                this.textBoxActive = value;
                NotifyOfPropertyChange(() => this.TextBoxActive);
            }
         }

        Visibility m_errorInfoVis = Visibility.Collapsed;
        public Visibility errorInfoVis
        {
            get
            {
                return this.m_errorInfoVis;
            }
            set
            {
                this.m_errorInfoVis = value;
                NotifyOfPropertyChange(() => this.errorInfoVis);
            }
        }

        public void keydown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               

                createBucket();
            }

        }

        int downloadingCount;

        public int DownloadingCount
        {
            get
            {
                return this.downloadingCount;
            }
            set
            {
                this.downloadingCount = value;
                NotifyOfPropertyChange(() => this.DownloadingCount);
            }
        }


        int uploadingCount;

        public int UploadingCount
        {
            get
            {
                return this.uploadingCount;
            }
            set
            {
                this.uploadingCount = value;
                NotifyOfPropertyChange(() => this.UploadingCount);
            }
        }

        int compeletedCount;
        public int CompeletedCount
        {
            get
            {
                return this.compeletedCount;
            }
            set
            {
                this.compeletedCount = value;
                NotifyOfPropertyChange(() => this.CompeletedCount);
            }
        }
   


        public BucketListModel buckets { get; set; }

    }
}
