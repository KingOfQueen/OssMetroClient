using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Oss;
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

        public async Task refreshBuckets()
        {
            IEnumerable<Bucket> bucketList = await ossClient.ListBuckets();

            foreach (Bucket temp in bucketList)
            {
                this.Add(new BucketModel(temp.Name));
            }
           
        }

        public async Task createBucket(string bucketName, CannedAccessControlList accessControl)
        {

                 Bucket bucket = await ossClient.CreateBucket(bucketName);
                 await ossClient.SetBucketAcl(bucketName, accessControl);

                 this.Add(new BucketModel(bucket.Name));


        }

        public async Task deleteBucket(string bucketName)
        {
            await ossClient.DeleteBucket(bucketName);

            foreach (BucketModel temp in this)
            {
                if (temp.name == bucketName)
                {
                    this.Remove(temp);
                    break;
                }
            }     
        }
    }
}
