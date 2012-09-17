using Caliburn.Micro;
using Oss;
using OssClientMetro.Events;
using OssClientMetro.Framework;
using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.ViewModels
{
    class NavigateViewModel : PropertyChangedBase,ILeftWorkSpace
    {
        readonly IEventAggregator events;
        readonly IClientService clientService;

        public NavigateViewModel(IEventAggregator _events, IClientService _clientService)
        {
            events = _events;
            clientService = _clientService;
            buckets = clientService.buckets;
        }

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


        private int m_selectedBuketIndex = 0;

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




        public async void createBucket()
        {
            await buckets.createBucket(inputBucketName, CannedAccessControlList.Private);
        }

        public async void deleteBucket()
        {
            if (selectedBuketIndex < 0)
            {
            }
            else
            {
                string bucketName = buckets[selectedBuketIndex].Name;
                await clientService.folders.deleteBuket(bucketName);
                await buckets.deleteBucket(bucketName);


            }
        }

        public BucketListModel buckets { get; set; }

    }
}
