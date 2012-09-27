using Caliburn.Micro;
using OssClientMetro.Events;
using OssClientMetro.Framework;
using OssClientMetro.Model;
using OssClientMetro.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.ViewModels
{
    class DownloadViewModel : PropertyChangedBase, IRightWorkSpace, IHandle<DownloadViewEvent>, IHandle<TaskEvent>
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
        downloadingListModel = new BindableCollection<ObjectModel>();
         uploadingListModel = new BindableCollection<ObjectModel>();
            compeletedListModel = new BindableCollection<ObjectModel>();
            var temp = CompleteTaskListFile.readFromFile();
            if (temp != null)
            {
                foreach (ObjectModel obj in temp)
                    compeletedListModel.Add(obj);
            }
 


        
            
        }

        public  void Handle(DownloadViewEvent message)
        {
            try
            {
                if (message.type == DownloadViewEventType.DOWNLOADINGVIEW)
                {
                    ObjectList = downloadingListModel;
                }
                else if (message.type == DownloadViewEventType.UPLOADINGVIEW)
                {
                    ObjectList = uploadingListModel;
                }
                else if (message.type == DownloadViewEventType.COMPELETEDVIEW)
                {
                    ObjectList = compeletedListModel;
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void Handle(TaskEvent taskEvent)
        {
            try
            {
                if (taskEvent.type == TaskEventType.DOWNLOADING)
                {
                    downloadingListModel.Add(taskEvent.obj);
                }
                else if (taskEvent.type == TaskEventType.UPLOADING)
                {
                    uploadingListModel.Add(taskEvent.obj);
                }
                else if (taskEvent.type == TaskEventType.DOWNLOADCOMPELETED)
                {
                    downloadingListModel.Remove(taskEvent.obj);
                    addToCompleteList(taskEvent.obj);

                }
                else if (taskEvent.type == TaskEventType.UPLOADCOMPELETED)
                {
                    uploadingListModel.Remove(taskEvent.obj);
                    addToCompleteList(taskEvent.obj);
                }

            }
            catch (Exception ex)
            {

            }
        }

        void addToCompleteList(ObjectModel obj)
        {
              compeletedListModel.Add(obj);
              List<ObjectModel> temp = new List<ObjectModel>();
              temp.AddRange(compeletedListModel);
              CompleteTaskListFile.writeToFile(temp);
        }

        public BindableCollection<ObjectModel> downloadingListModel;
        public BindableCollection<ObjectModel> uploadingListModel;
        public BindableCollection<ObjectModel> compeletedListModel;

        private BindableCollection<ObjectModel> objectList;

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

        public void openLocalFolder()
        {
            string localPath = objectList[selectedIndex].localPath;
            Process ExplorerWindowProcess = new Process();

            ExplorerWindowProcess.StartInfo.FileName = "explorer.exe";
            ExplorerWindowProcess.StartInfo.Arguments = "/select,\"" + localPath + "\""; ;

            ExplorerWindowProcess.Start();

        }
    }
}
