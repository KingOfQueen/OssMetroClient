using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aliyun.OpenServices.OpenStorageService;

namespace ossClient
{
    class Global
    {
        private Global() { }

        static Global instance = null;

        public static Global getInstance()
        {
            if (instance == null)
            {
                instance = new Global();
            }

            return instance;
        }

        public OssClient login(string id, string key)
        {
            try
            {
                accessID = id;
                accessKey = key;

                ossClient = new OssClient(accessID, accessKey);
            }
            catch (Exception e)
            {

            }
            return ossClient;

        }




        public string accessID { get; set; }
        public string accessKey { get; set; }
        public OssClient ossClient = null;



    }
}
