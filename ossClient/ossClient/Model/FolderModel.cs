using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public class FolderModel : ObjectModel
    {
       public  FolderModel()
       {
           modifyTimeVisible = false;
       }

       public static string lastName(string _key)
       {
           string[] fileNames = _key.Split('/');
           return  fileNames[fileNames.Length - 2];
       }



       public void initial()
       {
           try
           {
               modifyTimeVisible = false;
               string[] fileNames = key.Split('/');
               displayName = fileNames[fileNames.Length - 2];
               setIcon();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }



        public List<FileModel> objList { get; set; }
        public List<FolderModel> folderList { get; set; }

        public List<FileModel> objListAll { get; set; }
        


        public long lastSize = 0;

        void timer_Tick(object sender, EventArgs e)
        {
            factory.StartNew(() =>
            {
                long processSize = 0;
                foreach (FileModel fileModel in objListAll)
                {
                    processSize += fileModel.ProcessSize;
                }

                Speed = processSize - ProcessSize;
                ProcessSize = processSize;

                Percent = (int)(1.0 * ProcessSize / Size * 100);

                if (Percent == 100)
                {
                    timer.Stop();
                }


            });

        }

        public void startTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }




        override public void callback(HttpProcessData httpProcessData)
        {
            if (httpProcessData.ProgressPercentage == 100)
            {
                factory.StartNew((stateobj) =>
                {
                    HttpProcessData data = (HttpProcessData)stateobj;
                    ProcessSize += (long)data.TotalBytes;
                    Percent = (int)(1.0 * ProcessSize / Size * 100);
                }, httpProcessData);
            }
        }

        public void setIcon()
        {
            IconUri = new Uri("pack://application:,,,/Images/folder2.png");
        }

        public List<OssObjectSummary> objList2 { get; set; }
        public List<string> CommonPrefixes2 { get; set; }

        public void cancelTask()
        {
            foreach (FileModel file in objListAll)
            {
                if (file.tokenSource != null)
                    file.tokenSource.Cancel();
            }
        }
        
    }
}
