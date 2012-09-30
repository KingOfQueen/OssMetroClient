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
                foreach (ObjectModelForSerial obj in temp)
                {

                    if (obj.key.EndsWith("/"))
                    {
                        FolderModel folder = new FolderModel() { bucketName = obj.bucketName, key = obj.key };
                        folder.initial();
                        folder.localPath = obj.localPath;
                        folder.Size = obj.size;
                        compeletedListModel.Add(folder);
                    }
                    else
                    {
                        FileModel fileModel = new FileModel() { bucketName = obj.bucketName, key = obj.key, Size = obj.size };
                        fileModel.initial();
                        fileModel.localPath = obj.localPath;
                        compeletedListModel.Add(fileModel);
                    }
                }
                events.Publish(new TaskCountEvent(compeletedListModel.Count ,TaskCountEventType.COMPELETED));
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
                    events.Publish(new TaskCountEvent(downloadingListModel.Count, TaskCountEventType.DOWNLOADING));
                }
                else if (taskEvent.type == TaskEventType.UPLOADING)
                {
                    uploadingListModel.Add(taskEvent.obj);
                    events.Publish(new TaskCountEvent(uploadingListModel.Count, TaskCountEventType.UPLOADING));
                }
                else if (taskEvent.type == TaskEventType.DOWNLOADCOMPELETED)
                {
                    downloadingListModel.Remove(taskEvent.obj);
                    addToCompleteList(taskEvent.obj);
                    events.Publish(new TaskCountEvent(compeletedListModel.Count, TaskCountEventType.COMPELETED));


                }
                else if (taskEvent.type == TaskEventType.UPLOADCOMPELETED)
                {
                    uploadingListModel.Remove(taskEvent.obj);
                    addToCompleteList(taskEvent.obj);
                    events.Publish(new TaskCountEvent(compeletedListModel.Count, TaskCountEventType.COMPELETED));
                }

            }
            catch (Exception ex)
            {

            }
        }

        public string bucketName { get; set; }
        public string displayName { get; set; }
        public string key { get; set; }
        public string localPath { get; set; }
        public long size { get; set; }
        void addToCompleteList(ObjectModel obj)
        {
              compeletedListModel.Add(obj);
              List<ObjectModelForSerial> temp = new List<ObjectModelForSerial>();
              foreach (ObjectModel objM in compeletedListModel)
              {
                  temp.Add(new ObjectModelForSerial()
                  {
                      bucketName = objM.bucketName,
                      key = objM.key,
                      displayName = objM.displayName,
                      localPath = objM.localPath,
                      size = objM.Size
                  });
              }


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
