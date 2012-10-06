using Caliburn.Micro;
using Oss;
using OssClientMetro.Events;
using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.ViewModels
{
    class CreateBucketViewModel : Screen
    {

        readonly IEventAggregator events;
        public CreateBucketViewModel(IEventAggregator _events)
        {
            events = _events;

            accessControlList = new BindableCollection<AccessControlModel>();
            accessControlList.Add(new AccessControlModel(Oss.CannedAccessControlList.Private));
            accessControlList.Add(new AccessControlModel(Oss.CannedAccessControlList.PublicRead));
            accessControlList.Add(new AccessControlModel(Oss.CannedAccessControlList.PublicReadWrite));
        }
        
        string bucketName;



        public string BucketName
        {
            get
            {
                return this.bucketName;
            }
            set
            {
                this.bucketName = value;
                NotifyOfPropertyChange(() => this.BucketName);
            }
        }

        CannedAccessControlList selectedValue;

        public CannedAccessControlList SelectedValue
        {
            get
            {
                return this.selectedValue;
            }
            set
            {
                this.selectedValue = value;
                NotifyOfPropertyChange(() => this.SelectedValue);
            }
        }

        public void Create()
        {
            events.Publish(new CreateBucketEvent(BucketName, SelectedValue));
            this.TryClose();
        }




        public BindableCollection<AccessControlModel> accessControlList { get; set; }
    }
}
