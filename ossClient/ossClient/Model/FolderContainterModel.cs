using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OssClientMetro.Model
{
    public class FolderContainterModel 
    {
        public string buketName { get; set; }
        public string folderKey { get; set; }
        public List<OssObjectSummary> objList { get; set; }
        public List<string> CommonPrefixes { get; set; }

    }
}
