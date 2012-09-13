using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Oss;
using System.Threading.Tasks;

namespace ossClient.Model
{
    public class BucketListModel : BindableCollection<Bucket>
    {

        public BucketListModel(OssClient client)
        {
            _ossClient = client;
        }
        OssClient _ossClient;

        public async Task refreshBuckets()
        {
            IEnumerable<Bucket> bucketList = await _ossClient.ListBuckets();
            this.Clear();
            foreach (Bucket temp in bucketList)
            {
                this.Add(temp);
            }
           
        }

        public async Task createBucket(string bucketName, CannedAccessControlList accessControl)
        {

            Bucket bucket = await _ossClient.CreateBucket(bucketName);
            await _ossClient.SetBucketAcl(bucketName, accessControl);
            this.Add(bucket);
        }

        public async Task deleteBucket(string bucketName)
        {
            await _ossClient.DeleteBucket(bucketName);
            this.Remove(this.First(x => x.Name == bucketName));
        }
    }
}
