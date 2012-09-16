using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Oss;
using System.IO;
using OssClientMetro.Model;
using System.Threading.Tasks;
using NLog;
using OssClientMetro.Framework;
using System.ComponentModel.Composition;


namespace OssClientMetro
{
    [Export(typeof(IShell))]
    class MainWindowViewModel :  IShell
    {
        [ImportingConstructor]
        public MainWindowViewModel(ILeftView firstViewModel, IRightView secondviewModel) // etc, for each child ViewModel
        {
            LeftView = firstViewModel;
            RightView = secondviewModel;
        }

        public ILeftView LeftView { get; private set; }
        public IRightView RightView { get; private set; }



        //public ILeftView LeftView
        //{
        //    get
        //    {
        //        return this.leftView;
        //    }
        //    set
        //    {
        //        this.leftView = value;
        //        NotifyOfPropertyChange(() => this.LeftView);
        //    }
        //}



        //public IRightView RightView
        //{
        //    get
        //    {
        //        return this.rightView;
        //    }
        //    set
        //    {
        //        this.rightView = value;
        //        NotifyOfPropertyChange(() => this.RightView);
        //    }
        //}


    //   // "bm9crcnr0rtnuw8bnrfvq7w8", "RbtJoExTnA8vYLynUfDh7Ior+oM="
    //    OssClient ossClient;
    //    public  MainWindowViewModel()
    //    {
    //        if (Global.getInstance().login("bm9crcnr0rtnuw8bnrfvq7w8", "RbtJoExTnA8vYLynUfDh7Ior+oM=") == true)
    //        {


    //            ossClient = Global.getInstance().ossClient;
    //            _buckets = new BucketListModel(ossClient);



    //            ////OssClient client = new OssClient(id.Text, key.Text);

    //            //ObjectMetadata metadata = new ObjectMetadata();
    //            //metadata.UserMetadata.Add("myfield", "test");

    //            //string key = "test2/";

    //          // IEnumerable<Bucket> bucketList = ossClient.ListBuckets();

               
    //        }
            
    //    }

       

    //    private int m_selectedBuketIndex = 0;

    //    public int selectedBuketIndex
    //    {
    //        get
    //        {
    //            return this.m_selectedBuketIndex;
    //        }
    //        set
    //        {
    //            this.m_selectedBuketIndex = value;
    //            NotifyOfPropertyChange(() => this.selectedBuketIndex);
    //        }
    //    }


    //    private string m_inputBucketName = "";

    //    public string inputBucketName
    //    {
    //        get
    //        {
    //            return this.m_inputBucketName;
    //        }
    //        set
    //        {
    //            this.m_inputBucketName = value;
    //            NotifyOfPropertyChange(() => this.inputBucketName);
    //        }
    //    }

    //    private BucketListModel _buckets;

    //    public BucketListModel buckets
    //    {
    //        get
    //        {
    //            return this._buckets;
    //        }
    //        set
    //        {
    //            this._buckets = value;
    //            NotifyOfPropertyChange(() => this.buckets);
    //        }
    //    }



    //    public async void refreshBuckets()
    //    {
    //        await _buckets.refreshBuckets();
    //    }

    //    public async void createBucket()
    //    {
    //        await _buckets.createBucket(inputBucketName, CannedAccessControlList.Private);
    //    }

    //    public async  void deleteBucket()
    //    {
    //        await _buckets.deleteBucket(_buckets[selectedBuketIndex].name);
    //    }


    //    public async void ossTest()
    //    {
    //        _buckets.Clear();
    //        IEnumerable<Bucket> bucketList = await ossClient.ListBuckets();

    //        foreach (Bucket temp in bucketList)
    //        {
                
    //            _buckets.Add(new BucketModel(temp.Name));
    //        }





    //        //OssClient client = new OssClient("", "");

    //        ////OssClient client = new OssClient(id.Text, key.Text);

    //        //ObjectMetadata metadata = new ObjectMetadata();
    //        //metadata.UserMetadata.Add("myfield", "test");

    //        //string key = "test2/";

    //        //IEnumerable<Bucket> bucketList = client.ListBuckets();

    //        //string text = "I am from .net client";
    //        //string bucketName = bucketList.First().Name;


    //        //using (FileStream fStream = new FileStream("1.txt", FileMode.Open, FileAccess.Read))
    //        //{
    //        //    client.PutObject(bucketName, key, fStream, metadata);
    //        //}

    //        //client.DeleteObject(bucketName, key);
    //        // client.CreateBucket("mydoc");


    //    }

    }
}
