﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Aliyun.OpenServices.OpenStorageService;
using System.IO;
using ossClient.Model;
using System.Threading.Tasks;
using NLog;


namespace ossClient
{
    class MainWindowViewModel : PropertyChangedBase
    {

       // "bm9crcnr0rtnuw8bnrfvq7w8", "RbtJoExTnA8vYLynUfDh7Ior+oM="
        OssClient ossClient;
        public MainWindowViewModel()
        {
            if (Global.getInstance().login("bm9crcnr0rtnuw8bnrfvq7w8", "RbtJoExTnA8vYLynUfDh7Ior+oM=") == true)
            {


                ossClient = Global.getInstance().ossClient;
                _buckets = new BucketListModel(ossClient);

                // create the task
                Task<IEnumerable<Bucket>> taskWithFactoryAndState =
                    Task.Factory.StartNew<IEnumerable<Bucket>>(() =>
                    {
                        IEnumerable<Bucket> bucketList = ossClient.ListBuckets();

                        return bucketList;

                    });


                //and setup a continuation for it only on when faulted
                taskWithFactoryAndState.ContinueWith((ant) =>
                {
                    Exception aggEx = ant.Exception.GetBaseException();
                    Global.getInstance().logger.Error(aggEx.Message);
                }, TaskContinuationOptions.OnlyOnFaulted);


                ////and setup a continuation for it only on ran to completion
                taskWithFactoryAndState.ContinueWith((ant) =>
                {
                    IEnumerable<Bucket> bucketList = ant.Result;
                    foreach (Bucket temp in bucketList)
                    {
                        _buckets.Add(new BucketModel(temp.Name));
                    }


                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                ////OssClient client = new OssClient(id.Text, key.Text);

                //ObjectMetadata metadata = new ObjectMetadata();
                //metadata.UserMetadata.Add("myfield", "test");

                //string key = "test2/";

              // IEnumerable<Bucket> bucketList = ossClient.ListBuckets();

               
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

        private BucketListModel _buckets;

        public BucketListModel buckets
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



        public void refreshBuckets()
        {
            _buckets.refreshBuckets();
        }

        public void createBucket()
        {
            _buckets.createBucket(inputBucketName, CannedAccessControlList.Private);
        }

        public void deleteBucket()
        {
            _buckets.deleteBucket(_buckets[selectedBuketIndex].name);
        }
        

        public void ossTest()
        {
            _buckets.Clear();
            IEnumerable<Bucket> bucketList = ossClient.ListBuckets();

            foreach (Bucket temp in bucketList)
            {
                
                _buckets.Add(new BucketModel(temp.Name));
            }





            //OssClient client = new OssClient("", "");

            ////OssClient client = new OssClient(id.Text, key.Text);

            //ObjectMetadata metadata = new ObjectMetadata();
            //metadata.UserMetadata.Add("myfield", "test");

            //string key = "test2/";

            //IEnumerable<Bucket> bucketList = client.ListBuckets();

            //string text = "I am from .net client";
            //string bucketName = bucketList.First().Name;


            //using (FileStream fStream = new FileStream("1.txt", FileMode.Open, FileAccess.Read))
            //{
            //    client.PutObject(bucketName, key, fStream, metadata);
            //}

            //client.DeleteObject(bucketName, key);
            // client.CreateBucket("mydoc");


        }

    }
}
