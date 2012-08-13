using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Aliyun.OpenServices.OpenStorageService;
using System.Threading.Tasks;

namespace ossClient.Model
{
    class BucketListModel : BindableCollection<BucketModel>
    {

        public BucketListModel(OssClient _ossClient)
        {
            ossClient = _ossClient;
        }
        OssClient ossClient;

        public void refreshBuckets()
        {
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
                this.Clear();
                foreach (Bucket temp in bucketList)
                {
                    this.Add(new BucketModel(temp.Name));
                }


            }, TaskContinuationOptions.OnlyOnRanToCompletion);

        }

        public void createBucket(string bucketName, CannedAccessControlList accessControl)
        {
            Task<Bucket> taskWithFactoryAndState =
             Task.Factory.StartNew<Bucket>(() =>
             {
                 Bucket bucket = ossClient.CreateBucket(bucketName);
                 ossClient.SetBucketAcl(bucketName, accessControl);

                 return bucket;

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
                Bucket bucket = ant.Result;
                this.Add(new BucketModel(bucket.Name));

            }, TaskContinuationOptions.OnlyOnRanToCompletion);

        }

        public void deleteBucket(string bucketName)
        {
            Task<bool> taskWithFactoryAndState =
             Task.Factory.StartNew<bool>(() =>
             {
                 ossClient.DeleteBucket(bucketName);

                 return true;

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
                foreach (BucketModel temp in this)
                {
                    if (temp.name == bucketName)
                    {
                        this.Remove(temp);
                        break;
                    }
                }

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
          
        }
    }
}
