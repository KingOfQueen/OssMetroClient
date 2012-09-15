using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oss;
using System.Threading.Tasks;
using System.Windows;
using NLog;

namespace OssClientMetro
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


        public Logger logger = LogManager.GetCurrentClassLogger();

        public bool login(string id, string key)
        {

            bool result = false;
            try
            {
                if (id == "")
                    MessageBox.Show("id should not be empty");

                if (key == "")
                    MessageBox.Show("key should not be empty");

                accessID = id;
                accessKey = key;

                ossClient = new OssClient(accessID, accessKey);

            

                return true;




            }
            catch (Exception e)
            {
                return false;

            }

        }




        public string accessID { get; set; }
        public string accessKey { get; set; }
        public OssClient ossClient = null;



    }
}
