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
using System.Windows;

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
            objectModelForSerialList = CompleteTaskListFile.readFromFile();
            if (objectModelForSerialList != null)
            {
                foreach (ObjectModelForSerial obj in objectModelForSerialList)
                {

                    if (obj.key.EndsWith("/"))
                    {
                        FolderModel folder = new FolderModel() { bucketName = obj.bucketName, key = obj.key };
                        folder.initial();
                        folder.localPath = obj.localPath;
                        folder.Size = obj.size;
                        folder.completeTime = obj.completeTime;
                        folder.compeleteStatus = obj.compeleteStatus;
                        compeletedListModel.Add(folder);
                    }
                    else
                    {
                        FileModel fileModel = new FileModel() { bucketName = obj.bucketName, key = obj.key, Size = obj.size };
                        fileModel.initial();
                        fileModel.localPath = obj.localPath;
                        fileModel.completeTime = obj.completeTime;
                        fileModel.compeleteStatus = obj.compeleteStatus;
                        compeletedListModel.Add(fileModel);
                    }
                }
                events.Publish(new TaskCountEvent(compeletedListModel.Count, TaskCountEventType.COMPELETED));
            }
            else
            {
                objectModelForSerialList = new List<ObjectModelForSerial>();
            }
 


        
            
        }

        public  void Handle(DownloadViewEvent message)
        {
            try
            {
                if (message.type == DownloadViewEventType.DOWNLOADINGVIEW)
                {
                    ObjectList = downloadingListModel;
                    setDownloadVis();
                }
                else if (message.type == DownloadViewEventType.UPLOADINGVIEW)
                {
                    ObjectList = uploadingListModel;
                    setDownloadVis();
                }
                else if (message.type == DownloadViewEventType.COMPELETEDVIEW)
                {
                    //ObjectList = compeletedListModel;
                    setCompleteVis();
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
                    if (!isInTheList(downloadingListModel, taskEvent.obj))
                    {
                        downloadingListModel.Insert(0, taskEvent.obj);
                        events.Publish(new TaskStartEvent(taskEvent.obj, TaskStartEventType.DOWNLOAD));
                        events.Publish(new TaskCountEvent(downloadingListModel.Count, TaskCountEventType.DOWNLOADING));
                    }
                    else
                    {
                        windowManager.ShowMetroMessageBox("你已经将该任务添加到下载队列中", "Error",
                                     MessageBoxButton.OK);
                    }
                }
                else if (taskEvent.type == TaskEventType.UPLOADING)
                {
                    if (!isInTheList(uploadingListModel, taskEvent.obj))
                    {
                        uploadingListModel.Insert(0, taskEvent.obj);
                        events.Publish(new TaskStartEvent(taskEvent.obj, TaskStartEventType.UPLOAD));
                        events.Publish(new TaskCountEvent(uploadingListModel.Count, TaskCountEventType.UPLOADING));
                    }
                    else
                    {
                        windowManager.ShowMetroMessageBox("你已经将该任务添加到上传队列中", "Error",
                                     MessageBoxButton.OK);
                    }
                }
                else if (taskEvent.type == TaskEventType.DOWNLOADCOMPELETED)
                {
                    downloadingListModel.Remove(taskEvent.obj);
                    taskEvent.obj.completeTime = DateTime.Now;
                    taskEvent.obj.compeleteStatus = "下载完成";
                    addToCompleteList(taskEvent.obj);
                    events.Publish(new TaskCountEvent(downloadingListModel.Count, TaskCountEventType.DOWNLOADING));
                    events.Publish(new TaskCountEvent(compeletedListModel.Count, TaskCountEventType.COMPELETED));


                }
                else if (taskEvent.type == TaskEventType.UPLOADCOMPELETED)
                {
                    uploadingListModel.Remove(taskEvent.obj);
                    taskEvent.obj.completeTime = DateTime.Now;
                    taskEvent.obj.compeleteStatus = "上传完成";
                    addToCompleteList(taskEvent.obj);
                    events.Publish(new TaskCountEvent(uploadingListModel.Count, TaskCountEventType.UPLOADING));
                    events.Publish(new TaskCountEvent(compeletedListModel.Count, TaskCountEventType.COMPELETED));
                }
                else if (taskEvent.type == TaskEventType.DOWNLOADCANCEL)
                {
                    downloadingListModel.Remove(taskEvent.obj);
                    events.Publish(new TaskCountEvent(downloadingListModel.Count, TaskCountEventType.DOWNLOADING));
                }
                else if (taskEvent.type == TaskEventType.UPLOADCANCEL)
                {
                    uploadingListModel.Remove(taskEvent.obj);                  
                    events.Publish(new TaskCountEvent(uploadingListModel.Count, TaskCountEventType.UPLOADING));
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
        public List<ObjectModelForSerial> objectModelForSerialList;


        void addToCompleteList(ObjectModel obj)
        {
            compeletedListModel.Insert(0, obj);
            objectModelForSerialList.Insert(0, new ObjectModelForSerial()
                  {
                      bucketName = obj.bucketName,
                      key = obj.key,
                      displayName = obj.displayName,
                      localPath = obj.localPath,
                      size = obj.Size,
                      completeTime = obj.completeTime,
                      compeleteStatus = obj.compeleteStatus
                  });


              //List<ObjectModelForSerial> temp = new List<ObjectModelForSerial>();
              //foreach (ObjectModel objM in compeletedListModel)
              //{
              //    temp.Add(new ObjectModelForSerial()
              //    {
              //        bucketName = objM.bucketName,
              //        key = objM.key,
              //        displayName = objM.displayName,
              //        localPath = objM.localPath,
              //        size = objM.Size
              //    });
              //}


              CompleteTaskListFile.writeToFile(objectModelForSerialList);
        }

        void deleteInCompleteList(ObjectModel obj)
        {
            compeletedListModel.Remove(obj);
            objectModelForSerialList.RemoveAll( x => ( x.bucketName == obj.bucketName
                     && x.key == obj.key
                     && x.displayName == obj.displayName
                     && x.localPath == obj.localPath));


            CompleteTaskListFile.writeToFile(objectModelForSerialList);
        }


        bool isInTheList(BindableCollection<ObjectModel> list,  ObjectModel obj)
        {
            return list.FirstOrDefault(x => x.bucketName == obj.bucketName
                     && x.key == obj.key
                     && x.displayName == obj.displayName
                     && x.localPath == obj.localPath) != null;
        }

       


        public BindableCollection<ObjectModel> downloadingListModel;
        public BindableCollection<ObjectModel> uploadingListModel;
        public BindableCollection<ObjectModel> compeletedListModel { get; set; }

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

        public void deleteOperate()
        {
            ObjectModel objModel = null;
            if (CompleteVis)
            {
                objModel = compeletedListModel[selectedIndex];
            }
            else
            {
                objModel = objectList[selectedIndex];
            }
            deleteOperate(objModel);



        }

        public void deleteOperate(ObjectModel objModel)
        {
         
            if (CompleteVis)
            {
                if (windowManager.ShowMetroMessageBox("是否删除记录 " + objModel.displayName + "?", "Warning",
                                      MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    deleteInCompleteList(objModel);
                    events.Publish(new TaskCountEvent(compeletedListModel.Count, TaskCountEventType.COMPELETED));
                }
            }
            else
            {       string warningText = "";
                if (downloadingListModel == objectList)
                {
                    warningText += "是否取消下载";
                }
                else
                {
                    warningText += "是否取消上传";
                }
                if (objModel is FileModel)
                {
                    warningText += "文件 ";
                    if (windowManager.ShowMetroMessageBox(warningText + objModel.displayName + "?", "Warning",
                                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        objModel.tokenSource.Cancel();
                    }
                }
                else
                {
                    warningText += "文件夹 ";
                    if (windowManager.ShowMetroMessageBox(warningText + objModel.displayName + "?", "Warning",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        ((FolderModel)objModel).cancelTask();
                    }
                }
                
            }

        }

        public void openLocalFolder()
       {
           ObjectModel objModel = null;
           if (CompleteVis)
           {
               objModel = compeletedListModel[selectedIndex];
           }
           else
            {
                 objModel = objectList[selectedIndex];
           }
           openLocalFolder(objModel);


     }
        public void openLocalFolder(ObjectModel objModel)
        {
            Process ExplorerWindowProcess = new Process();

            ExplorerWindowProcess.StartInfo.FileName = "explorer.exe";
            ExplorerWindowProcess.StartInfo.Arguments = "/select,\"" + objModel.localPath + "\""; ;

            ExplorerWindowProcess.Start();

        }


        void setDownloadVis()
        {
            DownloadVis = true;
            CompleteVis = false;
        }

        void setCompleteVis()
        {
            DownloadVis = false;
            CompleteVis = true;
        }

        bool completeVis;

        public bool CompleteVis
        {
            get
            {
                return this.completeVis;
            }
            set
            {
                this.completeVis = value;
                NotifyOfPropertyChange(() => this.CompleteVis);
            }
        }

        bool downloadVis;

        public bool DownloadVis
        {
            get
            {
                return this.downloadVis;
            }
            set
            {
                this.downloadVis = value;
                NotifyOfPropertyChange(() => this.DownloadVis);
            }
        }

        
    }
}
