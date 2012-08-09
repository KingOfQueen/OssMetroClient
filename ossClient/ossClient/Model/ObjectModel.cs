using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ossClient.Model
{
    abstract class ObjectModel
    {
        string BucketName;
        string displayName;
        string key;
        DateTime modifyTime;
    }
}
