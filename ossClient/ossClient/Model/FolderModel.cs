using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public class FolderModel : ObjectModel
    {
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

        public List<OssObjectSummary> objList2 { get; set; }
        public List<string> CommonPrefixes2 { get; set; }

    }
}
