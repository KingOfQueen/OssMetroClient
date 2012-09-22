using Caliburn.Micro;
using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public abstract class ObjectModel : PropertyChangedBase
    {
        public static TaskFactory factory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());

        public string bucketName { get; set; }
        public string displayName { get; set; }
        public string key { get; set; }

        public DispatcherTimer timer;

        public abstract  void callback(HttpProcessData httpProcessData);

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

        public string localPath;
    } 
}
