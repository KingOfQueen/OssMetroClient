using Oss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OssClientMetro.Model
{
    public class FolderModel 
    {
        public string buketName { get; set; }
        public string folderKey { get; set; }
        public List<OssObjectSummary> objList { get; set; }

    }
}
