using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Aliyun.OpenServices.OpenStorageService;
using System.IO;
using ossClient.Model;


namespace ossClient
{
    class MainWindowViewModel : PropertyChangedBase
    {
        public MainWindowViewModel()
        {
            OssClient client = new OssClient("", "");

            ////OssClient client = new OssClient(id.Text, key.Text);

            //ObjectMetadata metadata = new ObjectMetadata();
            //metadata.UserMetadata.Add("myfield", "test");

            //string key = "test2/";

            IEnumerable<Bucket> bucketList = client.ListBuckets();

            foreach (Bucket temp in bucketList)
            {
                _buckets.Add(new BucketModel(temp.Name));
            }
        }

        private BindableCollection<BucketModel> _buckets = new BindableCollection<BucketModel>();

        public BindableCollection<BucketModel> buckets
        {
            get
            {
                return this._buckets;
            }
            set
            {
                this._buckets = value;
                NotifyOfPropertyChange(() => this.buckets);
            }
        }


        public void ossTest()
        {
            OssClient client = new OssClient("", "");

            //OssClient client = new OssClient(id.Text, key.Text);

            ObjectMetadata metadata = new ObjectMetadata();
            metadata.UserMetadata.Add("myfield", "test");

            string key = "test2/";

            IEnumerable<Bucket> bucketList = client.ListBuckets();

            string text = "I am from .net client";
            string bucketName = bucketList.First().Name;


            using (FileStream fStream = new FileStream("1.txt", FileMode.Open, FileAccess.Read))
            {
                client.PutObject(bucketName, key, fStream, metadata);
            }

            //client.DeleteObject(bucketName, key);
            // client.CreateBucket("mydoc");


        }

    }
}
