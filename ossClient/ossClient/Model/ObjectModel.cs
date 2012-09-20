using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OssClientMetro.Model
{
    public abstract class ObjectModel : PropertyChangedBase
    {
        public string bucketName { get; set; }
        public string displayName { get; set; }
        public string key { get; set; }



        private long? size;
        private long processSize;
        private int percent;



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
                this.processSize = value;
                NotifyOfPropertyChange(() => this.Percent);
            }
        }
    } 
}
