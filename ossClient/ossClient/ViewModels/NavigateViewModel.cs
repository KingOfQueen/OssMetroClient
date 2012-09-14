using Caliburn.Micro;
using Oss;
using ossClient.Events;
using ossClient.Framework;
using ossClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ossClient.ViewModels
{
    [Export(typeof(ILeftWorkSpace))]
    class NavigateViewModel : PropertyChangedBase,ILeftWorkSpace
    {
        readonly IEventAggregator events;

        [ImportingConstructor]
        public NavigateViewModel(IEventAggregator _events)
        {
            events = _events;
            buckets = new BucketListModel(new OssClient("bm9crcnr0rtnuw8bnrfvq7w8", "RbtJoExTnA8vYLynUfDh7Ior+oM="));
            buckets.refreshBuckets();
        }

        public void Publish()
        {
            events.Publish(new SelectedPathEvent(buckets[selectedBuketIndex].Name));

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
            await buckets.deleteBucket(buckets[selectedBuketIndex].Name);
        }

        public BucketListModel buckets { get; set; }

    }
}
