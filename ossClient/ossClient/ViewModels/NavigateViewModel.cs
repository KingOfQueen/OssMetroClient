using Caliburn.Micro;
using Oss;
using OssClientMetro.Events;
using OssClientMetro.Framework;
using OssClientMetro.Model;
using OssClientMetro.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OssClientMetro.Services;

namespace OssClientMetro.ViewModels
{
    class NavigateViewModel : PropertyChangedBase, ILeftWorkSpace, IHandle<BuketSelectedUiUpdateEvent>, IHandle<TaskCountEvent>,
        IHandle<CreateBucketEvent>, IHandle<ChangeBucketAcl>, IHandle<TaskAddNumEvent>
    {
        readonly IWindowManager windowManager;
        readonly IEventAggregator events;
        readonly IClientService clientService;

        public NavigateViewModel(IEventAggregator _events, IClientService _clientService,  IWindowManager _windowManager)
        {
            events = _events;
            clientService = _clientService;
            buckets = clientService.buckets;
            events.Subscribe(this);
            windowManager = _windowManager;
        }

        bool uiSelected = true;
        public void Publish()
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

        public bool IsDeleteAvialable
        {
            get { return selectedBuketIndex > -1; }
        }

        private int m_selectedBuketIndex = -1;

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
                NotifyOfPropertyChange(() => this.IsDeleteAvialable);
                if (uiSelected)
                {
                    Publish();
                }
                else
                {
                    uiSelected = true;
                }
                if (selectedBuketIndex != -1)
                {
                    selectedViewIndex = -1;
                    NotifyOfPropertyChange(() => this.selectedViewIndex);
                }
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

                await buckets.createBucket(inputBucketName, CannedAccessControlList.Private);
             
            }
            catch (Exception ex)
            {

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
                if (windowManager.ShowMetroMessageBox("是否删除Bucket " + bucketName + "?", "Warning",
                                       MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    await clientService.folders.deleteBuketResource(bucketName);
                    await buckets.deleteBucket(bucketName);
                    events.Publish(new DeleteBucketEvent(bucketName));
                }
            }
        }

        public void Handle(BuketSelectedUiUpdateEvent message)
        {
            if (message.BuketName != null)
            {
                uiSelected = false;
                Bucket bucket = buckets.First(x => x.Name == message.BuketName);
                if (bucket != null)
                    selectedBuketIndex = buckets.IndexOf(bucket);
                else
                    selectedBuketIndex = -1;
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

        private int selectedViewIndex = -1;

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
                if (selectedViewIndex != -1)
                {
                    uiSelected = false;
                    selectedBuketIndex = -1;
                    NotifyOfPropertyChange(() => this.selectedBuketIndex);
                }
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
            //if (TextBoxActive == Visibility.Visible && inputBucketName != "")
            //{
            //    createBucket();
            //}

            CreateBucketViewModel createBucketViewModel = new CreateBucketViewModel(events);
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            settings.Title = "创建Bucket";
            windowManager.ShowDialog(createBucketViewModel, null, settings);
        }

        public async void Handle(CreateBucketEvent createBucketEvent)
        {
            try
            {
                if (buckets.FirstOrDefault(x => x.Name == createBucketEvent.bucketName) != null)
                {
                    windowManager.ShowMetroMessageBox(createBucketEvent.bucketName + "已经存在！", "Warning",
                                       MessageBoxButton.OK);
                }
                else
                {
                    await buckets.createBucket(createBucketEvent.bucketName, createBucketEvent.type);
                }
            }
            catch (Exception ex)
            {
                windowManager.ShowMetroMessageBox(ex.Message, "Error",
                                       MessageBoxButton.OK);
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

        public async void changeBucketAcl()
        {
            string bucketName = buckets[selectedBuketIndex].Name;
            CannedAccessControlList type =  await buckets.getBucketAcl(bucketName);


            CreateBucketViewModel createBucketViewModel = new CreateBucketViewModel(bucketName, type, events);
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            settings.Title = "修改Bucket权限";
            windowManager.ShowDialog(createBucketViewModel, null, settings);

        }


        public async void Handle(ChangeBucketAcl changeBucketAcl)
        {
            try
            {
                await buckets.setBucketAcl(changeBucketAcl.bucketName, changeBucketAcl.type);
            }
            catch (Exception ex)
            {
                windowManager.ShowMetroMessageBox(ex.Message, "Error",
                                       MessageBoxButton.OK);
            }

        }
        bool addTaskNumVisibility = false;

        public bool AddTaskNumVisibility
        {
            get
            {
                return this.addTaskNumVisibility;
            }
            set
            {
                this.addTaskNumVisibility = value;
                NotifyOfPropertyChange(() => this.AddTaskNumVisibility);
            }
        }

        string addTaskNumText = "";

        public string AddTaskNumText
        {
            get
            {
                return this.addTaskNumText;
            }
            set
            {
                this.addTaskNumText = value;
                NotifyOfPropertyChange(() => this.AddTaskNumText);
            }
        }
        




       public async void Handle(TaskAddNumEvent massage)
       {
           AddTaskNumText = "+1";
           AddTaskNumVisibility = true;
           await Task.Delay(1000);
           AddTaskNumVisibility = false;

       }
      //  testVisibility



        public BucketListModel buckets { get; set; }

    }
}
