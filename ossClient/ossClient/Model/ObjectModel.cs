using Caliburn.Micro;
using Oss;
using OssClientMetro.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public abstract class ObjectModel : PropertyChangedBase
    {

       
        public static TaskFactory factory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());

        public static Hashtable iconsInfo = RegisteredFileType.GetFileTypeAndIcon();

        public string bucketName { get; set; }
        public string displayName { get; set; }
        public string key { get; set; }

        public bool modifyTimeVisible { get; set; }

        public DateTime modifyTime;

        public DispatcherTimer timer;



        public abstract  void callback(HttpProcessData httpProcessData);

        private object iconUri;


        private long? size;
        private long processSize = 0;
        private int percent;

        private long speed;

        public long Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
                NotifyOfPropertyChange(() => this.Speed);
            }
        }





        public long? Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                NotifyOfPropertyChange(() => this.Size);
            }
        }

        public long ProcessSize
        {
            get
            {
                return this.processSize;
            }
            set
            {
                this.processSize = value;
                NotifyOfPropertyChange(() => this.ProcessSize);
            }
        }

        public int Percent
        {
            get
            {
                return this.percent;
            }
            set
            {
                this.percent = value;
                NotifyOfPropertyChange(() => this.Percent);
            }
        }


        public object IconUri
        {
            get
            {
                return this.iconUri;
            }
            set
            {
                this.iconUri = value;
                NotifyOfPropertyChange(() => this.IconUri);
            }
        }

       // public DateTime modifyTime

        public string ModifyTime
        {
            get
            {
                return this.modifyTime.ToLocalTime().ToString();
            }
        }

        public string localPath { get; set; }
    } 
}
