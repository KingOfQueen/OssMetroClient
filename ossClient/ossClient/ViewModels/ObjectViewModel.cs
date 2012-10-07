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
using System.IO;
using OssClientMetro.Services;
using System.IO.Compression;
using System.Windows;
using System.Dynamic;

namespace OssClientMetro.ViewModels
{
    class ObjectViewModel : PropertyChangedBase, IRightWorkSpace, 
        IHandle<BuketSelectedEvent>, IHandle<CreateFolderEvent>, IHandle<DeleteBucketEvent>
    {
        readonly IEventAggregator events;
        readonly IClientService clientService;
        readonly IWindowManager windowManager;
        readonly IFileFolderDialogService fileFolderDialogService;

        public ObjectViewModel(IEventAggregator _events, IClientService _clientService,
            IWindowManager _windowManager, IFileFolderDialogService _fileFolderDialogServic)
       {
            this.events = _events;
            clientService = _clientService;
            windowManager = _windowManager;
            fileFolderDialogService = _fileFolderDialogServic;
            events.Subscribe(this);
            objectList = new BindableCollection<ObjectModel>();
            folderListModel = _clientService.folders;
            history = new History();
           
        }


       private int m_selectedIndex = -1;

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
               NotifyOfPropertyChange(() => this.IsDownloadEnable);
               NotifyOfPropertyChange(() => this.IsFolder);
               
           }
       }

       CreateFolderViewModel createFolderVM;

       public void createFolderOperate()
       {
           createFolderVM = new CreateFolderViewModel(events);
           dynamic settings = new ExpandoObject();
           settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;
          
           settings.Title = "创建文件夹";
           windowManager.ShowDialog(createFolderVM, null, settings);
       }

       private async Task createFolder(string bucketName, string key)  //key is the parent 
       {
           MemoryStream s = new MemoryStream();
           ObjectMetadata oMetaData = new ObjectMetadata();
           OssObjectSummary ossObjSummary = new OssObjectSummary();
           ossObjSummary.BucketName = bucketName;

           ossObjSummary.Key = key;

           await clientService.ossClient.PutObject(ossObjSummary.BucketName, ossObjSummary.Key, s, oMetaData);
           s.Dispose();
       }

        public void refreshObjectList(FolderModel folderModel)
        {
            if (folderModel == null)
            {
                objectList.Clear();
            }
            else
            {
                objectList.Clear();
                objectList.AddRange(folderModel.objList);
                objectList.AddRange(folderModel.folderList);
            }

        }

        public async void OpenFolder()
       {
           ProgressVisible = true;
           ObjectModel temp = objectList[selectedIndex];

           if (temp is FolderModel)
           {
               currentFolder = await folderListModel.getFolderModel(temp.bucketName, temp.key);
               refreshObjectList(currentFolder);
               history.add(temp.bucketName + "/" + temp.key);
              // addToComboBox(temp.bucketName + "/" + temp.key);
           }
           ProgressVisible = false;
       }

        public async Task refresh()
        {
            currentFolder = await folderListModel.refreshFolderModel(currentFolder.bucketName, currentFolder.key);
            refreshObjectList(currentFolder);
        }

    

         public async   void Handle(BuketSelectedEvent message)
         {
             if (message.BuketName != null)
             {
                 currentFolder = await folderListModel.getFolderModel(message.BuketName);
                 refreshObjectList(currentFolder);
                 history.add(message.BuketName + "/");
                 IsSearchEnabled = true;
                // addToComboBox(message.BuketName + "/");
             }
             else
             {
                 IsSearchEnabled = false;
                 currentFolder = null;
                 refreshObjectList(null);
                 history.reset();
             }
         }

         public async void Handle(CreateFolderEvent message)
         {
             try
             {
                  ProgressVisible = true;
                  await createFolder(currentFolder.bucketName, currentFolder.key + message.folderName + "/");
                  await refresh();
                  ProgressVisible = false;

             }
             catch (Exception ex)
             {
                 ProgressVisible = false;
             }
         }

         public async void Handle(DeleteBucketEvent message)
         {
             //try
             //{
             //   history.paths.Removemessage.bucketName
             //}
             //catch (Exception ex)
             //{
                 
             //}
         }


         public async void delete()
         {
             ProgressVisible = true;
             if (selectedIndex < 0)
             {


             }
             else
             {
                ObjectModel objModel = objectList[selectedIndex];
                if (objModel is FileModel)
                {
                    await folderListModel.deleteFile(objModel.bucketName, objModel.key);
                }
                else
                {
                    await folderListModel.deleteFolder(objModel.bucketName, objModel.key);
                }
              
                await refresh();
             }
             ProgressVisible = false;
         }


       public async  void goback()
         {
             ProgressVisible = true;
            history.goBack();
            //addToComboBox(history.NowPath);
            await refreshPath();
            ProgressVisible = false;
         }

       async Task refreshPath()
       {
           string[] ss = history.NowPath.Split('/');
           if (currentFolder.bucketName != ss[0])
           {
               events.Publish(new BuketSelectedUiUpdateEvent(ss[0]));
           }
           currentFolder = await folderListModel.getFolderModel(ss[0], history.NowPath.Substring(ss[0].Length + 1));
           refreshObjectList(currentFolder);
       }

       public void gofoward()
         {

             history.goForward();
             //addToComboBox(history.NowPath);
             refreshPath();

         }

       public async void searchOperate(string text)
       {
           ProgressVisible = true;
           if (text != "")
           {
               FolderModel searchFolder;
               if (currentFolder != null)
                   searchFolder = currentFolder;
               else
                   searchFolder = backUpCurrentFolder;

               FolderModel folderModel = new FolderModel();
               folderModel.folderList = new List<FolderModel>();
               folderModel.objList = new List<FileModel>();

               List<ObjectListing> listObjectListing = await folderListModel.getObjectListing(searchFolder.bucketName, searchFolder.key);

               foreach (ObjectListing objectlisting in listObjectListing)
               {
                   foreach (OssObjectSummary ossObj in objectlisting.ObjectSummaries)
                   {

                       if (ossObj.Key.EndsWith("/"))
                       {
                           if (FolderModel.lastName(ossObj.Key).Contains(text))
                           {
                               FolderModel folder = new FolderModel() { bucketName = ossObj.BucketName, key = ossObj.Key };
                               folder.initial();
                               folderModel.folderList.Add(folder);
                           }
                       }
                       else
                       {
                           if (FileModel.lastName(ossObj.Key).Contains(text))
                           {
                               FileModel fileModel = new FileModel() { bucketName = ossObj.BucketName, key = ossObj.Key, Size = ossObj.Size };
                               fileModel.modifyTime = ossObj.LastModified;
                               fileModel.initial();
                               folderModel.objList.Add(fileModel);
                           }
                       }

                   }
               }
               currentFolder = null;
 
               refreshObjectList(folderModel);
               ProgressVisible = false;
           }
        }
       
      

       public async void searchOpenLoaction()
       {
   

           ProgressVisible = true;
           ObjectModel objModel = objectList[selectedIndex];
           string[] ss = objModel.key.Split('/');
           string pathKey = "";
           if (objModel.key.EndsWith("/"))
           {
               for (int i = 0; i < ss.Length - 2; i++)
                   pathKey += ss[i] + "/";
           }
           else if (objModel.key == "")
           {
               pathKey = "";
           }
           else
           {
               for (int i = 0; i < ss.Length - 1; i++)
                   pathKey += ss[i] + "/";
           }
           currentFolder = await folderListModel.getFolderModel(objModel.bucketName, pathKey);
           refreshObjectList(currentFolder);
           history.add(objModel.bucketName + "/" + pathKey);
           ObjectModel temp = objectList.First(x => objModel.bucketName == x.bucketName && objModel.key == x.key);
           selectedIndex = objectList.IndexOf(temp);
           ProgressVisible = false;
       }





       async Task downloadfile(FileModel fileModel, string fileName)
       {
           FileStream fs = null;
           Stream stream = null;
           try
           {
               fileModel.tokenSource = new System.Threading.CancellationTokenSource();
               OssObject obj = await folderListModel.downloadFile(fileModel.bucketName, fileModel.key, fileModel.callback, fileModel.tokenSource.Token);
               stream = obj.Content;
               fs = new FileStream(fileName, FileMode.OpenOrCreate);
               await stream.CopyToAsync(fs);
               fs.Position = 0;
               fs.Flush();
           }
           catch (Exception ex)
           {
               throw ex;

           }
           finally
           {
               if(fs != null)
                  fs.Close();

               if (stream != null)
                  stream.Close();
           }

       }

       async Task downloadFolder(FolderModel folderModel, string savePath)
       {
           try
           {
               List<Task> taskList = new List<Task>();
               foreach (FileModel file in folderModel.objListAll)
               {
                   string fileName = savePath + "/" + file.key.Substring(folderModel.key.Length);
                   FileInfo fileInfo = new FileInfo(fileName);
                   if (!Directory.Exists(fileInfo.DirectoryName))
                       Directory.CreateDirectory(fileInfo.DirectoryName);

                   await downloadfile(file, fileName);
               }
           }
           catch (Exception ex)
           {
               throw ex;

           }
       }

       public async void download()
       {
            if (selectedIndex < 0)
             {


             }
             else
             {
                string foulderPath = fileFolderDialogService.openFolderDialog();
                if (foulderPath != null)
                {
                    if (foulderPath[foulderPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                    {
                        foulderPath += System.IO.Path.DirectorySeparatorChar;
                    }

                    ObjectModel objModel = objectList[selectedIndex];
                    if (objModel != null)
                    {

                        if (objModel is FileModel)
                        {
                            try
                            {
                                
                                string fileName = foulderPath + objModel.displayName;
                                objModel.localPath = fileName;
                                events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADING));
                                ((FileModel)objModel).startTimer();
                                await downloadfile((FileModel)objModel, fileName);
                                events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADCOMPELETED));
                            }
                            catch( Exception ex)
                            {
                                if (ex is System.Threading.Tasks.TaskCanceledException)
                                {
                                    events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADCANCEL));
                                }

                            }
                        }
                        else
                        {
                            try
                            {
                                await folderListModel.initFolderForDownload((FolderModel)objModel);
                                objModel.localPath = foulderPath + objModel.displayName;
                                events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADING));
                                ((FolderModel)objModel).startTimer();
                                await downloadFolder((FolderModel)objModel, objModel.localPath);
                                events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADCOMPELETED));
                            }
                            catch (Exception ex)
                            {
                                if (ex is System.Threading.Tasks.TaskCanceledException)
                                {
                                    events.Publish(new TaskEvent(objModel, TaskEventType.DOWNLOADCANCEL));
                                }

                            }
                        }
                    }
                }

             }
       }

       private async Task uploadSingleFile(FileModel fileModel)
       {
           FileStream fs = null;

           try
           {
               FileInfo fileInfo = new FileInfo(fileModel.localPath);
               fs = new FileStream(fileModel.localPath, FileMode.Open);
               ObjectMetadata oMetaData = new ObjectMetadata();
               fileModel.tokenSource = new System.Threading.CancellationTokenSource();
               await folderListModel.client.PutObject(fileModel.bucketName, fileModel.key, fs, oMetaData, fileModel.callback, fileModel.tokenSource.Token);           
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               if (fs != null)
               {
                   fs.Dispose();
               }
           }
       }

       private  FolderModel uploadfolderInit(string bucket, string parentKey, string dir)
       {

           DirectoryInfo dirInfo = new DirectoryInfo(dir);

           FolderModel folderModel = new FolderModel();
           folderModel.objListAll = new List<FileModel>();
           folderModel.Size = 0;
           folderModel.bucketName = bucket;
           folderModel.key = parentKey + dirInfo.Name + "/";

           FileInfo[] fileInfos = dirInfo.GetFiles();
           foreach (FileInfo fileInfo in fileInfos)
           {
               folderModel.objListAll.Add(new FileModel() { bucketName = folderModel.bucketName, key = folderModel.key + fileInfo.Name, localPath = fileInfo.FullName });
               folderModel.Size += fileInfo.Length;
           }

           DirectoryInfo[] sonDirInfos = dirInfo.GetDirectories();
           foreach (DirectoryInfo sonDirInfo in sonDirInfos)
           {
               FolderModel model = uploadfolderInit(bucket, folderModel.key, sonDirInfo.FullName);
               folderModel.objListAll.AddRange(model.objListAll);
               folderModel.Size += model.Size;
           }

           return folderModel;

       }


       private List<FileModel> uploadfolderGetALLFile(string bucket, string parentKey, string localDir)
       {
           List<FileModel> result = new List<FileModel>();
           DirectoryInfo dirInfo = new DirectoryInfo(localDir);


           string currentKey = parentKey + dirInfo.Name + "/";

           FileInfo[] fileInfos = dirInfo.GetFiles();
           foreach (FileInfo fileInfo in fileInfos)
           {
               result.Add(new FileModel() { bucketName = bucket, key = currentKey + fileInfo.Name, localPath = fileInfo.FullName });
           }

           DirectoryInfo[] sonDirInfos = dirInfo.GetDirectories();
           foreach (DirectoryInfo sonDirInfo in sonDirInfos)
           {
               result.AddRange(uploadfolderGetALLFile(bucket, currentKey, sonDirInfo.FullName));
           }

           return result;

       }

       private async Task createFolders(FolderModel folderModel, string localDir)
       {
           ProgressVisible = true;
           DirectoryInfo dirInfo = new DirectoryInfo(localDir);

           
           await createFolder(folderModel.bucketName, folderModel.key);

           DirectoryInfo[] sonDirInfos = dirInfo.GetDirectories();
           foreach (DirectoryInfo sonDirInfo in sonDirInfos)
           {
               await createFolders(new FolderModel() { bucketName = folderModel.bucketName, key = folderModel.key + sonDirInfo.Name + "/" }, 
                   sonDirInfo.FullName);
           }
           ProgressVisible = false;
       }

    
        private async Task uploadFileInCurrentFolder(string fileName)
        {
            FileModel objModel = null;
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                 objModel = new FileModel() { bucketName = currentFolder.bucketName, key = currentFolder.key + fileInfo.Name };
                 objModel.initial();
                objModel.localPath = fileName;
                events.Publish(new TaskEvent(objModel, TaskEventType.UPLOADING));
                objModel.startTimer();
                await uploadSingleFile(objModel);
                events.Publish(new TaskEvent(objModel, TaskEventType.UPLOADCOMPELETED));
            }
            catch(Exception ex)
            {
                if (ex is System.Threading.Tasks.TaskCanceledException)
                {
                    events.Publish(new TaskEvent(objModel, TaskEventType.UPLOADCANCEL));
                    folderListModel.deleteFile(objModel.bucketName, objModel.key);
                }
            }
        }


        async Task uploadFolder(FolderModel folderModel)
        {
            try
            {
                List<Task> taskList = new List<Task>();
                foreach (FileModel fileMode in folderModel.objListAll)
                {
                    await uploadSingleFile(fileMode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       public async void uploadFileOperate()
       {
           if (currentFolder != null)
           {
               string[] fileNames = fileFolderDialogService.openFileDialog();
               if (fileNames != null)
               {
                   foreach (string fileName in fileNames)
                   {
                       await uploadFileInCurrentFolder(fileName);
                   }
                   refresh();

               }
           }
       }

       private async Task uploadFolderInCurrentFolder(string Path)
       {
           FolderModel folderModel = null;
           try
           {
               folderModel = uploadfolderInit(currentFolder.bucketName, currentFolder.key, Path);
               folderModel.localPath = Path;

               events.Publish(new TaskEvent(folderModel, TaskEventType.UPLOADING));
               folderModel.startTimer();
               await createFolders(folderModel, Path);
               await uploadFolder(folderModel);
               events.Publish(new TaskEvent(folderModel, TaskEventType.UPLOADCOMPELETED));
           }
           catch (Exception ex)
           {
               if (ex is System.Threading.Tasks.TaskCanceledException)
               {
                   events.Publish(new TaskEvent(folderModel, TaskEventType.UPLOADCANCEL));
                   folderListModel.deleteFolder(folderModel.bucketName, folderModel.key);
               }
           }

       }


       public async void uploadFolderOperate()
       {
           if (currentFolder != null)
           {
               string foulderPath = fileFolderDialogService.openFolderDialog();
               if (foulderPath != null)
               {

                   await uploadFolderInCurrentFolder(foulderPath);
                 
               }
               refresh();
           }

       }

       public async void uploadFolderZipOperate()
       {
           if (currentFolder != null)
           {
               string foulderPath = fileFolderDialogService.openFolderDialog();
               if (foulderPath != null)
               {
                   DirectoryInfo dirInfo = new DirectoryInfo(foulderPath);
                   string zipFileName = dirInfo.FullName + ".zip";
                   int i = 0;

                   while (File.Exists(zipFileName))
                   {
                       zipFileName = dirInfo.FullName + "_" + i.ToString() + ".zip";
                   }

                   ZipFile.CreateFromDirectory(foulderPath, zipFileName);

                   await uploadFileInCurrentFolder(zipFileName);               
               }
               refresh();
           }
       }

       public async void DragEnter(DragEventArgs e)
       {
           string[] FileOrfolderList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

           foreach (string fileOrFolderName in FileOrfolderList)
           {
               if (Directory.Exists(fileOrFolderName))
               {
                   await uploadFolderInCurrentFolder(fileOrFolderName);
               }
               else
               {
                   await uploadFileInCurrentFolder(fileOrFolderName);
               }
               refresh();

           }

       }

       async Task refreshPath(string path)
       {
           string[] ss = path.Split('/');
           if (currentFolder.bucketName != ss[0])
           {
               events.Publish(new BuketSelectedUiUpdateEvent(ss[0]));
           }
           currentFolder = await folderListModel.getFolderModel(ss[0], path.Substring(ss[0].Length + 1));
           refreshObjectList(currentFolder);
       }

       string selectedSourceCountryTwoLetterCode;

       public string SelectedSourceCountryTwoLetterCode
       {
           get { return this.selectedSourceCountryTwoLetterCode; }
           set
           {
               this.selectedSourceCountryTwoLetterCode = value;
               NotifyOfPropertyChange(() => this.SelectedSourceCountryTwoLetterCode);
               refreshPath(SelectedSourceCountryTwoLetterCode);
           }
       }
       bool progressVisible = false;

      public bool ProgressVisible
       {
           get
           {
               return this.progressVisible;
           }
           set
           {
               this.progressVisible = value;
               NotifyOfPropertyChange(() => this.ProgressVisible);
           }
       }

      

      public bool IsFolder
      {
          get
          {
              return selectedIndex > -1 && objectList[selectedIndex] is FolderModel;
          }

      }

      public bool IsSearchView
      {
          get
          {
              return currentFolder == null;
          }
      }


      bool isCreateFolderEnabled = false;

      public bool IsCreateFolderEnabled
      {
          get
          {
              return currentFolder != null;
          }

      }

      bool isSearchEnabled = false;

      public bool IsSearchEnabled
      {
          get
          {
              return this.isSearchEnabled;
          }
          set
          {
              this.isSearchEnabled = value;
              NotifyOfPropertyChange(() => this.IsSearchEnabled);
          }
      }


      public bool IsDownloadEnable
      {
          get
          {
              return selectedIndex > -1;
          }

      }

      private FolderModel m_currentFolder = null;
      private FolderModel backUpCurrentFolder = null;

      public FolderModel currentFolder
      {
          get
          {
              return this.m_currentFolder;
          }
          set
          {
              this.m_currentFolder = value;
              NotifyOfPropertyChange(() => this.currentFolder);
              NotifyOfPropertyChange(() => this.IsCreateFolderEnabled);
              NotifyOfPropertyChange(() => this.IsSearchView);
          }
      }



        


       //void addToComboBox(string path)
       //{
       //    if (Countries.FirstOrDefault(x => x.Path == path) == null)
       //          Countries.Insert(0, new TempData(path));
       //    SelectedSourceCountryTwoLetterCode = path;
           
       //}


         //public BindableCollection<AccessControlModel> Countries  { get; set; }

         public FolderListModel folderListModel;

         public BindableCollection<ObjectModel> objectList { get; set; }
         public History history{get;set;}
    }
}
