using Oss;
using OssClientMetro.Framework;
using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro
{
    class ClientService : IClientService
    {
        public ClientService()
        {

        }

        //static ClientService current;

        //static public ClientService Current
        //{
        //    get
        //    {
        //        if (current == null)
        //        {
        //            current = new ClientService();
        //        }
        //        return current;

        //    }

        //}

        public async Task login(string userName, string userPassword)
        {
            try
            {
                ossClient = new OssClient(userName, userPassword);

                buckets = new BucketListModel(ossClient);
                await buckets.refreshBuckets();

                folders = new FolderListModel(ossClient);

               // objects = new ObjectListModel(ossClient);

               // await objects.createData(buckets);

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public OssClient ossClient { get; private set; }

        public BucketListModel buckets { get;  private set;}

        public FolderListModel folders { get; private set; }

       // public ObjectListModel objects { get; private set; }

    }
}
