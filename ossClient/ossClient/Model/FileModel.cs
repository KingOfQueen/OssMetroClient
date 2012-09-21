using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OssClientMetro.Model
{
    public class FileModel : ObjectModel
    {
        public DateTime modifyTime { get; set; }

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

    }
}
