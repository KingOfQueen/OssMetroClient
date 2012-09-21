using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OssClientMetro.Model
{
    public class FolderModel : ObjectModel
    {
        public List<FileModel> objList { get; set; }
        public List<FolderModel> folderList { get; set; }


        override public void callback(HttpProcessData httpProcessData)
        {
            factory.StartNew((stateobj) =>
            {
                HttpProcessData data = (HttpProcessData)stateobj;
                Percent = data.ProgressPercentage;
                Size = data.TotalBytes;
                ProcessSize = data.BytesTransferred;

            }, httpProcessData);
        }

        //public List<OssObjectSummary> objList2 { get; set; }
        //public List<string> CommonPrefixes2 { get; set; }

    }
}
