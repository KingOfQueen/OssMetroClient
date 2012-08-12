using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Aliyun.OpenServices.OpenStorageService;

namespace ossClient.Model
{
    class BucketListModel : BindableCollection<BucketModel>
    {
        OssClient ossClient = Global.getInstance().ossClient;

        public void refreshBuckets()
        {
            this.Clear();
            IEnumerable<Bucket> bucketList = ossClient.ListBuckets();

            foreach (Bucket temp in bucketList)
            {
                this.Add(new BucketModel(temp.Name));
            }

        }

        public void createBucket(string bucketName, CannedAccessControlList accessControl)
        {
            ossClient.CreateBucket(bucketName);
            ossClient.SetBucketAcl(bucketName, accessControl);
            refreshBuckets();
        }

        public void deleteBucket(string bucketName)
        {
            ossClient.DeleteBucket(bucketName);
            refreshBuckets();
        }
    }
}
