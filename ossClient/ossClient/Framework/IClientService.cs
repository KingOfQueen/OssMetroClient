using Oss;
using OssClientMetro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssClientMetro.Framework
{
    public interface IClientService
    {
        OssClient ossClient { get;  }

        BucketListModel buckets { get; }

         FolderContainterListModel folders { get;}

         Task login(string userName, string userPassword);

    }
}
